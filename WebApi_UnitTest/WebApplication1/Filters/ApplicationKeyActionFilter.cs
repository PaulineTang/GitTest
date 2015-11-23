using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApplication1.IServices;

namespace WebApplication1.Filters
{
    public class ApplicationKeyActionFilter : ActionFilterAttribute
    {
        public const string KeyHeaderName = "X-AuthKey";
        IKeyVerifier keyVerifier;
        public ApplicationKeyActionFilter()
        {

        }
        public ApplicationKeyActionFilter(IKeyVerifier _keyVerifier)
        {
            this.keyVerifier = _keyVerifier;
        }
        public Type keyVerifierType{get;set; }//Type表示类型声明：类类型、接口类型、数组类型、值类型、枚举类型、类型参数、泛型类型定义，以及开放或封闭构造的泛型类型。
                                              //Object这是 .NET Framework 中所有类的最终基类；它是类型层次结构的根
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if(keyVerifier == null)
            {
                if(keyVerifier == null)
                {
                    throw new Exception("The KeyVerifierType shoule be provided.");
                }
                this.keyVerifier = (IKeyVerifier)Activator.CreateInstance(this.keyVerifierType);// 使用指定类型的默认构造函数来创建该类型的实例。

                IEnumerable<string> values = null;

                if(actionContext.Request.Headers.TryGetValues(KeyHeaderName,out values))//  获取 HTTP 请求标头的集合
                {
                    var key = values.First();//??????????????
                    if(!this.keyVerifier.Verifier(key))//X-Auth标头的标头值（码值）是Key
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);//管道执行终端，返回401状态码
                    }
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                }
               
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            base.OnActionExecuted(actionContext);
        }
    }
}