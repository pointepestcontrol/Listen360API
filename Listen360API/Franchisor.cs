using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents a root franchisor or brand under which other organizations, such as <see cref="Division">divisions</see> and <see cref="Franchise">franchises</see>, are organized.
    /// </summary>
    public class Franchisor : OrganizationBase, IFranchisor
    {
        /// <exclude/>
        public Franchisor(IListen360 listen360, XmlNode node) : base(listen360, node)
        {
        }

        /// <inheritdoc/>
        public override string this[int index]
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <inheritdoc/>
        public ICustomAttributeDefinition[] GetCustomAttributeDefinitions()
        {
            string requestPath = string.Format("organizations/{0}/custom_attribute_definitions", Id);
            return (ICustomAttributeDefinition[])((ArrayList)ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath))).ToArray(typeof(ICustomAttributeDefinition));
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <exception cref="NotSupportedException"/>
        public override bool Save()
        {
            throw new NotSupportedException();
        }
    }
}
