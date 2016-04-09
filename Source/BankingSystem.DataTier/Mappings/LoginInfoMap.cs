using BankingSystem.Domain.Impl;
using FluentNHibernate.Mapping;

namespace BankingSystem.DataTier.Mappings
{
    /// <summary>
    /// Defines a mapping for <see cref="LoginInfo"/>
    /// </summary>
    /// <seealso cref="FluentNHibernate.Mapping.ClassMap{BankingSystem.DAL.Entities.LoginInfo}" />
    internal class LoginInfoMap : ClassMap<LoginInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginInfoMap"/> class.
        /// </summary>
        public LoginInfoMap()
        {
            Table("LoginInfos");
            CompositeId()
                .KeyProperty(x => x.LoginKey)
                .KeyProperty(x => x.ProviderName);
            References(x => x.Customer).Column("CustomerId").ForeignKey("FK_LoginInfo_Customer").Class<CustomerBase>();
        }
    }
}