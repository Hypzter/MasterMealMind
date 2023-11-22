using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealMind.Repository.Models
{
    public class Grocerie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        
    }
}
