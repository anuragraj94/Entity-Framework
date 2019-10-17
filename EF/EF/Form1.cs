using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF
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
            dataGridView1.DataSource = Display();                  
        }
       
        public List<StudentDetail> Display()   // Display Method is a common method to bind the Student details in datagridview after save,update and delete operation perform.
        {
            using (StudentInformationEntities _entity = new StudentInformationEntities())
            {       
                return _entity.StudentDetails.ToList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            StudentDetail stu = new StudentDetail();
            stu.Name = txtName.Text;
            stu.Age = Convert.ToInt32(txtAge.Text);
            stu.City = txtCity.Text;
            stu.Gender = cmGender.SelectedItem.ToString();
            bool result = SaveStudentDetails(stu); // calling SaveStudentDetails method to save the record in table.Here passing a student details object as parameter  
            ShowStatus(result, "Save");

        }
        public bool SaveStudentDetails(StudentDetail Stu) // calling SaveStudentMethod for insert a new record  
        {
            bool result = false;
            using (StudentInformationEntities _entity = new StudentInformationEntities())
            {
                _entity.StudentDetails.Add(Stu);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }      

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            StudentDetail stu = SetValues(Convert.ToInt32(lblId.Text), txtName.Text, Convert.ToInt32(txtAge.Text), txtCity.Text, cmGender.SelectedItem.ToString()); // Binding values to StudentInformationModel  
            bool result = UpdateStudentDetails(stu); // calling UpdateStudentDetails Method  
            ShowStatus(result, "Update");
        }
        public bool UpdateStudentDetails(StudentDetail Stu) // UpdateStudentDetails method for update a existing Record  
        {
            bool result = false;
            using (StudentInformationEntities _entity = new StudentInformationEntities())
            {
                StudentDetail _student = _entity.StudentDetails.Where(x => x.Id == Stu.Id).Select(x => x).FirstOrDefault();
                _student.Name = Stu.Name;
                _student.Age = Stu.Age;
                _student.City = Stu.City;
                _student.Gender = Stu.Gender;
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }
        public StudentDetail SetValues(int Id, string Name, int age, string City, string Gender) //Setvalues method for binding field values to StudentInformation Model class  
        {
            StudentDetail stu = new StudentDetail();
            stu.Id = Id;
            stu.Name = Name;
            stu.Age = age;
            stu.City = City;
            stu.Gender = Gender;
            return stu;
        }

        public void ShowStatus(bool result, string Action) // validate the Operation Status and Show the Messages To User  
        {
            if (result)
            {
                if (Action.ToUpper() == "SAVE")
                {
                    MessageBox.Show("Saved Successfully!..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Action.ToUpper() == "UPDATE")
                {
                    MessageBox.Show("Updated Successfully!..", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Deleted Successfully!..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong!. Please try again!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ClearFields();
            dataGridView1.DataSource= Display();
        }

        public void ClearFields() // Clear the fields after Insert or Update or Delete operation  
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtCity.Text = "";
            cmGender.Text = "Select";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            StudentDetail stu = SetValues(Convert.ToInt32(lblId.Text),txtName.Text, Convert.ToInt32(txtAge.Text), txtCity.Text, cmGender.SelectedItem.ToString()); // Binding values to StudentInformationModel  
            bool result = DeleteStudentDetails(stu); //Calling DeleteStudentDetails Method  
            ShowStatus(result, "Delete");
        }
        public bool DeleteStudentDetails(StudentDetail Stu) // DeleteStudentDetails method to delete record from table  
        {
            bool result = false;
            using (StudentInformationEntities _entity = new StudentInformationEntities())
            {
                StudentDetail _student = _entity.StudentDetails.Where(x => x.Id == Stu.Id).Select(x => x).FirstOrDefault();
                _entity.StudentDetails.Remove(_student);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows) // foreach datagridview selected rows values  
                {
                    lblId.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                    txtAge.Text = row.Cells[2].Value.ToString();
                    txtCity.Text = row.Cells[3].Value.ToString();
                    cmGender.SelectedItem = row.Cells[4].Value.ToString();
                }
            }
        }
    }
}
