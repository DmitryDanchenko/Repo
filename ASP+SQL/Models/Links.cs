using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReWeight.Models
{
    public class Links
    {
        [Key]
        public int Id { get; set; }
          
        public string ConsulDataCapture { get; set; }
        public string SGDK { get; set; }
        public string Nomad { get; set; }
        public string Сoefficients { get; set; }
      

      
    }
}
