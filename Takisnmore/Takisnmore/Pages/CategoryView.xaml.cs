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
    public partial class CategoryView : ContentPage
    {
        public CategoryView(string title, string id)
        {
            InitializeComponent();
            this.Title = title;
            FirstTagLabel.Text = title;
            LoadProducts(id, "1305", 0);
        }

        private void OnlyShowShops(object sender, EventArgs e)
        {
            OnlyProductsLine.BackgroundColor = Color.Transparent;
            OnlyShopsLine.BackgroundColor = Color.Black;
        }
        private void OnlyShowProducts(object sender, EventArgs e)
        {
            OnlyProductsLine.BackgroundColor = Color.Black;
            OnlyShopsLine.BackgroundColor = Color.Transparent;
        }

        public void LoadProducts(string categoryid, string search, int page)
        {
            //Request to the Server to get you the items in the category
            //Once you have them, loop them to make the UI.
        }
        public void LoadProductUI(string producttitle, string productshop, string price, string eta, string avgrating)
        {
            //Literally load the ui
            Grid ProductGrid = new Grid();

        }

    }
}