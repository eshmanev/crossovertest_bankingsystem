using System;
using System.Linq;
using BankingSystem.Domain;
using BankingSystem.Domain.Impl;
using FluentNHibernate;

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
            return Filter(x => x.UserName == userName).FirstOrDefault();
        }

        /// <summary>
        ///     Searches for a customer with the given email address.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public ICustomer FindByEmail(string email)
        {
            return Filter(x => x.Email == email).FirstOrDefault();
        }

        /// <summary>
        ///     Searches for a customer who has the specified account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        public ICustomer FindByAccount(string accountNumber)
        {
            return GetSession().QueryOver<CustomerBase>()
                .Left.JoinQueryOver(Reveal.Member<CustomerBase>("_accounts"))
                .WhereRestrictionOn(x => ((Account)x).AccountNumber).IsLike(accountNumber)
                .SingleOrDefault();
        }

        /// <summary>
        ///     Searches for a merchant with the given merchant identifier.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <returns>A merchant or null.</returns>
        public IMerchant FindByMerchantId(Guid merchantId)
        {
            return Filter(x => ((IMerchant) x).MerchantId == merchantId).Cast<IMerchant>().FirstOrDefault();
        }
    }
}