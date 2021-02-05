using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Vision_task.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string PhotoPath { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
