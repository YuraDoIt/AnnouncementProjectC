<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication1.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label Text="Name"  runat="server"/>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="name" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Title"  runat="server"/>
                    </td>
                    <td colspan="2">
                        <asp:TextBox Id = 'TextBox1' runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td> 
                        <asp:Label Text="Description"  runat="server"/>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="DateAdded" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="text"  runat="server"/>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox3" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="3">
                        <asp:Button Text="Save" runat="server" OnClick="Unnamed5_Click"/>
                        <asp:Button Text="Delete" runat="server"/>
                        <asp:Button Text="Change" runat="server"/>
                        <asp:Button Text="Clear" runat="server" />
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
                    <asp:BoundField DataField="name" HeaderText="Announcement" />
                    <asp:BoundField DataField="title" HeaderText="Announcement" />
                    <asp:BoundField DataField="description" HeaderText="Announcement" />
                    <asp:BoundField DataField="date" HeaderText="Announcement" />
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
