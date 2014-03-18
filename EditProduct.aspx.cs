using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using StoreWeb.BLL;
using StoreWeb.DAL;
using StoreWeb.Models;

namespace StoreWeb
{
    public partial class EditProduct : System.Web.UI.Page
    {
        private List<string> _validationErrors;

        public List<string> validationErrors
        {
            get { return _validationErrors; }
            set { _validationErrors = value; }
        }

        private List<Category> _categories;

        public List<Category> categories
        {
            get { return _categories; }
            set { _categories = value; }
        }


        public string productImage { get; set; }

        public string selectedCategory { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ItemID"] == null)
                Response.Redirect("/Admin");

            itemID = int.Parse(Request.QueryString["ItemID"]);
            validationErrors = new List<string>();
            var bl = new StoreBL();
            categories = bl.GetCategories();
            var requestedItem = bl.GetItem(itemID);
            clearanceItemBool = requestedItem is ClearanceItem;
            var clearanceItem = requestedItem as ClearanceItem;
            if (!IsPostBack)
            {             
                productName.Text = requestedItem.Name;
                productDescription.Text = requestedItem.Description;
                productPrice.Text = requestedItem.Price.ToString(CultureInfo.InvariantCulture);
                productSalePrice.Text = requestedItem.SalePrice.ToString();
                productSaleExpiration.Text = requestedItem.SaleExpiration.ToString();
                productImage = requestedItem.ImageName;
                selectedCategory = requestedItem.Category.CategoryName;
                if (clearanceItemBool)
                {
                    if (clearanceItem != null)
                    {
                        productClearancePrice.Text = clearanceItem.ClearancePrice.ToString();
                        productClearanceQuantity.Text = clearanceItem.ItemsLeft.ToString();
                    }
                }
            }

            if (IsPostBack)
            {
                var categorySelected = Request.Form.Get("dropDownList");
                var fn = "";
                if (string.IsNullOrEmpty(productName.Text))
                {
                    validationErrors.Add("Product Name Required.");
                }
                if (string.IsNullOrEmpty(productPrice.Text))
                {
                    validationErrors.Add("Product Price Required.");
                }
                if (string.IsNullOrEmpty(categorySelected))
                {
                    validationErrors.Add("Product Category Required.");
                }
                if (!string.IsNullOrEmpty(productPrice.Text))
                {
                    decimal d;
                    if (!decimal.TryParse(productPrice.Text, out d))
                    {
                        validationErrors.Add("Product price must be a decimal number.");
                    }
                }
                if (!string.IsNullOrEmpty(productSalePrice.Text))
                {
                    decimal d;
                    if (!decimal.TryParse(productSalePrice.Text, out d))
                    {
                        validationErrors.Add("Product sale price must be a decimal number.");
                    }
                }

                if (clearanceItemBool)
                {
                    if (string.IsNullOrEmpty(productClearancePrice.Text))
                    {
                        validationErrors.Add("Product Clearance Price Required.");
                    }
                    if (!string.IsNullOrEmpty(productClearancePrice.Text))
                    {
                        decimal d;
                        if (!decimal.TryParse(productClearancePrice.Text, out d))
                        {
                            validationErrors.Add("Product clearance price must be a decimal number.");
                        }
                    }

                    if (string.IsNullOrEmpty(productClearanceQuantity.Text))
                    {
                        validationErrors.Add("Clearance Quantity Left Required.");
                    }
                    if (!string.IsNullOrEmpty(productClearanceQuantity.Text))
                    {
                        int d;
                        if (!int.TryParse(productClearanceQuantity.Text, out d))
                        {
                            validationErrors.Add("Clearance Quantity Must Be A Valid Integer.");
                        }
                    }
                }
                if (validationErrors.Any())
                {
                    productImage = requestedItem.ImageName;
                    selectedCategory = requestedItem.Category.CategoryName;
                    return;
                }

                if ((inputFile.PostedFile != null) && (inputFile.PostedFile.ContentLength > 0))
                {
                    if (inputFile.PostedFile.ContentType.ToLower().Contains("jpeg"))
                    {
                        fn = Path.GetFileName(inputFile.PostedFile.FileName);
                        var fileExtension = fn.Substring(fn.LastIndexOf('.'));
                        var fileWithoutExtension = fn.Substring(0, fn.LastIndexOf('.'));
                        var existingImage = bl.GetExistingImage(fileWithoutExtension);
                        if (existingImage.Count > 0)
                        {                          
                            fn = fileWithoutExtension + (existingImage.Count + 1) + fileExtension;
                        }
                        var saveLocation = Server.MapPath("~") + "/content/images/" + fn;
                        try
                        {
                            inputFile.PostedFile.SaveAs(saveLocation);
                        }
                        catch (Exception ex)
                        {
                            Response.Write("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        productImage = requestedItem.ImageName;
                        selectedCategory = requestedItem.Category.CategoryName;
                        validationErrors.Add("Please upload a .jpg/.jpeg file.");
                        return;
                    }
                }

                var item = bl.GetItem(itemID);

                var name = productName.Text;
                var description = productDescription.Text;

                var categoryID = int.Parse(categorySelected);

                var price = decimal.Parse(productPrice.Text);
                decimal? salePrice = null;
                if (!string.IsNullOrEmpty(productSalePrice.Text))
                    salePrice = decimal.Parse(productSalePrice.Text);

                var fileName = "";
                fileName = !string.IsNullOrEmpty(fn) ? fn : item.ImageName;

                DateTime? saleExpiration = null;
                if (!string.IsNullOrEmpty(productSaleExpiration.Text))
                    saleExpiration = DateTime.Parse(productSaleExpiration.Text);
                decimal? clPrice = null;
                int? clQty = null;
                if (clearanceItemBool)
                {
                    clPrice = decimal.Parse(productClearancePrice.Text);
                    clQty = int.Parse(productClearanceQuantity.Text);
                }

                bl.UpdateSingleItem(itemID, name, description, categoryID, price, salePrice, saleExpiration, fileName, clPrice, clQty, clearanceItemBool);
                
                Response.Redirect("/Admin?success=2");
            }
        }

        [WebMethod]
        public static int MarkAsClearance(string itemID, string clearancePrice, string quantity)
        {
            var storeBL = new BLL.StoreBL();
            return storeBL.MarkItemAsClearance(itemID, clearancePrice, quantity);
        }

        public int itemID { get; set; }

        public bool clearanceItemBool { get; set; }
    }
}