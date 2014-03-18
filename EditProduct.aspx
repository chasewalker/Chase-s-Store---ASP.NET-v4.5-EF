<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="StoreWeb.EditProduct" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit product</h2>
    <div class="panel panel-default">
        <div class="panel panel-body">
            <% if (validationErrors.Any())
               {%>
            <div class="alert alert-danger">
                <ul>
                  <% foreach(var v in validationErrors){%>
                 <li><%=v %> </li>
               <%}%>
                    </ul>
                 </div>
               <%} %>
            <% if (clearanceItemBool)
               {%>
                 <span class="label label-danger">Clearance Item</span>  
               <%} %>
            <div class="form-group">
                <label for="productName">Name*</label>
                <asp:TextBox runat="server" ID="productName" CssClass="form-control" placeholder="Enter product name"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="productDescription">Description</label>
                <asp:TextBox ID="productDescription" TextMode="multiline" Columns="50" Rows="5" runat="server" CssClass="form-control" placeholder="Enter product description" />
            </div>
            <div class="form-group">
                <label for="productCategory">Category*</label>
                <select id="dropDownList" name="dropDownList" class="form-control">
                    <option value="">Please Select an Option</option>
                    <% foreach (var category in categories)
                       {
                           var selected = "";
                           if (selectedCategory == category.CategoryName)
                               selected = " selected='selected'";
                           %>
                           <option value="<%=category.CategoryID %>"<%=selected %>><%=category.CategoryName %></option>
                       <%} %>
                </select>
            </div>
            <div class="form-group">
                <label for="productPrice">Price*</label>
                <div class="input-group">
                    <span class="input-group-addon">$</span>
                    <asp:TextBox runat="server" ID="productPrice" CssClass="form-control" placeholder="Enter product price"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="productSalePrice">Sale Price</label>
                <div class="input-group">
                    <span class="input-group-addon">$</span>
                    <asp:TextBox runat="server" ID="productSalePrice" CssClass="form-control" placeholder="Enter sale price"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="productSaleExpiration">Sale Expiration Date</label>
                <asp:TextBox runat="server" ID="productSaleExpiration" CssClass="form-control datepicker"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="productImage">Image</label>
                <% if (!string.IsNullOrEmpty(productImage))
                   {%>
                       <img src="Content/images/<%=productImage %>"/>
                   <%}
                   else
                   {%>
                       <img src="Content/images/noimage.jpg"/>
                   <%} %>
            </div>
            <div class="form-group">
                <label for="inputFile">Choose a new product image</label>
                <input type="file" id="inputFile" runat="server" />
                <p class="help-block">upload a .jpg image</p>
            </div>
            <% if (clearanceItemBool)
               {%>
                 <div class="form-group">
                <label for="productClearancePrice">Clearance Price</label>
                <div class="input-group">
                    <span class="input-group-addon">$</span>
                    <asp:TextBox runat="server" ID="productClearancePrice" CssClass="form-control" placeholder=""></asp:TextBox>
                </div>
            </div>  
            <div class="form-group">
                <label for="productClearanceQuantity">Items Left?</label>
                <asp:TextBox runat="server" ID="productClearanceQuantity" CssClass="form-control" placeholder=""></asp:TextBox>
            </div>
               <%} %>
            <input type="submit" value="Submit" class="btn btn-success btn-sm"/>
            <% if (!clearanceItemBool)
               {%>
                   <input type="button" value="Mark as clearance" class="btn btn-danger btn-sm" id="clearanceButton"/>
               <%} %>          
            <input type="hidden" id="productID" value="<%=itemID %>"/>
        </div>
        <h3> * = required</h3>
    </div>
  <% if (!clearanceItemBool)
     { %>
    <div class="modal" id="clearanceItemModal">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
        <h4 class="modal-title">Mark this item as clearance.</h4>
      </div>
      <div class="modal-body">
          <div class="alert alert-danger" id="clearanceErrors" style="display:none;"></div>
        <div class="form-group">
                <label for="clearancePriceInput">Clearance Price</label>
                <div class="input-group">
                    <span class="input-group-addon">$</span>
                    <input type="text" id="clearancePriceInput" name="clearancePriceInput" placeholder="Enter clearance price" class="form-control"/>
                </div>
            </div>
          <div class="form-group">
                <label for="clearancePriceInput">Items Left?</label>
                <div class="input-group">
                    <input type="text" id="clearanceItemsLeft" name="clearanceItemsLeft" placeholder="" class="form-control"/>
                </div>
            </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary" id="saveClearanceItem">Save changes</button>
      </div>
    </div>
  </div>
</div>
    <% } %>
    <script type="text/javascript">
        $('.datepicker').datepicker();
        $('#clearanceButton').on('click', function() {
            $('#clearanceItemModal').modal();
        });
        $('#saveClearanceItem').on('click', function () {
            var id = $('#productID').val().replace(/^\D+/g, '');
            var decimalRegex = /^\s*(\+|-)?((\d+(\.\d+)?)|(\.\d+))\s*$/;
            var clearanceInput = $('#clearancePriceInput').val().trim();
            var itemsLeft = $('#clearanceItemsLeft').val().trim();
            var errorCount = 0;
            $('#clearanceErrors').hide();
            $('#clearanceErrors').empty();
            if (clearanceInput == "") {
                $('#clearanceErrors').append("Please enter a clearance price. ");
                $('#clearanceErrors').show();
                errorCount++;
            } else if(!decimalRegex.test(clearanceInput)) {
                $('#clearanceErrors').append("Please enter a valid decimal. ");
                $('#clearanceErrors').show();
                errorCount++;
            }
            if (itemsLeft == "") {
                $('#clearanceErrors').append("Please enter amount of items left. ");
                $('#clearanceErrors').show();
                errorCount++;
            }
            else if (!$.isNumeric(itemsLeft)) {
                $('#clearanceErrors').append("Please enter a valid integer for items left. ");
                $('#clearanceErrors').show();
                errorCount++;
            }
            if (errorCount == 0) {
                $.ajax({
                    type: "POST",
                    url: "EditProduct.aspx/MarkAsClearance",
                    contentType: "application/json; charset=utf-8",
                    data: '{itemID:' + id + ', clearancePrice:' + clearanceInput + ', quantity:' + itemsLeft + '}',
                    success: function (data) {
                        window.location.href = "EditProduct.aspx?itemID=" + data.d;
                    },
                    error: function (data) {
                    }
                });
            }
        });
    </script>
</asp:Content>
