using EF_CodeFirst_CRUD.DAL.Context;
using EF_CodeFirst_CRUD.DAL.Entities;
using EF_CodeFirst_CRUD.DTO;

namespace EF_CodeFirst_CRUD
{
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }
        private void OrderForm_Load_1(object sender, EventArgs e)
        {
            FillControl();
            FillOrderDetailGridLayout();
            FillForm();
        }

        private void FillOrderDetailGridLayout()
        {
            gridOrderDetail.AutoGenerateColumns = false;
            gridOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridOrderDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridOrderDetail.EditMode = DataGridViewEditMode.EditProgrammatically;
            gridOrderDetail.MultiSelect = false;

            var productColumn = new DataGridViewTextBoxColumn();
            productColumn.Name = "ProductName";
            productColumn.HeaderText = "Ürün Adı";
            productColumn.DataPropertyName = "ProductName";

            var unitPriceColumn = new DataGridViewTextBoxColumn();
            unitPriceColumn.Name = "UnitPrice";
            unitPriceColumn.HeaderText = "Ürün Fiyatı";
            unitPriceColumn.DataPropertyName = "UnitPrice";

            var QuantityColumn = new DataGridViewTextBoxColumn();
            QuantityColumn.Name = "Quantity";
            QuantityColumn.HeaderText = "Ürün Adedi";
            QuantityColumn.DataPropertyName = "Quantity";

            var discountedColumn = new DataGridViewTextBoxColumn();
            discountedColumn.Name = "Discounted";
            discountedColumn.HeaderText = "İndirim ";
            discountedColumn.DataPropertyName = "Discounted";

            gridOrderDetail.Columns.Add(productColumn);
            gridOrderDetail.Columns.Add(unitPriceColumn);
            gridOrderDetail.Columns.Add(QuantityColumn);
            gridOrderDetail.Columns.Add(discountedColumn);

        }

        private void FillForm()
        {
            int orderId = Convert.ToInt32(this.Tag);
            if (orderId > 0)
            {
                using (CRMContext DB = new CRMContext())
                {
                    var result = DB.Order.FirstOrDefault(t0 => t0.OrderId == orderId);
                    if (result != null)
                    {
                        dt_OrderDate.Value = Convert.ToDateTime(result.OrderDate);
                        cmbCustomer.SelectedValue = result.CustomerId;
                        txtOrderCode.Text = result.OrderCode;
                    }
                }
                FillOrderDetailGrid();
            }
        }

        private void FillOrderDetailGrid()
        {
            int orderId = Convert.ToInt32(this.Tag);
            using (CRMContext DB = new CRMContext())
            {

                var orderDetails = DB.OrderDetails.Where(t0 => t0.OrderId == orderId).Select(t0 => new OrderDetailDTO()
                {
                    ProductId = t0.ProductId,
                    Discount = Convert.ToSingle(t0.Discount),
                    OrderDetailId = t0.OrderDetailId,
                    ProductName = t0.Product.ProductName,
                    Quantity = Convert.ToSingle(t0.Quantity),
                    UnitPrice = Convert.ToDecimal(t0.Price)

                }).ToList();

                gridOrderDetail.DataSource = null;
                gridOrderDetail.DataSource = orderDetails;

            }

        }

        private void FillControl()
        {
            FillCustomer();
            FillProduct();
        }

        private void FillProduct()
        {
            using (CRMContext DB = new CRMContext())
            {
                var result = DB.Product.Select(t0 => new { Value = t0.ProductId, Text = t0.ProductName }).ToList();
                cmbProduct.DisplayMember = "Text";
                cmbProduct.ValueMember = "Value";
                cmbProduct.DataSource = result;
            }
        }

        private void FillCustomer()
        {
            using (CRMContext DB = new CRMContext())
            {
                var result = DB.Customer.Select(t0 => new { Value = t0.CustomerId, Text = t0.CustomerName }).ToList();
                cmbCustomer.DisplayMember = "Text";
                cmbCustomer.ValueMember = "Value";
                cmbCustomer.DataSource = result;
            }
        }
        private void FormClear()
        {
            txtOrderCode.Text = "";
            dt_OrderDate.Value = DateTime.Now;
            cmbCustomer.SelectedIndex = -1;
            cmbProduct.SelectedIndex = -1;
            nuPrice.Value = 0;
            nuQuantity.Value = 0;
            nuDiscount.Value = 0;

            // Alt detay gridini temizleme 
            gridOrderDetail.DataSource = null;
            this.Tag = 0;
        }
        private void FormSave()
        {
            int orderId = Convert.ToInt32(this.Tag);
            Order order = null;

            using (CRMContext DB = new CRMContext())
            {
                if (orderId == 0)
                {
                    // Yeni bir sipariş durumunda OrderCode genarete et.
                    order = new Order();
                    order.OrderCode = Guid.NewGuid().ToString();
                    DB.Order.Add(order);
                }
                else
                {
                    order = DB.Order.FirstOrDefault(t0 => t0.OrderId == orderId);
                }
                if (order != null)
                {
                    order.OrderDate = dt_OrderDate.Value;
                    order.CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                }
                DB.SaveChanges();
                this.Tag = order.OrderId;

                MessageBox.Show("Kayıt başarılı");


            }

        }
        private void OrderDetailAdd()
        {
            if (OrderDetailValidation())
            {
                // EntityValid
                using (CRMContext DB = new CRMContext())
                {
                    OrderDetail orderDetail = null;
                    if(selectedOrderDetailId == 0)
                    {
                        orderDetail = new OrderDetail();
                        DB.OrderDetails.Add(orderDetail);
                    }
                    else
                    {
                        orderDetail = DB.OrderDetails.FirstOrDefault(t0 => t0.OrderDetailId == selectedOrderDetailId);
                    }
                    orderDetail.ProductId = Convert.ToInt32(cmbProduct.SelectedValue);
                    orderDetail.Price = Convert.ToDecimal(nuPrice.Value);
                    orderDetail.Quantity = Convert.ToInt32(nuQuantity.Value);
                    orderDetail.Discount = Convert.ToInt32(nuDiscount.Value);
                    orderDetail.OrderId = Convert.ToInt32(this.Tag);
                    DB.SaveChanges();
                }

                FillOrderDetailGrid();
            }
        }

        public bool OrderDetailValidation()
        {
            bool result = true;
            int orderId = Convert.ToInt32(this.Tag);
            if (orderId <= 0)
            {
                result = false;
                MessageBox.Show("İlk başta sipariş kaydedilmelidir!");
            }

            if (cmbProduct.SelectedIndex <= -1)
            {
                result = false;
                MessageBox.Show("Ürün seçimi yapılmamış, ürün eklemeden önce ürün seçimi yapınız..");
            }

            return result;
        }

        private void OrderDetailControlClear()
        {
            cmbProduct.SelectedIndex = -1;
            nuDiscount.Value = 0;
            nuPrice.Value = 0;
            nuQuantity.Value = 0;
            selectedOrderDetailId = 0; // Seçimden kaynaklanan Id sıfırlandı.
        }

        private void OrderDetailDelete()
        {
            if (gridOrderDetail.SelectedRows.Count > 0)
            {
                int index = gridOrderDetail.SelectedRows[0].Index;
                if (gridOrderDetail.DataSource != null)
                {
                    var result = (gridOrderDetail.DataSource as List<OrderDetailDTO>)[index];
                    var dialog = MessageBox.Show("Seçilen satırı silmek istiyor musunuz?", "CRM", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.OK)
                    {
                        using (CRMContext DB = new CRMContext())
                        {
                            var orderDetailItem = DB.OrderDetails.FirstOrDefault(t0 => t0.OrderDetailId == result.OrderDetailId);
                            if (orderDetailItem != null)
                            {
                                DB.OrderDetails.Remove(orderDetailItem);
                                DB.SaveChanges();
                                FillOrderDetailGrid();
                            }
                        }
                    }
                }
            }
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            FormClear();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            FormSave();
        }

        private void btn_ProductAdd_Click(object sender, EventArgs e)
        {
            OrderDetailAdd();
            OrderDetailControlClear();
        }



        private void btn_DeleteProduct_Click(object sender, EventArgs e)
        {
            OrderDetailDelete();
        }
        int selectedOrderDetailId = 0;
        private void gridOrderDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int orderDetailId = (gridOrderDetail.DataSource as List<OrderDetailDTO>)[e.RowIndex].OrderDetailId;
                if (orderDetailId > 0)
                {
                    using (CRMContext DB = new CRMContext())
                    {
                        var orderDetail = DB.OrderDetails.FirstOrDefault(t0 => t0.OrderDetailId == orderDetailId);
                        if (orderDetail != null)
                        {
                            cmbProduct.SelectedValue = orderDetail.ProductId;
                            nuPrice.Value = Convert.ToDecimal(orderDetail.Price);
                            nuQuantity.Value = Convert.ToDecimal(orderDetail.Quantity); 
                            nuDiscount.Value = Convert.ToDecimal(orderDetail.Discount);
                            selectedOrderDetailId = orderDetail.OrderDetailId;


                        }
                    }
                }
            }
        }
    }
}
