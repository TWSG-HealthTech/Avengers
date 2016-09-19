using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPuff.Common.Gateway
{
    public interface IServerGateway
    {
        Task<T> GetAsync<T>(string path);
        Task<T> PutAsync<T>(string path, object parameter);
        Task PutAsync(string path, object parameter);
    }
}
