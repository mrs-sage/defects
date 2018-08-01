using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//user: master007
//senha: sage2008+

namespace x3OnSiteUsers
{
    public partial class Form1 : Form
    {
        public string SqlServerName = "customerservices.database.windows.net,1433";
        public string SqlDataBaseName = "ccsdefectsdata";
        public string SqlUser = "master007";
        public string SqlPassword = "Sage2008+";
        private SqlConnection SqlConnection;
        private string StartDate = "";
        private string EndtDate = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtActualDate.Text = DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
            txtActualUser.Text = Environment.UserName;
            txtMachine.Text = Environment.MachineName;
            txtX3Number.Focus();
            SearchYearCheck();
        }

        private void SearchYearCheck()
        {

            if (checkFiscalYear.Checked)
            {
                if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 9)
                {
                    var year = DateTime.Now.Year - 1;
                    StartDate = year.ToString() + "-10-01";

                    year = DateTime.Now.Year;
                    EndtDate = year.ToString() + "-09-30";
                }
                else
                {
                    var year = DateTime.Now.Year;
                    StartDate = year.ToString() + "-10-01";

                    year = DateTime.Now.Year + 1;
                    EndtDate = year.ToString() + "-09-30";
                }

            }
            else
            {
                var year = DateTime.Now.Year;
                StartDate = year.ToString() + "-01-01";
                EndtDate = year.ToString() + "-12-31";
            }

            lblOnSiteNumber.Text = "Nº de assistências já efetuadas entre " + StartDate + " e " + EndtDate + ":";
        }

        private string SQLOpenConnection()
        {
            string result = "ok";
            try
            {
                string ConnectionString = string.Format("Server={0};Database={1};uid={2};password={3};MultipleActiveResultSets=true",
                                                        SqlServerName, SqlDataBaseName, SqlUser, SqlPassword);
                SqlConnection = new SqlConnection(ConnectionString);
                SqlConnection.Open();
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }

        private void SQLCloseConnection()
        {
            try
            {
                SqlConnection.Close();
                SqlConnection.Dispose();
            }
            catch (Exception ex)
            {
                //WriteLogFile("** SQLCloseConnection -> " + ex.Message.ToString(), true);
            }
        }
        public bool SqlConnectionGetStatus()
        {
            bool sqlConnResult = true;
            string result = SQLOpenConnection();

            if (result != "ok")
            {
                SQLCloseConnection();
                sqlConnResult = false;
            }
            return sqlConnResult;
        }

        public string SqlConnectionGetMessage()
        {
            string sqlConnResult = SQLOpenConnection();

            if (sqlConnResult == "ok")
            {
                SQLCloseConnection();
                sqlConnResult = "Connection ok!";
            }
            return sqlConnResult;
        }

        public string SaveWebCode(string TransSerial, string TransDocument, string TransDocNumber)
        {
            string transactionID = DateTime.Now.Millisecond.ToString().PadLeft(3, '0') + TransSerial + TransDocument + TransDocNumber.ToString();


            string saveStatus = "ok";
            try
            {
                string query = string.Format("Insert into WebCodesDocuments Values ('{0}','{1}')",
                                            "",
                                            TransDocument + " " + TransSerial + "/" + TransDocNumber);
                SQLOpenConnection();
                SqlCommand cmd = new SqlCommand(query, SqlConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                SQLCloseConnection();

            }
            catch (Exception ex)
            {
                saveStatus = ex.Message.ToString();
            }
            return saveStatus;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLOpenConnection();
            //    try
            //    {
            //        var cb = new SqlConnectionStringBuilder();
            //        cb.DataSource = "your_server.database.windows.net";
            //        cb.UserID = "your_user";
            //        cb.Password = "your_password";
            //        cb.InitialCatalog = "your_database";

            //        using (var connection = new SqlConnection(cb.ConnectionString))
            //        {
            //            connection.Open();

            //            //Submit_Tsql_NonQuery(connection, "2 - Create-Tables",
            //            //   Build_2_Tsql_CreateTables());

            //            //Submit_Tsql_NonQuery(connection, "3 - Inserts",
            //            //   Build_3_Tsql_Inserts());

            //            //Submit_Tsql_NonQuery(connection, "4 - Update-Join",
            //            //   Build_4_Tsql_UpdateJoin(),
            //            //   "@csharpParmDepartmentName", "Accounting");

            //            //Submit_Tsql_NonQuery(connection, "5 - Delete-Join",
            //            //   Build_5_Tsql_DeleteJoin(),
            //            //   "@csharpParmDepartmentName", "Legal");

            //            //Submit_6_Tsql_SelectEmployees(connection);
            //        }
            //    }
            //    catch (SqlException e)
            //    {
            //        Console.WriteLine(e.ToString());
            //    }
            //    Console.WriteLine("View the report output here, then press any key to end the program...");
            //    Console.ReadKey();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCaseSSO.Text.TrimEnd().TrimStart().Length > 0)
            {
                try
                {
                    string query = string.Format("Insert into DeletedDefects values ('{0}', cast('{1}' as date), '{2}')", txtX3Number.Text, txtActualDate.Text, txtActualUser.Text);
                    SQLOpenConnection();
                    SqlCommand cmd = new SqlCommand(query, SqlConnection);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    SQLCloseConnection();
                    MessageBox.Show("Registo gravado com sucesso!");
                    txtX3Number.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro!" + Environment.NewLine + ex.Message.ToString() + Environment.NewLine + Environment.NewLine + "Reporte ao José Sousa!");
                }

                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                txtX3Number.Text = "";
                txtX3Number.Enabled = true;
                checkCurrentYear.Enabled = true;
                checkFiscalYear.Enabled = true;
                txtCaseSSO.Text = "";
                txtCaseSSO.Enabled = false;
                btnSave.Enabled = false;
                lblOnSiteNumber.Text = "Nº de assistências já efetuadas entre " + StartDate + " e " + EndtDate + ":";
            }
            else
            {
                txtCaseSSO.Focus();
                MessageBox.Show("Registo o nº do Case CRM ou SSO Number!");
            }
        }

        private void txtX3Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtX3Number.Text.Length > 4 && txtX3Number.Text.Length < 8)
                {
                    txtX3Number.Enabled = false;
                    checkCurrentYear.Enabled = false;
                    checkFiscalYear.Enabled = false;
                    txtCaseSSO.Enabled = true;
                    btnSave.Enabled = true;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();

                    //string query = string.Format("Select casenumber, date, userid from onsite Where x3user='{0}' and (date >= cast('{1}' as Date) and date <= cast('{2}' as Date))", txtX3Number.Text, StartDate, EndtDate);
                    //SQLOpenConnection();
                    //SqlCommand cmd = new SqlCommand(query, SqlConnection);

                    //SqlDataReader dr = cmd.ExecuteReader();

                    //int count = 0;

                    //while (dr.Read())
                    //{
                    //    string date = dr["date"].ToString();
                    //    string casenumber = dr["casenumber"].ToString();
                    //    string userid = dr["userid"].ToString();
                    //    //date = date. .Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                    //    date = date.Replace(" 00:00:00", "");
                    //    dataGridView1.Rows.Add(casenumber, userid, date);
                    //    count++;
                    //}
                    //cmd.Dispose();
                    //SQLCloseConnection();
                    //lblOnSiteNumber.Text = "Nº de assistências já efetuadas entre " + StartDate + " e " + EndtDate + ": " + count.ToString();
                    //txtCaseSSO.Focus();
                }
                else
                {
                    MessageBox.Show("O X3 indicado não é válido!");
                }
            }

            if (txtX3Number.Text.Length < 7)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {

                }
                else
                {
                    e.Handled = true;
                }

            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            txtX3Number.Text = "";
            txtX3Number.Enabled = true;
            checkCurrentYear.Enabled = true;
            checkFiscalYear.Enabled = true;
            txtCaseSSO.Text = "";
            txtCaseSSO.Enabled = false;
            lblOnSiteNumber.Text = "Nº de assistências já efetuadas entre " + StartDate + " e " + EndtDate + ":";
            txtX3Number.Focus();
        }

        private void txtCaseSSO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCaseSSO.Text.Length < 15)
            {

            }
            else
            {
                if (char.IsControl(e.KeyChar))
                {

                }
                else
                {
                    e.Handled = true;
                }

            }
        }

        private void checkFiscalYear_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFiscalYear.Checked)
            {
                checkCurrentYear.Checked = false;
            }
            else
            {
                checkFiscalYear.Checked = false;
                checkCurrentYear.Checked = true;
            }

            SearchYearCheck();
            txtX3Number.Focus();
        }

        private void checkCurrentYear_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCurrentYear.Checked)
            {
                checkFiscalYear.Checked = false;
            }
            else
            {
                checkFiscalYear.Checked = true;
                checkCurrentYear.Checked = false;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (txtCaseSSO.Enabled)
            {
                try
                {
                    string caseNumber = "";
                    caseNumber = Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);

                    if (caseNumber.Length > 0)
                    {
                        txtCaseSSO.Text = Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);
                    }

                }
                catch
                {

                }
            }
            else if (txtX3Number.Enabled)
            {
                try
                {
                    string x3 = "";
                    x3 = Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);

                    if (x3.Length > 4 && x3.Length < 8)
                    {
                        txtX3Number.Text = Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);
                    }

                }
                catch
                {

                }
            }
        }

        private void txtX3Number_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
