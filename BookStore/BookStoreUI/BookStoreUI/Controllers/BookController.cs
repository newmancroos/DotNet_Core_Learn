﻿using BookStoreUI.Models;
using BookStoreUI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreUI.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ViewResult GetAllBooks()
        {
            var data = _bookRepository.GetAllBooks();
            return View(data);
        }

        public ViewResult GetBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return View(book);
        }

        //public BookModel GetBook(int id)
        //{
        //    return _bookRepository.GetBookById(id);
        //}
    }
}
