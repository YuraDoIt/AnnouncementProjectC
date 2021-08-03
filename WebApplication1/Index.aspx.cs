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
    class Announcement
    {
        private int anounceid;
        private string title;
        private string descript;
        public int announcId { get { return anounceid; } set { anounceid = value; } }
        public string announcTitle { get { return title; } set { title = value; } }
        public string description { get { return descript; } set { descript = value; } }
    }

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
                        //test.Text = Convert.ToString(hfProductID.Value); Для перевірки id
                        sqlCmd.Parameters.AddWithValue("_name", txtName.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("_title", txtTitle.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("_description", txtDescription.Text.Trim());
                        if (txtDateAdd.Text == "")
                        {
                            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd");
                            sqlCmd.Parameters.AddWithValue("_dateAdd", dateString2);
                        }
                        else 
                        { 
                            sqlCmd.Parameters.AddWithValue("_dateAdd", Convert.ToDateTime(txtDateAdd.Text)); 
                        }
                        sqlCmd.ExecuteNonQuery();

                        SucessApper();
                        lbSucessMessage.Text = "Успішно додано";
                        GridFill();
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

                sqlCmd.Parameters.AddWithValue("_announcId", Convert.ToInt32(hfProductID.Value == "" ? "0" : hfProductID.Value));
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
                ErrorApper();
                throw ex;
            }          
        }

        //вибрати поелементно
        protected void itemSelect(object sender, EventArgs e)
        {
            try { 
                int AnnouncId = Convert.ToInt32((sender as LinkButton).CommandArgument);
                using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
                {
                    sqlCon.Open();
                    MySqlDataAdapter sqlDat = new MySqlDataAdapter("viewById", sqlCon);
                    sqlDat.SelectCommand.Parameters.AddWithValue("_announcId", AnnouncId);
                    sqlDat.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dtbl = new DataTable();
                    sqlDat.Fill(dtbl);
                    
                    txtName.Text = dtbl.Rows[0][1].ToString();
                    txtTitle.Text = dtbl.Rows[0][2].ToString();
                    txtDescription.Text = dtbl.Rows[0][3].ToString();
                    txtDateAdd.Text = dtbl.Rows[0][4].ToString();

                    SucessApper();
                    btnSave.Text = "Оновлено";
                
                }
            }
            catch (Exception ex)
            {
                ErrorApper();
                throw ex;
            }
        }

        protected void SucessApper()
        {          
            lbErrorMessage.Text = "";
        }
        protected void ErrorApper()
        {
            lbErrorMessage.Text = "Помилка";
            lbSucessMessage.Text = "";
        }

        protected void btnSimilar_Click(object sender, EventArgs e)
        {
            string CountQuery = "Select count(announcId) From announcement";
            var RowCount = 0;
            using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
            {

                using(MySqlCommand cmdCount = new MySqlCommand(CountQuery, sqlCon))
                {
                    sqlCon.Open();
                    RowCount = Convert.ToInt32(cmdCount.ExecuteScalar());
                    test.Text = Convert.ToString(RowCount);
                }
                
            }
            
           // Announcement[] obj = new Announcement[];
        }
    }
}