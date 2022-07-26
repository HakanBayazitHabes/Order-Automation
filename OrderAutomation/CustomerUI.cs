using OrderAutomation.Business.Abstract;
using OrderAutomation.Business.Concrete;
using OrderAutomation.DataAccsessLayer.Concrete.EntityFrameWork;
using OrderAutomation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderAutomation
{
    public partial class CustomerUI : Form
    {
        public CustomerUI()
        {
            InitializeComponent();
            _cashServices = new CashManager(new EfCashDal());
            _checkServices = new CheckManager(new EfCheckDal());
            _creditServices = new CreditManager(new EfCreditDal());
            _customerChooseServices = new CustomerChooseManager(new EfCustomerChooseDal());
            _customerServices = new CustomerManager(new EfCustomerDal());
            _itemServices = new ItemManager(new EfItemDal());
            _orderDetailServices = new OrderDetailManager(new EfOrderDetailDal());
            _orderServices = new OrderManager(new EfOrderDal());
        }
        public static string PalType;

        private ICashServices _cashServices;
        private ICheckServices _checkServices;
        private ICreditServices _creditServices;
        private ICustomerChooseServices _customerChooseServices;
        private ICustomerServices _customerServices;
        private IItemServices _itemServices;
        private IOrderDetailServices _orderDetailServices;
        private IOrderServices _orderServices;

        public CustomerUI(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public CustomerUI(IOrderDetailServices orderDetailServices)
        {
            _orderDetailServices = orderDetailServices;
        }

        public CustomerUI(IItemServices ıtemServices)
        {
            _itemServices = ıtemServices;
        }

        public CustomerUI(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        public CustomerUI(ICustomerChooseServices customerChooseServices)
        {
            _customerChooseServices = customerChooseServices;
        }

        public CustomerUI(ICreditServices creditServices)
        {
            _creditServices = creditServices;
        }

        public CustomerUI(ICheckServices checkServices)
        {
            _checkServices = checkServices;
        }

        public CustomerUI(ICashServices cashServices)
        {
            _cashServices = cashServices;
        }

        private void CustomerUI_Load(object sender, EventArgs e)
        {
            LoadItems();
            gbxCash.Enabled = false;
            gbxCheck.Enabled = false;
            gbxCredit.Enabled = false;
        }

        private void LoadItems()
        {
            dgwItem.DataSource = _itemServices.GetAll();
            PalType = cbxPayType.Text;
        }

        private void btnGiveanOrder_Click(object sender, EventArgs e)
        {
            if (tbxQuantity.Text != "" && tbxTax.Text != "" && tbxName.Text != "" && tbxAddress.Text != "" && tbxItemName.Text != "" && tbxShippingWeight.Text != "" && cbxPayType.Text != "" && tbxStatus.Text != ""  && tbxDescription.Text != "" && tbxPrice.Text != "")
            {
                _customerServices.Add(new Customer
                {
                    Name = tbxName.Text,
                    Address = tbxAddress.Text,
                });
            }
            else
            {
                MessageBox.Show("Lütfen Boş Alanları Doldurunuz!");
            }
           

            if (tbxQuantity.Text != "" && tbxTax.Text != "" && tbxName.Text != "" && tbxAddress.Text != "" && tbxItemName.Text != "" && tbxShippingWeight.Text != "" && cbxPayType.Text != "" && tbxStatus.Text != ""  && tbxDescription.Text != "" && tbxPrice.Text != "")
            {
                _orderDetailServices.Add(new OrderDetail
                {
                    Quantity = Convert.ToInt32(tbxQuantity.Text),
                    taxStatus = Convert.ToInt32(tbxTax.Text)
                });
            }
            else
            {
                MessageBox.Show("Lütfen Boş Alanları Doldurunuz!");
            }

            if (tbxQuantity.Text != "" && tbxTax.Text != "" && tbxName.Text != "" && tbxAddress.Text != "" && tbxItemName.Text != "" && tbxShippingWeight.Text != "" && cbxPayType.Text != "" && tbxStatus.Text != ""  && tbxDescription.Text != "" && tbxPrice.Text != "")
            {
                _orderServices.Add(new Order
                {
                    Status = tbxStatus.Text,
                    Date = Convert.ToDateTime(dtpDate.Value.ToShortDateString())
                });
            }

            if (tbxQuantity.Text != "" && tbxTax.Text != "" && tbxName.Text != "" && tbxAddress.Text != "" && tbxItemName.Text != "" && tbxShippingWeight.Text != "" && cbxPayType.Text != "" && tbxStatus.Text != ""  && tbxDescription.Text != "" && tbxPrice.Text != "")
            {
                _customerChooseServices.Add(new CustomerChoose
                {
                    CItemName = tbxItemName.Text,
                    CShippingWeight = Convert.ToDouble(tbxShippingWeight.Text),
                    CDescription = tbxDescription.Text,
                    CPrice = Convert.ToDecimal(tbxPrice.Text),
                    PayType = cbxPayType.Text
                });
            }
            else
            {
                MessageBox.Show("Lütfen listeden bir ürün ve ödeme türü seçiniz.");
            }


            if (cbxPayType.Text == "Cash")
            {
                if (tbxQuantity.Text != "" && tbxTax.Text != "" && tbxName.Text != "" && tbxAddress.Text != "" && tbxItemName.Text != "" && tbxShippingWeight.Text != "" && cbxPayType.Text != "" && tbxStatus.Text != ""  && tbxDescription.Text != "" && tbxPrice.Text != "")
                {
                    _cashServices.Add(new Cash
                    {
                        CashTendered = Convert.ToDouble(tbxCashTendered.Text)
                    });
                }
                else
                {
                    MessageBox.Show("Lütfen kart bilgilerinizi giiriniz.");
                }

            }
             if (cbxPayType.Text=="Check")
            {
                if (tbxQuantity.Text != "" && tbxTax.Text != "" && tbxName.Text != "" && tbxAddress.Text != "" && tbxItemName.Text != "" && tbxShippingWeight.Text != "" && cbxPayType.Text != "" && tbxStatus.Text != ""  && tbxDescription.Text != "" && tbxPrice.Text != "")
                {
                    _checkServices.Add(new Check
                    {
                        Name = tbxCheckName.Text,
                        BankID = tbxCheckBankID.Text,
                        Amount = Convert.ToDecimal(tbxCheckAmount.Text)
                    });
                }
                else
                {
                    MessageBox.Show("Lütfen kart bilgilerinizi giiriniz.");
                }
                
            }
            if (cbxPayType.Text == "Credit")
            {
                if (tbxQuantity.Text != "" && tbxTax.Text != "" && tbxName.Text != "" && tbxAddress.Text != "" && tbxItemName.Text != "" && tbxShippingWeight.Text != "" && cbxPayType.Text != "" && tbxStatus.Text != ""  && tbxDescription.Text != "" && tbxPrice.Text != "")
                {
                    _creditServices.Add(new Credit
                    {
                        Number = tbxCreditNumber.Text,
                        Type = cbxCreditType.Text,
                        ExpData = tbxCreditDate.Text,
                        Amount = Convert.ToDecimal(tbxCreditAmount.Text)
                    });
                }
                else
                {
                    MessageBox.Show("Lütfen kart bilgilerinizi giiriniz.");
                }
            }
            //else
            //{
            //    MessageBox.Show("Lütfen bir ödeme türü seçiniz.");
            //}
            if (tbxQuantity.Text != "" && tbxTax.Text != "" && tbxName.Text != "" && tbxAddress.Text != "" && tbxItemName.Text != "" && tbxShippingWeight.Text != "" && cbxPayType.Text != "")
                MessageBox.Show("Your Order has been Received");
        }
        private void cbxPayType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxPayType.Text == "Cash")
            {
                tbxCashTendered.Text = label2.Text;
                gbxCash.Enabled = true;
                gbxCheck.Enabled = false;
                gbxCredit.Enabled = false;
            }
            if (cbxPayType.Text == "Check")
            {
                tbxCheckAmount.Text = label2.Text;
                gbxCash.Enabled = false;
                gbxCheck.Enabled = true;
                gbxCredit.Enabled = false;
            }
            if (cbxPayType.Text == "Credit")
            {
                tbxCreditAmount.Text = label2.Text;
                gbxCash.Enabled = false;
                gbxCheck.Enabled = false;
                gbxCredit.Enabled = true;
            }

        }

        private void dgwItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgwItem.CurrentRow;
            tbxShippingWeight.Text = row.Cells[1].Value.ToString();
            tbxDescription.Text = row.Cells[2].Value.ToString();
            tbxPrice.Text = row.Cells[3].Value.ToString();
            tbxItemName.Text = row.Cells[4].Value.ToString();
        }

        private void tbxQuantity_TextChanged(object sender, EventArgs e)
        {
            int tbxprice = Convert.ToInt32(dgwItem.CurrentRow.Cells[3].Value);
            float tbxweight = Convert.ToInt32(dgwItem.CurrentRow.Cells[1].Value);

            int price=0;
            float weight = 0;
            int tax = 0;
            if (tbxQuantity.Text == "")
            {
                tbxQuantity.Text = "1";

            }
            price = _itemServices.GetPriceForWuantity(Convert.ToInt32(tbxQuantity.Text), tbxprice);
            weight = _itemServices.GetWeightForQuantity(Convert.ToInt32(tbxQuantity.Text), tbxweight);
            tax = _orderServices.GetTax(tbxprice, Convert.ToInt32(tbxQuantity.Text));
            
            tbxPrice.Text = price.ToString();
            tbxShippingWeight.Text = weight.ToString();
            tbxTax.Text = tax.ToString();
            label2.Text = Convert.ToString(price + tax);
            //if (cbxPayType.Text == "Cash")
            tbxCashTendered.Text = label2.Text;
            //if (cbxPayType.Text == "Check")
                tbxCheckAmount.Text = label2.Text;
            //if (cbxPayType.Text == "Credit")
                tbxCreditAmount.Text = label2.Text;
        }
    }
}
