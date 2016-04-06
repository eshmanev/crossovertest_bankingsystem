using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;

namespace BankingSystem.DAL.Session
{
    /// <summary>
    ///     Represents an implementation of session factory holder.
    /// </summary>
    public class SessionFactoryHolder : ISessionFactoryHolder
    {
        private readonly object _syncRoot = new object();
        private volatile ISessionFactory _sessionFactory;

        /// <summary>
        ///     Gets the session factory.
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory != null)
                {
                    return _sessionFactory;
                }

                lock (_syncRoot)
                {
                    if (_sessionFactory != null)
                    {
                        return _sessionFactory;
                    }

                    _sessionFactory = CreateSessionFactory();
                }

                return _sessionFactory;
            }
        }

        /// <summary>
        ///     Configures HiLo table used to generage entity identifiers with HiLo algorithm.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void ConfigureHiLoTable(NHibernate.Cfg.Configuration configuration)
        {
            var script = new StringBuilder();

            script.AppendLine("ALTER TABLE HiLo ADD [Entity] VARCHAR(128) NULL");
            script.AppendLine("GO");

            script.AppendLine("CREATE NONCLUSTERED INDEX IX_HiLo_Entity ON HiLo (Entity ASC);");
            script.AppendLine("GO");

            foreach (var name in configuration.ClassMappings.Select(m => m.EntityName).Distinct())
            {
                script.AppendLine($"INSERT INTO [HiLo] (Entity, NextHi) VALUES ('{name}',1);");
            }

            configuration.AddAuxiliaryDatabaseObject(
                new SimpleAuxiliaryDatabaseObject(
                    script.ToString(),
                    null,
                    new HashSet<string>
                    {
                        typeof (MsSql2000Dialect).FullName,
                        typeof (MsSql2005Dialect).FullName,
                        typeof (MsSql2008Dialect).FullName,
                        typeof (MsSql2012Dialect).FullName,
                        typeof (MsSqlAzure2008Dialect).FullName
                    }));
        }

        /// <summary>
        ///     Creates a new session factory.
        /// </summary>
        /// <returns>An instance of the <see cref="ISessionFactory" /></returns>
        private ISessionFactory CreateSessionFactory()
        {
            var configuration = Fluently.Configure()
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionFactoryHolder>())
                .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("BankingSystem")))
                .ExposeConfiguration(ConfigureHiLoTable)
                .BuildConfiguration();

            EnsureSchemaExists(configuration);
            return configuration.BuildSessionFactory();
        }

        /// <summary>
        ///     Checks if the database schema exists; if it's not then generates it.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private void EnsureSchemaExists(NHibernate.Cfg.Configuration configuration)
        {
            try
            {
                new SchemaValidator(configuration).Validate();
            }
            catch (HibernateException)
            {
                GenerateSchema(configuration);
            }
        }

        /// <summary>
        ///     Generates the database schema.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private void GenerateSchema(NHibernate.Cfg.Configuration configuration)
        {
            var schemaExport = new SchemaExport(configuration);
            schemaExport.Drop(false, true);
            schemaExport.Create(true, true);
        }
    }
}