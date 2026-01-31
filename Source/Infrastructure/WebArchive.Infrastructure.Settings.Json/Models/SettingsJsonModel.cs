using System;
using System.Text.Json.Serialization;
using WebArchive.Core.Settings.Contracts;

namespace WebArchive.Infrastructure.Settings.Json.Models
{
    public sealed class SettingsJsonModel : ISettings
    {
        public GlobalSettingsJsonModel Global { get; set; } = new GlobalSettingsJsonModel();

        [JsonIgnore]
        IGlobalSettings ISettings.Global => Global;
    }
}
