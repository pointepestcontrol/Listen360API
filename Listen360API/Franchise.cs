using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents an organization that performs <see cref="Job">jobs</see> or provides services to <see cref="Customer">customers</see>.
    /// </summary>
    public class Franchise : OrganizationBase, IFranchise
    {
        /// <exclude/>
        private protected Franchise()
        {

        }
        /// <summary>
        /// Initializes a new franchise.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        public Franchise(IListen360 listen360)
            : base(listen360)
        {
        }

        /// <summary>
        /// Initializes a new franchise under a specific parent organization.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="parentId">The unique identifier of the parent organization.</param>
        public Franchise(IListen360 listen360, long parentId) : base(listen360)
        {
            ParentId = parentId;
        }

        /// <exclude/>
        public Franchise(IListen360 listen360, XmlNode node) : base(listen360, node)
        {
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

        #region Attributes

        /// <inheritdoc/>
        public int? MarketingRadiusInMiles
        {
            get
            {
                return (int?)ReadAttribute("marketing-radius-miles");
            }
            set
            {
                WriteAttribute("marketing-radius-miles", value);
            }
        }

        /// <inheritdoc/>
        public int? OperatingRadiusInMiles
        {
            get
            {
                return (int?)ReadAttribute("operating-radius-miles");
            }
            set
            {
                WriteAttribute("operating-radius-miles", value);
            }
        }

        #endregion

        #region Hierarchy

        /// <exclude/>
        public override IOrganization FindChildByReference(string reference)
        {
            throw new NotSupportedException();
        }

        /// <exclude/>
        public override IOrganization[] GetChildren()
        {
            throw new NotSupportedException();
        }

        /// <exclude/>
        public override IOrganization[] GetChildren(int page)
        {
            throw new NotSupportedException();
        }

        /// <exclude/>
        public override IOrganization FindDescendentByReference(string reference)
        {
            throw new NotSupportedException();
        }

        /// <exclude/>
        public override IOrganization[] GetDescendents()
        {
            throw new NotSupportedException();
        }

        /// <exclude/>
        public override IOrganization[] GetDescendents(int page)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Customers

        /// <inheritdoc/>
        public ICustomer GetCustomerById(long id)
        {
            string requestPath = string.Format("{0}/customers/{1}", Path, id);
            return (ICustomer) ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath));
        }

        /// <inheritdoc/>
        public ICustomer FindCustomerByReference(string reference)
        {
            string requestPath = string.Format("{0}/customers?reference={1}", Path, System.Uri.EscapeUriString(reference));
            ArrayList matches = (ArrayList)(ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath)));
            ICustomer match = (matches.Count == 0) ? null : (ICustomer)matches[0];
            return match;
        }

        /// <inheritdoc/>
        public ICustomer[] GetCustomers()
        {
            return GetCustomers(1);
        }

        /// <inheritdoc/>
        public ICustomer[] GetCustomers(int page)
        {
            string requestPath = string.Format("{0}/customers?page={1}", Path, page);
            return (ICustomer[])((ArrayList)ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath))).ToArray(typeof(ICustomer));
        }

        #endregion

        #region Technicians

        /// <inheritdoc/>
        public ITechnician GetTechnicianById(int id)
        {
            string requestPath = string.Format("{0}/technicians/{1}", Path, id);
            return (ITechnician)ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath));
        }

        /// <inheritdoc/>
        public ITechnician FindTechnicianByReference(string reference)
        {
            string requestPath = string.Format("{0}/technicians?reference={1}", Path, System.Uri.EscapeUriString(reference));
            ArrayList matches = (ArrayList)(ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath)));
            ITechnician match = (matches.Count == 0) ? null : (ITechnician)matches[0];
            return match;
        }

        /// <inheritdoc/>
        public ITechnician[] GetTechnicians()
        {
            return GetTechnicians(1);
        }

        /// <inheritdoc/>
        public ITechnician[] GetTechnicians(int page)
        {
            string requestPath = string.Format("{0}/technicians?page={1}", Path, page);
            return (ITechnician[])((ArrayList)ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath))).ToArray(typeof(ITechnician));
        }

        #endregion

        #region Jobs

        /// <inheritdoc/>
        public IJob GetJobById(int id)
        {
            string requestPath = string.Format("{0}/jobs/{1}", Path, id);
            return (IJob)ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath));
        }

        /// <inheritdoc/>
        public IJob FindJobByReference(string reference)
        {
            string requestPath = string.Format("{0}/jobs?reference={1}", Path, System.Uri.EscapeUriString(reference));
            ArrayList matches = (ArrayList)(ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath)));
            IJob match = (matches.Count == 0) ? null : (IJob)matches[0];
            return match;
        }

        /// <inheritdoc/>
        public IJob[] GetJobs()
        {
            return GetJobs(1);
        }

        /// <inheritdoc/>
        public IJob[] GetJobs(int page)
        {
            string requestPath = string.Format("{0}/jobs?page={1}", Path, page);
            return (IJob[])((ArrayList)ModelBase.Deserialize(Listen360, Listen360.GetRequestResponseElement(requestPath))).ToArray(typeof(IJob));
        }

        #endregion
    }
}
