using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreWeb.Models;

namespace StoreWeb.DAL
{
    public class StoreInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {

            GetCategories().ForEach(c => context.Categories.Add(c));
            GetItems().ForEach(p => context.Items.Add(p));
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category>
            {
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "Electronics"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Sports"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Books"
                },
                new Category
                {
                    CategoryID = 4,
                    CategoryName = "Movies"
                },
                new Category
                {
                    CategoryID = 5,
                    CategoryName = "Games"
                },
            };
            return categories;
        }

        private static List<Item> GetItems()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Canon 5D Mark III",
                    CategoryID = 1,
                    Description = "Digital SLR Camera Description",
                    ImageName = "canonmarkiii.jpg",
                    Price = (decimal) 2399.99,
                    SalePrice = (decimal) 2299.99,
                    SaleExpiration = new DateTime(2014, 3, 30)
                },
                new Item
                {
                    Name = "Chase's Kayak",
                    CategoryID = 2,
                    Description = "Chase's Kayak Description",
                    ImageName = "chasekayak.jpg",
                    Price = (decimal) 999.99,
                    SalePrice = null,
                    SaleExpiration = null
                },
                new Item
                {
                    Name = "To Kill a Mockingbird - by Harper Lee (1960)",
                    CategoryID = 3,
                    Description = "Book Description",
                    ImageName = "mockingbird.jpg",
                    Price = (decimal) 9.99,
                    SalePrice = null,
                    SaleExpiration = null
                },
                new Item
                {
                    Name = "Non-Stop (2014)",
                    CategoryID = 4,
                    Description = "Liam Neeson",
                    ImageName = "nonstop.jpg",
                    Price = (decimal) 29.99,
                    SalePrice = null,
                    SaleExpiration = null
                },
                new Item
                {
                    Name = "NBA 2K14",
                    CategoryID = 5,
                    Description = "NBA 2k14 Description",
                    ImageName = "2k14.jpg",
                    Price = (decimal) 59.99,
                    SalePrice = null,
                    SaleExpiration = null
                },
                new Item
                {
                    Name = "Algorithms in C",
                    CategoryID = 3,
                    Description = "Classic Algorithms Textbook",
                    ImageName = "algorithms.jpg",
                    Price = (decimal) 59.99,
                    SalePrice = null,
                    SaleExpiration = null
                },
                new Item
                {
                    Name = "Honda ATV 4x4",
                    CategoryID = 2,
                    Description = "Honda ATV",
                    ImageName = "atv.jpg",
                    Price = (decimal) 3459.99,
                    SalePrice = (decimal) 3399.99,
                    SaleExpiration = new DateTime(2014, 3, 30)
                },
                new Item
                {
                    Name = "Deer Camera",
                    CategoryID = 1,
                    Description = "Deer Camera Description",
                    ImageName = "deercamera.jpg",
                    Price = (decimal) 149.99,
                    SalePrice = null,
                    SaleExpiration = null
                },
                new Item
                {
                    Name = "DVD Player",
                    CategoryID = 1,
                    Description = "DVD Player Description",
                    ImageName = "dvdplayer.jpg",
                    Price = (decimal) 39.99,
                    SalePrice = (decimal) 24.99,
                    SaleExpiration = new DateTime(2014, 4, 8)
                },
                new Item
                {
                    Name = "Rawlings Football",
                    CategoryID = 2,
                    Description = "Rawlings Football",
                    ImageName = "football.jpg",
                    Price = (decimal) 19.99,
                    SalePrice = null,
                    SaleExpiration = null
                },
                new Item
                {
                    Name = "Apple iPhone 5",
                    CategoryID = 1,
                    Description = "Apple iPhone 5 Description",
                    ImageName = "iphone.jpg",
                    Price = (decimal) 599.99,
                    SalePrice = (decimal) 499.99,
                    SaleExpiration = new DateTime(2014, 4, 8)
                },
                new Item
                {
                    Name = "Samsung 55-inch Smart TV",
                    CategoryID = 1,
                    Description = "Samsung's latest Smart TV",
                    ImageName = "smarttv.jpg",
                    Price = (decimal) 1599.99,
                    SalePrice = (decimal) 1399.99,
                    SaleExpiration = new DateTime(2014, 4, 8)
                },
                new ClearanceItem
                {
                    Name = "Nike Shoes",
                    CategoryID = 2,
                    Description = "Nike's Latest Tennis Shoes",
                    ImageName = "nike.jpg",
                    Price = (decimal) 79.99,
                    SalePrice = null,
                    SaleExpiration = null,
                    ClearancePrice = (decimal) 14.97,
                    ItemsLeft = 2
                }
            };
            return items;
        }
    }
}