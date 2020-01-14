using System;
using System.Collections.Generic;
using System.Text;

namespace Listen360API
{
    /// <summary>
    /// Provides the interface for implementation of the <see cref="OrganizationBase"/> class.
    /// </summary>
    public interface IOrganization : IModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the root organization.
        /// </summary>
        int RootId { get; }

        /// <summary>
        /// Gets or sets the unique identifier of the parent organization.
        /// </summary>
        long? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the unique value used by your business to identify the organization.
        /// </summary>
        string Reference { get; set; }

        /// <summary>
        /// Gets or sets the name used by your business to identify the organization.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the name used by customers to identify the organization.
        /// </summary>
        string ExternalName { get; set; }

        /// <summary>
        /// Gets or sets the status of the organization.
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets the organization's primary email address.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the organization's street address.
        /// </summary>
        string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the organization's city.
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// Gets or sets the organization's region, province or state.
        /// </summary>
        string Region { get; set; }

        /// <summary>
        /// Gets or sets the organization's postal or ZIP code.
        /// </summary>
        string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the organization's country in <a href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</a> format.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Gets or sets the organization's time zone.
        /// </summary>
        string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the organization's primary phone number.
        /// </summary>
        string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the organization's primary web site.
        /// </summary>
        string Website { get; set; }

        /// <summary>
        /// Finds a specific child organization using the unique value used by your business to identify the organization.
        /// </summary>
        /// <param name="reference">Your unique organization reference.</param>
        /// <returns>The corresponding organization if found; otherwise <b>null</b>.</returns>
        IOrganization FindChildByReference(string reference);

        /// <summary>
        /// Gets the first page of up to 100 child organizations.
        /// </summary>
        /// <returns>The organizations on the page.</returns>
        IOrganization[] GetChildren();

        /// <summary>
        /// Gets a specific page of up to 100 child organizations.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <returns>The organizations on the page.</returns>
        IOrganization[] GetChildren(int page);

        /// <summary>
        /// Finds a specific child organization using the unique value used by your business to identify the organization.
        /// </summary>
        /// <param name="reference">Your unique organization reference.</param>
        /// <returns>The corresponding organization if found; otherwise <b>null</b>.</returns>
        IOrganization FindDescendentByReference(string reference);

        /// <summary>
        /// Gets the first page of up to 100 descendent organizations.
        /// </summary>
        /// <returns>The organizations on the page.</returns>
        IOrganization[] GetDescendents();

        /// <summary>
        /// Gets a specific page of up to 100 descendent organizations.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <returns>The organizations on the page.</returns>
        IOrganization[] GetDescendents(int page);

        /// <summary>
        /// Finds a specific membership using the member's email address.
        /// </summary>
        /// <param name="email">The member's email address.</param>
        /// <returns>The corresponding membership if found; otherwise <b>null</b>.</returns>
        IMembership FindMembershipByEmail(string email);

        /// <summary>
        /// Gets the first page of up to 100 memberships.
        /// </summary>
        /// <returns>The memberships on the page.</returns>
        IMembership[] GetMemberships();

        /// <summary>
        /// Gets a specific page of up to 100 memberships.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <returns>The memberships on the page.</returns>
        IMembership[] GetMemberships(int page);
    }
}
