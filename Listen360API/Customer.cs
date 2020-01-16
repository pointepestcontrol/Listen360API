using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents a customer of a <see cref="Franchise"/>.
    /// </summary>
    public class Customer : ModelBase, ICustomer
    {
        /// <exclude/>
        private protected Customer()
        {

        }
        /// <summary>
        /// Initializes a new customer under a specific organization.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        public Customer(IListen360 listen360)
            : base(listen360)
        {
        }

        /// <summary>
        /// Initializes a new customer under a specific organization.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="organizationId">The organization's unique identifier.</param>
        public Customer(IListen360 listen360, long organizationId) : base(listen360)
        {
            _organizationId = organizationId;
        }

        /// <exclude/>
        public Customer(IListen360 listen360, XmlNode node)
            : base(listen360, node)
        {
        }

        #region Attributes

        long? _organizationId;

        /// <inheritdoc/>
        public long OrganizationId
        {
            get
            {
                if (!_organizationId.HasValue)
                {
                    _organizationId = (long)ReadAttribute("organization-id");
                }
                return _organizationId.Value;
            }
        }

        /// <inheritdoc/>
        public string Title
        {
            get
            {
                return (string)ReadAttribute("title");
            }
            set
            {
                WriteAttribute("title", value);
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
        public string NetPromoterLabel
        {
            get
            {
                return (string)ReadAttribute("net-promoter-label");
            }
        }

        /// <inheritdoc/>
        public string CompanyName
        {
            get
            {
                return (string)ReadAttribute("company-name");
            }
            set
            {
                WriteAttribute("company-name", value);
            }
        }

        /// <inheritdoc/>
        public string Email
        {
            get
            {
                return (string)ReadAttribute("work-email");
            }
            set
            {
                WriteAttribute("work-email", value);
            }
        }

        /// <inheritdoc/>
        public string WorkPhoneNumber
        {
            get
            {
                return (string)ReadAttribute("work-phone-number");
            }
            set
            {
                WriteAttribute("work-phone-number", value);
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
        public string HomePhoneNumber
        {
            get
            {
                return (string)ReadAttribute("home-phone-number");
            }
            set
            {
                WriteAttribute("home-phone-number", value);
            }
        }

        /// <inheritdoc/>
        public string StreetAddress
        {
            get
            {
                return (string)ReadAttribute("work-street");
            }
            set
            {
                WriteAttribute("work-street", value);
            }
        }

        /// <inheritdoc/>
        public string City
        {
            get
            {
                return (string)ReadAttribute("work-city");
            }
            set
            {
                WriteAttribute("work-city", value);
            }
        }

        /// <inheritdoc/>
        public string Region
        {
            get
            {
                return (string)ReadAttribute("work-region");
            }
            set
            {
                WriteAttribute("work-region", value);
            }
        }

        /// <inheritdoc/>
        public string PostalCode
        {
            get
            {
                return (string)ReadAttribute("work-postal-code");
            }
            set
            {
                WriteAttribute("work-postal-code", value);
            }
        }

        /// <inheritdoc/>
        public string Country
        {
            get
            {
                return (string)ReadAttribute("work-country");
            }
            set
            {
                WriteAttribute("work-country", value);
            }
        }

        /// <inheritdoc/>
        public bool? PermitsContact
        {
            get
            {
                return (bool?)ReadAttribute("permits-contact");
            }
        }

        /// <inheritdoc/>
        public bool? LastSurveyBounced
        {
            get
            {
                return (bool?)ReadAttribute("last-survey-bounced");
            }
        }

        /// <inheritdoc/>
        public void SendSurveyInvite()
        {
            string requestPath = string.Format("{0}/send_survey_invite", Path);
            Listen360.GetRequestResponseElement(requestPath, null, HttpVerb.Post);
        }

        #endregion

        /// <exclude/>
        public override string CollectionPath
        {
            get
            {
                return string.Format("organizations/{0}/customers", OrganizationId);
            }
        }
    }
}
