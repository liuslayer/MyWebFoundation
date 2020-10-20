using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace MyWebFoundation.Framework.AOP
{
    public class ExceptionLoggingBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn methodReturn = getNext()(input, getNext);

            Console.WriteLine("ExceptionLoggingBehavior");
            if (methodReturn.Exception == null)
            {
                Console.WriteLine("无异常");
            }
            else
            {
                Console.WriteLine($"异常:{methodReturn.Exception.Message}");
            }
            return methodReturn;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
