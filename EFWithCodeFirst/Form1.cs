using EFWithCodeFirst.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFWithCodeFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Student Details";
            lblId.Visible = false;
            lblmsg.Visible = false;
            cmGender.Text = "Select";
            cmGender.Items.Add("Male");   // Adding values for Gender Combobox
            cmGender.Items.Add("Female");
            Display();
        }
        public void Display()
        {
            using (var context=new ModelContext())
            {
                context.students.ToList();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var context=new ModelContext())
            {
                student stu = new student();
                stu.Name = txtName.Text;
                stu.Age = Convert.ToInt32(txtAge.Text);
                stu.City = txtCity.Text;
                stu.Gender = cmGender.SelectedItem.ToString();
                context.students.Add(stu);
                context.SaveChanges();
            }
            lblmsg.Visible = true;
            lblmsg.Text = "Data is saved...";
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var context=new ModelContext())
            {
                student stu = new student();
                stu.Name = txtName.Text;
                stu.Age = Convert.ToInt32(txtAge.Text);
                stu.City = txtCity.Text;
                stu.Gender = cmGender.SelectedItem.ToString();
                context.students.Add(stu);
                context.SaveChanges();
            }
            lblmsg.Text = "Data is Updated...";
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    lblId.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                    txtAge.Text = row.Cells[2].Value.ToString();
                    txtCity.Text = row.Cells[3].Value.ToString();
                    cmGender.SelectedItem = row.Cells[4].Value.ToString();
                }
            }
            ClearFields();
        }
        public void ClearFields() // Clear the fields after Insert or Update or Delete operation  
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtCity.Text = "";
            cmGender.Text = "Select";
        }
    }
}
