using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients.Attributes
{
    /// <summary>
    /// Delete 请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DeletePath : Attribute
    {
        /// <summary>
        /// 请求路径
        /// </summary>
        public string Path { get; set; }
        public DeletePath(string path)
        {
            this.Path = path;
        }
    }
}
