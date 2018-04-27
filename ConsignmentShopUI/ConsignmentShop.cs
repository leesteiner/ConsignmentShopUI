using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignmentShopLibrary;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData= new List<Item>();
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();
        private decimal storeProfit = 0;

        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListbox.DataSource = itemsBinding;
            itemsListbox.DisplayMember = "Display";
            itemsListbox.ValueMember = "Display";

            cartBinding.DataSource = shoppingCartData;
            shoppingCartListbox.DataSource = cartBinding;
            shoppingCartListbox.DisplayMember = "Display";
            shoppingCartListbox.ValueMember = "Display";

            vendorsBinding.DataSource = store.Vendors;
            vendorListbox.DataSource = vendorsBinding;
            vendorListbox.DisplayMember = "Display";
            vendorListbox.ValueMember = "Display";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SetupData()
        {
            store.Vendors.Add(new Vendor { FirstName = "Bill", LastName = "Smith", Commission = .5 });
            store.Vendors.Add(new Vendor { FirstName = "Sue", LastName = "Jones", Commission = .5 });
            store.Vendors.Add(new Vendor { FirstName = "Lee", LastName = "Steiner", Commission = .5 });
            store.Vendors.Add(new Vendor("Sandy", "Taylor"));

            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "A book about a whale",
                Price = 4.50M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "A Tale of Two Cities",
                Description = "A book about a revolution",
                Price = 3.80M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Harry Potter: Book 1",
                Description = "A book about a wizard",
                Price = 5.20M,
                Owner = store.Vendors[2]
            });

            store.Items.Add(new Item
            {
                Title = "Jane Eyre",
                Description = "A book about... Jane Eyre",
                Price = .50M,
                Owner = store.Vendors[3]
            });

            store.Name = "Lee's Book Store";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            // Figure out what is selected from ItemsList
            //Copy Item to Shopping Cart
            //Delete item from ItemsList
            Item selectedItem = (Item)itemsListbox.SelectedItem;
            shoppingCartData.Add(selectedItem);
            cartBinding.ResetBindings(false);
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            //Mark each item in the cart as sold
            //Clear the cart
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                if (item.Sold == true) { item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price; }
                storeProfit += item.Price - ((decimal)item.Owner.Commission * item.Price);
            }
            shoppingCartData.Clear();
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            storeProfitValue.Text = string.Format("${0}", storeProfit);

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);

        }
    }
}
