using System;
using System.Collections;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Provides the interface for implementation of the <see cref="ModelBase"/> class.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Gets the model's unique identifier.
        /// </summary>
        long? Id { get; }

        /// <summary>
        /// Gets the date and time at which the model was created.
        /// </summary>
        DateTime? CreatedAt { get; }

        /// <summary>
        /// Gets the date and time at which the model was last updated.
        /// </summary>
        DateTime? UpdatedAt { get; }

        /// <summary>
        /// Gets or sets a custom attribute label.
        /// </summary>
        /// <param name="ordinal">The ordinal of the custom attribute.</param>
        /// <returns>The current value of the custom attribute label at the specified ordinal.</returns>
        string this[int ordinal] { get; set; }

        /// <summary>
        /// Gets or sets a custom attribute label.
        /// </summary>
        /// <param name="attributeDefinition">The definition of the custom attribute.</param>
        /// <returns>The current value of the custom attribute label at the specified ordinal.</returns>
        /// <remarks>Use this overloaded accessor to negate the need to hard-code ordinals.</remarks>
        string this[ICustomAttributeDefinition attributeDefinition] { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the record has ever been saved.
        /// </summary>
        bool IsNewRecord { get; }

        /// <summary>
        /// Gets a value indicating whether or not the record is valid.
        /// </summary>
        /// <remarks>This value is only available after attempting to save the model instance.</remarks>
        /// <seealso cref="Errors"/>
        /// <see cref="Save"/>
        bool IsValid { get; }

        /// <summary>
        /// Gets a list of validation errors following an unsuccesful attempt to save the model instance.
        /// </summary>
        string[] Errors { get; }

        /// <summary>
        /// Gets a collection of changed attributes, indexed by name.
        /// </summary>
        Hashtable Changes { get; }

        /// <summary>
        /// Gets a value indicating whether or not the model been changed since it was last saved.
        /// </summary>
        bool HasChanged { get; }

        /// <summary>
        /// Serializes attribute changes into the an XML representation understood by the remote service.
        /// </summary>
        /// <returns>An XML representation of changed attributes as understood by the remote service.</returns>
        string SerializeChanges();

        /// <exclude/>
        void SerializeChanges(XmlDocument doc, XmlNode root);

        /// <summary>
        /// Pushes any local changes to the remote service.
        /// </summary>
        /// <returns><b>true</b> if the operation succeeded; otherwise <b>false</b>.</returns>
        /// <remarks>If operation fails, you should check the <see cref="Errors"/> collection to find out why.</remarks>
        /// <exception cref="System.Net.WebException">Raised when an unexpected error occurs, something other than a failure to validate.</exception>
        bool Save();

        /// <summary>
        /// Reloads the model data from the remote service.
        /// </summary>
        /// <exception cref="System.Net.WebException" />
        void Reload();

        /// <summary>
        /// Deletes the model instance from the remote service.
        /// </summary>
        /// <exception cref="System.Net.WebException" />
        void Delete();
    }
}
