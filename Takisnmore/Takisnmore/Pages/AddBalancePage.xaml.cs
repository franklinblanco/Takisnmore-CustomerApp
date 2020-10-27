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
    public partial class AddBalancePage : ContentPage
    {
        public AddBalancePage()
        {
            InitializeComponent();
        }
        private void AddFundsToBalance(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChoosePaymentMethod(/*Add a constructor to pass the data here*/));
        }
    }
}