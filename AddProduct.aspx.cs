using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StoreWeb.BLL;
using StoreWeb.DAL;
using StoreWeb.Models;

namespace StoreWeb
{
    public partial class AddProduct : System.Web.UI.Page
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


        protected void Page_Load(object sender, EventArgs e)
        {
            validationErrors = new List<string>();
            var bl = new StoreBL();
            categories = bl.GetCategories();
            if (IsPostBack)
            {
                var name = productName.Text.TrimStart().TrimEnd();
                var price = productPrice.Text.Trim();
                var categorySelected = Request.Form.Get("dropDownList");
                var fn = "";
                if (string.IsNullOrEmpty(name))
                {
                    validationErrors.Add("Product Name Required.");
                }
                if (string.IsNullOrEmpty(price))
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

                if (validationErrors.Any()) return;

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
                        validationErrors.Add("Please upload a .jpg/.jpeg file.");
                        return;
                    }
                }
                

                var item = new Item {Name = productName.Text, Description = productDescription.Text};

                if (categorySelected != null) item.CategoryID = int.Parse(categorySelected);

                item.Price = decimal.Parse(productPrice.Text);

                if (!string.IsNullOrEmpty(productSalePrice.Text))
                    item.SalePrice = decimal.Parse(productSalePrice.Text);

                if (!string.IsNullOrEmpty(productSaleExpiration.Text))
                    item.SaleExpiration = DateTime.Parse(productSaleExpiration.Text);

                if (!string.IsNullOrEmpty(fn))
                    item.ImageName = fn;

                bl.AddItem(item);
                
                Response.Redirect("/Admin?success=1");
            }
        }

        //protected void addProduct_Click(object sender, EventArgs e)
        //{
        //    var categorySelected = "";
        //    var item = new Item();
        //    if (string.IsNullOrEmpty(productName.Text))
        //    {              
        //        validationErrors.Add("Product Name Required.");
        //    }
        //    if (string.IsNullOrEmpty(productName.Text))
        //    {
        //        validationErrors.Add("Product Price Required.");
        //    }
        //    if (string.IsNullOrEmpty(categorySelected))
        //    {
        //        validationErrors.Add("Product Category Required.");
        //    }

        //    if ((inputFile.PostedFile != null) && (inputFile.PostedFile.ContentLength > 0))
        //    {
        //        if (inputFile.PostedFile.ContentType.ToLower().Contains("jpeg"))
        //        {
        //            var fn = System.IO.Path.GetFileName(inputFile.PostedFile.FileName);
        //            var saveLocation = Server.MapPath("~") + "/content/images/" + fn;
        //            try
        //            {
        //                inputFile.PostedFile.SaveAs(saveLocation);
        //                Response.Write("The file has been uploaded.");
        //            }
        //            catch (Exception ex)
        //            {
        //                Response.Write("Error: " + ex.Message);
        //            }
        //        }
        //        else
        //        {
        //            validationErrors.Add("Please upload a .jpg/.jpeg file.");
        //        }
        //    }
        //    if (validationErrors.Any())
        //    {
        //        return;
        //    }
           
        //    var bl = new StoreBL();
            
        //}
    }
}