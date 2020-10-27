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
    public partial class MetodoDePago : ContentPage
    {
        public MetodoDePago(PaymentMethod paymentMethod)
        {
            InitializeComponent();
            CardNumber.Text = "**** **** **** " + paymentMethod.Name;
            ExpirationDate.Text = paymentMethod.ExpirationDate;
            TypeOfCard.Text = paymentMethod.TypeOfPayment;
        }
    }
}