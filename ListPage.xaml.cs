using OlaruIrinaLab7.Models;
namespace OlaruIrinaLab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    
    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
      if(listView.SelectedItem is Product product)
        { await App.Database.DeleteProductAsync(product);

            var shopl = (ShopList)BindingContext;
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
            listView.SelectedItem = null;
        }
      else
        {
            await DisplayAlert("Atentie", "Selectati un articol din lista pentru a-l sterge.", "OK");
        }


    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }



    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem != null)
        {
            var product = listView.SelectedItem as Product;
            var shopl = (ShopList)BindingContext;
            await App.Database.DeleteListProductAsync(shopl.ID, product.ID);
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }
    }

}