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

namespace SQLTEST
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection();
        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Data Source=DESKTOP-NANJ2CM\MSSQLSERVERPLS;Initial Catalog=Amonic;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AllowUserToAddRows = false;

            DataSet ds = new DataSet(); ; // хранилище данных
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Users", connection); // передать в адаптер запрос и подключние
            adapter.Fill(ds); // добавляет возварщенные запросом строки в DataSet
            dataGridView1.DataSource = ds.Tables[0]; // задает источник данных для dataGridView и возвращает содержимое DataSet

            Search();
        }

        public void Search()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    string shit = dataGridView1.Rows[i].Cells[j].Value.ToString(); // обращение к значению строки и столбца и приведения к типу String

                    if (shit == "0")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red; // выбранная строка выделяется цветом
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // скрыть эту форму
            Form2 form = new Form2(); // создать объект второй формы
            form.Show(); // показать вторую форму
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex; //переменная хранит 
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                int a = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                Save.ID = a;

                MessageBox.Show(Convert.ToString(a));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Save.ID != null)
                {
                    connection.Open();

                    String updateQuary = "update Users set RoleID = 2 where ID = "+Save.ID+"";
                    SqlCommand updateRole = new SqlCommand(updateQuary, connection);
                    updateRole.ExecuteNonQuery();


                    connection.Close();
                }
            }
            catch 
            {
                MessageBox.Show("Возникла ошибка");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("select * from " + comboBox1.Text + "",connection);

            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
        }
    }
}
