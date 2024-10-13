using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface ICatRepo : IGenericRepo<Cat >
    {
        Task<List<Cat>> GetAllCats(int pageNumber, int pageSize);
        Task<Cat?> GetCatById(int Id);

        Task<Cat?> GetCatByName(string catName);
    }
}
