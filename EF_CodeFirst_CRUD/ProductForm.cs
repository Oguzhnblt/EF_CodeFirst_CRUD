using EF_CodeFirst_CRUD.DAL.Context;
using EF_CodeFirst_CRUD.DAL.Entities;

namespace EF_CodeFirst_CRUD
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            FormClear();
        }

        private void FormClear()
        {
            txtProductName.Text = "";
            txtProductCode.Text = "";
            txtDescription.Text = "";
            nuUnitPrice.Value = 0;
            this.Tag = 0;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            FormSave();
        }

        private void FormSave()
        {
            int productId = Convert.ToInt32(this.Tag);
            try
            {
                using (CRMContext DB = new CRMContext())
                {
                    Product product = null;
                    if (productId == 0)
                    {
                        product = new Product();
                        DB.Product.Add(product);
                    }
                    else
                    {
                        product = DB.Product.FirstOrDefault(t0 => t0.ProductId == productId);
                    }
                    if (product != null)
                    {
                        product.ProductName = txtProductName.Text;
                        product.ProductCode = txtProductCode.Text;
                        product.Description = txtDescription.Text;
                        product.UnitPrice = nuUnitPrice.Value;
                    }
                    DB.SaveChanges();
                    this.Tag = product.ProductId;
                    MessageBox.Show("Kayıt başarılı");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            FormDelete();
        }

        private void FormDelete()
        {
            int productId = Convert.ToInt32(this.Tag);
            if (productId > 0)
            {
                var dialog = MessageBox.Show("Seçili ürünü silmek istiyor musunuz?", "CRM", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    using (CRMContext DB = new CRMContext())
                    {
                        var product = DB.Product.FirstOrDefault(t0 => t0.ProductId == productId);
                        if (product != null)
                        {
                            DB.Product.Remove(product);
                            DB.SaveChanges();
                            MessageBox.Show("İşlem başarılı");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {

        }
    }
}
