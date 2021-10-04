using Filter_Search.Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filter_Search.Core
{
    public class BaseEntity:IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public void SetCreatedDate()
        {
            CreatedDate = DateTime.Now.ToLocalTime();
        }
        public BaseEntity()
        {
            SetCreatedDate();
        }
    }
}