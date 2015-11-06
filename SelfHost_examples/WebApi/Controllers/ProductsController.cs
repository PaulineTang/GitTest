using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;//该名称空间与自托管有关
using System.Threading;
using System.Web.Http;//ApiController在名称空间System.Web.Http中定义
using WebApi.Models;

namespace WebApi.Models
{
    //MVC WebAPI中的Controllers和普通MVC的Controllers类似，不过不再继承于Controller，而改为继承API的ApiController
    public class ProductsController : ApiController
    {
        static List<Product> products;
        static int counter = 2;
        static ProductsController()
        {
            products = new List<Product>();
            products.Add(new Product { Id = "001", Name = "张三", PhoneNo = "0512-12345678", EmailAddress = "zhangsan@gmail.com", Address = "江苏省苏州市星湖街328号" });
            products.Add(new Product { Id = "002", Name = "李四", PhoneNo = "0512-23456789", EmailAddress = "lisi@gmail.com", Address = "江苏省苏州市金鸡湖大道328号" });
        }
        public IEnumerable<Product> Get(string id = null)
        {
            return from product in products
                   where product.Id == id || string.IsNullOrEmpty(id)
                   select product;
        }
        public void Post(Product product)
        {
            Interlocked.Increment(ref counter);
            product.Id = counter.ToString("D3");
            products.Add(product);
        }

        public void Put(Product product)
        {
            products.Remove(products.First(c => c.Id == product.Id));
            products.Add(product);
        }

        public void Delete(string id)
        {
            products.Remove(products.First(c => c.Id == id));
        }
    }
}
