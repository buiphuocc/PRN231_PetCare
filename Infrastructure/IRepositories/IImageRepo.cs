using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.IRepositories
{
    public interface IImageRepo : IGenericRepo<EntityImage>
    {
        Task<EntityImage> GetImageInforById(int id);
        Task<IEnumerable<EntityImage>> GetAllImageInfors();
        Task<EntityImage> AddImage(EntityImage entityImage);
        Task DeleteImage(int id);

    }
}
