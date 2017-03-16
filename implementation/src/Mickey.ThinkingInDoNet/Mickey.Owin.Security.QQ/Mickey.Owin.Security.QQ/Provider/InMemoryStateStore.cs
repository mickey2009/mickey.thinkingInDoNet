using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ.Provider
{
    public class InMemoryStateStore : IStateStore
    {
        static readonly MemoryCache _StateCache = new MemoryCache(typeof(QQAuthenticationHandler).FullName + ":State");
        readonly TimeSpan m_StateExpiration = TimeSpan.FromMinutes(3);

        public InMemoryStateStore()
        { }

        public InMemoryStateStore(TimeSpan stateExpiration)
        {
            m_StateExpiration = stateExpiration;
        }

        public virtual Task<string> StoreAsync(string state)
        {
            var stateId = Guid.NewGuid().ToString("N");
            _StateCache.Add(stateId, state, DateTimeOffset.Now.Add(m_StateExpiration));
            return Task.FromResult<string>(stateId);
        }

        public virtual Task<string> RetrieveAsync(string stateId)
        {
            if (stateId == null)
                return Task.FromResult<string>(null);

            var result = _StateCache.Get(stateId) as string;
            return Task.FromResult(result);
        }

        public virtual Task RemoveAsync(string stateId)
        {
            if (stateId != null)
            {
                _StateCache.Remove(stateId);
            }
            return Task.FromResult<object>(null);
        }
    }
}
