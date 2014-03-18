<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StoreWeb.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Chase's Store</h1>
        <p class="lead">Specializing in item goodness</p>
        
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Products</h2>
            <p>
                View our awesome products!
            </p>
            <p>
                <p><a href="/Products" class="btn btn-primary btn-large">View our products &raquo;</a></p>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Admins</h2>
            <p>
                This is where you update your store items.
            </p>
            <p>
                <p><a href="/Admin" class="btn btn-primary btn-large">Edit Products &raquo;</a></p>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Shopping Cart</h2>
            <p>
                See the damage!
            </p>
            <p>
                <p><a href="/Cart" class="btn btn-primary btn-large">Go to cart &raquo;</a></p>
            </p>
        </div>
    </div>

</asp:Content>
