using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents a technician responsible for performing a <see cref="Job">jobs</see>.
    /// </summary>
    public class Technician : ModelBase, ITechnician
    {
        /// <summary>
        /// Initializes a new technician.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        public Technician(IListen360 listen360)
            : base(listen360)
        {
        }

        /// <summary>
        /// Initializes a new technician under a specific organization.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="organizationId">The organization's unique identifier.</param>
        public Technician(IListen360 listen360, long organizationId) : base(listen360)
        {
            _organizationId = organizationId;
        }

        /// <exclude/>
        public Technician(IListen360 listen360, XmlNode node)
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

        #endregion

        /// <exclude/>
        public override string CollectionPath
        {
            get
            {
                return string.Format("organizations/{0}/technicians", OrganizationId);
            }
        }
    }
}
