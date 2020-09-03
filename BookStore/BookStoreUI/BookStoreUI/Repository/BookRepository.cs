using BookStoreUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreUI.Repository
{
    public class BookRepository : IBookRepository
    {
        List<BookModel> Books;
        public BookRepository()
        {
            Books = new List<BookModel> {
                new BookModel{ Id =1, Title = "C# Design Patters", Author="Avinash Thakur", Description = "All design patterns in C#", Category = "Computer Languages"},
                new BookModel{ Id =2, Title = "Clean Architecture", Author="Robert C. Martin", Description = "All about Application architecture", Category = "Information Technology"},
                new BookModel{ Id =3, Title = "Learning React", Author="Chinnathambi", Description = "Very basic concept and sample about React js", Category = "Information Technology"},
                new BookModel{ Id =4, Title = "Microsft Azure Fundamentals", Author="Jim Cheshire", Description = "Preparation for Microsoft Az900 certification exam", Category = "Information Technology"},
                new BookModel{ Id =5, Title = "Java", Author="Webgentle", Description = "This is the description for Java book", Category = "Information Technology"},
                new BookModel{ Id =6, Title = "Azure DevOps", Author="Nithish", Description = "This is the description for Azure DevOps book", Category = "Information Technology"}

            };
        }
        //public async Task<int> AddNewBook(BookModel model)
        //{
        //    var newBook = new Books()
        //    {
        //        Author = model.Author,
        //        CreatedOn = DateTime.UtcNow,
        //        Description = model.Description,
        //        Title = model.Title,
        //        LanguageId = model.LanguageId,
        //        TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
        //        UpdatedOn = DateTime.UtcNow,
        //        CoverImageUrl = model.CoverImageUrl,
        //        BookPdfUrl = model.BookPdfUrl
        //    };

        //    newBook.bookGallery = new List<BookGallery>();

        //    foreach (var file in model.Gallery)
        //    {
        //        newBook.bookGallery.Add(new BookGallery()
        //        {
        //            Name = file.Name,
        //            URL = file.URL
        //        });
        //    }

        //    await _context.Books.AddAsync(newBook);
        //    await _context.SaveChangesAsync();

        //    return newBook.Id;

        //}

        public List<BookModel> GetAllBooks()
        {
            return Books
                  .Select(book => new BookModel()
                  {
                      Author = book.Author,
                      Category = book.Category,
                      Description = book.Description,
                      Id = book.Id,
                      Title = book.Title,
                  }).ToList();
        }

        public List<BookModel> GetTopBooks(int count)
        {
            return Books
                  .Select(book => new BookModel()
                  {
                      Author = book.Author,
                      Category = book.Category,
                      Description = book.Description,
                      Id = book.Id,
                      Title = book.Title,
                  }).Take(count).ToList();
        }

        public BookModel GetBookById(int id)
        {
            return Books.Where(x => x.Id == id)
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     Id = book.Id,
                     Title = book.Title,
                 }).FirstOrDefault();
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
        }
    }

    public interface IBookRepository
    {
        List<BookModel> GetAllBooks();
        BookModel GetBookById(int id);
        List<BookModel> GetTopBooks(int count);
        List<BookModel> SearchBook(string title, string authorName);
    }
}
