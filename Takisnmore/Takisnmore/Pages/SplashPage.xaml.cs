using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takisnmore.Scripts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.DeviceInfo;
using System.Threading;

namespace Takisnmore.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            
            AttemptConnection(true);
        }
        private void AttemptConnectionClicked(object sender, EventArgs e) //this is for the button click to just call the method.
        {
            AttemptConnection(false);
        }
        private async void AttemptConnection(bool wait)
        {
            if (wait)
            {
                await Task.Delay(1000);
            }
            if (CustomerClient.Instance.ConnectToServer())
            {
                errorlabel.IsVisible = false;
                tryagainbtn.IsEnabled = false;
                tryagainbtn.IsVisible = false;


                string userdatapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "UserData.txt");
                //check if there is authdata first, then auth if there is, if there isn't then make it.-
                if (File.Exists(userdatapath))
                {
                    string phonenumber = File.ReadAllText(userdatapath);
                    if (CustomerClient.Instance.Authenticate(phonenumber, CrossDeviceInfo.Current.Id, false))
                    {
                        await Navigation.PushAsync(new HomePage());
                    } 
                    else
                    {
                        File.Delete(userdatapath);
                        CustomerClient.Instance.Reconnect();
                        Navigation.PushAsync(new LoginPage());
                    }
                }
                else
                {
                    Navigation.PushAsync(new LoginPage());
                }
                //Auth and decide wether to throw customer to login screen or to the app.
            }
            else
            {
                errorlabel.IsVisible = true;
                tryagainbtn.IsEnabled = true;
                tryagainbtn.IsVisible = true;
            }
        }
        private void GetAuthData()
        {
            //Store auth data in a file (encrypted)
        }
    }
}