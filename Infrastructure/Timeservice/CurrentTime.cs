
using Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CurrentTime : ICurrentTime
    {
        public DateTime GetCurrentTime()
        {
           return DateTime.Now;
        }
    }
}
