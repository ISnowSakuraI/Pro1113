using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace three
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new View.PageLogin());
            MainPage = new View.TabbedPageProduct();
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
