using Microsoft.AspNetCore.Mvc;

namespace SSO_Sample.Models
{
    public class HomeViewModel
    {
        public string targetUrl { get; set; }
        public string corpCode { get; set; }
        public string ssoName { get; set; }
        public string account { get; set; }
    }
}
