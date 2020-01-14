using System;

namespace Listen360API
{
    /// <summary>
    /// Provides the base interface for implementation of the <see cref="CustomAttributeDefinition"/> class.
    /// </summary>
    public interface ICustomAttributeDefinition : IModel
    {
        /// <summary>
        /// Gets the ID of the organization.
        /// </summary>
        long OrganizationId { get; }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the ordinal of the attribute.
        /// </summary>
        int Ordinal { get; }

        /// <summary>
        /// Gets the name of the model to which the attribute is attached.
        /// </summary>
        string AttachedTo { get; }

        /// <summary>
        /// Gets a value indicating whether or not the attribute is considered mandatory.
        /// </summary>
        bool IsMandatory { get; }

        /// <summary>
        /// Gets a value indicating whether or not the attribute is restricted to predefined choices.
        /// </summary>
        bool IsRestrictedToPredefinedChoices { get; }
    }
}
