using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Retador_360.Vistas;

namespace Retador_360
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new Captura());
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
