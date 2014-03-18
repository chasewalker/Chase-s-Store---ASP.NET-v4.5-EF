using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreWeb.Models
{
    public class Item
    {
        [ScaffoldColumn(false)]
        public int ItemID { get; set; }

        [Required]
        public string Name { get; set; }      
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string ImageName { get; set; }
        public DateTime? SaleExpiration { get; set; }
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}