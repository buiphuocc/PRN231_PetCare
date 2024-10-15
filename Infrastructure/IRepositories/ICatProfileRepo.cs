using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface ICatProfileRepo : IGenericRepo<CatProfile>
    {

        Task<List<CatProfile>> GetAllCatPs(int pageNumber, int pageSize);
        Task<CatProfile?> GetCatPById(int Id);

        Task<CatProfile?> GetCatPByCatId(int catId);
    }
}
