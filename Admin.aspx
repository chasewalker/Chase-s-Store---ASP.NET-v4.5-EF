<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="StoreWeb.Admin" %>
<%@ Import Namespace="StoreWeb.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="/Scripts/jquery.dataTables.min.js"></script>
    <div class="jumbotron">
        <h1>Admin Utilities</h1>
        <p class="lead">Manage Products</p>

    </div>
        <% if (!string.IsNullOrEmpty(success))
       {%>
         <div class="alert alert-dismissable alert-info">
          <button type="button" class="close" data-dismiss="alert">×</button>
          <strong><%=success %></strong>.
        </div>  
       <%} %>
    <h2>Products</h2>
    <p>
        <a href="/AddProduct.aspx" class="btn btn-success">Create new product</a>
    </p>
    <div class="table-responsive">
        <table id="products" class="table table-striped table-bordered table-hover">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Category</th>
                    <th>Price</th>
                    <th>Sale Price</th>
                    <th>Sale Expiration</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var item in items)
                   { %>
                <tr>
                    <td><%=item.ItemID %></td>
                    <td>
                        <%=item.Name %> 
                        <% if (item is ClearanceItem)
                           {
                               var clearanceItem = item as ClearanceItem; %>
                              <span class="label label-danger">Clearance Item!</span> <span class="label label-success"><%=clearanceItem.ItemsLeft %> left</span>
                           <% } %>
                    </td>
                    <td><%=item.Description %></td>
                    <td><%=item.Category.CategoryName %></td>
                    <td>
                        <%=item.Price %>
                        <% if (item is ClearanceItem)
                           {
                               var clearanceItem = item as ClearanceItem; %>
                              <span class="label label-danger">Clearance Price: <%=clearanceItem.ClearancePrice %></span>
                           <% } %>
                    </td>
                    <td><%=item.SalePrice %></td>
                    <td><%=item.SaleExpiration %></td>
                    <td>
                        <a href="EditProduct.aspx?itemID=<%=item.ItemID %>">
                            <span class="glyphicon glyphicon-pencil"></span> Edit 
                        </a>
                        <a href="#" id="item-<%=item.ItemID %>" class="deleteItem">
                            <span class="glyphicon glyphicon-remove"></span> Delete 
                        </a>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#products').dataTable({
                "aLengthMenu": [
                [25, 50, 100, 200, -1],
                [25, 50, 100, 200, "All"]
                ],


                "iDisplayLength": -1
            });
            $(document).on("click", "a.deleteItem", function () {
                if (confirm('Are you sure you want to delete this item?')) {
                    var id = $(this).attr('id').replace(/^\D+/g, '');
                    $.ajax({
                        type: "POST",
                        url: "Admin.aspx/DeleteItem",
                        contentType: "application/json; charset=utf-8",
                        data: '{itemID:' + id + '}',
                        success: function(data) {
                            window.location.reload();
                        },
                        error: function(data) {
                        }
                    });
                }
            });
        });       
    </script>
</asp:Content>
