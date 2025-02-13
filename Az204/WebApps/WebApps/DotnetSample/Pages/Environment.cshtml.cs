using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetSample.Pages
{
    public class EnvironmentModel : PageModel
    {
        public Dictionary<string, string> EnvironmentVariables { get; private set; }
        public void OnGet()
        {   EnvironmentVariables = new Dictionary<string, string>();
            foreach (var envVar in System.Environment.GetEnvironmentVariables())
            {
                var entry = (System.Collections.DictionaryEntry)envVar;
                EnvironmentVariables.Add(entry.Key.ToString(), entry.Value.ToString());
            }
        }
    }
}
