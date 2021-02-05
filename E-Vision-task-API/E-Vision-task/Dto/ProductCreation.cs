using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Vision_task.Dto
{
    public class ProductCreation
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public IFormFile Photo { get; set; }
    }
}
