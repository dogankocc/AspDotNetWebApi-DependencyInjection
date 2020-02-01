using RehabilServer.Models;
using RehabilServer.Models.Repositories;
using RehabilServer.Service;
using RehabilServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RehabilServer.Controllers
{
    public class AccountController : ApiController
    {
        private readonly  IAccountService service;
        
        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody] AccountViewModel accountView)
        {
            if (accountView == null) return new HttpResponseMessage(HttpStatusCode.NotFound);
            else
            {
                Guid id = Guid.NewGuid();

                Account account = new Account()
                {
                    Id = id,
                    Email = accountView.Email,
                    PasswordHash = accountView.PasswordHash,
                    AccountTypeId = Guid.Parse("5AD9E729-7AE1-479C-9C4A-2ADF46F02256"),
                    isVerified = accountView.isVerified,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                if (service.Add(account) == 1)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

        }
    }
}
