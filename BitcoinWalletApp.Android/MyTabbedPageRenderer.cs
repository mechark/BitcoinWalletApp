using Xamarin.Forms;
using BitcoinWalletApp.Views;
using Xamarin.Forms.Platform.Android.AppCompat;
using TabbedDemo.Droid;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using Google.Android.Material.BottomNavigation;
using BitcoinWalletApp;

[assembly: ExportRenderer(typeof(ParentPage), typeof(MyTabbedPageRenderer))]
namespace TabbedDemo.Droid
{
    public class MyTabbedPageRenderer : TabbedPageRenderer
    {
        public MyTabbedPageRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null && e.NewElement != null)
            {
                for (int i = 0; i <= this.ViewGroup.ChildCount - 1; i++)
                {
                    var childView = this.ViewGroup.GetChildAt(i);
                    if (childView is ViewGroup viewGroup)
                    {
                        for (int j = 0; j <= viewGroup.ChildCount - 1; j++)
                        {
                            var childRelativeLayoutView = viewGroup.GetChildAt(j);
                            if (childRelativeLayoutView is BottomNavigationView)
                            {
                                ((BottomNavigationView)childRelativeLayoutView).ItemIconTintList = null;
                            }
                        }
                    }
                }
            }
        }

    }
}