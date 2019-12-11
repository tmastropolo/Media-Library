using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
   
    public class MovieFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public string filePath { get; set; }
        public List <Movie> Movies { get; set; }
        public MovieFile(string path)
        {
            Movies = new List<Movie>();
            filePath = path;
            
            StreamReader sr = new StreamReader(filePath);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                Movie movie = new Movie();
                string line = sr.ReadLine();
                int idx = line.IndexOf('"');
                if (idx == -1)
                {
                    String[] movieDetail = line.Split(',');
                    movie.mediaId = UInt16.Parse(movieDetail[0]);
                    movie.title = movieDetail[1];
                    movie.genres = movieDetail[2].Split('|').ToList();
                    movie.director = movieDetail[3];
                    string timestring = movieDetail[4];
                    string[] time = timestring.Split(':');
                    TimeSpan timeSpan = TimeSpan.FromHours(double.Parse(time[0]));
                    timeSpan = TimeSpan.FromMinutes(double.Parse(time[1]));
                    timeSpan = TimeSpan.FromSeconds(double.Parse(time[2]));
                    
                    movie.runningTime = timeSpan;

                }
                else
                {

                    String[] moviedetail = line.Split(',');
                    movie.mediaId = UInt16.Parse(moviedetail[0]);
                    movie.title = moviedetail[1] + ","+ moviedetail[2];
                    movie.genres = moviedetail[3].Split('|').ToList();
                    movie.director = moviedetail[4];
                    string timestring = moviedetail[5];
                    string[] time = timestring.Split(':');
                    TimeSpan timeSpan = TimeSpan.FromHours(double.Parse(time[0]));
                    timeSpan = TimeSpan.FromMinutes(double.Parse(time[1]));
                    timeSpan = TimeSpan.FromSeconds(double.Parse(time[2]));

                    movie.runningTime = timeSpan;

                    
                }
                Movies.Add(movie);
            }
            sr.Close();

        }

        public bool isUniqueTitle(string title)
        {
            if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Dup;icate movie title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddMovie(Movie movie)
        {
            movie.mediaId = Movies.Max(m => m.mediaId) + 1;
            string title = movie.title.IndexOf(',') != -1 ? $"\"{movie.title}\"" : movie.title;
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine($"{movie.mediaId}, {title}, {string.Join("|", movie.genres)}, {movie.director}, {movie.runningTime}");
            sw.Close();
            logger.Info("Movie id {Id} added", movie.mediaId);
        }


    }
}
