using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StoreWeb.Models;

namespace StoreWeb
{
    public partial class Products : System.Web.UI.Page
    {
        private List<Item> _items;

        public List<Item> items
        {
            get { return _items; }
            set { _items = value; }
        }

        private List<Category> _categories;

        public List<Category> categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        private string _categoryString;

        public string categoryString
        {
            get { return _categoryString; }
            set { _categoryString = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var category = Request.QueryString["category"];
            var categoryID = 0;
            if (!string.IsNullOrEmpty(category))
            {
                categoryID = int.Parse(category);
            }
            var storeBL = new BLL.StoreBL();
            categories = storeBL.GetCategories();
            categoryString = categoryID == 0 ? "All Products" : storeBL.GetCategories().Where(x => x.CategoryID == categoryID).Select(s => s.CategoryName).First();
            items = storeBL.GetItems(categoryID);
        }
    }
}