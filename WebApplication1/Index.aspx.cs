using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
            if (!IsPostBack)
            {
                clearFields();
            }
        }

        protected void gvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Unnamed5_Click(object sender, EventArgs e)
        {
            if(txtName.Text != "" || txtTitle.Text != "" || txtDescription.Text != "") { 
                try { 
                    using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
                    {
                    
                        sqlCon.Open();
                        MySqlCommand sqlCmd = new MySqlCommand("posterAddorEdit", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("_announcementID", Convert.ToInt32(hfProductID.Value == "" ? "0" : hfProductID.Value));
                        sqlCmd.Parameters.AddWithValue("_name", txtName.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("_title", txtTitle.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("_description", txtDescription.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("_dateAdd", Convert.ToDateTime(txtDateAdd.Text));                                                  
                        sqlCmd.ExecuteNonQuery();

                        clearFields();

                        lbSucessMessage.Text = "Успішно додано";
                        clearFields();
                    }
                }
                catch (Exception ex)
                {
                    lbErrorMessage.Text = "Submited failed" + ex.Message;
                    throw ex;
                }
            }
            else
            {
                lbErrorMessage.Text = "Ви не ввели жодного поля";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        public void clearFields()
        {
            txtName.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtDateAdd.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            lbErrorMessage.Text = lbSucessMessage.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        void GridFild()
        {
            using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
            {
                sqlCon.Open();
                MySqlDataAdapter sqlDat = new MySqlDataAdapter();

            }
        }
    }
}