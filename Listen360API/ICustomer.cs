using System;
using System.Collections.Generic;
using System.Text;

namespace Listen360API
{
    /// <summary>
    /// Provides the base interface for implementation of the <see cref="Customer"/> class.
    /// </summary>
    public interface ICustomer : IModel
    {
        /// <summary>
        /// Gets the ID of the organization to which the customer belongs.
        /// </summary>
        long OrganizationId { get; }

        /// <summary>
        /// Gets or sets the customer's title.
        /// </summary>
        /// <example><code>customer.Title = "Mrs"</code></example>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the customer's last name.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the unique value used by your business to identify the customer.
        /// </summary>
        string Reference { get; set; }

        /// <summary>
        /// Gets or sets the customer's status.
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets the customer's Net Promoter label.
        /// </summary>
        string NetPromoterLabel { get; }

        /// <summary>
        /// Gets or sets the customer's company name.
        /// </summary>
        string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the customer's email address, used for the purposes of gathering feedback.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the customer's work phone number.
        /// </summary>
        string WorkPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer's mobile phone number.
        /// </summary>
        string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer's home phone number.
        /// </summary>
        string HomePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer's street address.
        /// </summary>
        string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the customer's city.
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// Gets or sets the customer's province, state or region.
        /// </summary>
        string Region { get; set; }

        /// <summary>
        /// Gets or sets the customer's postal or ZIP code.
        /// </summary>
        string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the customer's country in <a href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</a> format.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the customer permits contact.
        /// </summary>
        bool? PermitsContact { get; }

        /// <summary>
        /// Gets a value indicating whether or not the last survey email sent to the customer bounced.
        /// </summary>
        bool? LastSurveyBounced { get; }

        /// <summary>
        /// Forces delivery of a survey invite to the customer.
        /// </summary>
        /// <remarks>It's generally better to record <see cref="Job">jobs</see> and allow the remote service to determine when it's appropriate to send a survey invite.</remarks>
        void SendSurveyInvite();
    }
}
