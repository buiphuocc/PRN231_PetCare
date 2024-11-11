using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.ImageDTO
{
    public class ImageUploadRequest
    {
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
