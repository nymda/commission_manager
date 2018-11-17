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
    public partial class Form1 : Form
    {
        public Form1()
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

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if(dataGridView1.Rows.Count == 0)
                {
                    label3.Text = "TODO: 0";
                    label4.Text = "DONE: 0";
                    label5.Text = "DONE (%): 0%";
                    label6.Text = "MONEY: £0";
                }

                Console.WriteLine("2 " + (Convert.ToBoolean(dataGridView1.Rows[i].Cells[2].Value)));
                Console.WriteLine("3 " + (Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value)));
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value) == false)
                {
                    Console.WriteLine("found false");
                    todo++;
                }
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value) == true)
                {
                    done++;
                }
                total++;

                string[] o = data[i].Split(':');

                money = money + Convert.ToInt32(o[1]);

                Console.WriteLine(todo);
                Console.WriteLine(done);
                Console.WriteLine(total);

                label3.Text = "TODO: " + todo.ToString();
                label4.Text = "DONE: " + done.ToString();

                if (!(done == 0))
                {
                    label5.Text = "DONE (%): " + ((done * 100 / total * 100) / 100).ToString() + "%"; //maff
                    percentage = done / total;
                    Console.WriteLine(percentage);
                }

                label6.Text = "MONEY: £" + money;
            }


            Console.WriteLine("\n\n\n\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Price";
            string[] row = new string[] { textBox1.Text, numericUpDown1.Value.ToString() };
            dataGridView1.Rows.Add(row);

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn chk2 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk);
            dataGridView1.Columns.Add(chk2);
            chk.HeaderText = "Sketched";
            chk2.HeaderText = "Complete";
            chk.Name = "chk";
            chk2.Name = "chk2";

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Remove";
            btn.Text = "Remove";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;

            data.Add(textBox1.Text + ":" + numericUpDown1.Value.ToString() + ":" + "False" + ":" + "False");

            File.WriteAllLines(datpath, data);

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string[] o = data[i].Split(':');

                if (o[2] == "True")
                {
                    Console.WriteLine(o[2]);
                    dataGridView1.Rows[i].Cells[2].Value = true;
                }
                if (o[2] == "False")
                {
                    dataGridView1.Rows[i].Cells[2].Value = false;
                }

                if (o[3] == "True")
                {
                    dataGridView1.Rows[i].Cells[3].Value = true;
                }
                if (o[3] == "False")
                {
                    dataGridView1.Rows[i].Cells[3].Value = false;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //i seriously have no fucking clue whats going on here

            try
            {
                if(e.ColumnIndex == 4)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    data.RemoveAt(e.RowIndex);
                    File.WriteAllLines(datpath, data);
                }
                if (e.ColumnIndex == 3)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !(Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                    string[] tdata = data[e.RowIndex].Split(':');
                    tdata[3] = (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).ToString());
                    data[e.RowIndex] = string.Join(":", tdata);
                    File.WriteAllLines(datpath, data);

                }
                if (e.ColumnIndex == 2)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !(Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                    string[] tdata = data[e.RowIndex].Split(':');
                    tdata[2] = (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).ToString());
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
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].Name = "Name";
                dataGridView1.Columns[1].Name = "Price";

                string[] o = i.Split(':');

                string[] row = new string[] { o[0], o[1], "", "" };

                dataGridView1.Rows.Add(row);

                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                DataGridViewCheckBoxColumn chk2 = new DataGridViewCheckBoxColumn();
                dataGridView1.Columns.Add(chk);
                dataGridView1.Columns.Add(chk2);
                chk.HeaderText = "Sketched";
                chk2.HeaderText = "Complete";
                chk.Name = "chk";
                chk2.Name = "chk2";

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Remove";
                btn.Text = "Remove";
                btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[2].Value) == false)
                {
                    todo++;
                }
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value) == true)
                {
                    done++;
                }
                total++;

                label3.Text = todo.ToString();
                label4.Text = done.ToString();

                if (!(done == 0))
                {
                    label5.Text = "DONE (%) " + ((done * 100 / total * 100) / 100).ToString() + "%"; //maff
                    percentage = done / total;
                    Console.WriteLine(percentage);
                }

                string[] o = data[i].Split(':');

                if (o[2] == "True")
                {
                    Console.WriteLine(o[2]);
                    dataGridView1.Rows[i].Cells[2].Value = true;
                }
                if (o[2] == "False")
                {
                    dataGridView1.Rows[i].Cells[2].Value = false;
                }

                if (o[3] == "True")
                {
                    dataGridView1.Rows[i].Cells[3].Value = true;
                }
                if (o[3] == "False")
                {
                    dataGridView1.Rows[i].Cells[3].Value = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateStats();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
