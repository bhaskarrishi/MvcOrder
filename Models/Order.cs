using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcOrder.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Range(0, 150)]
        public float Quantity { get; set; }

        [Required]
        public float Price { get; set; }

        public float Totalqty { get; set; }

    }
}


