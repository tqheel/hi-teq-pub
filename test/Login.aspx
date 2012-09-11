<%@ Page Title="" Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Enter Your Login Credentials to Access the System</h2>
    <asp:Label ID="lblError" runat="server" CssClass="error" Text="Label"></asp:Label>
    <dl class="form">
        <dt>Enter Name:</dt>
        <dd><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></dd>
        <dt>Enter E-mail Address:</dt>
        <dd><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></dd>
        <dt>Enter Password:</dt>
        <dd><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <span class="tip">For demo purposes, the password is </span>
            <asp:Label ID="lblPassword" runat="server" Text="Label" CssClass="tip"></asp:Label>
        </dd>
        <dt>&nbsp;</dt>
        <dd>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /></dd>
    </dl>
    

</asp:Content>

