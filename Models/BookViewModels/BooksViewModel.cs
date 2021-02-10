using System.Collections.Generic;
using POILibrary.Classes.Enums;

namespace POILibrary.Models.BookViewModels
{
    public class BooksViewModel
    {
        public List<BookViewModel> Books { get; set; } 

        public BookSortMethod SortMethod { get; set; }
    }
}