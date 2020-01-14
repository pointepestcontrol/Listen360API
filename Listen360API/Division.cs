using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents a division of a <see cref="Franchisor">franchise</see>, under which <see cref="Franchise">franchises</see> can be organized.
    /// Divisions can also be nested to model arbitrarily deep organizational hierarchies.
    /// </summary>
    public class Division : OrganizationBase, IDivision
    {
        /// <exclude/>
        public Division(IListen360 listen360, XmlNode node) : base(listen360, node)
        {
        }

        /// <summary>
        /// Initializes a new division under a specific parent organization.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="parentId">The unique identifier of the parent organization.</param>
        public Division(IListen360 listen360, long parentId) : base(listen360)
        {
            ParentId = parentId;
        }

        /// <exclude/>
        public override string CreatePath
        {
            get
            {
                if (ParentId.HasValue)
                {
                    return string.Format("{0}?parent_id={1}", CollectionPath, ParentId);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
