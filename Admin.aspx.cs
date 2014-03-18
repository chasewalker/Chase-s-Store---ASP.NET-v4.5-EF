using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using StoreWeb.Models;

namespace StoreWeb
{
    public partial class Admin : System.Web.UI.Page
    {
        private List<Item> _items;

        public List<Item> items
        {
            get { return _items; }
            set { _items = value; }
        }

        public string success { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["success"] != null)
            {
                switch (Request.QueryString["success"])
                {
                    case "1":
                        success = "Item was successfully created.";
                        break;
                    case "2":
                        success = "Item was successfully updated.";
                        break;
                }
            }
            var storeBL = new BLL.StoreBL();
            items = storeBL.GetItems(0);
        }

        [WebMethod]
        public static string DeleteItem(string itemID)
        {
            var storeBL = new BLL.StoreBL();
            return storeBL.DeleteItem(itemID) ? "success" : "failure";
        }
    }
}