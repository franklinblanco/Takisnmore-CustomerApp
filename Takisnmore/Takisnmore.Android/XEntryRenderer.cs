
using Android.Content;
using Android.Graphics.Drawables;
using Takisnmore;
using Takisnmore.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XEntry), typeof(XEntryRenderer))]
namespace Takisnmore.Droid
{
    class XEntryRenderer : EntryRenderer
    {
        public XEntryRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}