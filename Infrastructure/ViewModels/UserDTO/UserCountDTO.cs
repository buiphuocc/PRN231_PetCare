using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.UserDTO
{
    public class UserCountDTO
    {
        public int UserCount { get; set; }
        public string RoleName { get; set; }
    }
}
