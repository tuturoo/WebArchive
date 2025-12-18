using System.ComponentModel;
using System.Reflection;
using WebArchive.Core.Exceptions;
using WebArchive.Core.Settings.Contracts;
using WebArchive.Core.Settings.Exceptions;
using WebArchive.Core.Settings.Providers;

namespace WebArchive.Infrastructure.Settings.Implementations
{
    /// <summary>
    /// Провайдер свойств настроек на основе рефлексии.
    /// Возвращает свойства в формате Root.Child.Property
    /// </summary>
    public sealed class ReflectionSettingsPropertyProvider : ISettingsPropertyProvider
    {
        /// <inheritdoc/>
        public Task<IEnumerable<KeyValuePair<string, string>>> GetPropertiesListAsync(ISettings settings, CancellationToken token = default)
        {
            return Task.FromResult(
                GetPropertiesList(settings, prefix: string.Empty));
        }

        /// <inheritdoc/>
        public Task UpdatePropertyAsync(ISettings settings, string key, string value, CancellationToken token = default)
        {
            try
            {
                var foundedProperty = FindPropertyByPath(
                    propertyPath: key.Split('.'),
                    source: settings);

                var converter = TypeDescriptor.GetConverter(foundedProperty.Property.PropertyType);

                foundedProperty.Property.SetValue(
                    foundedProperty.Instance,
                    converter.ConvertFromInvariantString(value));
            }
            catch (DomainException)
            {
                throw;
            }
            catch (Exception ex)
            {
                const string error = "Произошла ошибка при обновлении свойства.";

                throw new SettingsUpdateKeyException(error, key, value, ex);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Метод рекурсивного обхода объекта.
        /// </summary>
        /// <param name="source">Объект для рекурсивного обхода</param>
        /// <param name="prefix">Текущий префикс пути</param>
        /// <returns>Итератор по найденным свойствам</returns>
        private IEnumerable<KeyValuePair<string, string>> GetPropertiesList(object? source, string prefix)
        {
            // При формировании списка свойств идея такая, что если тип можно преобразовать через конвертер, то этот объект конечный, т. е. не имеет вложенности
            // Например, мы могли бы создать TypeConverter для Ip адреса и десериализовать значение из строки и соответственно не стали бы спускаться в него
            
            // Но по факту есть стандартный TypeConverter, который применим ко всем типам, включая Record и т.п.
            // Поэтому формируем правило, то чтобы объект считался вложенным он должен иметь в имени Settings


            // Рекурсивный обход завершился на значении свойства, который равен NULL
            if (source is null)
            {
                yield return new KeyValuePair<string, string>(prefix, "<empty>");

                yield break;
            }

            var type = source.GetType();

            var converter = TypeDescriptor.GetConverter(type);

            // Проверка на составность объекта
            if (converter is not null && !type.Name.Contains("Settings"))
            {
                // Здесь объект не составной, а значит конечный - сериализуем его в строку для отображение

                var value = converter.ConvertToString(source);

                if (value is null)
                {
                    string error = string.Format("Значение объекта с типом {0} вернуло NULL.", source.GetType().Name);
                
                    throw new ArgumentException(error, nameof(source));
                }

                yield return new KeyValuePair<string, string>(prefix, value);

                yield break;
            }

            // Выполняем рекурсивный обход по текущему объекту
            foreach (var property in type.GetProperties())
            {
                var propertyValue = property.GetValue(source);

                var propertyPrefix = string.IsNullOrEmpty(prefix) ? property.Name : $"{prefix}.{property.Name}";

                foreach (var innerProperty in GetPropertiesList(propertyValue, propertyPrefix))
                    yield return innerProperty;
            }
        }

        /// <summary>
        /// Поиск свойства по пути в объекте.
        /// Путь в данном случае это массив формата ["Settings", "Global"], что соответствует обращению к Settings.Global
        /// </summary>
        /// <param name="propertyPath">Путь к свойству в виде массива. Пример: Global.Name -> ['Global', 'Name']</param>
        /// <param name="source">Объект, для которого ищем свойство</param>
        /// <returns>Объект, в котором было найдено свойство и его свойство</returns>
        /// <exception cref="SettingsKeyNotFoundException">По данному пути не было найдено свойство</exception>
        private (object Instance, PropertyInfo Property) FindPropertyByPath(string[] propertyPath, object source)
        {
            PropertyInfo foundedProperty = default!;

            for (int currentPropertyIndex = 0; currentPropertyIndex < propertyPath.Length; currentPropertyIndex++)
            {
                if (!TryFindProperty(source, propertyPath[currentPropertyIndex], out foundedProperty))
                {
                    throw new SettingsKeyNotFoundException(
                        message: "По указанному пути отсутствует свойство.",
                        notFoundKey: propertyPath[currentPropertyIndex]);
                }

                if (currentPropertyIndex < propertyPath.Length - 1)
                    source = foundedProperty.GetValue(source)!;
            }

            return (source, foundedProperty)!;
        }

        /// <summary>
        /// Выполняет поиск свойства по имени в объекте
        /// </summary>
        /// <param name="source">Объект, где должно находиться свойство</param>
        /// <param name="propertyName">Название свойства</param>
        /// <param name="propertyInfo">Найденное свойство</param>
        /// <returns>Было ли найдено свойство</returns>
        private bool TryFindProperty(object source, string propertyName, out PropertyInfo propertyInfo)
        {
            propertyInfo = source.GetType().GetProperty(propertyName)!;

            return propertyInfo is not null;
        }
    }
}
