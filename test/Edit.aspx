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
                savePub();
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
                $('#results').text('');
                $('#select-pub-id').val(pub.PubID);
                $('#<%= txtName.ClientID %>').val(pub.Name);
                $('#<%= txtCity.ClientID %>').val(pub.City);
                $('#errors').html('');
                })
            .error(function (xhr) {
                var r = JSON.parse(xhr.responseText);
                $.unblockUI();
                alert(r.Message);
            });
        }

        function savePub() {
            $.blockUI({ message: '<h4> Saving Publisher Info ... </h4>' });
            var pubID = $('#select-pub-id').val();
            var name = $('#<%= txtName.ClientID %>').val();
            var city = $('#<%= txtCity.ClientID %>').val();
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: 'WebService.asmx/SavePublisher',
                data: JSON.stringify({ pubID: pubID, name: name, city: city }),
                dataType: 'json'
            })
            .success(function (data) {
                $.unblockUI();
                $('#results').text('Changes saved at ' + getCurrentTime());
                $('#errors').html('');
                refreshDDL(name);
            })
            .error(function (xhr) {
                var r = JSON.parse(xhr.responseText);
                $.unblockUI();
                var error = r.Message;
                $('#results').text('');
                $('#errors').html('<ul>' + error + '</ul>');
            });
        }

        function getCurrentTime() {
            var currentTime = new Date();
            var hours = currentTime.getHours();
            var minutes = currentTime.getMinutes();
            var amPm = "AM";
            if (hours >= 12) amPm = "PM";
            if (hours > 12) hours = hours - 12;
            else if (hours == 0) hours = 12;
            if (minutes < 10) minutes = "0" + minutes;
            return hours.toString() + ":" + minutes.toString() + " " + amPm;
        }

        function refreshDDL( newText) {
            $('#<%= ddlPub.ClientID %> option:selected').text(newText);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Edit Publishers</h3>
    Select Publisher to Edit:&nbsp;
    <asp:DropDownList ID="ddlPub" runat="server"></asp:DropDownList>
    <fieldset id="pub-detail">
        <div class="error" id="errors">

        </div>
        <input id="select-pub-id" type="hidden" />
        <span id="results" class="tip"></span>
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

