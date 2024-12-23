using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages
{
    public class CounterModel : PageModel
    {
        internal readonly Counter Counter;
        [BindProperty]
        public int CounterValue { get; set; }
        public CounterModel(Counter _counter)
        {
            Counter = _counter;
        }
        public void OnGet()
        {
        }

        public void OnPost() {
            Counter.Count = CounterValue;
        }

    }
}
