using EF_CodeFirst_CRUD.DAL.Context;
using EF_CodeFirst_CRUD.DAL.Entities;

namespace EF_CodeFirst_CRUD
{
    public partial class ProductListForm : Form
    {
        public ProductListForm()
        {
            InitializeComponent();
        }



        private void ProductListForm_Load(object sender, EventArgs e)
        {
            FillProducts();
        }

        private void FillProducts()
        {
            using (CRMContext DB = new CRMContext())
            {
                var result = DB.Product.ToList();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = result;
            }
        }
        public T CreateForm<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));

        }


        private void btn_New_Click(object sender, EventArgs e)
        {
            CreateForm<ProductForm>().ShowDialog();
            FillProducts();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var product = (dataGridView1.DataSource as List<Product>)[e.RowIndex];
                if (product != null)
                {
                    CreateForm<ProductForm>().ShowDialog();
                    FillProducts();

                }
            }
        }
    }
}
