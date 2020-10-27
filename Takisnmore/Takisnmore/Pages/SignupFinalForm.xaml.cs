using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Takisnmore.Scripts;

namespace Takisnmore.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupFinalForm : ContentPage
    {
        string PhoneNumber = "";
        public SignupFinalForm(string phonenumber)
        {
            InitializeComponent();
            PhoneNumber = phonenumber;
        }
        private void SendForm(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string pwd = PasswordEntry.Text;
            string gender = null;
            if (maleradiobutton.IsChecked)
                gender = "M";
            if (femaleradiobutton.IsChecked)
                gender = "F";
            if (name != null && pwd != null && gender != null)
            {
                if (name.Length < 50 && !name.Contains(".") && pwd.Length >= 4 && pwd.Length <= 6 && pwd == ConfirmPasswordEntry.Text)
                {
                    //send form
                    if(CustomerClient.Instance.SendCustomerCredentials(name, pwd, gender))
                    {
                        Navigation.PushAsync(new HomePage());
                    }
                    return;
                }
            }
            NameEntry.Text = "";
            PasswordEntry.Text = "";
            ConfirmPasswordEntry.Text = "";
            //Display error message
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}