using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// An abstract base class for all organizations.
    /// </summary>
    public abstract class OrganizationBase : ModelBase, IOrganization
    {
        /// <exclude/>
        private protected OrganizationBase()
        {

        }
        /// <exclude/>
        public OrganizationBase(IListen360 listen360) : base(listen360)
        {
        }

        /// <exclude/>
        public OrganizationBase(IListen360 listen360, XmlNode node) : base(listen360, node)
        {
        }

        /// <exclude/>
        public override string CollectionPath
        {
            get { return "organizations"; }
        }

        #region Attributes

        /// <inheritdoc/>
        public long RootId
        {
            get
            {
                return (long)ReadAttribute("root-id");
            }
        }

        /// <inheritdoc/>
        public long? ParentId
        {
            get
            {
                return (long?)ReadAttribute("parent-id");
            }
            set
            {
                WriteAttribute("parent-id", value);
            }
        }

        /// <inheritdoc/>
        public string Reference
        {
            get
            {
                return (string)ReadAttribute("reference");
            }
            set
            {
                WriteAttribute("reference", value);
            }
        }

        /// <inheritdoc/>
        public string Name
        {
            get
            {
                return (string)ReadAttribute("name");
            }
            set
            {
                WriteAttribute("name", value);
            }
        }

        /// <inheritdoc/>
        public string ExternalName
        {
            get
            {
                return (string)ReadAttribute("external-name");
            }
            set
            {
                WriteAttribute("external-name", value);
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
        public string StreetAddress
        {
            get
            {
                return (string)ReadAttribute("street-address");
            }
            set
            {
                WriteAttribute("street-address", value);
            }
        }

        /// <inheritdoc/>
        public string City
        {
            get
            {
                return (string)ReadAttribute("city");
            }
            set
            {
                WriteAttribute("city", value);
            }
        }

        /// <inheritdoc/>
        public string Region
        {
            get
            {
                return (string)ReadAttribute("region");
            }
            set
            {
                WriteAttribute("region", value);
            }
        }

        /// <inheritdoc/>
        public string PostalCode
        {
            get
            {
                return (string)ReadAttribute("postal-code");
            }
            set
            {
                WriteAttribute("postal-code", value);
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
        public string TimeZone
        {
            get
            {
                return (string)ReadAttribute("time-zone");
            }
            set
            {
                WriteAttribute("time-zone", value);
            }
        }

        /// <inheritdoc/>
        public string PhoneNumber
        {
            get
            {
                return (string)ReadAttribute("phone-number");
            }
            set
            {
                WriteAttribute("phone-number", value);
            }
        }

        /// <inheritdoc/>
        public string Website
        {
            get
            {
                return (string)ReadAttribute("website");
            }
            set
            {
                WriteAttribute("website", value);
            }
        }

        #endregion

        #region Hierarchy

        /// <inheritdoc/>
        public virtual IOrganization FindChildByReference(string reference)
        {
            string requestPath = string.Format("organizations/{0}/children?reference={1}", Id, System.Uri.EscapeUriString(reference));
            ArrayList matches = (ArrayList)(ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath)));
            IOrganization match = (matches.Count == 0) ? null : (IOrganization)matches[0];
            return match;
        }

        /// <inheritdoc/>
        public virtual IOrganization[] GetChildren()
        {
            return GetChildren(1);
        }

        /// <inheritdoc/>
        public virtual IOrganization[] GetChildren(int page)
        {
            string requestPath = string.Format("organizations/{0}/children?page={1}", Id, page);
            return (IOrganization[])((ArrayList)ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath))).ToArray(typeof(IOrganization));
        }

        /// <inheritdoc/>
        public virtual IOrganization FindDescendentByReference(string reference)
        {
            string requestPath = string.Format("organizations/{0}/descendents?reference={1}", Id, System.Uri.EscapeUriString(reference));
            ArrayList matches = (ArrayList)(ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath)));
            IOrganization match = (matches.Count == 0) ? null : (IOrganization)matches[0];
            return match;
        }

        /// <inheritdoc/>
        public virtual IOrganization[] GetDescendents()
        {
            return GetDescendents(1);
        }

        /// <inheritdoc/>
        public virtual IOrganization[] GetDescendents(int page)
        {
            string requestPath = string.Format("organizations/{0}/descendents?page={1}", Id, page);
            return (IOrganization[])((ArrayList)ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath))).ToArray(typeof(IOrganization));
        }

        #endregion

        #region Memberships

        /// <inheritdoc/>
        public IMembership FindMembershipByEmail(string email)
        {
            string requestPath = string.Format("{0}/memberships?email={1}", Path, System.Uri.EscapeUriString(email));
            ArrayList matches = (ArrayList)(ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath)));
            IMembership match = (matches.Count == 0) ? null : (IMembership)matches[0];
            return match;
        }

        /// <inheritdoc/>
        public IMembership[] GetMemberships()
        {
            return GetMemberships(1);
        }

        /// <inheritdoc/>
        public IMembership[] GetMemberships(int page)
        {
            string requestPath = string.Format("{0}/memberships?page={1}", Path, page);
            return (IMembership[])((ArrayList)ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath))).ToArray(typeof(IMembership));
        }

        #endregion
    }
}
