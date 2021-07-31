using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class Index : System.Web.UI.Page
    {
        string connectionString = @"
            Server = localhost;
            Database = poster;
            Uid = root;
            Pwd = 123456;
        ";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Unnamed5_Click(object sender, EventArgs e)
        {
            using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
            {
                sqlCon.Open();
                MySqlCommand sqlCmd = new MySqlCommand
            }
        }
    }
}