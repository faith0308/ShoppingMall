using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.UserServices.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("users")]
    public class User
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { set; get; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickname { set; get; }

        /// <summary>
        /// 用户QQ
        /// </summary>
        public string UserQQ { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserPhone { set; get; }
    }
}
