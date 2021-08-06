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
        private string name;
        private int anounceid;
        private string title;
        private string descript;

        public string Name { get; set; }
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

        //addOrEdit button
        protected void addOrChange(object sender, EventArgs e)
        {
            if (txtName.Text != "" || txtTitle.Text != "" || txtDescription.Text != "")
            {
                try
                {
                    using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
                    {

                        sqlCon.Open();
                        MySqlCommand sqlCmd = new MySqlCommand("posterAddorEdit", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("_announcementID", Convert.ToInt32(hfProductID.Value == "" ? "0" : hfProductID.Value));
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
                            sqlCmd.Parameters.AddWithValue("_dateAdd", Convert.ToString(txtDateAdd.Text));
                        }
                        sqlCmd.ExecuteNonQuery();

                        SucessApper("Успішно додано");
                        btnShow_Click(sender, e);
                        SucessApper("Успішно додано");                     
                    }
                }
                catch (Exception ex)
                {
                    ErrorApper("Помилка із додаванням");
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

            if (hfProductID.Value != "") { 
                using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
                {
                    sqlCon.Open();
                    MySqlCommand sqlCmd = new MySqlCommand("announcDelete", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("_announcId", Convert.ToInt32(hfProductID.Value == "" ? "0" : hfProductID.Value));
                    sqlCmd.ExecuteNonQuery();

                    SucessApper("Успішно видалено");
                    GridFill();
                    clearFields();
                }
            }
            else
            {
                ErrorApper("Не вибрано жодного поля");
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
                sqlCon.Close();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
            SucessApper("Очищено успішно");
            ErrorApper("");
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
                ErrorApper("Помилка при виведені");
                throw ex;
            }
        }

        //Вибрати поелементно
        protected void itemSelect(object sender, EventArgs e)
        {
            try
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

                    hfProductID.Value = dtbl.Rows[0][0].ToString();

                    txtName.Text = dtbl.Rows[0][1].ToString();
                    txtTitle.Text = dtbl.Rows[0][2].ToString();
                    txtDescription.Text = dtbl.Rows[0][3].ToString();
                    txtDateAdd.Text = dtbl.Rows[0][4].ToString();

                    SucessApper("");
                    btnSave.Text = "Оновлено";

                }
            }
            catch (Exception ex)
            {
                ErrorApper("Помилка вибору");
                throw ex;
            }
        }

        #region Clear Fields
        protected void SucessApper(string text)
        {
            lbSucessMessage.Text = text;
            lbErrorMessage.Text = "";
        }
        protected void ErrorApper(string text)
        {
            lbErrorMessage.Text = text;
            lbSucessMessage.Text = "";
        }
        public void clearFields()
        {
            hfProductID.Value = "";
            txtName.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtDateAdd.Text = "";
            btnSave.Text = "Save";
            test.Text = "";
        }
        #endregion

        protected void btnSimilar_Click(object sender, EventArgs e)
        {

            //Find out how many row in announcement
            string CountQuery = "Select count(announcId) From announcement";

            var RowCount = 0;
            using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmdCount = new MySqlCommand(CountQuery, sqlCon))
                {
                    sqlCon.Open();
                    RowCount = Convert.ToInt32(cmdCount.ExecuteScalar());
                }
            }

            test.Text = "";

            string topQuery = "Select  name, announcId, title, `description` From announcement";

            Announcement[] obj = null;

            using (MySqlConnection sqlCon = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmdTop = new MySqlCommand(topQuery, sqlCon))
                {
                    sqlCon.Open();
                    using (var reader = cmdTop.ExecuteReader())
                    {
                        var list = new List<Announcement>();
                        while (reader.Read())
                        {
                            list.Add(new Announcement
                            {
                                Name = reader.GetString(0),
                                announcId = reader.GetInt32(1),
                                announcTitle = reader.GetString(2),
                                description = reader.GetString(3)
                            });
                            obj = list.ToArray();
                        }
                    }
                    sqlCon.Close();

                }
            }

            if (obj == null) ErrorApper("Не має жодного поля");


            var listRes = new List<Announcement>();

            int i, j = 0;
            for (i = 0; i < obj.Length; i++)
            {
                for (j = i + 1; j < obj.Length; j++)
                {
                    if (CompareField(obj[i].announcTitle, obj[j].announcTitle) == true
                        && CompareField(obj[i].description, obj[j].description) == true)
                    {
                        //test.Text += Convert.ToString("" + obj[i].announcId + " " + obj[i].announcTitle + " " + obj[i].description + "   " + obj[j].announcId + " " + obj[j].announcTitle + " " + obj[j].description + "<br>");
                        if (listRes.Contains(obj[i]))
                        { }
                        else  listRes.Add(obj[i]);
                        if (listRes.Contains(obj[j]))
                        { }
                        else  listRes.Add(obj[j]);
                    }
                }
            }

            
            //Find top 3 element
            var listSel = from announ in listRes
                          group announ by new
                          {
                              announ.announcTitle,
                              announ.description,
                          } into g
                          select new
                          {
                              g.Key.announcTitle,
                              g.Key.description,
                              AnnounCount = g.Count()
                          };

            test.Text += "<br>";

            test.Text += "<br>Toп 3 Announcement<br><br> Із параметрами:<br>";
            List<Announcement> top3 = new List<Announcement>();
            foreach (var a in listSel)
            {
                if(a.AnnounCount == 3 || a.AnnounCount > 3)
                {
                    foreach(var lis in listRes)
                    {
                        if(lis.announcTitle == a.announcTitle && lis.description == a.description) {
                            top3.Add(lis);
                            
                        }
                    }                   
                }   
                else
                {
                    test.Text += "Такої кількості однакових полів не має";
                }
            }

            foreach(var lis in top3)
            {
                test.Text += Convert.ToString(" " + lis.Name + " " + lis.announcId + " " + lis.announcTitle + " " + lis.description + "<br>");
            }

        }

        #region MethodCompare Fields
        //Метод для визнчення наявності слів
        static public bool CompareField(string oneField, string secondField)
        {
            int countOfSimilar = 0;
            bool result = true;

            if (oneField == "" || secondField == "")
            {
                return false;
            }
            else
            {
                string[] firstSub = oneField.Split(' ');
                string[] secondSub = secondField.Split(' ');

                for (int i = 0; i < firstSub.Length; i++)
                {
                    for (int j = 0; j < secondSub.Length; j++)
                    {
                        if (firstSub[i] == secondSub[j])
                        {
                            countOfSimilar++;
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

    }
}