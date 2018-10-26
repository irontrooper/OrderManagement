using OrderManagement.Core;
using OrderManagement.Data;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services
{
    /// <summary>
    /// Represents a class which manages the customer operations
    /// </summary>
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class UserService : OrderManagementService<User>
    {
        #region Members

        private UserRepository _UserRepository;

        #endregion

        /// <summary>
        /// Gets instance of service
        /// </summary>
        public static UserService Instance
        {
            get
            {
                return OrderManagementCore.Instance.Resolve<UserService>();
            }
        }


        #region Initialization

        /// <summary>
        /// Initializes a new instance of CustomerService
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userRepository"></param>
        public UserService(UserRepository userRepository) : base(userRepository)
        {
            _UserRepository = userRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Is user exists in database
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsUserExist(string userName, string password, out int userId)
        {
            var user = Instance.QuerableSearch().FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password);
            if (user == null)
            {
                userId = 0;
                return true;
            }

            userId = user.Id;
            return true;
        }
        #endregion
    }
}
