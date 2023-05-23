using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Retador_360.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Net.Http;
using Plugin.Media.Abstractions;

namespace Retador_360.ViewModel
{
    public class CapturaViewModel : BaseViewModel
    {
        public ObservableCollection<Video> EventVideos { get; set; }
        public Command LoadVideosDeEventoCommand { get; set; }

        bool isBusy2 = false;
        Stream videoToUpload;
        string filePath;
        MediaFile file;
        public bool IsBusy2
        {
            get { return isBusy2; }
            set { SetProperty(ref isBusy2, value); }
        }

        bool _NotFound_Visibility_VideosEvento = false;
        public bool NotFound_Visibility_VideosEvento
        {
            get { return _NotFound_Visibility_VideosEvento; }
            set { SetProperty(ref _NotFound_Visibility_VideosEvento, value); }
        }

        public CapturaViewModel(Stream video, string fp)
        {
            EventVideos = new ObservableCollection<Video>();
            videoToUpload = video;
            filePath = fp;
            LoadVideosDeEventoCommand = new Command(async () =>
            {
                await ExecuteLoadCuponesDisponiblesCommand();
                LoadVideosHardcode();
            });
        }

        public CapturaViewModel(MediaFile file)
        {
            EventVideos = new ObservableCollection<Video>();
            this.file = file;
            LoadVideosDeEventoCommand = new Command(async () =>
            {
                await ExecuteLoadCuponesDisponiblesCommand();
                LoadVideosHardcode();
            });
        }

        async Task ExecuteLoadCuponesDisponiblesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var content = new MultipartFormDataContent
                {
                    {
                        new StreamContent(file.GetStream()),
                        "\"file\"",
                        $"\"{file.Path}\""
                    }
                };

                var httpClient = new HttpClient();

                var uploadServicBaseAdress = "http://127.0.0.1:5000/uploadVideo";

                var httpResponseMessage = await httpClient.PostAsync(uploadServicBaseAdress, content);
                string res = await httpResponseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(res);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void LoadVideosHardcode()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                EventVideos.Clear();
                IEnumerable<Video> cuponesDisponibles = null;

                List<Video> lista = new List<Video>();
                // Documents folder
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                //documentsPath += "/Movies/Sample/";
                var folders = Directory.GetDirectories(documentsPath);
                if (Directory.Exists(documentsPath))
                {
                    Console.WriteLine("Folder exist!!!: \t " + documentsPath);
                    if (folders.Any())
                    {
                        Console.WriteLine("Folders on array exist!!!: \t ");
                        foreach (var folder in folders)
                        {
                            Console.WriteLine("Folder: \t " + folder);

                        }
                    }
                    //await DisplayAlert("Video Recorded", "Location: " + documentsPath, "OK");
                }
                var cacheDir = FileSystem.CacheDirectory;
                var mainDir = FileSystem.AppDataDirectory;
                //Directory.Exists
                for (int i = 0; i < 10; i++)
                {
                    Video video = new Video();
                    video.video_name = "video.mp4";
                    video.path_video = mainDir; // "/Load/Videos/Path";
                    video.duration = cacheDir;
                    video.size = documentsPath;
                    video.format = "MP4";
                    video.date_created = "May-10-2023";
                    lista.Add(video);
                }

                cuponesDisponibles = lista;

                foreach (var item in cuponesDisponibles) EventVideos.Add(item);

                if (EventVideos.Count == 0) NotFound_Visibility_VideosEvento = true; else NotFound_Visibility_VideosEvento = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }


        }

    }
}