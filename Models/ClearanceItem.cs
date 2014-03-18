using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreWeb.Models
{
    public class ClearanceItem : Item
    {
        public decimal ClearancePrice { get; set; }

        public int ItemsLeft { get; set; }

    }
}