using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace POILibrary.Models.BookViewModels
{
    public class BookViewModel : Book
    {
        public BookViewModel() {}

        public BookViewModel(Book book)
        {
            base.Author = book.Author;
            base.ID = book.ID;
            base.Pages = book.Pages;
            base.State = book.State;
            base.Title = book.Title;
            base.YearPublished = book.YearPublished;
            base.YearReaded = book.YearReaded;
        }

        public string StateToString { 
            get 
            {
                switch (State)
                {
                    case BookState.PLAN_TO_READ: { return "Plan to Read"; }
                    case BookState.READING: { return "Reading"; }
                    case BookState.FINISHED: { return "Finished"; }
                    default: { return "Unknown"; }
                }
            }
            set 
            {  
                switch (value) 
                {
                    case "Plan to Read": { State = BookState.PLAN_TO_READ; break; }
                    case "Reading": { State = BookState.READING; break; }
                    case "Finished": { State = BookState.FINISHED; break; }
                }
            }   
        }
        public IEnumerable<SelectListItem> States { get; } = new List<SelectListItem>
        {
            new SelectListItem{Value= "Plan To Read", Text="Plan To Read"},
            new SelectListItem{Value= "Reading", Text= "Reading"},
            new SelectListItem{Value= "Finished", Text="Finished"},
        };
    }
}