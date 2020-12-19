using MyWebFoundation.Framework.Extensions;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.SuperSocket
{
    public class SuperSocketBootstrap
    {
        private static IBootstrap bootstrap = null;
        public static void Startup()
        {
            if (bootstrap != null)
            {
                return;
            }
            bootstrap = BootstrapFactory.CreateBootstrap();
            if (!bootstrap.Initialize())
            {
                typeof(SuperSocketBootstrap).Error(MethodBase.GetCurrentMethod().Name, new Exception("初始化失败!"));
                return;
            }
            var result = bootstrap.Start();
            typeof(SuperSocketBootstrap).Info(MethodBase.GetCurrentMethod().Name, $"服务正在启动: {result}!");
            if (result == StartResult.Failed)
            {
                typeof(SuperSocketBootstrap).Error(MethodBase.GetCurrentMethod().Name, new Exception("服务启动失败!"));
                return;
            }
        }

        public static  void Stop()
        {
            if (bootstrap != null)
            {
                bootstrap.Stop();
            }
        }
    }
}
