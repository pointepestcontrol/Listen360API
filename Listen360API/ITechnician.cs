using System;

namespace Listen360API
{
    /// <summary>
    /// Provides the interface for implementation of the <see cref="Technician"/> class.
    /// </summary>
    public interface ITechnician : IModel
    {
        /// <summary>
        /// Gets the unique identifier of the organization to which the technician reports.
        /// </summary>
        long? OrganizationId { get; }

        /// <summary>
        /// Gets or sets the unique value used by your business to identify the technician.
        /// </summary>
        string Reference { get; set; }

        /// <summary>
        /// Gets or sets the technician's status.
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets the technician's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the technician's email address.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the technician's mobile phone number.
        /// </summary>
        string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the technician's country in <a href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</a> format.
        /// </summary>
        string Country { get; set; }
    }
}
