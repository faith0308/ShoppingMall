using NETCore.Encrypt;
using Quanzk.Cores.Registry;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Cluster
{
    /// <summary>
    /// Hash 一致性算法
    /// </summary>
    public class ConsistentHashLoadBalance : AbstractLoadBalance
    {
        private ConcurrentDictionary<string, ConsistentHashSelector> selectors = new ConcurrentDictionary<string, ConsistentHashSelector>();

        public override ServiceNode DoSelect(IList<ServiceNode> serviceNodes)
        {
            string key = serviceNodes[0].Url;
            int identityHashCode = serviceNodes.GetHashCode();
            ConsistentHashSelector selector = (ConsistentHashSelector)selectors[key];
            if (selector == null || selector.GetHashCode() != identityHashCode)
            {
                selectors.TryAdd(key, new ConsistentHashSelector(serviceNodes, "", identityHashCode));
                selector = (ConsistentHashSelector)selectors[key];
            }
            return selector.Select(key);
        }


        /// <summary>
        /// Hash 散列选择器
        /// </summary>
        private class ConsistentHashSelector
        {
            private SortedDictionary<long, ServiceNode> virtualServiceUrls;

            private int replicaNumber;
            private int identityHashCode;
            private int[] argumentIndex;

            public ConsistentHashSelector(IList<ServiceNode> serviceUrls, string methodName, int identityHashCode)
            {
                this.virtualServiceUrls = new SortedDictionary<long, ServiceNode>();
                this.identityHashCode = identityHashCode;
                string url = serviceUrls[0].Url;
                // 默认多少个虚拟节点
                this.replicaNumber = 160;
                string[] index = new string[] { };
                argumentIndex = new int[index.Length];
                for (int i = 0; i < index.Length; i++)
                {
                    argumentIndex[i] = int.Parse(index[i]);
                }
                foreach (ServiceNode serviceUrl in serviceUrls)
                {
                    string address = serviceUrl.Url;
                    for (int i = 0; i < replicaNumber / 4; i++)
                    {
                        byte[] digest = md5(address + i);
                        for (int h = 0; h < 4; h++)
                        {
                            long m = hash(digest, h);
                            virtualServiceUrls.Add(m, serviceUrl);
                        }
                    }
                }
            }

            public ServiceNode Select(string url)
            {
                string key = url;
                byte[] digest = md5(key);
                return selectForKey(hash(digest, 0));
            }

            private string toKey(Object[] args)
            {
                StringBuilder buf = new StringBuilder();
                foreach (int i in argumentIndex)
                {
                    if (i >= 0 && i < args.Length)
                    {
                        buf.Append(args[i]);
                    }
                }
                return buf.ToString();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hash"></param>
            /// <returns></returns>
            private ServiceNode selectForKey(long hash)
            {
                KeyValuePair<long, ServiceNode> entry = virtualServiceUrls.GetEnumerator().Current;
                if ("null".Equals(entry) || "".Equals(entry))
                {
                    entry = virtualServiceUrls.GetEnumerator().Current;
                }
                return entry.Value;
            }

            /// <summary>
            /// Hash
            /// </summary>
            /// <param name="digest"></param>
            /// <param name="number"></param>
            /// <returns></returns>
            private long hash(byte[] digest, int number)
            {
                return (((long)(digest[3 + (number * 4)] & 0xFF) << 24) | ((long)(digest[2 + (number * 4)] & 0xFF) << 16) | ((long)(digest[1 + (number * 4)] & 0xFF) << 8) | (digest[number * 4] & 0xFF)) & 0xFFFFFFFFL;
            }

            /// <summary>
            /// MD5
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            private byte[] md5(string value)
            {
                var hashed = EncryptProvider.Md5(value);
                byte[] bytes = Encoding.UTF8.GetBytes(hashed);
                return bytes;
            }

        }
    }
}
