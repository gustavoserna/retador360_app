using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Retador_360.ViewModel;

namespace Retador_360.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gallery : ContentPage
    {
        GalleryViewModel galleryViewModel;
        public Gallery()
        {
            InitializeComponent();
            Title = "Mis videos";

            BindingContext = galleryViewModel = new GalleryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (galleryViewModel.EventVideos.Count == 0)
            {
                galleryViewModel.LoadVideosDeEventoCommand.Execute(null);
            }
        }
    }
}