using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Quanzk.Cores.Swagger.Extentions
{
    /// <summary>
    ///  Swagger 文档 注释所需的xml文件 合并
    /// </summary>
    public static class SwaggerXmlFileMergeExtentions
    {
        public static IServiceCollection AddXmlFileMerge(this IServiceCollection services)
        {
            // 定义主项目注释文档名称
            string xmlFileName = "Main.xml";
            // 获取文件存放路径
            var directory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            // 找到所有的注释文档， 根据你的项目情况加上通配符。
            var files = Directory.GetFiles(directory, "Quanzk.*.xml").ToList();
            if (files.Count > 0)
            {
                // 创建xml文件
                CreateXmlFile(xmlFileName, directory, "doc", files);
            }
            return services;
        }

        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="path">文件存放路径</param>
        /// <param name="rootNodeName">根节点名称</param>
        /// <param name="files">待拼接的xml文件</param>
        private static void CreateXmlFile(string fileName, string path, string rootNodeName, List<string> files)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement(rootNodeName);
            xmlDoc.AppendChild(root);
            //创建节点集合
            XmlNode members = xmlDoc.CreateNode(XmlNodeType.Element, "members", null);
            // 循环遍历合并文件
            files.ForEach(file =>
            {
                var doc = new XmlDocument();
                doc.Load(file);
                var nodes = doc.SelectNodes("//member");
                foreach (XmlNode node in nodes)
                {
                    var importNode = xmlDoc.ImportNode(node, true);
                    members.AppendChild(importNode);
                }
            });
            // 移除多余的文件
            files.ForEach(u =>
            {
                File.Delete(u);
            });
            root.AppendChild(members);
            try
            {
                xmlDoc.Save(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmldoc">xml文档 </param>   
        /// <param name="parentnode">父节点</param>    
        /// <param name="name">节点名</param>    
        /// <param name="value">节点值</param>    
        ///   
        private static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }
    }
}
