using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents a job or service performed by a <see cref="Franchise"/> on behalf of a <see cref="Customer"/>.
    /// </summary>
    public class Job : ModelBase, IJob
    {
        /// <exclude/>
        private protected Job()
        {

        }
        /// <summary>
        /// Initializes a new job.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        public Job(IListen360 listen360)
            : base(listen360)
        {
        }

        /// <summary>
        /// Initializes a new job for a specific customer under a specific organization.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="organizationId">The organization's unique identifier.</param>
        /// <param name="customerId">The customer's unique identifier.</param>
        public Job(IListen360 listen360, long organizationId, long customerId) : base(listen360)
        {
            _organizationId = organizationId;
            _customerId = customerId;
            WriteAttribute("customer-id", customerId);
        }

        /// <exlude/>
        public Job(IListen360 listen360, XmlNode node) : base(listen360, node)
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

        long? _customerId;

        /// <inheritdoc/>
        public long CustomerId
        {
            get
            {
                if (!_customerId.HasValue)
                {
                    _customerId = (long)ReadAttribute("customer-id");
                }
                return _customerId.Value;
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
        public string Currency
        {
            get
            {
                return (string)ReadAttribute("currency");
            }
            set
            {
                WriteAttribute("currency", value);
            }
        }

        /// <inheritdoc/>
        public decimal? Value
        {
            get
            {
                return (decimal?)ReadAttribute("value");
            }
            set
            {
                WriteAttribute("value", value);
            }
        }

        /// <inheritdoc/>
        public DateTime? CommencedAt
        {
            get
            {
                return (DateTime?)ReadAttribute("commenced-at");
            }
            set
            {
                WriteAttribute("commenced-at", value);
            }
        }

        /// <inheritdoc/>
        public DateTime? CompletedAt
        {
            get
            {
                return (DateTime?)ReadAttribute("completed-at");
            }
            set
            {
                WriteAttribute("completed-at", value);
            }
        }

        /// <inheritdoc/>
        public bool ForceFeedback
        {
            set
            {
                WriteAttribute("force-feedback", value);
            }
        }

        #endregion

        List<Technician> _performers;

        /// <inheritdoc/>
        public List<Technician> Performers
        {
            get
            {
                if (_performers == null)
                {
                    _performers = (List<Technician>)ReadAttribute("performers");
                    if (_performers == null) { _performers = new List<Technician>(); }
                }
                return _performers;
            }
        }

        /// <exclude/>
        public override string CollectionPath
        {
            get
            {
                return string.Format("organizations/{0}/jobs", OrganizationId);
            }
        }

        /// <exclude/>
        protected override void FinalizeSerialization(XmlDocument doc, XmlNode root)
        {
            XmlElement performersElement = doc.CreateElement("performers");
            performersElement.SetAttribute("type", "array");

            foreach (Technician performer in Performers)
            {
                XmlElement performerElement = doc.CreateElement("performer");
                performer.SerializeChanges(doc, performerElement);
                performersElement.AppendChild(performerElement);
            }

            root.AppendChild(performersElement);
            base.FinalizeSerialization(doc, root);
        }
    }
}
