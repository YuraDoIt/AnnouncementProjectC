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

        static int count = 0;
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
                        test.Text = Convert.ToString(hfProductID.Value);
                        sqlCmd.Parameters.AddWithValue("_name", txtName.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("_title", txtTitle.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("_description", txtDescription.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("_dateAdd", Convert.ToDateTime(txtDateAdd.Text));
                        sqlCmd.ExecuteNonQuery();

                        SucessApper();
                        lbSucessMessage.Text = "Успішно додано";
                        //clearFields();
                    }
                }
                catch (Exception ex)
                {
                    ErrorApper();
                    lbErrorMessage.Text = "Submited failed" + ex.Message;
                    throw ex;
                }
            }
            else
            {
                lbErrorMessage.Text = "Ви не ввели жодного поля";
            }
        }

        //Запит на видалення
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
            using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
            {
                sqlCon.Open();
                MySqlCommand sqlCmd = new MySqlCommand("announcDelete", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                lbErrorMessage.Text = "AllIsFine";
                test.Text = (hfProductID.Value);

                sqlCmd.Parameters.AddWithValue("_announcementID", Convert.ToInt32(hfProductID.Value == "" ? "0" : hfProductID.Value));
                sqlCmd.ExecuteNonQuery();
   
                lbSucessMessage.Text = "Успішно видалено";
                clearFields();
            }
        }

        //Для виведення інформації про всі поля 
        void GridFill()
        {
            using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
            {
                sqlCon.Open();
                MySqlDataAdapter sqlDat = new MySqlDataAdapter("PosterViewAll", sqlCon);
                sqlDat.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtbl = new DataTable();
                sqlDat.Fill(dtbl);
                gvPoster.DataSource = dtbl;
                gvPoster.DataBind();
            }
        }

        public void clearFields()
        {
            txtName.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtDateAdd.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
            lbSucessMessage.Text = "Очищено успішно";
            lbErrorMessage.Text = "";
            lbSucessMessage.Text = "";
        }


        //Вивести всю таблицю
        protected void btnShow_Click(object sender, EventArgs e)
        {
            
            try
            {


                count++;
                if (count % 2 == 1)
                {
                    gvPoster.Visible = true;
                    GridFill();
                    btnShow.Text = "Сховати поля";
                    lbSucessMessage.Text = "Виведено успішно";
                }
                else
                {
                    gvPoster.Visible = false;
                    btnShow.Text = "Показати поля";
                    lbSucessMessage.Text = "";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //вибрати поелементно
        protected void itemSelect(object sender, EventArgs e)
        {
            int AnnouncId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
            {
                sqlCon.Open();
                MySqlDataAdapter sqlDat = new MySqlDataAdapter("viewById", sqlCon);
                sqlDat.SelectCommand.Parameters.AddWithValue("_announcId", AnnouncId);
                sqlDat.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtbl = new DataTable();
                sqlDat.Fill(dtbl);

                hfProductID.Value = 
                test.Text = hfProductID.Value = dtbl.Rows[0][0].ToString();

                txtName.Text = dtbl.Rows[0][0].ToString();
                txtTitle.Text = dtbl.Rows[0][1].ToString();
                txtDescription.Text = dtbl.Rows[0][2].ToString();
                txtDateAdd.Text = dtbl.Rows[0][3].ToString();
                //txtDateAdd.Text = dtbl.Rows[0][4].ToString();

                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        protected void SucessApper()
        {
            
            lbErrorMessage.Text = "";
        }
        protected void ErrorApper()
        {
            
            lbErrorMessage.Text = "";
        }
    }
}