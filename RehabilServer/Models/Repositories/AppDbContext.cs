using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RehabilServer.Models.Repositories
{
    public static class AppDbContext
    {
        public static RehabilEntities DB = new RehabilEntities();
    }
}