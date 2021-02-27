<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FileProjectUpload._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/jquery-3.4.1.js" type="text/javascript"></script>
    <script language ="javascript" type="text/javascript">

        $(function () {
            var scntDiv = $('#FileUploaders');
            var i = $('#FileUploaders p').size() + 1;

            $('#addUploader').live('click', function () {
                $('<p><input type="file" id="FileUploader' + 1 + '" name="FileUploader' + 1 + '" /></label> <a href="#" id="removeUploader">Remove</a></p>').appendTo(scntDiv);
               
                i++;
                return false;
            });
            $('#removeUploader').live('click', function () {
                if (i > 2) {
                    $(this).parent('p').remove();
                    i--;
                }
                return false;
            });
        });
    </script>

<h3>Upload Your Files</h3>
    <div style="padding:10px; border:1px solid blue">
    <div id="FileUploaders">
        <p>
            <input type="file" id="FileUploader1" name="FileUploader1" />
        </p>
    </div>

        <a href="#" id="addUploader">Add Another</a>
   <br />
    <asp:Button ID="btnUploadAll" runat="server" Text="Upload File(s)" OnClick="btnUploadAll_Click" />
    </div>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CellPadding="10">
        <Columns>
            <asp:BoundField HeaderText="File name" DataField="FileName" />
            <asp:BoundField HeaderText="File size" DataField="FileSize" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="IbtnDownload" runat="server" CommandName="Download" CommandArgument='<%#Eval("FileID") %>'>
                        Download</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

</asp:Content>
