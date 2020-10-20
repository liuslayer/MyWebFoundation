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
    public class CachingBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        private static Dictionary<string, object> CachingBehaviorDictionary = new Dictionary<string, object>();

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("CachingBehavior");
            string key = $"{input.MethodBase.Name}_{Newtonsoft.Json.JsonConvert.SerializeObject(input.Inputs)}";
            if (CachingBehaviorDictionary.ContainsKey(key))
            {
                return input.CreateMethodReturn(CachingBehaviorDictionary[key]);//直接返还  断路器  不再往下
            }
            else
            {
                IMethodReturn result = getNext().Invoke(input, getNext);
                if (result.ReturnValue != null)
                    CachingBehaviorDictionary.Add(key, result.ReturnValue);
                return result;
            }


        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
