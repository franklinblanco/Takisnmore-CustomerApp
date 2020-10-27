using System;
using Takisnmore.Pages;
using Takisnmore.Scripts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Takisnmore
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental", "AppTheme_Experimental" });
            InitializeComponent();
            

            //Change this loginpage to the splashpage, then attempt to connect, and if connected, take to login.
            MainPage = new NavigationPage(new SplashPage());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
