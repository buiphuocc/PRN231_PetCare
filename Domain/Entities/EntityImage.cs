using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EntityImage
    {
        public int ImageId { get; set; }
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public string ImageUrl { get; set; }
        public DateTime UploadAt { get; set; }
        public bool IsPrimary { get; set; }
    }
}
