using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using three.ViewModels;
using three.Models;
using three.View;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace three.ViewModels
{
    class PageLoginViewModels : INotifyPropertyChanged
    {
        public LoginModels loginModels { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;



        public string result;
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }


        public string Result
        {
            get => result;

            set
            {
                result = value;
                var arg = new PropertyChangedEventArgs(nameof(Result));
                PropertyChanged?.Invoke(this, arg);
            }
        }

        public PageLoginViewModels()
        {
            loginModels = new LoginModels();

            LoginCommand = new Command(async () =>
            {


                if (loginModels.Email == "fluke@gmail.com" && loginModels.Password == "12345")
                {
                    Result = "Success";
                    await Application.Current.MainPage.Navigation.PushAsync(new View.TabbedPageProduct());
                }
                else
                {
                    Result = "Fail";
                }

            });

            RegisterCommand = new Command(async () =>
            {

                await Application.Current.MainPage.Navigation.PushAsync(new View.PageRegister());
            });
        }
    }
}
