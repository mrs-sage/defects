using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        string[] arrayPath = new string[4];


        public Form1()
        {
            InitializeComponent();
            arrayPath[0] = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\AnaliseRD\Linha 50cloud\";
            arrayPath[1] = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\AnaliseRD\Linha 100cloud\";
            arrayPath[2] = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\AnaliseRD\Linha Gesrest\";
            arrayPath[3] = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\AnaliseRD\Linha S4Accountants";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtActualDate.Text = DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
            txtActualUser.Text = Environment.UserName;
            txtMachine.Text = Environment.MachineName;

            SearchYearCheck();
        }

        private void SearchYearCheck()
        {

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

        }

        private void ExecuteTxt(string file)
        {

            try
            {
                //var text = File.ReadAllText(file);
                //text = text.Replace("•	", "");

                System.Collections.Generic.IEnumerable<String> lines = File.ReadLines(file);

                var numberOfLines = lines.Count();

                int count = 1;
                foreach (string l in lines)
                {
                    lblTEste.Text = string.Format("A validar «{0} de {1}»!", count, numberOfLines);
                    lblTEste.Update();
                    lblTEste.Refresh();
                    lblTEste.Update();
                    try
                    {
                        var defectID = l.Substring(2, l.Length - 2);
                        var spacePosition = defectID.IndexOf(" ");
                        defectID = defectID.Substring(0, spacePosition);

                        if (IsNumeric(defectID.Substring(1, 1)))
                        {
                            DeleteData(defectID);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    count++;
                }

                lblTEste.Text = "Validação concluída!";
            }
            catch
            {

            }
        }

        private bool IsNumeric(string s)
        {
            return float.TryParse(s, out float output);
        }

        private void DeleteData(string DefectID)
        {
            foreach (string path in arrayPath)
            {
                DirectoryInfo di = new DirectoryInfo(path);
                DirectoryInfo[] directories = di.GetDirectories();

                foreach (var directory in directories)
                {
                    if (directory.Name == DefectID)
                    {
                        foreach (FileInfo file in directory.GetFiles())
                        {

                            file.Delete();

                        }
                        
                        directory.Delete(true);
                        try
                        {
                            string query = string.Format("Insert into DeletedDefects values ('{0}', cast('{1}' as date), '{2}')", DefectID, txtActualDate.Text, txtActualUser.Text);
                            SQLOpenConnection();
                            SqlCommand cmd = new SqlCommand(query, SqlConnection);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            SQLCloseConnection();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                SQLCloseConnection();
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open a Text File";
            ofd.Filter = "Text Files | *.txt";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ExecuteTxt(ofd.FileName);
            }

            //if (txtCaseSSO.Text.TrimEnd().TrimStart().Length > 0)
            //{
            //    try
            //    {
            //        string query = string.Format("Insert into DeletedDefects values ('{0}', cast('{1}' as date), '{2}')", txtX3Number.Text, txtActualDate.Text, txtActualUser.Text);
            //        SQLOpenConnection();
            //        SqlCommand cmd = new SqlCommand(query, SqlConnection);
            //        cmd.ExecuteNonQuery();
            //        cmd.Dispose();
            //        SQLCloseConnection();
            //        MessageBox.Show("Registo gravado com sucesso!");
            //        txtX3Number.Focus();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Erro!" + Environment.NewLine + ex.Message.ToString() + Environment.NewLine + Environment.NewLine + "Reporte ao José Sousa!");
            //    }

            //    dataGridView1.Rows.Clear();
            //    dataGridView1.Refresh();
            //    txtX3Number.Text = "";
            //    txtX3Number.Enabled = true;
            //    checkCurrentYear.Enabled = true;
            //    checkFiscalYear.Enabled = true;
            //    txtCaseSSO.Text = "";
            //    txtCaseSSO.Enabled = false;
            //    btnSave.Enabled = false;
            //    lblOnSiteNumber.Text = "Nº de assistências já efetuadas entre " + StartDate + " e " + EndtDate + ":";
            //}
            //else
            //{
            //    txtCaseSSO.Focus();
            //    MessageBox.Show("Registo o nº do Case CRM ou SSO Number!");
            //}
        }

        private void txtX3Number_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void txtCaseSSO_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void checkFiscalYear_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkCurrentYear_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
        }

        private void txtX3Number_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogSuccess_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string query = "Select DefectID, EliminationDate, EliminationMachineID from DeletedDefects Order by EliminationDate Desc";
            SQLOpenConnection();
            SqlCommand cmd = new SqlCommand(query, SqlConnection);

            SqlDataReader dr = cmd.ExecuteReader();

            int count = 0;

            while (dr.Read())
            {
                string date = dr["EliminationDate"].ToString();
                string casenumber = dr["DefectID"].ToString();
                string userid = dr["EliminationMachineID"].ToString();

                date = date.Replace(" 00:00:00", "");
                dataGridView1.Rows.Add(casenumber, userid, date);
                count++;
            }
            cmd.Dispose();
            SQLCloseConnection();
            
        }
    }
}
