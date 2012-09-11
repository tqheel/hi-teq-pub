<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= ddlPub.ClientID %>').change(function () {
                var pubID = $('#<%= ddlPub.ClientID %>').val();
                $.blockUI({ message: '<h4> Getting Publisher Info ... </h4>' });
                $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    url: '../WebServices/ScorecardWS.asmx/SaveChapterROAStatus',
                    data: JSON.stringify({ chapRoaID: selectedROA, status: checked }),
                    dataType: 'json'
                })
                .success(function (data) {
                    $.unblockUI();
                    //populateROAs(chapID, fy)
                    populateScorecardAchievements(chapID, fy);
                })
                .error(function (xhr) {
                    var r = JSON.parse(xhr.responseText);
                    $.unblockUI();
                    alert(r.Message);
                });
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Edit Publishers</h3>
    Select Publisher to Edit:&nbsp;
    <asp:DropDownList ID="ddlPub" runat="server"></asp:DropDownList>
    <fieldset id="pub-detail">
        <dl class="form">
        <dt>Publisher Name:</dt>
        <dd>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </dd>
        <dt>City:</dt>
        <dd>
            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        </dd>
        <dt>&nbsp;</dt>
        <dd>
            
        </dd>
    </dl>

    </fieldset>
</asp:Content>

