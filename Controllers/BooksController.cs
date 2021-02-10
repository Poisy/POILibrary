using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using POILibrary.Models.BookViewModels;
using POILibrary.Services;
using POILibrary.Filters;
using POILibrary.Models;
using POILibrary.Classes.Enums;

namespace ASPMVC.Controllers
{
    
    [Authorize]
    public class BooksController : Controller
    {
        private LibraryService _service;

        private UserManager<ApplicationUser> _userManager;

        public BooksController(LibraryService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;   
            _userManager = userManager;  
        }

        [LogResourceFilter]
        public IActionResult ShowBooks()
        {
            var books = _service.GetBooksByUser(GetUserId().Result);
            var bookViewModelList = books.Select(b => new BookViewModel(b)).ToList();
            var booksViewModel = new BooksViewModel { 
                Books = bookViewModelList,
                SortMethod = BookSortMethod.ByID
            };
            
            return View("../Books/BooksView", booksViewModel);
        }

        public IActionResult ShowSortedBooks(int sort, string search)
        {
            BookSortMethod method = (BookSortMethod)sort;

            var books = _service.GetBooksByUser(GetUserId().Result);

            if (!String.IsNullOrWhiteSpace(search))
            {
                books.RemoveAll(b => !b.Title.Contains(search));
            }

            var bookViewModelList = books.Select(b => new BookViewModel(b)).ToList();
            var booksViewModel = new BooksViewModel { 
                Books = bookViewModelList,
                SortMethod = method
            };

            BookServie.SortBooksFromBooksViewModel(booksViewModel, method);

            return View("../Books/BooksView", booksViewModel);
        }

        [HttpPost]
        public IActionResult AddBook(BookViewModel book)
        {
            if (_service.GetBooksByUser(GetUserId().Result).Contains(book))
            {
                ModelState.AddModelError(nameof(book.Title), "This book is already added!");
            }
            else if (ModelState.IsValid)
            {
                if (book.State != BookState.FINISHED && book.YearReaded != default)
                {
                    ModelState.AddModelError(nameof(book.YearReaded), "Can't fill 'Year Readed' when the book is not finished!");
                }
                else if (book.State == BookState.FINISHED && book.YearPublished > book.YearReaded)
                {
                    ModelState.AddModelError(nameof(book.YearReaded), "You can't read a book until is not published. Not here!");
                }
                else
                {
                    book.ID_User = GetUserId().Result;

                    _service.AddBook(book);

                    // Clears the form when a book is added
                    ModelState.Clear();

                    TempData["success"] = "true";
                    TempData["title"] = book.Title;
                }
            }
            
            return View("../Books/AddBookView", new BookViewModel());
        }

        public IActionResult AddBook()
        {
            return View("../Books/AddBookView", new BookViewModel());
        }

        // Used in BooksView
        public IActionResult EditBook(int id)
        {
            var b = _service.GetBook(id);

            if (b != null && b.ID_User == GetUserId().Result)
            {
                BookViewModel resultBook = new BookViewModel(b);

                return View("../Books/EditBookView", resultBook);
            }

            return ShowBooks();
        }

        // Used in EditBookView
        public IActionResult UpdateBook(BookViewModel book)
        {
            int id_user = GetUserId().Result;

            if (_service.GetBooksByUser(id_user).Contains(book))
            {
                ModelState.AddModelError(nameof(book.Title), "This book is already added!");
            }
            else if (book.State != BookState.FINISHED && book.YearReaded != default)
            {
                ModelState.AddModelError(nameof(book.YearReaded), "Can't fill 'Year Readed' when the book is not finished!");
            }
            else if (book.State == BookState.FINISHED && book.YearPublished > book.YearReaded)
            {
                ModelState.AddModelError(nameof(book.YearReaded), "You can't read a book until is not published. Not here!");
            }
            else if (_service.GetBook(book.ID) == null)
            {
                // User tries to edit a book which does not exist
            }
            else if (_service.GetBook(book.ID).ID_User != id_user)
            {
                // User tries to edit a book which he didn't own
            }
            else
            {
                book.ID_User = id_user;

                _service.UpdateBook(book);

                TempData["success"] = "true";
            }

            return View("../Books/EditBookView", book);
        }

        public IActionResult DeleteBook(int id)
        {
            if (_service.GetBook(id) != null)
            {
                if (_service.GetBook(id).ID_User == GetUserId().Result)
                {
                    _service.DeleteBook(id);
                }
            }
            
            return ShowBooks();
        }

        [HttpGet]
        public async Task<int> GetUserId()
        {
            ApplicationUser usr = await _userManager.GetUserAsync(HttpContext.User);
            return Convert.ToInt32(usr.Id);
        }
    }
}