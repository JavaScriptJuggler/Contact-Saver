using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.VisualBasic;
namespace Contact_Saver
{
    public partial class Form1 : Form
    {
        OleDbCommand cmd;
        OleDbConnection con;
        OleDbDataAdapter adpt;
        //OleDbDataReader red;
        DataSet ds;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new OleDbConnection("PROVIDER=MICROSOFT.JET.OLEDB.4.0;DATA SOURCE=DATABASE1.MDB");
            con.Open();
           // dataGridView1.Hide();
            listBox1.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            checkBox1.Hide();
            panel1.Hide();
            cmd = new OleDbCommand("select * from security", con);
            adpt = new OleDbDataAdapter("select * from security", con);
            ds = new DataSet();
            adpt.Fill(ds);
            if (ds.Tables[0].Rows.Count==0)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Password", "Enter Password ");
                cmd = new OleDbCommand("insert into security values('" + input + "')", con);
                cmd.ExecuteNonQuery();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                cmd = new OleDbCommand("select * from security where password='" + textBox1.Text + "'", con);
                OleDbDataReader read = cmd.ExecuteReader();
                if (read.Read() == true)
                {
                    //gradientPanel1.Visible = false;
                    button2.Show();
                    button3.Show();
                    button4.Show();
                    label2.Hide();
                    textBox1.Hide();
                    button1.Hide();
                    label3.Show();
                    label4.Show();
                    label5.Show();
                    textBox2.Show();
                    textBox3.Show();
                    textBox4.Show();
                    textBox2.Enabled=false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    checkBox1.Enabled = false;
                    checkBox1.Show();
                    button2.Text = "New";
                    button3.Text = "View";
                    button4.Text = "Quit";
                    panel1.Show();
                   
                }
                else
                {
                    MessageBox.Show("Enter valid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox1.Focus();
                }

            }
            else
                MessageBox.Show("Password cant be blank", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
        }
        bool i = true;
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 100;
            if (i == true)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                checkBox1.Enabled = true;
                button3.Hide();
                button4.Hide();
                button2.Text = "Save";
                textBox2.Focus();
                i = false;
            }
            else
            {
                //DialogResult dr;
                if (textBox2.Text != "" || textBox3.Text != "")
                {

                    cmd = new OleDbCommand("insert into info values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    DialogResult dr = MessageBox.Show("Contact Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dr == DialogResult.OK)
                    {
                        button2.Show();
                        button3.Show();
                        button4.Show();
                        listBox1.Hide();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox2.Enabled = false;
                        textBox3.Enabled = false;
                        textBox4.Enabled = false;
                        checkBox1.Enabled = false;
                        button2.Text = "New";
                        i = true;
                    }
                }
                else
                {
                    MessageBox.Show("Can't be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && checkBox1.Checked == true)
            {
                textBox4.Text = textBox3.Text;
                textBox4.Enabled = false;
            }
            else
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Please Enter Phone Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox3.Focus();
                }
                if (checkBox1.Checked == false)
                {
                    textBox4.Enabled = true;
                    MessageBox.Show("Please Enter WhatsApp Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox4.Focus();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            { 
                textBox4.Text=textBox3.Text;
                textBox4.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label7.Visible = true;
           listBox1.Items.Clear();
            listBox1.Show();
            adpt = new OleDbDataAdapter("select Contact_Name from info", con);
            dt = new DataTable();
            adpt.Fill(dt);
             foreach (DataRow dr in dt.Rows)
                {
                    listBox1.Items.Add(dr["Contact_Name"]);
                  
                }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            label7.Visible = false;
            button2.Show();
            button3.Show();
            button4.Show();
            listBox1.Hide();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            checkBox1.Enabled = false;
            button2.Text = "New";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string a = Convert.ToString(listBox1.SelectedItem);
            adpt = new OleDbDataAdapter("select * from info where Contact_Name='" + a + "'", con);
            ds=new DataSet();
            adpt.Fill(ds);
            textBox2.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
            textBox3.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
            textBox4.Text = Convert.ToString(ds.Tables[0].Rows[0][2]);
            toolTip1.SetToolTip(listBox1, "Name: '" + textBox2.Text + "'\n Mobile: '" + textBox3.Text + "'\nwhatsApp: '" + textBox4.Text + "'");

        }

        private void label7_Click(object sender, EventArgs e)
        {
           DialogResult dr= MessageBox.Show("Sure to Delete", "Stop", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
           if (dr == DialogResult.OK)
           {
               cmd = new OleDbCommand("delete from info where Contact_Name='" + textBox2.Text + "'and Phone_Number='" + textBox3.Text + "'", con);
               cmd.ExecuteNonQuery();
               listBox1.Items.Clear();
               textBox2.Clear();
               textBox3.Clear();
               textBox4.Clear();
               adpt = new OleDbDataAdapter("select Contact_Name from info", con);
               dt = new DataTable();
               adpt.Fill(dt);
               foreach (DataRow dr1 in dt.Rows)
               {
                   listBox1.Items.Add(dr1["Contact_Name"]); ;
               }
           }
        }

      

    }
}
