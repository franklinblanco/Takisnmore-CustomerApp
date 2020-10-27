using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takisnmore.Scripts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.DeviceInfo;
using System.IO;

namespace Takisnmore.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationPage : ContentPage
    {
        string customerphonenumber = "";
        public VerificationPage(string phonenumber)
        {
            InitializeComponent();

            customerphonenumber = phonenumber;

            char[] PhoneNumber = phonenumber.ToCharArray();
            string phoneNumber = "("+PhoneNumber[0] + PhoneNumber[1] + PhoneNumber[2] +
                ")-" + PhoneNumber[3] + PhoneNumber[4] + PhoneNumber[5] + "-" + PhoneNumber[6] + PhoneNumber[7] + PhoneNumber[8] + PhoneNumber[9];

            var FormattedPhoneNumber = new FormattedString();
            FormattedPhoneNumber.Spans.Add(new Span { Text = "Hemos Envíado un código de 6 dígitos por SMS al número ", ForegroundColor = Color.Black, FontSize = 18 });
            FormattedPhoneNumber.Spans.Add(new Span { Text = phoneNumber, ForegroundColor = Color.Black, FontSize = 18, FontAttributes = FontAttributes.Bold });


            VerificationInfoLabel.FormattedText = FormattedPhoneNumber;
        }
        private void VerifyOTP(object sender, EventArgs e)
        {
            string OTP = OTPEntry.Text;
            if (OTP.Length == 6)
            {
                if (CustomerClient.Instance.VerifyNumber(OTP)) 
                {
                    string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "UserData.txt");
                    File.WriteAllText(filename, customerphonenumber);

                    //Success
                    Navigation.PushAsync(new SignupFinalForm(customerphonenumber));
                }
                   
            } else
            {
                //Display error message
            }
            
        }
        private void ChangeNumber(object sender, EventArgs e)
        {
            CustomerClient.Instance.DisconnectFromServer();
            CustomerClient.Instance.ConnectToServer();
            Navigation.PushAsync(new LoginPage());
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}