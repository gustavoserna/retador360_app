using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Retador_360.Models;
using Xamarin.Forms;

namespace Retador_360.ViewModel
{
    public class GalleryViewModel : BaseViewModel
    {
        public ObservableCollection<Video> EventVideos { get; set; }
        public Command LoadVideosDeEventoCommand { get; set; }

        bool isBusy2 = false;

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

        public GalleryViewModel() {
            EventVideos = new ObservableCollection<Video>();

            LoadVideosDeEventoCommand = new Command(async () =>
            {
                // await ExecuteLoadVideosDeEventoCommand();
                LoadVideosHardcode();
            });

            
        }

        public void LoadVideosHardcode()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                EventVideos.Clear();
                IEnumerable<Video> cuponesDisponibles = null;

                List<Video> lista = new List<Video>();

                for ( int i = 0; i < 10; i++)
                {
                    Video video = new Video();
                    video.video_name = "video.mp4";
                    video.path_video = "/Load/Videos/Path";
                    video.duration = "0:15";
                    video.size = "3MB";
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