using EF_CodeFirst_CRUD.DAL.Context;
using EF_CodeFirst_CRUD.DAL.Entities;
using System.Data;

namespace EF_CodeFirst_CRUD
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            FillControl();
            FillForm();
        }

        private void FillForm()
        {
            int customerId = Convert.ToInt32(this.Tag);
            if (customerId > 0)
            {
                using (CRMContext DB = new CRMContext())
                {
                    var result = DB.Customer.FirstOrDefault(t0 => t0.CustomerId == customerId);
                    if (result != null)
                    {
                        txtCustomerName.Text = result.CustomerName;
                        txtEmail.Text = result.Email;
                        txtPhone.Text = result.Phone;
                        txtAdress.Text = result.Adress;
                        cmbCountry.SelectedValue = result.CountryId;
                        cmbCity.SelectedValue = result.CityId;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormSave();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FormDelete();
        }

        private void FormDelete()
        {
            int customerId = Convert.ToInt32(this.Tag);
            if (customerId > 0) // Mevcut bir kayıt açık mı ?
            {
                var dialog = MessageBox.Show("Mevcut kaydı silmek istiyor musunuz ?", "CRM", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    using (CRMContext DB = new CRMContext())
                    {
                        var result = DB.Customer.FirstOrDefault(t0 => t0.CustomerId == customerId);
                        if (result != null)
                        {


                            DB.Customer.Remove(result);
                            DB.SaveChanges();
                            MessageBox.Show("İşlem başarılı");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FormClear();
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCity();
        }
        private void FillControl()
        {
            FillCountry();
            FillCity();
        }

        private void FillCountry()
        {
            using (CRMContext DB = new CRMContext())
            {
                var result = DB.Country.Select(t0 => new { Value = t0.CountryId, Text = t0.CountryName }).ToList();

                cmbCountry.DisplayMember = "Text";
                cmbCountry.ValueMember = "Value";
                cmbCountry.DataSource = result;
            }
        }

        private void FillCity()
        {
            if (cmbCountry.SelectedIndex > -1)
            {
                int selectedCountryId = (int)cmbCountry.SelectedValue;
                using (CRMContext DB = new CRMContext())
                {
                    var result = DB.City.Where(t0 => t0.CountryId == selectedCountryId).Select(t0 => new { Value = t0.CityId, Text = t0.CityName }).ToList();

                    cmbCity.DisplayMember = "Text";
                    cmbCity.ValueMember = "Value";
                    cmbCity.DataSource = result;
                }
            }
            else
            {
                cmbCity.DataSource = null;
            }

        }

        private void FormClear()
        {
            txtCustomerName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAdress.Text = "";
            cmbCountry.SelectedIndex = -1;
            cmbCity.SelectedIndex = -1;
            this.Tag = 0; // Formun Tag Id bilgisi kontrol edilecek.
        }
        private void FormSave()
        {
            int customerId = Convert.ToInt32(this.Tag);

            try
            {
                using (CRMContext DB = new CRMContext())
                {
                    Customer customerObject = null;
                    if (customerId == 0)
                    {
                        // Yeni bir müşteri eklemek için

                        customerObject = new Customer();
                        DB.Customer.Add(customerObject);
                    }
                    else
                    {
                        // Var olan bir kaydı güncelleştirme

                        customerObject = DB.Customer.FirstOrDefault(t0 => t0.CustomerId == customerId);
                    }

                    if (customerObject != null)
                    {
                        customerObject.CustomerName = txtCustomerName.Text;
                        customerObject.Adress = txtAdress.Text;
                        customerObject.Email = txtEmail.Text;
                        customerObject.CountryId = (int)cmbCountry.SelectedValue;
                        customerObject.CityId = (int)cmbCity.SelectedValue;
                        customerObject.Phone = txtPhone.Text;

                        DB.SaveChanges();
                        this.Tag = customerObject.CustomerId;
                        MessageBox.Show("İşlem başarılı bir şekilde gerçekleşti.");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }

        }




    }
}
