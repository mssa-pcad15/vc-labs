using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;

namespace RazorPagesMovie.Pages
{
    public class IndexModel : PageModel //PageModel is the base class of all Model that supports cshtml razor page
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            ILogger<IndexModel> logger,
            Counter injectedCounter,
            Menu injectedMenu //got figure out how to inject a Menu instance
            ) //constructor,called when this class is instantiated
        {
            //keep a reference to injected instances of service
            _logger = logger;
            this._counter = injectedCounter;
            this.Menu = injectedMenu;
            //step 3: request Menu to be injected via Constructor Injection.Use this.Menu to maintain a reference.
        }
        // why do we use Dependency Injection? //
        private Counter _counter;
        //private Counter _counter=new(); not constructing a counter, use the injected one instead.
        public int PageViews => _counter.Count;
        public Menu Menu { get; private set; } //step 3b: allocate a property in this class for the razor cshtml page to access
        public void OnGet()
        {
            _counter.Count++;
        }
    }
}
