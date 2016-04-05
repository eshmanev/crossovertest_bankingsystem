using NHibernate;

namespace BankingSystem.DAL
{
    /// <summary>
    /// Provides a session factory.
    /// </summary>
    public interface ISessionFactoryHolder
    {
        /// <summary>
        /// Gets the session factory.
        /// </summary>
        /// <value>
        /// The session factory.
        /// </value>
        ISessionFactory SessionFactory { get; }
    }
}