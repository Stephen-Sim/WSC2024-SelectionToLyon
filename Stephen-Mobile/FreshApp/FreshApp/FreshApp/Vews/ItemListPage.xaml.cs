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
	public partial class ItemListPage : ContentPage
	{
        public APIService apiService { get; set; }
        public ItemListPage ()
		{
			InitializeComponent();

            apiService = new APIService ();

            this.titleLbl.Text = $"Item Delivery App - {App.User.Key}";

            loadData();
        }

        public List<Item> Items { get; set; }

        async void loadData()
        {
            var res = await apiService.GetItemTypes();
            
            res.Insert(0, "All Item Types");

            typePicker.ItemsSource = res;

            Items = await apiService.GetItems();

            Items.ForEach(item => { item.imageSource = ImageSource.FromStream(() => new MemoryStream(item.image)); });

            itemListView.ItemsSource = Items;

            itemCountLabel.Text = $"{Items.Count} of {Items.Count} items to deliver.";
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
			Preferences.Clear();

			Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
			App.Current.MainPage.Navigation.PushAsync(new AddItemPage());
        }

        private void typePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItemType = (string)typePicker.SelectedItem;

            if (selectedItemType == "All Item Types")
            {
                itemListView.ItemsSource = Items;

                itemCountLabel.Text = $"{Items.Count} of {Items.Count} items to deliver.";
            }
            else
            {
                var items = Items.Where(x => x.type == selectedItemType).ToList();
                itemListView.ItemsSource = items;

                itemCountLabel.Text = $"{items.Count} of {Items.Count} items to deliver.";
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var item = (Item)(sender as Frame).BindingContext;

            if (item.status)
            {
                await DisplayAlert("", "You cannot update the completed item status.", "ok");
                return;
            }

            var isCompleted = await DisplayAlert("", "are you sure to update this item status?", "yes", "no");

            if (isCompleted)
            {
                var res = await apiService.UpdateItemStatus(item.id);

                if (res)
                {
                    await DisplayAlert("", "item status updated", "ok");
                    loadData();
                }
                else
                {
                    await DisplayAlert("", "item status update failed.", "ok");
                }
            }

        }
    }
}