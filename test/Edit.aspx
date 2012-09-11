<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            getPub();

            $('#<%= ddlPub.ClientID %>').change(function () {
                getPub();
            });

            //event handler for save button click
            $('#save-button').click(function (e) {
                e.preventDefault();

            });
        }); //end doc ready function

        function getPub() {
            var pubID = $('#<%= ddlPub.ClientID %>').val();
            $.blockUI({ message: '<h4> Getting Publisher Info ... </h4>' });
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: 'WebService.asmx/GetPublisher',
                data: JSON.stringify({ pubID: pubID }),
                dataType: 'json'
            })
            .success(function (data) {
                $.unblockUI();
                var pub = data.d;
                $('#select-pub-id').val(pub.PubID);
                $('#<%= txtName.ClientID %>').val(pub.Name);
                $('#<%= txtCity.ClientID %>').val(pub.City);
                })
            .error(function (xhr) {
                var r = JSON.parse(xhr.responseText);
                $.unblockUI();
                alert(r.Message);
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Edit Publishers</h3>
    Select Publisher to Edit:&nbsp;
    <asp:DropDownList ID="ddlPub" runat="server"></asp:DropDownList>
    <fieldset id="pub-detail">
        <div id="errors">

        </div>
        <input id="select-pub-id" type="hidden" />
        <dl class="form">
        <dt>Publisher Name:</dt>
        <dd>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <span class="tip">Cannot be blank. Limit 40 characters.</span>
        </dd>
        <dt>City:</dt>
        <dd>
            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            <span class="tip">Limit 20 characters.</span>
        </dd>
        <dt>&nbsp;</dt>
        <dd>
            <input id="save-button" type="button" value="Save Changes" />
        </dd>
    </dl>

    </fieldset>
</asp:Content>

