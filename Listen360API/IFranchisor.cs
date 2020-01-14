using System;

namespace Listen360API
{
    /// <summary>
    /// Provides the base interface for implementation of the <see cref="Franchisor"/> class.
    /// </summary>
    public interface IFranchisor : IOrganization
    {
        /// <summary>
        /// Gets a list of all available custom attribute definitions.
        /// </summary>
        /// <returns></returns>
        ICustomAttributeDefinition[] GetCustomAttributeDefinitions();
    }
}
