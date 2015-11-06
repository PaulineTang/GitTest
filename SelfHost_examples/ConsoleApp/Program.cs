using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using WebApi.Models;

//利用HttpClient来调用以selfhost方式寄宿的Web API
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Process();
            Console.Read();
        }
        private async static void Process()
        {
            //获取当前联系人列表
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost/selfhost/tyz/api/products");
            IEnumerable<Product> products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            Console.WriteLine("当前联系人列表：");
            ListContacts(products);

            //添加新的联系人
            Product product = new Product { Name = "王五", PhoneNo = "0512-34567890", EmailAddress = "wangwu@gmail.com" };
            await httpClient.PostAsJsonAsync<Product>("http://localhost/selfhost/tyz/api/products", product);
            Console.WriteLine("添加新联系人“王五”：");
            response = await httpClient.GetAsync("http://localhost/selfhost/tyz/api/products");
            products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            ListContacts(products);

            //修改现有的某个联系人
            response = await httpClient.GetAsync("http://localhost/selfhost/tyz/api/products/001");
            product = (await response.Content.ReadAsAsync<IEnumerable<Product>>()).First();
            product.Name = "赵六";
            product.EmailAddress = "zhaoliu@gmail.com";
            await httpClient.PutAsJsonAsync<Product>("http://localhost/selfhost/tyz/api/products/001", product);
            Console.WriteLine("修改联系人“001”信息：");
            response = await httpClient.GetAsync("http://localhost/selfhost/tyz/api/products");
            products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            ListContacts(products);

            //删除现有的某个联系人
            await httpClient.DeleteAsync("http://localhost/selfhost/tyz/api/products/002");
            Console.WriteLine("删除联系人“002”：");
            response = await httpClient.GetAsync("http://localhost/selfhost/tyz/api/products");
            products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            ListContacts(products);
        }
        private static void ListContacts(IEnumerable<Product> products)
        {
            foreach (Product product in products)
            {
                Console.WriteLine("{0,-6}{1,-6}{2,-20}{3,-10}", product.Id, product.Name, product.EmailAddress, product.PhoneNo);
            }
            Console.WriteLine();
        }
    }
}
