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
        string[] arrayPathSageFolders = new string[5];


        public Form1()
        {
            InitializeComponent();
            arrayPathSageFolders[0] = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\AnaliseRD\Linha 50cloud\";
            arrayPathSageFolders[1] = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\AnaliseRD\Linha 100cloud\";
            arrayPathSageFolders[2] = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\AnaliseRD\Linha Gesrest\";
            arrayPathSageFolders[3] = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\AnaliseRD\Linha S4Accountants";
            arrayPathSageFolders[4] = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\AnaliseRD\Linha Plus";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string query = "Select Max(EliminationDate) from  DeletedDefects";
            SQLOpenConnection();
            SqlCommand cmd = new SqlCommand(query, SqlConnection);
            string lastDeletation = Convert.ToString(cmd.ExecuteScalar());
            cmd.Dispose();
            SQLCloseConnection();

            lblMultiFunctions.Text = "Última eliminação em " + lastDeletation.Substring(0, 16);

            //txtActualDate.Text = DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
            txtActualUser.Text = Environment.UserName;
            txtMachine.Text = Environment.MachineName;

            }

        private void SearchYearCheck()
        {

        }

        private string SQLOpenConnection()
        {
            string result = "ok";
            try
            {
                string ConnectionString = string.Format("Server={0};Database={1};uid={2};password={3};MultipleActiveResultSets=true; timeout=10",
                                                        SqlServerName, SqlDataBaseName, SqlUser, SqlPassword);
                SqlConnection = new SqlConnection(ConnectionString);
                SqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ligar ao azure!");
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

        private void ExecuteTxt(string file)
        {

            try
            {
                System.Collections.Generic.IEnumerable<String> lines = File.ReadLines(file);
                SQLOpenConnection();

                var numberOfLines = lines.Count();
                int count = 1;
                foreach (string l in lines)
                {
                    lblMultiFunctions.Text = string.Format("A validar «{0} de {1}» registos!", count, numberOfLines);
                    lblMultiFunctions.Update();
                    lblMultiFunctions.Refresh();
                    lblMultiFunctions.Update();
                    try
                    {
                        var defectID = l.Substring(2, l.Length - 2);
                        var spacePosition = defectID.IndexOf(" ");
                        defectID = defectID.Substring(0, spacePosition);

                        if (IsNumeric(defectID.Substring(0, 1)))
                        {
                            DeleteData(defectID);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    count++;
                }

                lblMultiFunctions.Text = "Validação concluída!";
                
                SQLCloseConnection();
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

            foreach (string path in arrayPathSageFolders)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    DirectoryInfo[] directories = di.GetDirectories();

                    foreach (var directory in directories)
                    {
                        if (directory.Name == DefectID)
                        {
                            try
                            {
                                foreach (FileInfo file in directory.GetFiles())
                                {

                                    file.Delete();

                                }

                                directory.Delete(true);

                                string query = string.Format("Insert into DeletedDefects (DefectId, EliminationMachineID, ApplicationFolder) values ('{0}', '{1}' , '{2}')", DefectID, txtActualUser.Text, di.Name);
                                SQLOpenConnection();
                                SqlCommand cmd = new SqlCommand(query, SqlConnection);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();

                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    string query = string.Format("Insert into ErrorDeleteDefects (DefectId, EliminationMachineID, ApplicationFolder) values ('{0}', '{1}' , '{2}')", DefectID, txtActualUser.Text, di.Name);
                                    SQLOpenConnection();
                                    SqlCommand cmd = new SqlCommand(query, SqlConnection);
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch (Exception ex) { }
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
            GetDataFromAz(false);


        }

        private void GetDataFromAz(bool withError)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("DefectId", typeof(string));
            dt.Columns.Add("Pasta Aplicação", typeof(string));
            dt.Columns.Add("Data Elim.", typeof(string));
            dt.Columns.Add("User", typeof(string));

            string query = "";

            if (withError)
            {
                query = "Select DefectID, EliminationDate, EliminationMachineID, ApplicationFolder from ErrorDeleteDefects Order by EliminationDate Desc, ApplicationFolder ASC";
            }
            else
            {
                query = "Select DefectID, EliminationDate, EliminationMachineID, ApplicationFolder from DeletedDefects Order by EliminationDate Desc, ApplicationFolder ASC";
            }

            SQLOpenConnection();
            SqlCommand cmd = new SqlCommand(query, SqlConnection);
            SqlDataReader dr = cmd.ExecuteReader();

            int count = 0;
            while (dr.Read())
            {
                string date = dr["EliminationDate"].ToString();
                string defectId = dr["DefectID"].ToString();
                string userid = dr["EliminationMachineID"].ToString();
                string applicationFolder = dr["ApplicationFolder"].ToString();

                if (withError)
                {
                    applicationFolder = "ERRO na pasta " + applicationFolder;
                }

                date = date.Substring(0, 16);
                dt.Rows.Add(defectId, applicationFolder, date, userid);
                count++;
            }
            cmd.Dispose();
            SQLCloseConnection();

            dataGridView1.DataSource = dt;

            if (withError)
                dataGridView1.Columns["DefectId"].ReadOnly = true;

            dataGridView1.Columns["Pasta Aplicação"].ReadOnly = true;
            dataGridView1.Columns["Data Elim."].ReadOnly = true;
            dataGridView1.Columns["User"].ReadOnly = true;

            dataGridView1.Columns["DefectId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["Pasta Aplicação"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["Data Elim."].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["User"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns["DefectId"].Width = 70;
            dataGridView1.Columns["Pasta Aplicação"].Width = 388;
            dataGridView1.Columns["Data Elim."].Width = 94;
            dataGridView1.Columns["User"].Width = 77;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open a Text File";
            ofd.Filter = "Text Files | *.txt";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ExecuteTxt(ofd.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogSuccess_Click_1(object sender, EventArgs e)
        {
            GetDataFromAz(false);

        }

        private void btnLogError_Click(object sender, EventArgs e)
        {
            GetDataFromAz(true);

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                this.Size = new Size(679, 389);
            }
            else
            {
                this.Size = new Size(526, 153);
            }
        }
    }
}
