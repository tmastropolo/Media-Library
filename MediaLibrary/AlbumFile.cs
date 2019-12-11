using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    class AlbumFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public string filePath { get; set; }
        public List<Album> Albums{ get; set; }

        public AlbumFile(string path)
        {
            Albums = new List<Album>();
            filePath = path;
            StreamReader sr = new StreamReader(filePath);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                Album album = new Album();
                string line = sr.ReadLine();
                int idx = line.IndexOf('"');
                if (idx == -1)
                {
                    String[] albumDetail = line.Split(',');
                    album.mediaId = UInt16.Parse(albumDetail[0]);
                    album.title = albumDetail[1];
                    album.genres = albumDetail[2].Split('|').ToList();
                    album.artist = albumDetail[3];
                    album.recordLabel = albumDetail[4];
                }
                else
                {
                    album.mediaId = UInt16.Parse(line.Substring(0, idx - 1));
                    line = line.Substring(idx + 1);
                    idx = line.IndexOf('"');
                    album.title = line.Substring(0, idx);
                    line = line.Substring(idx + 2);
                    album.genres = line.Split('|').ToList();
                    album.artist = line.Substring(idx + 3);
                    album.recordLabel = line.Substring(idx + 4);
                }
                Albums.Add(album);
            }
            sr.Close();
        }

        public bool isUniqueTitle (string title)
        {
            if (Albums.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Dup;icate movie title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddAlbum(Album album)
        {
            album.mediaId = Albums.Max(m => m.mediaId) + 1;
            string title = album.title.IndexOf(',') != -1 ? $"\"{album.title}\"" : album.title;
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine($"{album.mediaId}, {title}, {string.Join("|", album.genres)}, {album.artist}, {album.recordLabel}");
            sw.Close();
            logger.Info("Movie id {Id} added", album.mediaId);
        }

    }
}
