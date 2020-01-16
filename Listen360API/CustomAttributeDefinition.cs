using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents a custom attribute definition.
    /// <para>
    /// Custom attributes are values defined in the Listen360 system, which allow filtering reports on <see cref="Customer">customers</see>, <see cref="Job">jobs</see>, and <see cref="Franchise">franchises</see>. They are defined by <see cref="Franchisor">franchisors</see>.
    /// </para>
    /// </summary>
    public class CustomAttributeDefinition : ModelBase, ICustomAttributeDefinition
    {

        /// <exclude/>
        private protected CustomAttributeDefinition()
        {

        }

        /// <inheritdoc/>
        public CustomAttributeDefinition(IListen360 listen360, XmlNode node)
            : base(listen360, node)
        {
        }

        #region Attributes

        /// <inheritdoc/>
        public long OrganizationId
        {
            get
            {
                return (long)ReadAttribute("organization-id");
            }
        }

        /// <inheritdoc/>
        public string Name
        {
            get
            {
                return (string)ReadAttribute("name");
            }
        }

        /// <inheritdoc/>
        public int Ordinal
        {
            get
            {
                return (int)ReadAttribute("ordinal");
            }
        }

        /// <inheritdoc/>
        public string AttachedTo
        {
            get
            {
                return (string)ReadAttribute("attach-to");
            }
        }

        /// <inheritdoc/>
        public bool IsMandatory
        {
            get
            {
                return (bool)ReadAttribute("mandatory");
            }
        }

        /// <inheritdoc/>
        public bool IsRestrictedToPredefinedChoices
        {
            get
            {
                return (bool)ReadAttribute("restrict-to-predefined-choices");
            }
        }

        #endregion

        /// <inheritdoc/>
        public override string CollectionPath
        {
            get { return string.Format("organizations/{0}/custom_attribute_definitions"); }
        }

        /// <inheritdoc/>
        public override bool Save()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public override void Reload()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public override void Delete()
        {
            throw new NotSupportedException();
        }
    }
}
