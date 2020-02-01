using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RehabilServer.Models.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        void SaveChange();
    }
}