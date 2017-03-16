using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ.Provider
{
    class InUrlStateStore : IStateStore
    {
        public Task<string> StoreAsync(string state)
        {
            return Task.FromResult<string>(state);
        }

        public Task<string> RetrieveAsync(string stateId)
        {
            return Task.FromResult<string>(stateId);
        }

        public Task RemoveAsync(string stateId)
        {
            return Task.FromResult<object>(null);
        }
    }
}
