using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace MyWebFoundation.Framework.AOP
{
    /// <summary>
    /// 不需要特性
    /// </summary>
    public class LogBeforeBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("LogBeforeBehavior");
            Console.WriteLine(input.MethodBase.Name);
            foreach (var item in input.Inputs)
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
                //反射&序列化获取更多信息
            }
            return getNext().Invoke(input, getNext);//
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
