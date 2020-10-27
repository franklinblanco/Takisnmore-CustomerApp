using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Takisnmore.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cartera : ContentPage
    {
        public IList<PaymentMethod> PaymentMethods { get; private set; }
        public Cartera()
        {
            InitializeComponent();

            //Here you have to fill in the payment methods data from a database
            PaymentMethods = new List<PaymentMethod>();
            PaymentMethods.Add(new PaymentMethod
            {
                //Always use the **** to make it good looking

                Name = "5102",
                ExpirationDate = "10/2050",
                TypeOfPayment = "Visa",
                ImageUrl = "CardIconVisa"
            }) ;

            PaymentMethods.Add(new PaymentMethod
            {

                Name = "1132",
                ExpirationDate = "05/2025",
                TypeOfPayment = "MasterCard",
                ImageUrl = "CardIconMasterCard"
            });

            PaymentMethods.Add(new PaymentMethod
            {
                Name = "Efectivo",
                ExpirationDate = null,
                TypeOfPayment = "Cash",
                ImageUrl = "CashIcon"
            }) ;
            BindingContext = this;
        }
        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Leave this empty for now. You don't need to do anything when the client selects the payment method.
        }
        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            PaymentMethod tappedPayment = e.Item as PaymentMethod;
            
            if (tappedPayment.Name == "Efectivo")
            {
                Navigation.PushAsync(new CashInfo());
                return;
            }
            Navigation.PushAsync(new MetodoDePago(tappedPayment));
        }
        private void AddPayment(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarMetodoDePago());
        }
        private void AddBalance(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddBalancePage());
        }
    }
}