using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Product
    {
        public string Id { get; set; }//Model必须提供public的属性，用于json或xml反序列化时的赋值
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
    }
}