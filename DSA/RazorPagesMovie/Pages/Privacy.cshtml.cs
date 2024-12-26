using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger,Counter _counter)
        {
            _logger = logger;
            Counter = _counter;
        }

        public Counter Counter { get; }

        public void OnGet()
        {
            Counter.Count++;
        }
    }

}
