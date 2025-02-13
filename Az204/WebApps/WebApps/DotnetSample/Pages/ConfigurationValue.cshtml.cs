using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DotnetSample.Pages
{
    public class ConfigurationModel : PageModel
    {
        public Dictionary<string, string> ConfigurationValues { get; private set; }

        private readonly IConfiguration _configuration;

        public ConfigurationModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            ConfigurationValues = new Dictionary<string, string>();
            foreach (var kvp in _configuration.AsEnumerable())
            {
                ConfigurationValues.Add(kvp.Key, kvp.Value);
            }
        }
    }
}