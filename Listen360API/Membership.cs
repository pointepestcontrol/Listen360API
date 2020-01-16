using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents a user's membership of an organization.
    /// </summary>
    /// <remarks>Once a membership invite has been created, only its role can be changed. Delete a membership to revoke it.</remarks>
    public class Membership : ModelBase, IMembership
    {
        /// <exclude/>
        private protected Membership()
        {
           
        }
        /// <summary>
        /// Initializes a new membership under a specific organization.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="organizationId">The organization's unique identifier.</param>
        /// <remarks>Once the membership is saved an invite will be sent to the appropriate email address. It then be accepted by the recipient.</remarks>
        public Membership(IListen360 listen360, long organizationId) : base(listen360)
        {
            _organizationId = organizationId;
        }

        /// <exclude/>
        public Membership(IListen360 listen360, XmlNode node)
            : base(listen360, node)
        {
        }

        #region Attributes

        long? _organizationId = null;

        /// <inheritdoc/>
        public long? OrganizationId
        {
            get
            {
                if (_organizationId.HasValue) { return _organizationId; }
                _organizationId = (long?)ReadAttribute("organization-id");
                return _organizationId;
            }
            set
            {
                _organizationId = value;
            }
        }

        /// <inheritdoc/>
        public string Status
        {
            get
            {
                return (string)ReadAttribute("status");
            }
            set
            {
                WriteAttribute("status", value);
            }
        }

        /// <inheritdoc/>
        public string Role
        {
            get
            {
                return (string)ReadAttribute("role");
            }
            set
            {
                WriteAttribute("role", value);
            }
        }

        /// <inheritdoc/>
        public string FirstName
        {
            get
            {
                return (string)ReadAttribute("first-name");
            }
            set
            {
                WriteAttribute("first-name", value);
            }
        }

        /// <inheritdoc/>
        public string LastName
        {
            get
            {
                return (string)ReadAttribute("last-name");
            }
            set
            {
                WriteAttribute("last-name", value);
            }
        }

        /// <inheritdoc/>
        public string Email
        {
            get
            {
                return (string)ReadAttribute("email");
            }
            set
            {
                WriteAttribute("email", value);
            }
        }

        /// <inheritdoc/>
        public string MobilePhoneNumber
        {
            get
            {
                return (string)ReadAttribute("mobile-phone-number");
            }
            set
            {
                WriteAttribute("mobile-phone-number", value);
            }
        }

        /// <inheritdoc/>
        public string Country
        {
            get
            {
                return (string)ReadAttribute("country");
            }
            set
            {
                WriteAttribute("country", value);
            }
        }

        /// <inheritdoc/>
        public DateTime? AcceptedAt
        {
            get { return (DateTime?)ReadAttribute("accepted-at"); }
        }

        /// <inheritdoc/>
        public DateTime? RevokedAt
        {
            get { return (DateTime?)ReadAttribute("revoked-at"); }
        }

        #endregion

        /// <exclude/>
        public override string CollectionPath
        {
            get
            {
                return string.Format("organizations/{0}/memberships", OrganizationId);
            }
        }
    }
}
