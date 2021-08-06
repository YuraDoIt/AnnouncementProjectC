<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication1.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Announcment Site</title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="/js/jquery.maskedinput.js"></script>
    <script src="/scripts/jquery-2.1.0.min.js"></script>

    <script type="text/javascript">

        function tryPlaceholder(txtbox, placehold) {
            if (txtbox.value.length == 0) {
                txtbox.value = placehold.toString();
                txtbox.style.color = "#959595";
            }
            else {
                if (txtbox.value == placehold.toString()) {
                    txtbox.value = '';
                    txtbox.style.color = "#000000";
                }
            }
        }
    </script>
   
</head>

<body>

    <header>
        <p><strong>Announcement Project</strong></p>
    </header>
    

    <main>
        <div id="Content">
            <form id="form1" runat="server">
                <asp:HiddenField ID="hfProductID" runat="server" />
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label Text="Назва" runat="server" />
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtName" runat="server"  CssClass="TextBox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label Text="Заголовок" runat="server" />
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID='txtTitle' runat="server"  CssClass="TextBox"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label Text="Опис" runat="server" label="Опис" />
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDescription" runat="server"  CssClass="TextBox"/>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label Text="Дата" runat="server" />
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDateAdd" runat="server" CssClass="TextBox" onclick="tryPlaceholder(this,'yyyy-MM-dd')"/>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td colspan="3">
                                <asp:Button Text="Показати поля" ID="btnShow" runat="server" OnClick="btnShow_Click" CssClass="button" />
                                <asp:Button Text="Збререгти" ID="btnSave" runat="server" OnClick="Unnamed5_Click" CssClass="button" />
                                <asp:Button Text="Видалити" ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="button" />
                                <asp:Button Text="Очистити" ID="btnClear" runat="server" OnClick="btnClear_Click" CssClass="button" />
                                <asp:Button Text="Топ 3" ID="similar" runat="server" OnClick="btnSimilar_Click" CssClass="button" />
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:Label Text="" ID="lbSucessMessage" runat="server" ForeColor="Green"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:Label Text="" ID="lbErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:Label Text="message" ID="test" runat="server" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <br />

                    <asp:GridView ID="gvPoster" runat="server" AutoGenerateColumns="false" Height="165px" Width="745px"
                        CssClass="mydatagrid" PagerStyle-CssClass="pager"
                        HeaderStyle-CssClass="header" RowStyle-CssClass="rows">
                        <Columns>

                            <asp:BoundField DataField="name" HeaderText="name" />
                            <asp:BoundField DataField="title" HeaderText="title" />
                            <asp:BoundField DataField="description" HeaderText="description" />
                            <asp:BoundField DataField="dateAdd" HeaderText="dateAdd" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton Text="Вибрати" ID="lnkSelect" CommandArgument='<%# Eval("AnnouncId") %>' runat="server" OnClick="itemSelect">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </form>
        </div>
    </main>

    <footer class="footer">
        <p>Yura Tsudzenko</p>
    </footer>
    

</body>
</html>
