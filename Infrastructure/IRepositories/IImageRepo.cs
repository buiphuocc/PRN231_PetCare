using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.IRepositories
{
    public interface IImageRepo : IGenericRepo<EntityImage>
    {
        Task<List<EntityImage>> GetImagesByEntityIdAndType(int entityId, string entityType);
        Task<IEnumerable<EntityImage>> GetAllImageInfors();
        Task<EntityImage> AddImage(EntityImage entityImage);
        Task DeleteImage(int id);

    }
}
