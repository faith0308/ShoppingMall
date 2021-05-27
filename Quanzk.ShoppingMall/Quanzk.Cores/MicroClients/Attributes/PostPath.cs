using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients.Attributes
{
    /// <summary>
    /// Post 请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PostPath : Attribute
    {
        /// <summary>
        /// 请求路径
        /// </summary>
        public string Path { get; set; }
        public PostPath(string path)
        {
            this.Path = path;
        }
    }
}
