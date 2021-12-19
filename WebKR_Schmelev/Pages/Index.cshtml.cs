using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebKR_Schmelev.Pages
{
    public class IndexModel : PageModel
    {
        private readonly postgresContext _postgresContext;

        public List<Test> Tests { get; set; }

        public IndexModel(postgresContext db)
        {
            _postgresContext = db;
        }

        public void OnGet()
        {
            Tests = _postgresContext.Tests.ToList();
        }
    }
}
