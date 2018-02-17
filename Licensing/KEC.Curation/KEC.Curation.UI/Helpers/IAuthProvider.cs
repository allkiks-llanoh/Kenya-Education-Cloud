using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KEC.Curatiom.Web.UI.Helpers
{
    public interface IAuthProvider
    {
        Task<string> GetUserAccessTokenAsync();
    }
}