using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using POILibrary.Models.BookViewModels;

namespace POILibrary.Filters
{
    public class LogResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == 
            "Development")
            {
                var result = context.Result as ViewResult;
                var books = result.Model as BooksViewModel;
                if (books != null)
                {
                    books.Books.ForEach(book => {
                        Console.WriteLine($"|{book.Title}| - |{book.Pages}| - |{(new BookViewModel(book)).StateToString}|");
                    });
                }
            }
        }


        public void OnResourceExecuting(ResourceExecutingContext context) { }
    }
}