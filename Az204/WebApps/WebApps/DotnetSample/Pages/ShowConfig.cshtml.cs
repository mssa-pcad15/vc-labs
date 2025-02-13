using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetSample.Pages
{
    public class ShowConfigModel : PageModel
    {
        public IDictionary<string, string> ConfigurationValues { get; private set; }

        public readonly IConfiguration _configuration;

        public ShowConfigModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            ConfigurationValues = new SortedDictionary<string, string>();
            foreach (var kvp in _configuration.AsEnumerable())
            {
                ConfigurationValues.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
