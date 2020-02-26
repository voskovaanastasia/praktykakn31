using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace WindowsFormsApplication10
{
    public partial class Form2 : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database31.mdb";
        private OleDbConnection myConnection;
        public Form2()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
        }
        private void addData()
        {
            try
            {
                string query = "INSERT INTO Curs (Curs_name, Vukladac, Place, Ocinka)" + "VALUES ('" + this.textBox5.Text + "','" + this.textBox6.Text + "','" + this.textBox7.Text + "','" + this.textBox8.Text  + "')";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                MessageBox.Show("Дані додано",
                             "Інформація",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Введіть правильно дані",
                                "Помилка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 add = new Form1();
            add.ShowDialog();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT Curs_name, Vukladac, Place, Ocinka FROM Curs";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + "  " );
            }

            // закриваємо OleDbDataReader
            reader.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            Object selectedItem = listBox1.SelectedItem;
            string g = selectedItem.ToString();
            string[] words = g.Split(' ');
            string text = words[0];
            string query = "SELECT * FROM Curs  where Curs_name='" + words[0] + "'";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                textBox1.Text = dr[1].ToString();
                textBox2.Text = dr[2].ToString();
                textBox3.Text = dr[3].ToString();
                textBox4.Text = dr[4].ToString();
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {try
            {
            
                int selectedIndex = listBox1.SelectedIndex;
                Object selectedItem = listBox1.SelectedItem;
                string g = selectedItem.ToString();
                string[] words = g.Split(' ');
                string text = words[0];
                string query = "UPDATE Curs SET Curs_name='" + textBox1.Text +
                    "',Vukladac='" + textBox2.Text +
                    "',Place='" + textBox3.Text +
                    "',Ocinkar='" + textBox4.Text + "'where Curs_name='" + text + "'";
                MessageBox.Show(query);
                OleDbCommand command = new OleDbCommand(query, myConnection);
                MessageBox.Show("Дані відредаговано");
                command.ExecuteNonQuery();
            }
            catch
            {
                
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            Object selectedItem = listBox1.SelectedItem;
            string g = selectedItem.ToString();
            string[] words = g.Split(' ');
            string text = words[0];
            string query = "DELETE * FROM Curs  where Curs_name='" + words[0] + "'";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((textBox5.Text.Length > 0) && (textBox6.Text.Length > 0) && (textBox7.Text.Length > 0) && (textBox8.Text.Length > 0))
            {

                addData();


            }
            else
            {
                MessageBox.Show("Заповніть всі поля!",
                               "Помилка",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Curs where Curs_name like '%" + textBox9.Text + "%' ";
            listBox1.Items.Clear();
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[1].ToString() + " "  );
            }
            reader.Close();
        }
    }
}
