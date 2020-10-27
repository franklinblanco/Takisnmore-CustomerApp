using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takisnmore.Objects;
using Takisnmore.Scripts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Takisnmore.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemView : ContentPage
    {
        public IList<ImageSource> Photos { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string StoreName { get; private set; }
        public string Price { get; private set; }
        public ItemView(Item item)
        {
            InitializeComponent();
            Name = item.Title;
            Description = item.Description;
            StoreName = item.StoreName;
            Price = item.Price;

            //Pictures setup
            Photos = new List<ImageSource>();
            foreach (string picid in item.MediaIds)
            {
                Photos.Add(CacheManager.Instance.GetImageSource(picid));
            }

            BindingContext = this;
        }
    }
}