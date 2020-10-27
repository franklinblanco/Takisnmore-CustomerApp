using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;
using Takisnmore.Scripts;
using Plugin.DeviceInfo;

namespace Takisnmore.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BackgroundColor = Color.Black;
            
        }
        private void VerifyNumber(object sender, EventArgs e)
        {
            string phonenumber = PhoneNumberEntry.Text;
            //This is to change between pages
            if (phonenumber == null)
                return;
            if (phonenumber.Length == 10)
            {
                if (CustomerClient.Instance.Authenticate(phonenumber, CrossDeviceInfo.Current.Id, true))
                {
                    Navigation.PushAsync(new HomePage());
                    return;
                }
                 Navigation.PushAsync(new VerificationPage(phonenumber));
            }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}