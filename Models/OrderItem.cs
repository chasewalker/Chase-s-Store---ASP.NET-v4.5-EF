using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreWeb.Models
{
    public class OrderItem
    {
        [Key]
        public string OrderItemId { get; set; }

        public string OrderId { get; set; }

        public int Quantity { get; set; }

        public int ItemId { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Item Item { get; set; }
    }
}