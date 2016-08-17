using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Xamarin;
using Android.Graphics.Drawables;
using Android.Content.PM;
using HighEnergy.TreeView.Demo;

namespace HighEnergy.TreeView.Demo.Android
{
    [Activity (Label = "HighEnergy.TreeView for Xamarin Forms", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}