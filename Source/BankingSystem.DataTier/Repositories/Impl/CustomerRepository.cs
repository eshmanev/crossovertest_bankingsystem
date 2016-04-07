using System.Linq;
using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Repositories.Impl
{
    /// <summary>
    ///     Represents a repository of customers.
    /// </summary>
    public class CustomerRepository : Repository<ICustomer>, ICustomerRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public CustomerRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Searches for a customer with the given name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public ICustomer FindByUserName(string userName)
        {
            return base.Filter(x => x.UserName == userName).FirstOrDefault();
        }

        /// <summary>
        ///     Searches for a customer with the given email address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public ICustomer FindByEmail(string email)
        {
            return base.Filter(x => x.Email == email).FirstOrDefault();
        }
    }
}