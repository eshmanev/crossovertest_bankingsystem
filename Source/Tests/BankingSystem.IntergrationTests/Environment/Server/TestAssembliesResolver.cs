using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace BankingSystem.IntegrationTests.Environment.Server
{
    /// <summary>
    ///     Represents a resolver that loads tested assemblies.
    /// </summary>
    /// <seealso cref="System.Web.Http.Dispatcher.DefaultAssembliesResolver" />
    public class TestAssembliesResolver : DefaultAssembliesResolver
    {
        /// <summary>
        ///     Returns a list of assemblies available for the application.
        /// </summary>
        /// <returns>
        ///     A &lt;see cref="T:System.Collections.ObjectModel.Collection`1" /&gt; of assemblies.
        /// </returns>
        public override ICollection<Assembly> GetAssemblies()
        {
            var baseAssemblies = base.GetAssemblies();
            var assemblies = new List<Assembly>(baseAssemblies);
            var controllersAssembly = typeof(BankingSystem.WebPortal.MvcApplication).Assembly;
            baseAssemblies.Add(controllersAssembly);
            return assemblies;
        }
    }
}