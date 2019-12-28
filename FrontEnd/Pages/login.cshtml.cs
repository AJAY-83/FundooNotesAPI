using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages
{
    public class LoginModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your Login page.";
        }
    }
}
