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
        public string SqlDataBaseName = "ccsdata";
        public string SqlUser = "oi";
        public string SqlPassword = "al";
        private SqlConnection SqlConnection;
        private bool RunOpenFolder = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string query = "Select Max(EliminationDate) from  DeletedDefects";
            SQLOpenConnection();
            SqlCommand cmd = new SqlCommand(query, SqlConnection);
            string lastDeletation = Convert.ToString(cmd.ExecuteScalar());
            cmd.Dispose();
            SQLCloseConnection();

            try
            {
                lblMultiFunctions.Text = "Última eliminação em " + lastDeletation.Substring(0, 16);
            }
            catch
            {
                lblMultiFunctions.Text = "Ainda não foi eliminada nenhuma pasta!";
            }

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
            int successLogs = 0;
            int errorLogs = 0;
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
                            var atcLog = DeleteData(defectID);
                            successLogs = successLogs + atcLog.Item1;
                            errorLogs = errorLogs + atcLog.Item2;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    count++;
                }

                if (successLogs > 0 && errorLogs > 0)
                {
                    lblMultiFunctions.Text = string.Format("«{0}» pastas eliminadas; «{1}» pastas com erro!", successLogs, errorLogs);
                }
                else if (successLogs > 0)
                {
                    lblMultiFunctions.Text = string.Format("«{0}» pastas eliminadas!", successLogs);
                }
                else if (errorLogs > 0)
                {
                    lblMultiFunctions.Text = string.Format("«{0}» pastas com erro ao eliminar!", errorLogs);
                }
                else
                {
                    lblMultiFunctions.Text = "Validação concluída!";
                }


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

        private Tuple<int, int> DeleteData(string DefectID)
        {
            int successLogs = 0;
            int errorLogs = 0;
            string folderPath = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\Defects\";
            try
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                DirectoryInfo[] directories = di.GetDirectories();

                foreach (var directory in directories)
                {
                    if (directory.Name.Trim() == DefectID.Trim())
                    {
                        try
                        {
                            foreach (FileInfo file in directory.GetFiles())
                            {

                                file.Delete();

                            }

                            directory.Delete(true);

                            string query = string.Format("Insert into DeletedDefects (DefectId, UserId) values ('{0}', '{1}')", DefectID, txtActualUser.Text);
                            SQLOpenConnection();
                            SqlCommand cmd = new SqlCommand(query, SqlConnection);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            successLogs = successLogs + 1;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                string query = string.Format("Insert into ErrorDeletedDefects (DefectId, UserId) values ('{0}', '{1}')", DefectID, txtActualUser.Text);
                                SQLOpenConnection();
                                SqlCommand cmd = new SqlCommand(query, SqlConnection);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                errorLogs = errorLogs + 1;
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex) { }

            return Tuple.Create(successLogs, errorLogs);
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
            dt.Columns.Add("Data Eliminação", typeof(string));
            dt.Columns.Add("User", typeof(string));

            string query = "";

            if (withError)
            {
                query = "Select DefectID, EliminationDate, UserId from ErrorDeletedDefects Order by EliminationDate Desc";
            }
            else
            {
                query = "Select DefectID, EliminationDate, UserId from DeletedDefects Order by EliminationDate Desc";
            }

            SQLOpenConnection();
            SqlCommand cmd = new SqlCommand(query, SqlConnection);
            SqlDataReader dr = cmd.ExecuteReader();

            int count = 0;
            while (dr.Read())
            {
                string date = dr["EliminationDate"].ToString();
                string defectId = dr["DefectID"].ToString();
                string userid = dr["UserId"].ToString();

                if (withError)
                {
                    defectId = "Não foi possível eliminar a pasta " + defectId;
                }

                date = date.Substring(0, 16);
                dt.Rows.Add(defectId, date, userid);
                count++;
            }
            cmd.Dispose();
            SQLCloseConnection();

            dataGridView1.DataSource = dt;


            dataGridView1.Columns["DefectId"].ReadOnly = true;
            dataGridView1.Columns["Data Eliminação"].ReadOnly = true;
            dataGridView1.Columns["User"].ReadOnly = true;

            dataGridView1.Columns["DefectId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["Data Eliminação"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["User"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            if (count > 11)
            {
                if (withError)
                {
                    dataGridView1.Columns["DefectId"].Width = 300;
                    dataGridView1.Columns["Data Eliminação"].Width = 107;
                    dataGridView1.Columns["User"].Width = 62;
                }
                else
                {
                    dataGridView1.Columns["DefectId"].Width = 162;
                    dataGridView1.Columns["Data Eliminação"].Width = 162;
                    dataGridView1.Columns["User"].Width = 145;
                }
            }
            else
            {
                if (withError)
                {
                    dataGridView1.Columns["DefectId"].Width = 300;
                    dataGridView1.Columns["Data Eliminação"].Width = 108;
                    dataGridView1.Columns["User"].Width = 78;
                }
                else
                {
                    dataGridView1.Columns["DefectId"].Width = 162;
                    dataGridView1.Columns["Data Eliminação"].Width = 162;
                    dataGridView1.Columns["User"].Width = 162;
                }
            }
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
            lblOpenFolder.Visible = false;
            RunOpenFolder = false;
            GetDataFromAz(false);

        }

        private void btnLogError_Click(object sender, EventArgs e)
        {
            lblOpenFolder.Visible = true;
            RunOpenFolder = true;
            GetDataFromAz(true);

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                this.Size = new Size(530, 394);
            }
            else
            {
                this.Size = new Size(530, 153);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString());
            if (RunOpenFolder)
            {
                var value = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var pos = value.LastIndexOf(" ");
                var length = value.Length - pos - 1;
                var final = value.Substring(pos + 1, length);

                string folderPath = @"\\sagept-fs-cloud\dep_srv\Suporte CCS\BdDefects\Defects\" + final;

                if (Directory.Exists(folderPath))
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.FileName = folderPath;
                    proc.Start();
                    proc.Close();
                }
                else
                {
                    MessageBox.Show("A pasta atualmente já não existe!");
                }
            }
        }
    }
}
