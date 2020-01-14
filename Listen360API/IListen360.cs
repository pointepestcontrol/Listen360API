using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Provides the interface for implementation of the <see cref="Listen360"/> class.
    /// </summary>
    public interface IListen360
    {
        /// <exclude/>
        XmlElement GetRequestResponseElement(string requestPath);

        /// <exclude/>
        XmlElement GetRequestResponseElement(string requestPath, string data);

        /// <exclude/>
        XmlElement GetRequestResponseElement(string requestPath, string data, HttpVerb verb);

        /// <exclude/>
        string DeleteRequest(string requestPath);

        /// <summary>
        /// Gets the first page of up to 100 possible entry points.
        /// </summary>
        /// <returns>The organizations on the page.</returns>
        IOrganization[] GetEntryPoints();

        /// <summary>
        /// Gets a specific page of up to 100 entry points.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <returns>The organizations on the page.</returns>
        IOrganization[] GetEntryPoints(int page);

        /// <summary>
        /// Gets a specific organization.
        /// </summary>
        /// <param name="id">The organization's unique identifier.</param>
        /// <returns>The specified organization.</returns>
        IOrganization GetOrganizationById(long id);
    }
}
