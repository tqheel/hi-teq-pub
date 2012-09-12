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
            <tr class="{#cycle values=['altCellGray','altCellWhite']}">
                <td>{$T.StoreName}</td>
                <td>{$T.OrderNumber}</td>
                <td>{$T.OrderDate}</td>
                <td>{$T.Title}</td>
                <td>{$T.Author}</td>
                <td>{$T.Royalty}</td>
            </tr>
        {#/template ROW}
    </script>

    <script type="text/html" id="contact-template">
        {#template MAIN}
            <table id="contact-table">
                <thead>
                    <th>Job Title</th>
                    <th>Name</th>
                </thead>
                {#foreach $T.d as contact}
                    {#include ROW root=$T.contact}
                {#/for}
            </table>
        {#/template MAIN}
        {#template ROW}
            <tr class="{#cycle values=['altCellGray','altCellWhite']}">
                <td>{$T.JobTitle}</td>
                <td>{$T.Name}</td>
            </tr>
        {#/template ROW}
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#tabs').tabs();
            getRoyalties();

            //event handler for pub select list change
            $('#<%= ddlPubsJquery.ClientID %>').change(function () {
                getRoyalties();
            });
        });//end doc ready block

        function getRoyalties() {
            $.blockUI({ message: '<h4> Retrieving Author Royalties ... </h4>' });
            var pubID = $('#<%= ddlPubsJquery.ClientID %>').val();
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: 'WebService.asmx/GetRoyalties',
                data: JSON.stringify({ pubID: pubID }),
                dataType: 'json'
            })
                .success(function (data) {
                    $.unblockUI();
                    //Instantiate a template with data
                    applyRoyaltyTemplate(data);
                    getContacts();
                })
                .error(function (xhr) {
                    $.unblockUI();
                    var r = JSON.parse(xhr.responseText);
                    alert(r.Message);

                });
        }

        function applyRoyaltyTemplate(royalties) {
            $('#royalty-container').setTemplate($('#royalty-template').html());
            $('#royalty-container').processTemplate(royalties);
            //Initialize the jQuery-UI data-table now that the template has been populated
            $('#royalty-table').dataTable({
                bJQueryUI: true,
                //sPaginationType: 'full_numbers',
                bPaginate: false,
                //bScrollCollapse: true,
                //sScrollY: '',
                //sScrollX: '',
                bSortClasses: false,
                bLengthChange: false,
                //aaSorting: [],
                //aoColumns: [{ bSearchable: false, bVisible: false }, null, null, null, null],
                //iDisplayLength: 10,
                sDom: '<"top"il>rt<"bottom"fp><"clear">'
            });
        }

        function getContacts() {
            $.blockUI({ message: '<h4> Retrieving Author Royalties ... </h4>' });
            var pubID = $('#<%= ddlPubsJquery.ClientID %>').val();
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: 'WebService.asmx/GetContacts',
                data: JSON.stringify({ pubID: pubID }),
                dataType: 'json'
            })
                .success(function (data) {
                    $.unblockUI();
                    //Instantiate a template with data
                    applyContactTemplate(data);
                })
                .error(function (xhr) {
                    $.unblockUI();
                    var r = JSON.parse(xhr.responseText);
                    alert(r.Message);

                });
        }

        function applyContactTemplate(contacts) {
            $('#contacts-container').setTemplate($('#contact-template').html());
            $('#contacts-container').processTemplate(contacts);
            //Initialize the jQuery-UI data-table now that the template has been populated
            $('#contact-table').dataTable({
                bJQueryUI: true,
                //sPaginationType: 'full_numbers',
                bPaginate: false,
                //bScrollCollapse: true,
                //sScrollY: '',
                //sScrollX: '',
                bSortClasses: false,
                bLengthChange: false,
                //aaSorting: [],
                //aoColumns: [{ bSearchable: false, bVisible: false }, null, null, null, null],
                //iDisplayLength: 10,
                sDom: '<"top"il>rt<"bottom"fp><"clear">'
            });
        }

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
            <h4>Author Royalties Per Title/Author</h4>
            <div id="royalty-container">
                <!--gets populated by royalty-template-->
            </div>
            <h4>PR and Sales Contacts for the Selected Publisher</h4>
            <div id="contacts-container">
                <!--gets populated by contact-template--> 
            </div>
        </div>
    </div><!--end tabs-->
</asp:Content>

