using LogApi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.Contracts
{
    public interface IDataLogManager
    {
        Task<bool> AddAsync(DataLog data);
        List<DataLog> GetAllAsync();
    }
}
