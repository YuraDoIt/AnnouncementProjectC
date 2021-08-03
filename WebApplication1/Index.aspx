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

       function toggle_visibility(id) {
           var e = document.getElementById(id);
           if(e.style.display == 'block')
              e.style.display = 'none';
           else
              e.style.display = 'block';
           }
          
    </script>

    <style>
        * {
           
            padding:0px;
            margin:0px;
            max-width: 100%;
            max-height: 100%;
        }

        

        #Content{
            padding:20px;
        }

        p {
            font-size: 16pt;
            padding-top:20px;
            text-align:center; 
            border-color: aquamarine;
            background-color: aquamarine;
            height : 45px;
            font-weight: normal;
        }

        #btnShow{
            margin-right:30px;
        }

        .button {
          border: none;
          color: white;
          padding: 16px 32px;
          text-align: center;
          text-decoration: none;
          display: inline-block;
          font-size: 16px;
          margin: 4px 2px;
          transition-duration: 0.4s;
          cursor: pointer;
          border-radius:12px;
        }
        /*Стилі для кнопок*/
        #btnShow {
            background-color: white; 
            color: black;
            border: 2px solid #4CAF50;
        }

        #btnShow:hover {
          background-color: #4CAF50;
          color: white;
        }

        #btnSave {
            background-color: white; 
            color: black; 
            border: 2px solid #008CBA;
        }

        #btnSave:hover {
          background-color: #008CBA;
          color: white;
        }

        #btnDelete {
          background-color: white; 
          color: black; 
          border: 2px solid #f44336;
        }

        #btnDelete:hover {
          background-color: #f44336;
          color: white;
        }

        #btnClear {
          background-color: white;
          color: black;
          border: 2px solid #e7e7e7;
        }

        btnClear:hover {
            background-color: #e7e7e7;
        }
        
        #similar {
            background-color: white; 
            color: black;
            border: 2px solid #4CAF50;
        }
        #lbSucessMessage{
            color: white;
            padding: 8px;
            font-family: Arial;
        }
        #Content{
            font-family: Arial
        }
       /* #gvPoster {
            display:none;
        }*/

    </style>
</head>
    
<body>
    <p style =""><strong>Announcement Project</strong></p>

    <div id="Content">
        <form id="form1" runat="server">
            <asp:HiddenField ID="hfProductID" runat="server" />
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label Text="Назва"  runat="server"/>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtName" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Заголовок"  runat="server"/>
                        </td>
                        <td colspan="2">
                            <asp:TextBox Id = 'txtTitle' runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td> 
                            <asp:Label Text="Опис"  runat="server" label="Опис"/>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDescription" runat="server"/>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label Text="Дата"  runat="server"/>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDateAdd" runat="server" /><%-- MaximumValue="1/1/2050" MinimumValue="2000/1/1" Type="Date" Format="yyyy MM dd"/>--%>

                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3">
                            <asp:Button Text="Показати поля" id="btnShow" runat="server" OnClick="btnShow_Click" CssClass="button" />
                            <asp:Button Text="Збререгти" id="btnSave" runat="server" OnClick="Unnamed5_Click" CssClass="button" />
                            <asp:Button Text="Видалити" id="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="button" />                       
                            <asp:Button Text="Очистити" id="btnClear" runat="server" OnClick="btnClear_Click" CssClass="button" />
                            <asp:Button Text="Топ 3 " id="similar" runat="server" OnClick="btnSimilar_Click" CssClass="button" />
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="2">
                            <asp:Label Text="" ID="lbSucessMessage" runat="server" ForeColor="Green" ></asp:Label>        
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="2">
                            <asp:Label Text="" ID="lbErrorMessage" runat="server" ForeColor="Red" ></asp:Label>        
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <asp:Label Text="message" ID="test" runat="server" ForeColor="Red" ></asp:Label>        
                        </td>
                    </tr>
                </table>

                <br />

                <asp:GridView ID="gvPoster" runat="server" AutoGenerateColumns="false" Height="165px" Width="745px" >
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

</body>
</html>
