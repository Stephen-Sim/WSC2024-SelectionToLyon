using FreshApp.Models;
using FreshApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreshApp.Vews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddItemPage : ContentPage
	{
        public APIService apiService { get; set; }
        public AddItemPage()
		{
			InitializeComponent();

            apiService = new APIService();

            loadData();
        }

        async void loadData()
        {
            var res = await apiService.GetItemTypes();

            typePicker.ItemsSource = res;

            loadCaptcha();
        }

        async void loadCaptcha()
        {
            Captcha = await apiService.GetCaptcha();
            CaptchaImage.Source = ImageSource.FromStream(() => new MemoryStream(Captcha.Image));
        }

        public Captcha Captcha { get; set; }

        public byte[] itemImageArray { get; set; } = null;

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string action= await DisplayActionSheet("Choose From Media", "Cancel", null, "Camare", "File");

            if (action == "Camare" || action == "File")
            {
                var image = action == "File" ? await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo"
                }) :
                await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo"
                });

                if (image != null)
                {
                    var stream = await image.OpenReadAsync();
                    itemImage.Source = ImageSource.FromStream(() => stream);
                    itemImageArray = File.ReadAllBytes(image.FullPath);
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (itemImageArray == null 
                || string.IsNullOrEmpty(nameEntry.Text) 
                || string.IsNullOrEmpty(addressEntry.Text) 
                || typePicker.SelectedItem == null)
            {
                await DisplayAlert("", "All fields are required", "ok");
                return;
            }

            if (captchaEntry.Text != Captcha.Key)
            {
                await DisplayAlert("", "Invalid captcha", "ok");
                captchaEntry.Text = string.Empty;
                loadCaptcha();
                return;
            }

            if (datePicker.Date <= DateTime.Today.Date)
            {
                await DisplayAlert("", "the due date must be at least one day after today.", "ok");
                return;
            }

            var itemDTO = new ItemDTO
            {
                name = nameEntry.Text,
                address = addressEntry.Text,
                type = (string) typePicker.SelectedItem,
                expiry_date = datePicker.Date.ToString("dd MMMM yyyy"),
                image = itemImageArray,
                userId = App.User.Id
            };

            var res = await apiService.StoreItem(itemDTO);

            if (res)
            {
                await DisplayAlert("", "item added.", "ok");
                App.Current.MainPage = new NavigationPage(new ItemListPage());
            }
            else
            {
                await DisplayAlert("", "item add failed.", "ok");
            }
        }
    }
}