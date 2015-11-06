using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_tyz.Models
{
    public class Product
    {
        public int Id { get; set; }//Model必须提供public的属性，用于json或xml反序列化时的赋值
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}