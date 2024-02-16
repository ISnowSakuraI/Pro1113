using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using three.API;
using three.Models;
using Xamarin.Forms;

namespace three.ViewModels
{
    internal class addviewmodels : INotifyPropertyChanged
    {
        ApiService apiService;

        public event PropertyChangedEventHandler PropertyChanged;
        public Command AddCommand { get; }

        public int img_id;

        public string img_name;

        public string img_description;

        public string img_category;

        public string UserName;

        public string img_url;

        public string ImgName
        {
            get => img_name;

            set
            {
                img_name = value;
                var arg = new PropertyChangedEventArgs(nameof(ImgName));
                PropertyChanged?.Invoke(this, arg);
            }
        }
        public string Description
        {
            get => img_description;

            set
            {
                img_description = value;
                var arg = new PropertyChangedEventArgs(nameof(Description));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        public string Category
        {
            get => img_category;

            set
            {
                img_category = value;
                var arg = new PropertyChangedEventArgs(nameof(Category));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        public string User
        {
            get => UserName;

            set
            {
                UserName = value;
                var arg = new PropertyChangedEventArgs(nameof(User));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        public string Img
        {
            get => img_url;

            set
            {
                img_url = value;
                var arg = new PropertyChangedEventArgs(nameof(Img));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        public addviewmodels()
        {
            apiService = new ApiService();

            AddCommand = new Command(async () =>
            {
                Products products = new Products();

                products.img_name = ImgName;
                products.img_description = Description;
                products.img_category = Category;
                products.UserName = User;
                products.img_url = Img;

                var response = await apiService.AddProducts(products);
                if (response)
                {
                    await Application.Current.MainPage.DisplayAlert("Products", "products added!!", "OK");
                    Application.Current.MainPage = new View.TabbedPageProduct();
                }
            });
        }

    }
}

