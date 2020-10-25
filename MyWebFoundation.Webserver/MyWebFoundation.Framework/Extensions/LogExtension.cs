using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Framework.Extensions
{
    public static class LogExtension
    {
        static LogExtension()
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CfgFiles\\log4net.Config")));
            ILog Log = LogManager.GetLogger(typeof(LogExtension));
            Log.Info("系统初始化Logger模块");
        }

        private static ILog loger = null;

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Fatal(this Type type, string methodName, Exception ex = null)
        {
            loger = LogManager.GetLogger(type);
            loger.Fatal(methodName, ex);
        }
        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(this Type type, string methodName, Exception ex = null)
        {
            loger = LogManager.GetLogger(type);
            loger.Error(methodName , ex);
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(this Type type, string methodName, string msg)
        {
            loger = LogManager.GetLogger(type);
            loger.Warn(methodName + ":" + msg);
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(this Type type, string methodName, string msg)
        {
            loger = LogManager.GetLogger(type);
            loger.Info(methodName + ":" + msg);
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(this Type type, string methodName, string msg)
        {
            loger = LogManager.GetLogger(type);
            loger.Debug(methodName + ":" + msg);
        }
    }
}
