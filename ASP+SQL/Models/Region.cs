using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReWeight.Models
{
    public class Region
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpEmoiv { get; set; }
        public List<ControlPoint> controlPoints { get; set; }
        public Region()
        {
            controlPoints = new List<ControlPoint>();
        }
    }
}
