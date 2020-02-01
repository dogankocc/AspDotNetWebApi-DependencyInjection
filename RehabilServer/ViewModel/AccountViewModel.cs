using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RehabilServer.ViewModel
{
    public class AccountViewModel
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool isVerified { get; set; }
    }
}