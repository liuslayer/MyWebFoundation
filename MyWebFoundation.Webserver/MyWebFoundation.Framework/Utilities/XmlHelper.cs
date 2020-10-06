using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MyWebFoundation.Framework.Utilities
{
    public static class XmlHelper
    {

        /// <summary>
        /// XmlSerializer序列化实体为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToXml<T>(T t) where T : new()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(t.GetType());
            Stream stream = new MemoryStream();
            xmlSerializer.Serialize(stream, t);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            return text;
        }

        /// <summary>
        /// 字符串序列化成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ToObject<T>(string content) where T : new()
        {
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                return (T)xmlFormat.Deserialize(stream);
            }
        }

        /// <summary>
        /// 文件反序列化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T FileToObject<T>(string fileName) where T : new()
        {
            using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                return (T)xmlFormat.Deserialize(fStream);
            }
        }

        /// <summary>   
        /// 实体转化为XML   
        /// </summary>   
        public static string ParseToXml<T>(this T model, string fatherNodeName)
        {
            var xmldoc = new XmlDocument();
            var modelNode = xmldoc.CreateElement(fatherNodeName);
            xmldoc.AppendChild(modelNode);

            if (model != null)
            {
                foreach (PropertyInfo property in model.GetType().GetProperties())
                {
                    var attribute = xmldoc.CreateElement(property.Name);
                    if (property.GetValue(model, null) != null)
                        attribute.InnerText = property.GetValue(model, null).ToString();
                    //else
                    //    attribute.InnerText = "[Null]";
                    modelNode.AppendChild(attribute);
                }
            }
            return xmldoc.OuterXml;
        }

        /// <summary>
        /// XML转换为实体,默认 fatherNodeName="body"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="fatherNodeName"></param>
        /// <returns></returns>
        public static T ParseToModel<T>(this string xml, string fatherNodeName = "body") where T : class, new()
        {
            if (string.IsNullOrEmpty(xml))
                return default(T);
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            T model = new T();
            var attributes = xmldoc.SelectSingleNode(fatherNodeName).ChildNodes;
            foreach (XmlNode node in attributes)
            {
                foreach (var property in model.GetType().GetProperties().Where(property => node.Name == property.Name))
                {
                    if (!string.IsNullOrEmpty(node.InnerText))
                    {
                        property.SetValue(model,
                                          property.PropertyType == typeof(Guid)
                                              ? new Guid(node.InnerText)
                                              : Convert.ChangeType(node.InnerText, property.PropertyType));
                    }
                    else
                    {
                        property.SetValue(model, null);
                    }
                }
            }
            return model;
        }
    }
}
