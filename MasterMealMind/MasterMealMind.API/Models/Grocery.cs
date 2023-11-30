using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealMind.API.Models
{
    public class Grocery
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Quantity { get; set; }
        public string? Description { get; set; }

        //public DateTime BestBefore { get; set; }
        //public string ---> MÄNGD <---
        //public Storage Storage { get; set; }
    }
}
