using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Java.Security;
using Plugin.GoogleClient;

namespace MisiakAPp.Droid
{
    [Activity(Label = "MisiakAPp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CrossGoogleClient.Current.OnLogin += (s, a) =>
            {
                switch (a.Status)
                {
                    case GoogleActionStatus.Completed:
                        Toast.MakeText(ApplicationContext, "Google auth succeeded", ToastLength.Long).Show();
                        break;
                    default:
                        Toast.MakeText(ApplicationContext, string.Format("Google auth failed with code: {0}", a.Status.ToString()), ToastLength.Long).Show();
                        break;
                }
            };
            CrossGoogleClient.Current.LoginAsync();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}