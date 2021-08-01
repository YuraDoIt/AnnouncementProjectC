<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication1.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Announcment Site</title>
    <script type="text/javascript" src="/js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="/js/jquery.maskedinput.js"></script>
    <script src="/scripts/jquery-2.1.0.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtDateAdd").mask("999-999-9999");
        });
    </script>
</head>
    
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfProductID" runat="server" />
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label Text="Name"  runat="server"/>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtName" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Title"  runat="server"/>
                    </td>
                    <td colspan="2">
                        <asp:TextBox Id = 'txtTitle' runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td> 
                        <asp:Label Text="Description"  runat="server"/>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDescription" runat="server"/>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label Text="DateAdd"  runat="server"/>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDateAdd" runat="server"/><%-- MaximumValue="1/1/2050" MinimumValue="2000/1/1" Type="Date" Format="yyyy MM dd"/>--%>

                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td colspan="3">
                        <asp:Button Text="Save" id="btnSave" runat="server" OnClick="Unnamed5_Click"/>
                        <asp:Button Text="Delete" id="btnDelete" runat="server" OnClick="btnDelete_Click"/>
                        <asp:Button Text="Change" id="btnChange" runat="server"/>
                        <asp:Button Text="Clear" id="btnClear" runat="server" OnClick="btnClear_Click" />
                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td colspan="2">
                        <asp:Label Text="message" ID="lbSucessMessage" runat="server" ForeColor="Green"></asp:Label>        
                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td colspan="2">
                        <asp:Label Text="message" ID="lbErrorMessage" runat="server" ForeColor="Red"></asp:Label>        
                    </td>
                </tr>
            </table>

            <br />

            <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Height="165px" Width="745px">
                <Columns>
                    <asp:BoundField DataField="name" HeaderText="name" />
                    <asp:BoundField DataField="title" HeaderText="title" />
                    <asp:BoundField DataField="description" HeaderText="description" />
                    <asp:BoundField DataField="date" HeaderText="dateAdd" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton Text="text" ID="lnkSelect" CommandArgument="<% Eval(ProductID) %>" runat="server">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
