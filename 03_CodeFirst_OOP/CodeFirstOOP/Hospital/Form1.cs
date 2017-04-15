using Hospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var context = new HospitalContext())
            {
                context.Database.Initialize(true);

                //var pe = new Patient() { FirstName = "Pesho" };
                //context.Patients.Add(pe);
                //context.SaveChanges();

                this.listBox1.DataSource = context.Patients.Select(p => p.FirstName).ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var context = new HospitalContext())
            {
                if(string.IsNullOrEmpty(this.newPatientNameTextBox.Text) == false)
                {
                    var newPatient = new Patient() { FirstName = this.newPatientNameTextBox.Text };

                    context.Patients.Add(newPatient);
                    context.SaveChanges();

                    this.listBox1.DataSource = context.Patients.Select(p => p.FirstName).ToList();
                    this.listBox1.Update();
                }
            }
        }
    }
}
