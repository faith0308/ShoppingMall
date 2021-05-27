using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.UserServices.Context;
using Quanzk.ShoppingMall.UserServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.UserServices.Repositories
{
    /// <summary>
    /// 用户仓储实现 
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;

        /// <summary>
        /// 构造函数 依赖注入
        /// </summary>
        /// <param name="userContext"></param>
        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Create(User user)
        {
            var falg = false;
            if (user != null)
            {
                user.CreateTime = DateTime.Now;
                _userContext.Users.Add(user);
                var result = _userContext.SaveChanges();
                if (result > 0)
                {
                    falg = true;
                }
            }
            return falg;
        }

        /// <summary>
        /// 根据Id删除对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            var falg = false;
            if (Id > 0)
            {
                var entity = _userContext.Users.Find(Id);
                if (entity != null)
                {
                    _userContext.Users.Remove(entity);
                    var result = _userContext.SaveChanges();
                    if (result > 0)
                    {
                        falg = true;
                    }
                }
            }
            return falg;
        }

        /// <summary>
        /// 根据Id获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            var result = _userContext.Users.Find(id);
            return result ?? new User();
        }

        /// <summary>
        /// 根据名称获取对象
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public User GetUserByName(string UserName)
        {
            var result = _userContext.Users.First(p => p.UserName == UserName);
            return result ?? new User();
        }

        /// <summary>
        /// 查询所有对象集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {
            var result = _userContext.Users.ToList();
            return result ?? new List<User>();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(User user)
        {
            var falg = false;
            if (user != null)
            {
                var entity = _userContext.Users.Find(user.Id);
                if (entity != null)
                {
                    entity.UserName = user.UserName;
                    entity.UserNickname = user.UserNickname;
                    entity.UserPhone = user.UserPhone;
                    entity.UserQQ = user.UserQQ;
                    _userContext.Users.Update(entity);
                    var result = _userContext.SaveChanges();
                    if (result > 0)
                    {
                        falg = true;
                    }
                }
            }
            return falg;
        }

        /// <summary>
        /// 根据Id判断用户是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UserExists(int id)
        {
            return _userContext.Users.Any(p => p.Id == id);
        }

        /// <summary>
        /// 根据用户名判断用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UserNameExists(string userName)
        {
            return _userContext.Users.Any(p => p.UserName == userName);
        }
    }
}
