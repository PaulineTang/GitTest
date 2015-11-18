using BookShop.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webUI_mvc.Controllers
{
    [HandleError]
    public class BookController : Controller
    {
        private IBookRepository repository;

        public BookController(IBookRepository bookRepository)
        {
            repository = bookRepository;
        }

        public ActionResult List()//ActionResult是一个抽象类
        {
            return View(repository.Books);//里氏替换原则，任何使用府内的地方，都可以使用基类进行替换
        }

    }
}