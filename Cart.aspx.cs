using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StoreWeb.BLL;
using StoreWeb.Models;

namespace StoreWeb
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var bl = new StoreBL())
            {
                decimal orderTotal = 0;
                orderTotal = bl.GetTotal();
                if (orderTotal > 0)
                {
                    lblTotal.Text = String.Format("{0:c}", orderTotal);
                }
                else
                {
                    LabelTotalText.Text = "";
                    lblTotal.Text = "";
                    CartTitle.InnerText = "Shopping Cart is Empty";
                    UpdateBtn.Visible = false;
                }
            }
        }

        public List<OrderItem> GetOrderItems()
        {
            var bl = new StoreBL();
            return bl.GetOrderItems();
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateOrderItems();
        }

        public List<OrderItem> UpdateOrderItems()
        {
                var bl = new StoreBL();
                var orderId = bl.GetOrderId();

                var orderItemUpdates = new StoreBL.OrderItemUpdates[CartList.Rows.Count];

                for (var i = 0; i < CartList.Rows.Count; i++)
                {
                    var rowValues = GetValues(CartList.Rows[i]);
                    orderItemUpdates[i].ItemID = Convert.ToInt32(rowValues["ItemId"]);

                    var cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remove");
                    orderItemUpdates[i].RemoveItem = cbRemove.Checked;

                    var quantityTextBox = (TextBox)CartList.Rows[i].FindControl("Quantity");
                    orderItemUpdates[i].Quantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                }
                bl.UpdateOrderDatabase(orderId, orderItemUpdates);
                CartList.DataBind();
                lblTotal.Text = String.Format("{0:c}", bl.GetTotal());
                return bl.GetOrderItems();
        }

        public static IOrderedDictionary GetValues(GridViewRow row)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.Visible)
                {
                    cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
                }
            }
            return values;
        }
    }
}