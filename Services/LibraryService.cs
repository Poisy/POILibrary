using System.Collections.Generic;
using System.Linq;
using POILibrary.Models;
using POILibrary.Data;
using POILibrary.Classes.Enums;

namespace POILibrary.Services
{
    public class LibraryService
    {
        readonly ApplicationDbContext _database;

        public LibraryService(ApplicationDbContext db)
        {
            _database = db;
        }

        public int AddBook(Book b)
        {
            var book = CopyBook(b);

            _database.Add(book);
            _database.SaveChanges();

            return book.ID;
        }

        public List<Book> GetAllBooks()
        {
            return _database.Books.Select(b => CopyBook(b)).ToList();
        }

        public List<Book> GetBooksByUser(int id_user)
        {
            return GetAllBooks().Where(b => b.ID_User == id_user).ToList();
        }
    
        public Book GetBook(string title)
        {
            return _database.Books
                .Where(b => b.Title == title)
                .Select(b => CopyBook(b))
                .SingleOrDefault();
        }

        public Book GetBook(int id)
        {
            return _database.Books
                .Where(b => b.ID == id)
                .Select(b => CopyBook(b))
                .SingleOrDefault();
        }
        
        public bool UpdateBook(Book b)
        {
            Book book = _database.Books.Find(b.ID);

            if (book == null) return false;

            book.ID_User = b.ID_User;
            book.Title = b.Title;
            book.Pages = b.Pages;
            book.State = b.State;
            book.Author = b.Author;
            book.YearPublished = b.YearPublished;
            book.YearReaded = b.YearReaded;

            _database.SaveChanges();

            return true;
        }        

        public bool DeleteBook(int id)
        {
            Book book = _database.Books.Find(id);

            if (book == null) return false;

            _database.Remove(book);
            _database.SaveChanges();

            return true;
        }

        public static Book CopyBook(Book b)
        {
            return new Book {
                ID = b.ID,
                ID_User = b.ID_User,
                Title = b.Title, 
                Pages = b.Pages, 
                State = b.State,
                Author = b.Author, 
                YearPublished = b.YearPublished,
                YearReaded = b.YearReaded 
            };
        }

        public List<Book> SortBooks(List<Book> books, BookSortMethod m)
        {
            switch (m)
            {
                case BookSortMethod.ByID: 
                    books.Sort((x, y) => x.ID.CompareTo(y.ID)); 
                    break;
                case BookSortMethod.ByTitle: 
                    books.Sort((x, y) => x.Title.CompareTo(y.Title)); 
                    break;
                case BookSortMethod.ByAuthor: 
                    books.Sort((x, y) => x.Author.CompareTo(y.Author)); 
                    break;
                case BookSortMethod.ByPages: 
                    books.Sort((x, y) => x.Pages.CompareTo(y.Pages)); 
                    break;
                case BookSortMethod.ByYearReaded: 
                    books.Sort((x, y) => x.YearReaded.CompareTo(y.YearReaded)); 
                    break;
                case BookSortMethod.ByYearRelease: 
                    books.Sort((x, y) => x.YearPublished.CompareTo(y.YearPublished)); 
                    break; 
                case BookSortMethod.ByState: 
                    books.Sort((x, y) => x.State.CompareTo(y.State)); 
                    break;
            }

            return books;
        }
    }
}