using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StoreWeb.BLL;

namespace StoreWeb
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["ItemID"];
            int itemId;
            if (!String.IsNullOrEmpty(id) && int.TryParse(id, out itemId))
            {
                using (var order = new StoreBL())
                {
                    order.AddToCart(Convert.ToInt16(id));
                }

            }
            else
            {
                throw new Exception("Must specify an item id.");
            }
            Response.Redirect("Cart.aspx");
        }
    }
}