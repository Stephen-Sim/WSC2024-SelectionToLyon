using FreshApp.Models;
using FreshApp.Vews;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreshApp
{
    public partial class App : Application
    {
        public static User User { get; set; }
        public App()
        {
            InitializeComponent();

            User = new User();

            var user = Preferences.Get("user", null);

            if (user == null)
            {
               MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                User = JsonConvert.DeserializeObject<User>(user);
                MainPage = new NavigationPage(new ItemListPage());
            }

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
