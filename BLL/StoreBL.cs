using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using StoreWeb.DAL;
using StoreWeb.Models;

namespace StoreWeb.BLL
{
    public class StoreBL : IDisposable
    {
        readonly StoreContext _db = new StoreContext();

        public string OrderId { get; set; }

        public const string OrderSessionKey = "OrderId";

        public void AddToCart(int id)
        {

            OrderId = GetOrderId();

            var orderItem = _db.OrderItems.SingleOrDefault(
                c => c.OrderId == OrderId
                && c.ItemId == id);
            if (orderItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                orderItem = new OrderItem
                {
                    OrderItemId = Guid.NewGuid().ToString(),
                    ItemId = id,
                    OrderId = OrderId,
                    Item = _db.Items.SingleOrDefault(
                     p => p.ItemID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                _db.OrderItems.Add(orderItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                orderItem.Quantity++;
            }
            _db.SaveChanges();
        }

        public string GetOrderId()
        {
            if (HttpContext.Current.Session[OrderSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[OrderSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[OrderSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[OrderSessionKey].ToString();
        }

        public decimal GetTotal()
        {
            OrderId = GetOrderId();
            var total = (decimal?) 0.0;
            foreach (var orderItem in _db.OrderItems.Where(x => x.OrderId == OrderId))
            {
                if (orderItem.Item.SalePrice != null && orderItem.Item.SalePrice != decimal.Zero)
                {
                    total += orderItem.Item.SalePrice*orderItem.Quantity;
                }
                else
                {
                    total += orderItem.Item.Price*orderItem.Quantity;
                }
            }
            
            return total ?? decimal.Zero;
        }

        public int GetCartItemCount()
        {
            OrderId = GetOrderId();
            var total = (from orderItems in _db.OrderItems
                         where orderItems.OrderId == OrderId
                         select (int?)orderItems.Quantity).Sum();
            return total ?? 0;
        }

        public List<OrderItem> GetOrderItems()
        {
            OrderId = GetOrderId();

            return _db.OrderItems.Where(
                c => c.OrderId == OrderId).ToList();
        }

        public List<Item> GetItems(int category)
        {
            return category == 0 ? _db.Items.ToList() : _db.Items.Where(x => x.CategoryID == category).ToList();
        }

        public Item GetItem(int itemID)
        {
            return _db.Items.SingleOrDefault(x => x.ItemID == itemID);
        }

        public List<Category> GetCategories()
        {
            return _db.Categories.ToList();
        }

        public void AddItem(Item item)
        {
            item.Category = _db.Categories.SingleOrDefault(x => x.CategoryID == item.CategoryID);
            _db.Items.Add(item);
            _db.SaveChanges();
        }

        public void UpdateOrderDatabase(String orderId, OrderItemUpdates[] OrderItemUpdates)
        {
            try
            {
                var orderItemCount = OrderItemUpdates.Count();
                var myOrder = GetOrderItems();
                foreach (var orderItem in myOrder.ToList())
                {
                    for (var i = 0; i < orderItemCount; i++)
                    {
                        if (orderItem.ItemId == OrderItemUpdates[i].ItemID)
                        {
                            if (OrderItemUpdates[i].Quantity < 1 || OrderItemUpdates[i].RemoveItem)
                            {
                                RemoveItem(orderId, orderItem.ItemId);
                            }
                            else
                            {
                                UpdateItem(orderId, orderItem.ItemId, OrderItemUpdates[i].Quantity);
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Update OrderItem Database - " + exp.Message.ToString(), exp);
            }
        }

        public void RemoveItem(string removeOrderID, int removeItemID)
        {
            try
            {
                var myItem = (from c in _db.OrderItems where c.OrderId == removeOrderID && c.Item.ItemID == removeItemID select c).FirstOrDefault();
                if (myItem != null)
                {
                    // Remove Item.
                    _db.OrderItems.Remove(myItem);
                    _db.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
            }
        }

        public void UpdateItem(string updateOrderID, int updateItemID, int quantity)
        {
            try
            {
                var myItem = (from c in _db.OrderItems where c.OrderId == updateOrderID && c.Item.ItemID == updateItemID select c).FirstOrDefault();
                if (myItem != null)
                {
                    myItem.Quantity = quantity;
                    _db.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
            }
        }

        public void UpdateSingleItem(int itemID, string name, string description, int categoryID, decimal price, decimal? salePrice, DateTime? saleExpiration, string fn, decimal? clPrice, int? clQty, bool clearance)
        {
            
            if (!clearance)
            {
                var item = _db.Items.Find(itemID);
                item.Name = name;
                item.Description = description;
                item.CategoryID = categoryID;
                item.Price = price;
                item.SalePrice = salePrice;
                item.SaleExpiration = saleExpiration;
                item.Category = _db.Categories.FirstOrDefault(x => x.CategoryID == categoryID);
                item.ImageName = fn;
                _db.SaveChanges();
            }
            else
            {
                var clearanceItem = (ClearanceItem) _db.Items.Find(itemID);
                clearanceItem.Name = name;
                clearanceItem.Description = description;
                clearanceItem.CategoryID = categoryID;
                clearanceItem.Price = price;
                clearanceItem.SalePrice = salePrice;
                clearanceItem.SaleExpiration = saleExpiration;
                clearanceItem.Category = _db.Categories.FirstOrDefault(x => x.CategoryID == categoryID);
                clearanceItem.ImageName = fn;
                if (clPrice != null) clearanceItem.ClearancePrice = (decimal) clPrice;
                if (clQty != null) clearanceItem.ItemsLeft = (int) clQty;
                _db.SaveChanges();
            }
            
            
            
        }

        public void EmptyCart()
        {
            OrderId = GetOrderId();
            var orderItems = _db.OrderItems.Where(
                c => c.OrderId == OrderId);
            foreach (var orderItem in orderItems)
            {
                _db.OrderItems.Remove(orderItem);
            }
            _db.SaveChanges();
        }

        public struct OrderItemUpdates
        {
            public int ItemID;
            public int Quantity;
            public bool RemoveItem;
        }

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool DeleteItem(string itemID)
        {
            var id = int.Parse(itemID);
            var myItem = _db.Items.SingleOrDefault(x => x.ItemID == id);
            try
            {
                _db.Items.Remove(myItem);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<string> GetExistingImage(string fn)
        {
            var existingImageList = _db.Items.Where(x => x.ImageName.Contains(fn)).ToList();
            return existingImageList.Select(x => x.ImageName).ToList();
        }

        public int MarkItemAsClearance(string itemId, string clearancePrice, string quantity)
        {
            var id = int.Parse(itemId);
            var existingItem = _db.Items.SingleOrDefault(x => x.ItemID == id);
            if (existingItem != null)
            {
                var clearanceItem = new ClearanceItem
                {
                    Category = existingItem.Category,
                    CategoryID = existingItem.CategoryID,
                    ClearancePrice = decimal.Parse(clearancePrice),
                    Description = existingItem.Description,
                    ImageName = existingItem.ImageName,
                    ItemsLeft = int.Parse(quantity),
                    Name = existingItem.Name,
                    SaleExpiration = existingItem.SaleExpiration,
                    SalePrice = existingItem.SalePrice,
                    Price = existingItem.Price
                };
                try
                {
                    _db.Items.Remove(existingItem);
                    _db.Items.Add(clearanceItem);
                    _db.SaveChanges();
                    return clearanceItem.ItemID;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException(e.Message);
                }
            }
            return 0;
        }
    }
}