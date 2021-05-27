using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.UserServices.Models;
using Quanzk.ShoppingMall.UserServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.UserServices.Services
{
    /// <summary>
    /// 用户服务实现
    /// </summary>
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public UserServiceImpl(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Create(User user)
        {
            return _userRepository.Create(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            return _userRepository.Delete(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserByName(string userName)
        {
            return _userRepository.GetUserByName(userName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(User user)
        {
            return _userRepository.Update(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UserNameExists(string userName)
        {
            return _userRepository.UserNameExists(userName);
        }

    }
}
