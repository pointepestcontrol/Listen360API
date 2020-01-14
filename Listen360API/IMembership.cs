using System;

namespace Listen360API
{
    /// <summary>
    /// Provides the base interface for implementation of the <see cref="Membership"/> class.
    /// </summary>
    public interface IMembership : IModel
    {
        /// <summary>
        /// Gets the unique identifier of the organization.
        /// </summary>
        long? OrganizationId { get; }

        /// <summary>
        /// Gets the membership's status.
        /// </summary>
        string Status { get; }

        /// <summary>
        /// Gets or sets the membership's role.
        /// </summary>
        string Role { get; set; }

        /// <summary>
        /// Gets or set's the member's first name.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the member's last name.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the member's email address.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or set's the member's mobile phone number.
        /// </summary>
        string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the member's country in <a href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</a> format.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Gets the date and time at which the membership invite was accepted by the target user.
        /// </summary>
        DateTime? AcceptedAt { get; }

        /// <summary>
        /// Gets the date and time at which the membership was revoked.
        /// </summary>
        DateTime? RevokedAt { get; }
    }
}
