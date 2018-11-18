using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace feef_commission_thing
{
    public partial class mainUI : Form
    {
        public mainUI()
        {
            InitializeComponent();
        }

        public string datpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/comminfo.dat";
        public List<string> data = new List<String> { };
        public int money;
        public int todo;
        public int done;
        public int total;
        public float percentage;
        public int startcounter;

        public void updateStats()
        {
            todo = 0;
            done = 0;
            total = 0;
            money = 0;
            for (int i = 0; i < MainTable.Rows.Count; i++)
            {
                if(MainTable.Rows.Count == 0)
                {
                    label3.Text = "TODO: 0";
                    label4.Text = "DONE: 0";
                    label5.Text = "DONE (%): 0%";
                    label6.Text = "MONEY: £0";
                }
                if (Convert.ToBoolean(MainTable.Rows[i].Cells[3].Value) == false)
                {
                    todo++;
                }
                if (Convert.ToBoolean(MainTable.Rows[i].Cells[3].Value) == true)
                {
                    done++;
                }
                total++;
                string[] o = data[i].Split(':');
                money = money + Convert.ToInt32(o[1]);
                label3.Text = "TODO: " + todo.ToString();
                label4.Text = "DONE: " + done.ToString();
                if (!(done == 0))
                {
                    label5.Text = "DONE (%): " + ((done * 100 / total * 100) / 100).ToString() + "%"; //maff
                    percentage = done / total;
                }
                label6.Text = "MONEY: £" + money;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string id = new string(Enumerable.Repeat(chars, 5).Select(s => s[rand.Next(s.Length)]).ToArray());
       
            MainTable.ColumnCount = 2;
            MainTable.Columns[0].Name = "Name";
            MainTable.Columns[1].Name = "Price";
            string[] row = new string[] { NameTxtBox.Text, PriceNumeric.Value.ToString() };
            MainTable.Rows.Add(row);

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn chk2 = new DataGridViewCheckBoxColumn();
            MainTable.Columns.Add(chk);
            MainTable.Columns.Add(chk2);
            chk.HeaderText = "Sketched";
            chk2.HeaderText = "Complete";
            chk.Name = "chk";
            chk2.Name = "chk2";

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            MainTable.Columns.Add(btn);
            btn.HeaderText = "Remove";
            btn.Text = "Remove";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;

            data.Add(NameTxtBox.Text + ":" + PriceNumeric.Value.ToString() + ":" + "False" + ":" + "False" + id);

            File.WriteAllLines(datpath, data);

            for (int i = 0; i < MainTable.Rows.Count; i++)
            {
                string[] o = data[i].Split(':');

                if (o[2] == "True")
                {
                    MainTable.Rows[i].Cells[2].Value = true;
                }
                if (o[2] == "False")
                {
                    MainTable.Rows[i].Cells[2].Value = false;
                }

                if (o[3] == "True")
                {
                    MainTable.Rows[i].Cells[3].Value = true;
                }
                if (o[3] == "False")
                {
                    MainTable.Rows[i].Cells[3].Value = false;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.ColumnIndex == 4)
                {
                    MainTable.Rows.RemoveAt(e.RowIndex);
                    data.RemoveAt(e.RowIndex);
                    File.WriteAllLines(datpath, data);
                }
                if (e.ColumnIndex == 3)
                {
                    MainTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !(Convert.ToBoolean(MainTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                    string[] tdata = data[e.RowIndex].Split(':');
                    tdata[3] = (Convert.ToBoolean(MainTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).ToString());
                    data[e.RowIndex] = string.Join(":", tdata);
                    File.WriteAllLines(datpath, data);

                }
                if (e.ColumnIndex == 2)
                {
                    MainTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !(Convert.ToBoolean(MainTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                    string[] tdata = data[e.RowIndex].Split(':');
                    tdata[2] = (Convert.ToBoolean(MainTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).ToString());
                    data[e.RowIndex] = string.Join(":", tdata);
                    File.WriteAllLines(datpath, data);
                }
            }
            catch
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            if (File.Exists(datpath))
            {
                data = File.ReadAllLines(datpath).ToList();
            }

            foreach (string i in data)
            {
                startcounter++;
                startcounter = startcounter - 1;
                MainTable.ColumnCount = 2;
                MainTable.Columns[0].Name = "Name";
                MainTable.Columns[1].Name = "Price";

                string[] o = i.Split(':');

                string[] row = new string[] { o[0], o[1], "", "" };

                MainTable.Rows.Add(row);

                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                DataGridViewCheckBoxColumn chk2 = new DataGridViewCheckBoxColumn();
                MainTable.Columns.Add(chk);
                MainTable.Columns.Add(chk2);
                chk.HeaderText = "Sketched";
                chk2.HeaderText = "Complete";
                chk.Name = "chk";
                chk2.Name = "chk2";

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                MainTable.Columns.Add(btn);
                btn.HeaderText = "Remove";
                btn.Text = "Remove";
                btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;
            }

            for (int i = 0; i < MainTable.Rows.Count; i++)
            {
                if (Convert.ToBoolean(MainTable.Rows[i].Cells[2].Value) == false)
                {
                    todo++;
                }
                if (Convert.ToBoolean(MainTable.Rows[i].Cells[3].Value) == true)
                {
                    done++;
                }
                total++;

                label3.Text = todo.ToString();
                label4.Text = done.ToString();

                if (!(done == 0))
                {
                    label5.Text = "DONE (%) " + ((done * 100 / total * 100) / 100).ToString() + "%"; //maff
                }

                string[] o = data[i].Split(':');

                if (o[2] == "True")
                {
                    MainTable.Rows[i].Cells[2].Value = true;
                }
                if (o[2] == "False")
                {
                    MainTable.Rows[i].Cells[2].Value = false;
                }

                if (o[3] == "True")
                {
                    MainTable.Rows[i].Cells[3].Value = true;
                }
                if (o[3] == "False")
                {
                    MainTable.Rows[i].Cells[3].Value = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateStats();
        }
    }
}
