using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_IoC_NinjectContainer
{

    public class Product
    {
        public string Name;
        public decimal Price;
        public int Num;

    }

    public class ShoppingCart
    {
        IValueCalculator calculator;

        //构造函数，参数为实现了IValueCalculator接口的类的实例
        public ShoppingCart(IValueCalculator calcParam)
        {
            calculator = calcParam;
        }

        //计算购物车内商品总价钱
        public decimal CalculateStockValue()
        {
            Product[] products =
           {
            new Product {Name = "西瓜",  Price = 2.3M},
            new Product {Name = "苹果",  Price = 4.9M},
            new Product {Name = "空心菜", Price = 2.2M},
            new Product {Name = "地瓜",  Price = 1.9M}
            };

            //计算商品总价钱 
            decimal totalValue = calculator.ValueProducts(products);

            return totalValue;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IValueCalculator
    {
        decimal ValueProducts(params Product[] products);
    }
    /// <summary>
    /// 
    /// </summary>
    public class MyValueCalculator : IValueCalculator
    {
        //把IDiscounHelper接口作为依赖添加到LinqValueCalculator类中。代码如下：
        private IDiscountHelper discounter;
        public MyValueCalculator(IDiscountHelper a)
        {
            discounter = a;
        }
        public decimal ValueProducts(params Product[] products)
        {
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }

    /// <summary>
    /// 折扣计算接口
    /// </summary>
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }

    /// <summary>
    /// 默认折扣计算器
    /// </summary>
    public class DefaultDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)//定义了打9折的ApplyDiscount方法。
        {
            return (totalParam - (1m / 10m * totalParam));
        }
    }
}
