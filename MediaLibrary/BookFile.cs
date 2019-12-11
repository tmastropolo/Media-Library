using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    class BookFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public string filePath { get; set; }
        public List<Book> Books { get; set; }

        public BookFile(String path)
        {
            Books = new List<Book>();
            filePath = path;

            StreamReader sr = new StreamReader(filePath);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                Book book = new Book();
                string line = sr.ReadLine();
                int idx = line.IndexOf('"');
                if (idx == -1)
                {
                    String[] bookDetail = line.Split(',');
                    book.mediaId = UInt16.Parse(bookDetail[0]);
                    book.title = bookDetail[1];
                    book.genres = bookDetail[2].Split('|').ToList();
                    book.author = bookDetail[3];
                    book.publisher = bookDetail[4];
                    book.pageCount = UInt16.Parse(bookDetail[5]);

                }
                else
                {
                    book.mediaId = UInt16.Parse(line.Substring(0, idx - 1));
                    line = line.Substring(idx + 1);
                    idx = line.IndexOf('"');
                    book.title = line.Substring(0, idx);
                    line = line.Substring(idx + 2);
                    book.genres = line.Split('|').ToList();
                    book.author = line.Substring(idx + 3);
                    book.publisher = line.Substring(idx + 4);
                    book.pageCount = UInt16.Parse(line.Substring(idx + 5));

                }
                Books.Add(book);            
            }
            sr.Close();
        }

        public bool isUniqueTitle(string title)
            {
                if (Books.ConvertAll( m => m.title.ToLower()).Contains(title.ToLower()))
                {
                    logger.Info("Dup;icate movie title {Title}", title);
                    return false;
                }
                return true;
            }
        public void AddBook(Book book)
            {
                book.mediaId = Books.Max(m => m.mediaId) +1;
                string title = book.title.IndexOf(',') != -1 ? $"\"{book.title}\"" : book.title;
                StreamWriter sw = new StreamWriter (filePath, true);
                sw.WriteLine($"{book.mediaId}, {title}, {string.Join("|", book.genres)}, {book.author}, {book.publisher}, {book.pageCount}");
                sw.Close();
                logger.Info("Book id {Id} added", book.mediaId);
            }
    }
}
    