using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients.Attributes
{
    /// <summary>
    /// 请求路径 变量 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class PathVariable : Attribute
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }
        public PathVariable(string name)
        {
            this.Name = name;
        }
    }
}
