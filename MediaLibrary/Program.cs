using System;
using NLog;

namespace MediaLibrary
{
    class MainClass
    {
        // create a class level instance of logger (can be used in methods other than Main)
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            string file = "../../movies.scrubbed.csv";
            string bkfile = "books.csv";
            string cdfile = "cd.csv";
            logger.Info("Program started");

            MovieFile movieFile = new MovieFile(file);
            BookFile bookFile = new BookFile(bkfile);
            AlbumFile albumFile = new AlbumFile(cdfile);
            string choice = "";
            string mchoice = "";
            string bchoice = "";
            string achoice = "";

            do
            {
                Console.WriteLine("1) Movie");
                Console.WriteLine("2) Book");
                Console.WriteLine("3) Album");
                Console.WriteLine("Enter to quit");
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);

                if (choice == "1")
                {

                    do
                    {
                        Console.WriteLine("1) Add Movie");
                        Console.WriteLine("2) Display All Movies");
                        Console.WriteLine("3) Enter to quit");
                        mchoice = Console.ReadLine();
                        logger.Info("User choice: {Choice}", mchoice);
                        if (mchoice == "1")
                        {
                            Movie movie = new Movie();
                            Console.WriteLine("Enter movie title");
                            movie.title = Console.ReadLine();
                            if (movieFile.isUniqueTitle(movie.title))
                            {
                                string input;
                                do
                                {
                                    Console.WriteLine("Enter Genre (or done to quit)");
                                    input = Console.ReadLine();
                                    if (input != "done" && input.Length > 0)
                                    {
                                        movie.genres.Add(input);

                                    }

                                } while (input != "done");
                                if (movie.genres.Count == 0)
                                {
                                    movie.genres.Add("(no genres listed)");
                                }
                                Console.WriteLine("Enter movie Director");
                                movie.director = Console.ReadLine();
                                Console.WriteLine("Enter running time");
                                movie.runningTime = TimeSpan.Parse(Console.ReadLine());
                                movieFile.AddMovie(movie);
                            }
                            else
                            {
                                Console.WriteLine("Movie title already exists\n");
                            }
                        }
                        else if (mchoice == "2")
                        {
                            foreach (Movie m in movieFile.Movies)
                            {
                                Console.WriteLine(m.Display());
                            }
                        }
                    } while (mchoice == "1" || mchoice == "2");
                }
                else if (choice == "2")
                {
                    Console.WriteLine("1) Add Book");
                    Console.WriteLine("2) Display All Books");
                    Console.WriteLine("3) Enter to quit");
                    bchoice = Console.ReadLine();
                    logger.Info("User choice: {Choice}", bchoice);
                    do
                    {


                        if (mchoice == "1")
                        {
                            Book book = new Book();
                            Console.WriteLine("Enter book title");
                            book.title = Console.ReadLine();
                            if (bookFile.isUniqueTitle(book.title))
                            {

                                string input;
                                do
                                {
                                    Console.WriteLine("Enter Genre (or done to quit");
                                    input = Console.ReadLine();
                                    if (input != "done" && input.Length > 0)
                                    {
                                        book.genres.Add(input);

                                    }

                                } while (input != "done");

                                if (book.genres.Count == 0)
                                {
                                    book.genres.Add("(no genres listed)");
                                }
                                Console.WriteLine("Enter book author");
                                book.author = Console.ReadLine();
                                Console.WriteLine("Enter book publisher");
                                book.publisher = Console.ReadLine();
                                Console.WriteLine("Enter the amount of pages");
                                book.pageCount = UInt16.Parse(Console.ReadLine());
                                bookFile.AddBook(book);

                            }
                            else
                            {
                                Console.WriteLine("Book title already exists\n");
                            }
                        }
                        else if (bchoice == "2")
                        {
                            foreach (Book b in bookFile.Books)
                            {
                                Console.WriteLine(b.Display());
                            }
                        }
                    } while (bchoice == "1" || bchoice == "2");

                }
                else if (choice == "3")
                {
                    Console.WriteLine("1) Add Album");
                    Console.WriteLine("2) Display All Albums");
                    Console.WriteLine("3) Enter to quit");
                    bchoice = Console.ReadLine();
                    logger.Info("User choice: {Choice}", bchoice);
                    do
                    {


                        if (achoice == "1")
                        {
                            Album album = new Album();
                            Console.WriteLine("Enter Album title");
                            album.title = Console.ReadLine();
                            if (albumFile.isUniqueTitle(album.title))
                            {

                                string input;
                                do
                                {
                                    Console.WriteLine("Enter Genre (or done to quit");
                                    input = Console.ReadLine();
                                    if (input != "done" && input.Length > 0)
                                    {
                                        album.genres.Add(input);

                                    }

                                } while (input != "done");

                                if (album.genres.Count == 0)
                                {
                                    album.genres.Add("(no genres listed)");
                                }
                                Console.WriteLine("Enter album artist");
                                album.artist = Console.ReadLine();
                                Console.WriteLine("Enter record label");
                                album.recordLabel = Console.ReadLine();
                                albumFile.AddAlbum(album);

                            }
                            else
                            {
                                Console.WriteLine("Album title already exists\n");
                            }
                        }
                        else if (achoice == "2")
                        {
                            foreach (Album a in albumFile.Albums)
                            {
                                Console.WriteLine(a.Display());
                            }
                        }
                    } while (achoice == "1" || achoice == "2");

                }



            } while ((choice == "1" || choice == "2" || choice == "3"));
            logger.Info("Program Ended");
        }



    }
}

