using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using three.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using three.API;
using three.View;

namespace three.ViewModels
{
    class ProductsListViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

#pragma warning disable IDE1006 // Naming Styles
        public ObservableCollection<Products> products
#pragma warning restore IDE1006 // Naming Styles
        {
            get
            {
                return myproducts;
            }
            set
            {
                myproducts = value;
                var args = new PropertyChangedEventArgs(nameof(products));
                PropertyChanged?.Invoke(this, args);
            }
        }

        ObservableCollection<Products> myproducts;
        public Command SelectCommand { get; }
        public Command ClosCommand { get; }
        public Command AddCommand { get; }
        public Command viewCommand { get; }
        public Command DeleteCommand { get; }
        public Products selectProduct { get; set; }


        ApiService apiService;
        public ProductsListViewModels()
        {
            products = new ObservableCollection<Products>();
            apiService = new ApiService();

            GetProduct();



            SelectCommand = new Command(async () =>
            {
                var sendVar = new { selectProduct = selectProduct, ClosCommand = ClosCommand, DeleteCommand = DeleteCommand };
                var ProdecutDetail = new View.ProductDetail
                {
                    BindingContext = sendVar 
                };
                await Application.Current.MainPage.Navigation.PushModalAsync(ProdecutDetail);
            });

          

            ClosCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();

            });



            viewCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new View.addimg() );
            });




            DeleteCommand = new Command(async () =>

            {
                if (selectProduct != null)
                {
                    // ทำการเรียกใช้เมธอดที่เราสร้างไว้เพื่อลบ
                    bool isDeleted = await apiService.DeleteProducts(selectProduct.img_id);



                    if (isDeleted)
                    {
                        await Application.Current.MainPage.DisplayAlert("IMG", "img Deleted", "OK");
                        Application.Current.MainPage = new TabbedPageProduct();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("IMG", "Error  Deleted Try Again", "OK");
                    }
                }
            });



        }
        async void GetProduct()
        {
            products = await apiService.GetProducts();
            Console.WriteLine(products);
        }

    }
}

