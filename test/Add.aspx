<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Add Publishers</h3>
    <dl class="form">
        <dt>Enter Publisher Name:</dt>
        <dd>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <span class="tip">Cannot be blank. Limit 40 characters.</span>
        </dd>
        <dt>Enter City:</dt>
        <dd>
            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            <span class="tip">Limit 20 characters.</span>
        </dd>
        <dt>Select State:</dt>
        <dd>
            <asp:DropDownList ID="ddlState" runat="server">
                <asp:ListItem Text="NC" Value="NC"></asp:ListItem>
                <asp:ListItem Text="TN" Value="TN"></asp:ListItem>
                <asp:ListItem Text="SC" Value="SC"></asp:ListItem>
                <asp:ListItem Text="GA" Value="GA"></asp:ListItem>
                <asp:ListItem Text="MS" Value="MS"></asp:ListItem>
            </asp:DropDownList>
        </dd>
        <dt>&nbsp;</dt>
        <dd>
            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" style="height: 26px" />
            <asp:Button ID="btnReset" runat="server" Text="Add Another" OnClick="btnReset_Click" />
        </dd>
    </dl>
    <asp:Label ID="lblResult" runat="server" Text="The new Publisher has been added."></asp:Label>
    
</asp:Content>

