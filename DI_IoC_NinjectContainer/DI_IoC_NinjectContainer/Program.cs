using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_IoC_NinjectContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel ninjectKernel = new StandardKernel();//在使用Ninject前先要创建一个Ninject内核对象，代码如下：
            //这个绑定操作就是告诉Ninject，当接收到一个请求IValueCalculator接口的实现时，就返回一个MyValueCalculator类的实例。
            ninjectKernel.Bind<IValueCalculator>().To<MyValueCalculator>();
            //编写用于计算购物车中商品折后总价钱的代码
            ninjectKernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>();

            //用Ninject的Get方法去获取IValueCalculator接口的实现。这一步，Ninject将自动为我们创建LinqValueCalculator类的实例，并返回该实例的引用。然后我们可以把这个引用通过构造函数注入到ShoppingCart类
            //获得实现接口的对象实例
            IValueCalculator calImpl = ninjectKernel.Get<IValueCalculator>();

            //IValueCalculator calcImpl = new MyValueCalculator();

            // 创建ShoppingCart实例并注入依赖
            ShoppingCart cart = new ShoppingCart(calImpl);
            // 计算商品总价钱并输出结果
            Console.WriteLine("Total: {0:c}", cart.CalculateStockValue());
            Console.Read();



        }
    }
}
