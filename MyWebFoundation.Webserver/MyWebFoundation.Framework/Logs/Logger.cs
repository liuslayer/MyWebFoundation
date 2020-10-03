using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Framework.Logs
{
    public class Logger
    {
        static Logger()
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CfgFiles\\log4net.Config")));
            ILog Log = LogManager.GetLogger(typeof(Logger));
            Log.Info("系统初始化Logger模块");
        }

        private ILog loger = null;
        private Logger(Type type)
        {
            loger = LogManager.GetLogger(type);
        }

        /// <summary>
        /// 创建Logger
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Logger CreateLogger(Type type)
        {
            return new Logger(type);
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Fatal(string msg = "出现严重异常", Exception ex = null)
        {
            loger.Fatal(msg, ex);
        }
        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Error(string msg = "出现异常", Exception ex = null)
        {
            loger.Error(msg, ex);
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        public void Warn(string msg)
        {
            loger.Warn(msg);
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        public void Info(string msg)
        {
            loger.Info(msg);
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        public void Debug(string msg)
        {
            loger.Debug(msg);
        }
    }
}
