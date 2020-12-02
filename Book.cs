using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Library
{
    public class Book
    {
        private string name;
        private string author;
        private ImageSource cover;
        private int pages;
        private int yearOfPublication;
        private string genre;
        private string description;
        private DateTime start;
        private DateTime finish;
        
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        public string Author
        {
            set { author = value; }
            get { return author; }
        }
        public ImageSource Cover
        {
            get { return cover; }
            set { cover = value; }
        }
        public int Pages
        {
            get { return pages; }
            set
            {
                if (value < 1) pages = 1;
                else pages = value;
            }
        }
        public int YearOfPublication
        {
            set { yearOfPublication = value; }
            get { return yearOfPublication; }
        }
        public string Genre
        {
            set { genre = value; }
            get { return genre; }
        }
        public string Description
        {
            set { description = value; }
            get { return description; }
        }
        public DateTime Start
        {
            set { start = value; }
            get { return start; }
        }
        public DateTime Finish
        {
            set { finish = value; }
            get { return finish; }
        }

        public Book(string name, string author, ImageSource cover, int pages, int yearOfPublication, string genre, string description)
        {
            Name = name;
            Author = author;
            Cover = cover;
            Pages = pages;
            YearOfPublication = yearOfPublication;
            Genre = genre;
            Description = description;
        }

        public Book(string name, string author, ImageSource cover, int pages, int yearOfPublication, string genre, string description, DateTime start)
        {
            Name = name;
            Author = author;
            Cover = cover;
            Pages = pages;
            YearOfPublication = yearOfPublication;
            Genre = genre;
            Description = description;
            Start = start;
        }

        public Book(string name, string author, ImageSource cover, int pages, int yearOfPublication, string genre, string description, DateTime start, DateTime finish)
        {
            Name = name;
            Author = author;
            Cover = cover;
            Pages = pages;
            YearOfPublication = yearOfPublication;
            Genre = genre;
            Description = description;
            Start = start;
            Finish = finish;
        }

    }
}
