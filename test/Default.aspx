<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
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
            <li><a href="#gridview">GridView Style Report</a></li>
            <li><a href="#datatable">jQuery Datatable Style Report</a></li>
        </ul>
        <div id="gridview">
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </div>
        <div id="datatable">
            dataTable goes here

        </div>
    </div><!--end tabs-->
</asp:Content>

