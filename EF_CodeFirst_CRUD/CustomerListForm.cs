using EF_CodeFirst_CRUD.DAL.Context;
using EF_CodeFirst_CRUD.DAL.Entities;

namespace EF_CodeFirst_CRUD
{
    public partial class CustomerListForm : Form
    {
        public CustomerListForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowCustomerForm(0);
            FillCustomer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillForm();
        }

        private void FillForm()
        {
            FillCustomer();
        }

        private void FillCustomer()
        {
            using (CRMContext DB = new CRMContext())
            {
                var result = DB.Customer.ToList();
                dataGridView1.DataSource = result;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var customer = (dataGridView1.DataSource as List<Customer>)[e.RowIndex];
                if (customer != null)
                {
                    ShowCustomerForm(customer.CustomerId);
                    FillCustomer();
                }
            }
        }

        private void ShowCustomerForm(int customerId)
        {
            var form = new CustomerForm();
            form.Tag = customerId; // Mevcut bir kaydý düzenlemek için yapýldý.
            form.ShowDialog();
        }
    }
}