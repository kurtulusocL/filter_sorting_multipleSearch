using Filter_Search.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filter_Search.Models.Concrete
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
}
}