<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="StoreWeb.AddProduct" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create new product</h2>
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
                       {%>
                           <option value="<%=category.CategoryID %>"><%=category.CategoryName %></option>
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
                <label for="inputFile">Choose a product image</label>
                <input type="file" id="inputFile" runat="server" />
                <p class="help-block">upload a .jpg image</p>
            </div>
            <input type="submit" value="Submit" class="btn btn-success btn-sm"/>
            <%--<asp:Button runat="server" Text="Add" OnClick="addProduct_Click" CssClass="btn btn-success btn-sm" />--%>
        </div>
        <h3> * = required</h3>
    </div>

    <script type="text/javascript">
        $('.datepicker').datepicker();
    </script>
</asp:Content>
