using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class SystemAvailabilityTime:IEntity
    {
        public int Id { get; set; }
        public int OpenTime { get; set; }
        public int CloseTime { get; set; }
    }
}
