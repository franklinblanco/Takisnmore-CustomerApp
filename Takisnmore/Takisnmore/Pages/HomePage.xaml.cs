using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Takisnmore.Objects;
using Twilio.Rest.Studio.V2.Flow;
using Xamarin.Forms;
using Xamarin.Forms.Core;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using Takisnmore.Scripts;

namespace Takisnmore.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        
        private bool isMenuOpen = true;
        public IList<Item> Items { get; private set; }

        #region Main Method
        public HomePage()
        {
            
            InitializeComponent();
            Load();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        #endregion
        public void Load()
        {
            //Load all the content in the homepage
            LoadSections();
            LoadCategories();
        }
        public void LoadSections()
        {
            string[] sections = CustomerClient.Instance.GetSections();
            SectionButton.Text = sections[0];
        }
        #region Category loading
        public void LoadCategories(string sectionid, int pagenumber)
        {
            //get all info in strings with client command
            string categoriesraw = CustomerClient.Instance.GetSectionCategories("Categories-" + sectionid + "-" + pagenumber.ToString());

            //Divide them into string arrays
            string[] Categories = categoriesraw.Split('/');


            for (int x = 0; x < Categories.Length; x++)
            {
                StackLayout categorycontainer = new StackLayout
                {
                    Children =
                            {
                                new Grid
                                {
                                    Children =
                                    {
                                        new Label { Text = Categories[x].Split(':')[1], FontAttributes = FontAttributes.Bold, FontSize = 25, Margin = new Thickness(30,0,0,0)},
                                        new Label { Text = "Ver Todos", FontSize = 18, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center,
                                            Margin = new Thickness(0,0,30,0), TextColor = Color.PaleVioletRed } //Add tapgesture recognizer.
                                    }
                                }
                            }
                };
                categoryholder.Children.Add(categorycontainer);

                //Start from the bottom up
                Image productimage = new Image { BackgroundColor = Color.Black };
                Image productimagewhite = new Image { BackgroundColor = Color.White };
                //Make a videomanager
                //Make an if statement that decides what goes inside the frame

                StackLayout ItemsContainer = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(20, 10, 20, 0),
                    Spacing = 20
                };

                ScrollView scrollView = new ScrollView
                {
                    Orientation = ScrollOrientation.Horizontal,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                    Content = ItemsContainer
                };
                categorycontainer.Children.Add(scrollView);

                //Made the category grid, now add the items.
                string items = CustomerClient.Instance.GetHomePageInfo("CategoryItems-" + Categories[x].Split(':')[0]);
                string[] Items = items.Split('/');

                for (int z = 0; z < Items.Length; z++)
                {
                    string[] itemproperties = Items[z].Split(':');
                    string[] itemmediaids = itemproperties[4].Split(',');

                    //UI Elements for Product display
                    Frame brandlogoframe = new Frame
                    {
                        HeightRequest = 60,
                        WidthRequest = 60,
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.Start,
                        BackgroundColor = Color.White,
                        BorderColor = Color.LightGray,
                        CornerRadius = 30,
                        IsClippedToBounds = true,
                        Margin = new Thickness(10),
                        Padding = new Thickness(0)
                    };

                    Grid descriptiongrid = new Grid
                    {
                        RowDefinitions =
                        {
                            new RowDefinition { Height = new GridLength(3.5, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(5, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength(2, GridUnitType.Star) }
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(3.5, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) }
                        },
                        Margin = new Thickness(20)
                    };

                    Frame productimageframe = new Frame
                    {
                        CornerRadius = 30,
                        IsClippedToBounds = true,
                        Margin = new Thickness(0),
                        Padding = new Thickness(0),
                        BorderColor = Color.Transparent
                    };

                    Grid productgrid = new Grid
                    {
                        Children = { productimageframe },
                        HeightRequest = 255, //Height of the image/video
                        WidthRequest = 340, //Width of the image/video
                    };

                    Button gobackbtn = new Button
                    {
                        BackgroundColor = Color.Transparent,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };
                    gobackbtn.Clicked += async (sender, args) =>
                    {
                        brandlogoframe.IsVisible = true;
                        brandlogoframe.IsEnabled = true;
                        descriptiongrid.IsVisible = false;
                        descriptiongrid.IsEnabled = false;
                    };

                    Label descriptiontitle = new Label
                    {
                        Text = itemproperties[0],
                        VerticalOptions = LayoutOptions.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.Start,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(0, 10, 0, 0),
                        FontSize = 23,
                        TextColor = Color.Black,
                        MaxLines = 1
                    };

                    Label descriptionbrandnamelabel = new Label
                    {
                        Text = itemproperties[3],
                        FontSize = 14,
                        VerticalOptions = LayoutOptions.End,
                        VerticalTextAlignment = TextAlignment.End
                    };

                    Label descriptiontext = new Label
                    {
                        Text = itemproperties[2],
                        FontSize = 13,
                        TextColor = Color.Black,
                        MaxLines = 6,
                        VerticalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(0, 0, 18, 0)
                    };

                    Label descriptionrating = new Label
                    {
                        Text = "4.5★",
                        TextColor = Color.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                        MaxLines = 1
                    };
                    Label descriptionpricing = new Label
                    {
                        Text = "RD$" + itemproperties[1],
                        TextColor = Color.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                        MaxLines = 1
                    };

                    Frame descriptionbrandlogo = new Frame
                    {
                        HeightRequest = 100,
                        WidthRequest = 100,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.End,
                        BorderColor = Color.LightGray,
                        CornerRadius = 100,
                        IsClippedToBounds = true,
                        Margin = new Thickness(0),
                        Padding = new Thickness(0)
                    };

                    TapGestureRecognizer opendescriptiongr = new TapGestureRecognizer();
                    opendescriptiongr.Tapped += (s, e) =>
                    {
                        brandlogoframe.IsVisible = false;
                        brandlogoframe.IsEnabled = false;
                        descriptiongrid.IsVisible = true;
                        descriptiongrid.IsEnabled = true;
                    };

                    TapGestureRecognizer gotoitemmenu = new TapGestureRecognizer();
                    gotoitemmenu.Tapped += (s, e) =>
                    {
                        Navigation.PushAsync(new ItemView(new Item { ItemId = itemproperties[6], Price = itemproperties[1], Title = itemproperties[0], MediaIds = itemmediaids, Description = itemproperties[2], StoreName = itemproperties[3] }));
                        //Open the Xaml Page with the product information and the addtocart/buy button.
                    };

                    Image brandlogoimg = new Image { Aspect = Aspect.AspectFill };
                    brandlogoimg.GestureRecognizers.Add(opendescriptiongr);

                    Image productImage = new Image { Aspect = Aspect.AspectFill };
                    productImage.GestureRecognizers.Add(gotoitemmenu);

                    BoxView descriptionbg = new BoxView { CornerRadius = 30, BackgroundColor = Color.White };

                    ItemsContainer.Children.Add(productgrid);
                    productgrid.Children.Add(descriptiongrid);
                    productgrid.Children.Add(brandlogoframe);

                    //Descriptiongrid stuff
                    Grid.SetRowSpan(descriptionbrandlogo, 2);
                    Grid.SetColumnSpan(descriptionbg, 3);
                    Grid.SetRowSpan(descriptionbg, 3);
                    Grid.SetColumnSpan(descriptiontitle, 2);
                    Grid.SetColumn(descriptiontitle, 1);
                    Grid.SetColumnSpan(descriptiontext, 2);
                    Grid.SetColumn(descriptiontext, 1);
                    Grid.SetRow(descriptiontext, 1);
                    Grid.SetColumn(descriptionbrandnamelabel, 1);
                    Grid.SetColumnSpan(descriptionbrandnamelabel, 2);
                    Grid.SetColumnSpan(gobackbtn, 3);
                    Grid.SetRowSpan(gobackbtn, 3);

                    descriptiongrid.Children.Add(descriptionbg);
                    descriptiongrid.Children.Add(descriptionbrandlogo);
                    descriptiongrid.Children.Add(descriptiontitle);
                    descriptiongrid.Children.Add(descriptiontext);
                    descriptiongrid.Children.Add(descriptionrating, 1, 2);
                    descriptiongrid.Children.Add(descriptionpricing, 2, 2);
                    descriptiongrid.Children.Add(descriptionbrandnamelabel);
                    descriptiongrid.Children.Add(gobackbtn);

                    descriptiongrid.IsVisible = false;
                    descriptiongrid.IsEnabled = false;
                    //Divide the item string and put the info in the respective UI elements
                    if (itemproperties.Length > 4)

                    {
                        if (itemproperties[4].Contains('I'))
                        {
                            productImage.Source = CacheManager.Instance.GetImageSource(itemproperties[4]);
                            productimageframe.Content = productImage;
                        }
                    }
                    if (itemproperties.Length > 5)
                    {
                        brandlogoimg.Source = CacheManager.Instance.GetImageSource(itemproperties[5]);
                        brandlogoframe.Content = brandlogoimg;
                        descriptionbrandlogo.Content = new Image { Source = CacheManager.Instance.GetImageSource(itemproperties[5]), Aspect = Aspect.AspectFill };
                    }
                }
            }
        }
        #endregion
        public void LoadProducts()
        {

        }
        #region Menu Stuff

        #region Menu button methods
        private async void ToggleMenu(object sender, EventArgs e)
        {
            //Method to open & close the Side Menu.
            if (isMenuOpen)
            {
                MenuGrid.IsVisible = true;
                MenuGrid.IsEnabled = true;
                ShadeBG.IsEnabled = true;
                ShadeBG.IsVisible = true;
                await MenuGrid.TranslateTo(MenuGrid.X - 100, MenuGrid.Y, 200);
            }
            else
            {
                ShadeBG.IsEnabled = false;
                ShadeBG.IsVisible = false;
                await MenuGrid.TranslateTo(MenuGrid.X - Width, MenuGrid.Y, 100);
                MenuGrid.IsVisible = false;
                MenuGrid.IsEnabled = false;
            }

            isMenuOpen = !isMenuOpen;

        }

        
        private void Cartera(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cartera());
        }
        private void Perfil(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }
        private void Direcciones(object sender, EventArgs e)
        {
            
        }
        private void ElegirDireccion(object sender, EventArgs e)
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                .SetTitle("Elija una dirección")
                .Add("Casa", null, "homeicon.png")
                 );
        }
        private void Pedidos(object sender, EventArgs e)
        {

        }
        private void Amigos(object sender, EventArgs e)
        {

        }
        private void Favoritos(object sender, EventArgs e)
        {
        }
        #endregion
        #endregion

        #region General Button Methods
        private void FocusSearchBar(object sender, EventArgs e)
        {
            SearchBar.Focus();
        }
        private void SeeAll(object sender, EventArgs e)
        {

        }
        private async void ChooseShop(object sender, EventArgs e)
        {
            string[] allsections = CustomerClient.Instance.GetSections();
            string selectedShop = await DisplayActionSheet("¿Que deseas?", "Volver", null, allsections);
            SectionButton.Text = selectedShop;
            //Change the store depending on this
        }
        private void SeeItem(object sender, EventArgs e)
        {
            Image Sender = (Image)sender;
            foreach (Item item in Items)
            {
                if (item.ItemId == Sender.AutomationId)
                {
                    Navigation.PushAsync(new ItemView(item));
                    break;
                }
                else
                {
                    Console.WriteLine("The Item Requested was not found.");
                }
            }
        }
        private void seeStore(object sender, EventArgs e)
        {

        }
        private void seeItemReviews(object sender, EventArgs e)
        {
            
            //Navigation.PushAsync(new ItemView());
        }
        #endregion
    }
}