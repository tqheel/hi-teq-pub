<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/html" id="royalty-template">
        {#template MAIN}
            <table id="royalty-table">
                <thead>
                    <th>Store Name</th>
                    <th>Order Number</th>
                    <th>Order Date</th>
                    <th>Title</th>
                    <th>Author</th>
                    <th>Royalty</th>
                </thead>
                {#foreach $T.d as royalty}
                    {#include ROW root=$T.royalty}
                {#/for}
            </table>
        {#/template MAIN}
        {#template ROW}
            <tr class="{#cycle values=['','evenRow']}">
                <td>>{$T.StoreName}</td>
                <td>{$T.OrderNumber}</td>
                <td>{$T.OrderDate}</td>
                <td>{$T.Title}</td>
                <td>{$T.Author}</td>
                <td>{$T.Royalty}</td>
            </tr>
        {#/template ROW}
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#tabs').tabs();
        });//end doc ready block

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Publisher Reports</h3>
    
    <div class="pageText" id="tabs">
        <ul>
            <li><a href="#gridview">GridView Control Report Example</a></li>
            <li><a href="#datatable">jQuery Datatable Report Example</a></li>
        </ul>
        
        <div id="gridview">
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="ddlPubs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPubs_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    Loading Table...Please Wait...
                </ProgressTemplate>
                
            </asp:UpdateProgress>
                <h4>Author Royalties Per Title/Author</h4>
                <asp:GridView ID="GridView1" runat="server"
                            CssClass="altCellWhite"
                            AlternatingRowStyle-CssClass="altCellGray" >
                </asp:GridView>
                <h4>PR and Sales Contacts for the Selected Publisher</h4>
                <asp:GridView ID="GridView2" runat="server"
                    CssClass="altCellWhite"
                            AlternatingRowStyle-CssClass="altCellGray" >
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlPubs" EventName="SelectedIndexChanged" />

            </Triggers>

            </asp:UpdatePanel>
            
        </div>
        <div id="datatable">
            <asp:DropDownList ID="ddlPubsJquery" runat="server" AutoPostBack="False">
            </asp:DropDownList>

        </div>
    </div><!--end tabs-->
</asp:Content>

