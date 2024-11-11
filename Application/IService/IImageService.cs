using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.ImageDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IImageService
    {
        Task<ServiceResponse<ImageDTO>> UploadImage(IFormFile file, int entityId, string entityType);
        Task<ServiceResponse<IEnumerable<ImageDTO>>> GetAllImageInfors();
        Task<ServiceResponse<IEnumerable<ImageDTO>>> GetImageInforById(int entityId, string entityType);
        Task<ServiceResponse<string>> DeleteImage(int id);
    }
}
