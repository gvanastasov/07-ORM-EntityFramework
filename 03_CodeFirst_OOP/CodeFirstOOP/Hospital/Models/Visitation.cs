using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Visitation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
    }
}
