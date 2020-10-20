using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace MyWebFoundation.Framework.AOP
{
    public class ParameterCheckBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("ParameterCheckBehavior");
            //User user = input.Inputs[0] as User;//可以不写死类型，反射+特性完成数据有效性监测
            //if (user.Password.Length < 10)//可以过滤一下敏感词
            //{
            //    //返回一个异常
            //    return input.CreateExceptionMethodReturn(new Exception("密码长度不能小于10位"));
            //}
            //else
            //{
            //    Console.WriteLine("参数检测无误");
            //    return getNext().Invoke(input, getNext);
            //}
            return getNext().Invoke(input, getNext);
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
