using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Contracts
{
    public interface ILoggerService
    {
        public void LogInfo(string message);
        public void LogWarning(string message); 
        public void LogDebug(string message); 
        public void LogError(string message);
    }
}
