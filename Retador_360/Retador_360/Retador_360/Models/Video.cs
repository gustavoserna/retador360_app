using System;
using System.Collections.Generic;
using System.Text;

namespace Retador_360.Models
{
    public class Video
    {
        string _video_name = string.Empty;

        public string video_name
        {
            get { return _video_name; }
            set { _video_name = value; }
        }

        public string path_video { get; set; }
        public string duration { get; set; }
        public string size { get; set; }
        public string format { get; set; }
        public string date_created { get; set; }
    }
}
