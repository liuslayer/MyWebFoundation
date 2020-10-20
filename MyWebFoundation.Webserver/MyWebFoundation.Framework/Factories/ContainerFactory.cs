using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MyWebFoundation.Framework.Factories
{
    /// <summary>
    /// 容器工厂--生成容器实例--单例的---容器单例就够了，不需要每次都去读配置文件
    /// </summary>
    public class ContainerFactory
    {
        private static IUnityContainer _IUnityContainer = null;
        static ContainerFactory()
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");//找配置文件的路径
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
            _IUnityContainer = new UnityContainer();
            section.Configure(_IUnityContainer, "MyFoundationContainer");
        }

        public static IUnityContainer CreateContainer()
        {
            return _IUnityContainer;
        }
    }
}
