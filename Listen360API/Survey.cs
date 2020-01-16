using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents a survey.
    /// </summary>
    /// <example>
    /// This sample shows you how to use the Survey class to send a feedback request about a specific <see cref="Job"/>.
    /// <code>
    /// // Obtain a reference to the remote service.
    /// Listen360API.Listen360 listen360 = Listen360API.Listen360.GetInstance("https://www.listen360.net", "ABC123");
    ///
    /// // Specify the franchise requesting feedback.
    /// Listen360API.IFranchise franchise = new Listen360API.Franchise(listen360);
    /// franchise.Reference = "ATLANTA-1"; // Replace ATLANTA-1 with your primary key or reference number.
    ///
    /// // Identify the customer who should respond to the feedback request
    /// Listen360API.ICustomer customer = new Listen360API.Customer(listen360);
    /// customer.Reference = "1234"; // Your primary key or reference number.
    /// customer.FirstName = "Bob";
    /// customer.LastName = "Smith";
    /// customer.Email = "bob@smith.name";
    /// customer.WorkPhoneNumber = "111-111-1111";
    ///
    /// // Identify the job for which we are soliciting feedback.
    /// Listen360API.Job job = new Listen360API.Job(listen360);
    /// job.Reference = "INV000234"; // Your primary key or reference number.
    /// job.Value = 100;
    /// job.CompletedAt = DateTime.Now;
    /// job[1] = "Deep Clean"; // Custom attribute assignment.
    ///
    /// // Submit the survey.
    /// Listen360API.Survey survey = new Listen360API.Survey(listen360, 1000, franchise, customer, job);
    /// survey.Save();
    /// </code>
    /// </example>
    public class Survey : ModelBase
    {
        /// <exclude/>
        private protected Survey()
        {

        }
        /// <summary>
        /// Initializes a new survey under a specific organization.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="organizationId">The root organization's unique identifier.</param>
        /// <param name="surveyOrg">The organization requesting feedback.</param>
        /// <param name="customer">The customer who should respond to the feedback request.</param>
        /// <remarks>The API will take care of creating or updating customer and job requests. It will also take feedback frequency caps into consideration before creating a survey.</remarks>
        public Survey(IListen360 listen360, long organizationId, IOrganization surveyOrg, ICustomer customer)
            : this(listen360, organizationId, surveyOrg, customer, null)
        {
        }

        /// <summary>
        /// Initializes a new survey under a specific organization.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="organizationId">The root organization's unique identifier.</param>
        /// <param name="surveyOrg">The organization requesting feedback.</param>
        /// <param name="customer">The customer who should respond to the feedback request.</param>
        /// <param name="job">The job about which we are asking for feedback.</param>
        /// <remarks>The API will take care of creating or updating customer and job requests. It will also take feedback frequency caps into consideration before creating a survey.</remarks>
        public Survey(IListen360 listen360, long organizationId, IOrganization surveyOrg, ICustomer customer, IJob job) : base(listen360)
        {
            _organizationId = organizationId;
            WriteAttribute("organization", surveyOrg);
            WriteAttribute("customer", customer);
            WriteAttribute("job", job);
        }

        /// <exclude/>
        public Survey(IListen360 listen360, XmlNode node)
            : base(listen360, node)
        {
        }

        #region Attributes

        long? _organizationId;

        /// <inheritdoc/>
        public string Channel
        {
            get
            {
                return (string)ReadAttribute("channel");
            }
            set
            {
                WriteAttribute("channel", value);
            }
        }

        #endregion

        /// <exclude/>
        public override string CollectionPath
        {
            get
            {
                return string.Format("organizations/{0}/surveys", _organizationId);
            }
        }
    }
}
