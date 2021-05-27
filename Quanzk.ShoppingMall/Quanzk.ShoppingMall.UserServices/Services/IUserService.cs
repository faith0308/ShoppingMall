using Quanzk.Commons.Messages;
using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.UserServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.UserServices.Services
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService : IDependency
    {
        /// <summary>
        /// 返回所有用户
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetUsers();

        /// <summary>
        /// 根据用户名获取用户对象
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        User GetUserByName(string UserName);

        /// <summary>
        /// 根据用户Id获取用户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(int id);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        bool Create(User user);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        bool Update(User user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        bool Delete(int Id);

        /// <summary>   
        /// 根据Id判断用户是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool UserExists(int id);

        /// <summary>
        /// 根据用户名判断用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool UserNameExists(string userName);
    }
}
