namespace EF_CodeFirst_CRUD
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void menuProductListItem_Click(object sender, EventArgs e)
        {
            CreateForm<ProductListForm>().ShowDialog();
        }


        private void menuCustomerListItem_Click(object sender, EventArgs e)
        {
            CreateForm<CustomerListForm>().ShowDialog();

        }

        public T CreateForm<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        private void menuOrderListItem_Click(object sender, EventArgs e)
        {
            CreateForm<OrderListForm>().ShowDialog();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
