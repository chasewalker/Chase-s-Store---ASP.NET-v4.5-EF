<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="StoreWeb.Cart" %>
<%@ Import Namespace="System.Activities.Statements" %>
<%@ Import Namespace="StoreWeb.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CartTitle" runat="server" class="ContentHead"><h1>Shopping Cart</h1></div>
    <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="False" ShowFooter="True" GridLines="Vertical" CellPadding="4"
        ItemType="StoreWeb.Models.OrderItem" SelectMethod="GetOrderItems" 
        CssClass="table table-striped table-bordered" >   
        <Columns>
        <asp:BoundField DataField="ItemId" HeaderText="ID" SortExpression="ItemId"/>       
        <asp:BoundField DataField="Item.Name" HeaderText="Name" />        
<%--        <asp:BoundField DataField="Item.Price" HeaderText="Price (each)" DataFormatString="{0:c}"/> --%>    
            <asp:TemplateField HeaderText="Price (each)">            
                <ItemTemplate>
                    <%# Eval("Item.SalePrice") == null ? String.Format("{0:c}", (Convert.ToDouble(Item.Item.Price))) : String.Format("{0:c}", (Convert.ToDouble(Item.Item.SalePrice)))%>
                </ItemTemplate>        
        </asp:TemplateField>
        <asp:TemplateField   HeaderText="Quantity">            
                <ItemTemplate>
                    <asp:TextBox ID="Quantity" Width="40" runat="server" Text="<%#: Item.Quantity %>"></asp:TextBox> 
                </ItemTemplate>        
        </asp:TemplateField>    
        <asp:TemplateField HeaderText="Item Total">            
                <ItemTemplate>
                    <%# Eval("Item.SalePrice") == null ? String.Format("{0:c}", ((Convert.ToDouble(Item.Quantity)) *  Convert.ToDouble(Item.Item.Price))) : String.Format("{0:c}", ((Convert.ToDouble(Item.Quantity)) *  Convert.ToDouble(Item.Item.SalePrice)))%>
                </ItemTemplate>        
        </asp:TemplateField> 
        <asp:TemplateField HeaderText="Remove Item">            
                <ItemTemplate>
                    <asp:CheckBox id="Remove" runat="server"></asp:CheckBox>
                </ItemTemplate>        
        </asp:TemplateField>    
        </Columns>    
    </asp:GridView>
    <div>
        <p></p>
        <strong>
            <asp:Label ID="LabelTotalText" runat="server" Text="Order Total: "></asp:Label>
            <asp:Label ID="lblTotal" runat="server" EnableViewState="false"></asp:Label>
        </strong> 
    </div>
    <br />
    <div>
        <p></p>
        <asp:Button ID="UpdateBtn" runat="server" Text="Update" OnClick="UpdateBtn_Click" />
    </div>
</asp:Content>
