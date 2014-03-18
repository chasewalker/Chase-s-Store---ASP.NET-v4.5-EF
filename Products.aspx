<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="StoreWeb.Products" %>
<%@ Import Namespace="System.Security.Policy" %>
<%@ Import Namespace="StoreWeb.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <% if (!string.IsNullOrEmpty(categoryString))
           { %>
                <h1><%= categoryString %></h1>  
        <p>
            <% 
                foreach (var category in categories)
               {
                    var cssClass = "btn-default";
                    if (category.CategoryName == categoryString)
                        cssClass = "btn-success";
                   %>
                  <a href="/Products.aspx?category=<%=category.CategoryID%>" class="btn <%=cssClass %>"><%=category.CategoryName %></a> 
              <% } %>
            
        </p>    
        <% }
           else
           { %>
                <h1>Products</h1> 
        
        <% } %>
    </div>
    <div class="row">
        <% foreach (var item in items)
           { %>
        <div class="col-md-4">
            <h2><%=item.Name %></h2>
            <p><span class="badge alert-success"><%=item.Category.CategoryName %></span></p>
            <p>
                <%=item.Description %>
            </p>
            <p>
                <% if (!string.IsNullOrEmpty(item.ImageName))
                   {%>
                       <img src="Content/images/<%=item.ImageName %>"/>
                   <%}
                   else
                   {%>
                       <img src="Content/images/noimage.jpg"/>
                   <%} %>
            </p>
            <p>
                <% if (item.SalePrice != null)
                   { %>
                <span class="line-through" style="font-size: 13px;">$<%= item.Price %></span>
                <span style="color:red;font-weight: bold;">$<%= item.SalePrice %></span>
                <% if (item.SaleExpiration != null)
                   { %>
                     <span style="color:red;"> (Sale Expires <%= DateTime.Parse(item.SaleExpiration.ToString()).ToShortDateString() %>)</span>  
                   <% } %>
                <% }
                   else if(item is ClearanceItem)
                   {
                        var clearanceItem = item as ClearanceItem;%>
                        <span class="line-through" style="font-size: 13px;">$<%= item.Price %></span>
                        <span style="color:red;font-weight: bold;">$<%= clearanceItem.ClearancePrice%></span>
                <span class="label label-danger">Clearance Item! Only <%=clearanceItem.ItemsLeft %> left!</span>
                  <% }else
                   { %>
                       $<%= item.Price %>
                  <% } %>
            </p>
            <p>
                <p>
                    <a href="/AddToCart.aspx?itemID=<%=item.ItemID %>">               
                                        <span class="btn btn-primary btn-large">
                                            Add To Cart &raquo;
                                        </span>           
                                    </a>
                </p>
            </p>
        </div>
        <% } %>
    </div>
</asp:Content>
