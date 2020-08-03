using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms.Platform.Android;

namespace UI.Mobie.AppCore.Droid
{
    public class AppActivity<T> :FormsAppCompatActivity where T:AppStartup,new()
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            var app = new T();
            LoadApplication(app);
        }

        protected virtual IServiceCollection CreateServiceContainer()
        {
            return new ServiceCollection();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}