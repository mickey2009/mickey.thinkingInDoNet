using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ.Provider
{
    public interface IStateStore
    {
        Task<string> StoreAsync(string state);
        Task<string> RetrieveAsync(string stateId);
        Task RemoveAsync(string stateId);
    }
}
