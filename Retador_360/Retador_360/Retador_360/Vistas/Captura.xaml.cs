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
using System.Net.Http;
using Retador_360.ViewModel;

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
                    string result = null;
                    result = await DisplayPromptAsync("Video will be recorder", "Video Name");
                    if (result == null) { return; }
                    var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
                    {
                        Name = result+".mp4",
                        Directory = "Sample",
                        //SaveToAlbum = true
                        SaveToAlbum = false
                    });

                    if (file == null)
                        return;
                    await DisplayAlert("Video Recorded", "Location: " + (file.AlbumPath), "OK");

                    //Subir archivo al server
                    /*string path1 = file.Path.Replace(result + ".mp4", "");
                    Console.WriteLine(path1);
                    var files = Directory.GetFiles(path1);
                    if (files.Any()) { 
                        Console.WriteLine("files exists!!!");
                        foreach (var file1 in files) {
                            Console.WriteLine(file1);
                            
                        }
                    }*/

                    string fileName = file.Path;
                    byte[] videoAsBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.GetStream().CopyTo(memoryStream);
                        file.Dispose();
                        videoAsBytes = memoryStream.ToArray();
                        if (videoAsBytes != null) {
                            Console.WriteLine("Video Converted to Byte");
                        }
                    }
                    CapturaViewModel cvw = new CapturaViewModel(file);
                    file.Dispose();
                
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
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