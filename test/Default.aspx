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
            dataTable goes here

        </div>
    </div><!--end tabs-->
</asp:Content>

