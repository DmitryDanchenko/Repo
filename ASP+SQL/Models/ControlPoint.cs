using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReWeight.Models
{
    public class ControlPoint
    {
        [Key]
        public int Id { get; set; }
        public int ControlPointId { get; set; }
        public string Name { get; set; }
        public string Iplokal { get; set; }
        public string IpVpn { get; set; }
        public string IPMI { get; set; }
        [ForeignKey("RegionId")]
        public int RegionId { get; set; }
        public Region Region { get; set; }

        public int LinksId {get;set;}
        [ForeignKey("LinksId")]
        public Links Links { get; set; }
        public string Information { get; set; }
        public Train Train { get; set; }

        public static implicit operator ControlPoint(Links v) => throw new NotImplementedException();

        public enum SortState
        {

            RegionAsc, // по компании по возрастанию
            RegionDesc // по компании по убыванию
        }

    }
}
