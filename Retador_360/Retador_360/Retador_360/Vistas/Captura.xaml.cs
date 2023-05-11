using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Media.Abstractions;
using Retador_360.Vistas;

namespace Retador_360.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Captura : ContentPage
    {
        public Captura()
        {
            InitializeComponent();


            
            takeVideo.Clicked += async (sender, args) =>
            {
                
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                try
                {
                    var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
                    {
                        Name = "video.mp4",
                        Directory = "Sample",
                        SaveToAlbum = true
                    });

                    if (file == null)
                        return;
                    await DisplayAlert("Video Recorded", "Location: " + (file.AlbumPath), "OK");

                    file.Dispose();


                    //Subir archivo al server
                }
                catch //(Exception ex)
                {
                    // Xamarin.Insights.Report(ex);
                    await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured it in Xamarin Insights! Thanks.", "OK");
                }
            };
            goGallery.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new Gallery());
            };
        }
    }
}