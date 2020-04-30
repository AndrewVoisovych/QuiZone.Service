﻿using Microsoft.Extensions.Configuration;
using QuiZone.DataAccess.Models.Const;
using System.IO;

namespace QuiZone.API.Utils.Helpers
{
    /// <summary>
    /// Helper for obtaining connection data
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Get parametr in appsettings.json
        /// </summary>
        /// <returns></returns>
        public static string GetAppSettingsValue(string parameter)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            var value = config.GetValue<string>(parameter);

            return value;
        }
    }
}
