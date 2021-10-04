using Filter_Search.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filter_Search.Models.Concrete
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Status { get; set; }

        public int CategoryId { get; set; }
        public int CountryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Country Country { get; set; }
    }
}