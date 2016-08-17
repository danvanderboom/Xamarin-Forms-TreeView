using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace HighEnergy.TreeView.Demo
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        UIWindow window;

        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            window = new UIWindow (UIScreen.MainScreen.Bounds);

            Forms.Init();
            LoadApplication(new App());

            UINavigationBar.Appearance.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
            UINavigationBar.Appearance.TintColor = UIColor.Blue;
            UINavigationBar.Appearance.BarTintColor = UIColor.White;
            UINavigationBar.Appearance.SetTitleTextAttributes(
                new UITextAttributes
                {
                    TextColor = UIColor.White
                });

            window.RootViewController = BuildView();
            window.MakeKeyAndVisible ();

            return true;
        }

        static UIViewController BuildView()
        {
            var root = new DemoPage();
            var controller = root.CreateViewController();
            return controller;
        }
    }

}