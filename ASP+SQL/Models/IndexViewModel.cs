using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReWeight.Models
{
    public class IndexViewModel
    {
        public IEnumerable<ControlPoint> ControlPoints { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
