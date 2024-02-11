using FreshApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreshApp.Vews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public APIService apiService { get; set; }

        public MainPage()
        {
            InitializeComponent();

            apiService = new APIService();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var res = await apiService.Login(usernameEntry.Text, pwdEntry.Text);

            if (res)
            {
                Application.Current.MainPage = new NavigationPage(new ItemListPage());
            }
            else
            {
                await DisplayAlert("", "login failed", "ok");
            }
        }
    }
}