using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Streamye.Cores.Middleware
{
    public interface IMiddleService
    {
        public Task<MiddleResult> GetAsync(string middleUrl, IDictionary<string, object> middleParam);
        
        public Task<MiddleResult> PostAsync(string middleUrl, IDictionary<string, object> middleParam);
        
        public Task<MiddleResult> PostAsync(string middleUrl, IList<IDictionary<string, object>> middleParams);
        
        public Task<MiddleResult> DeleteAsync(string middleUrl, IDictionary<string, object> middleParam);
        
        public Task<MiddleResult> PutAsync(string middleUrl, IDictionary<string, object> middleParam);
        
        public Task<MiddleResult> PutAsync(string middleUrl, IList<IDictionary<string, object>> middleParams);
    }
    
}