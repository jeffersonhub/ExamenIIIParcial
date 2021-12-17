using ExamenIIIParcial.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenIIIParcial
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListEmployees());
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
