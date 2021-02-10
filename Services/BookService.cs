using POILibrary.Models.BookViewModels;
using POILibrary.Classes.Enums;

namespace POILibrary.Services
{
    public static class BookServie
    {
        public static void SortBooksFromBooksViewModel(BooksViewModel model, BookSortMethod m)
        {
            switch (m)
            {
                case BookSortMethod.ByID: 
                    model.Books.Sort((x, y) => x.ID.CompareTo(y.ID)); 
                    break;
                case BookSortMethod.ByTitle: 
                    model.Books.Sort((x, y) => x.Title.CompareTo(y.Title)); 
                    break;
                case BookSortMethod.ByAuthor: 
                    model.Books.Sort((x, y) => x.Author.CompareTo(y.Author)); 
                    break;
                case BookSortMethod.ByPages: 
                    model.Books.Sort((x, y) => x.Pages.CompareTo(y.Pages)); 
                    break;
                case BookSortMethod.ByYearReaded: 
                    model.Books.Sort((x, y) => x.YearReaded.CompareTo(y.YearReaded)); 
                    break;
                case BookSortMethod.ByYearRelease: 
                    model.Books.Sort((x, y) => x.YearPublished.CompareTo(y.YearPublished)); 
                    break; 
                case BookSortMethod.ByState: 
                    model.Books.Sort((x, y) => x.State.CompareTo(y.State)); 
                    break;
            }
        }
    }
}