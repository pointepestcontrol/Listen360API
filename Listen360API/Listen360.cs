using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// Represents the remote service and provides a jumping-off point for traversing and manipulating the model hierarchy.
    /// </summary>
    /// <example>
    /// This sample shows you how to use the Listen360 class to create a <see cref="Job"/> under a specific <see cref="Franchise"/>.
    /// <code>
    /// // Obtain a reference to the remote service.
    /// Listen360API.Listen360 listen360 = Listen360API.Listen360.GetInstance("https://app.listen360.com", "ABC123");
    ///
    /// // Obtain the parent franchisor.
    /// Listen360API.IFranchisor franchisor = (Listen360API.IFranchisor)listen360.GetOrganizationById(1000);
    ///
    /// // Obtain the franchise using its unique reference.
    /// Listen360API.IFranchise franchise = (Listen360API.IFranchise)franchisor.FindDescendentByReference("ATLANTA-1");
    ///
    /// // Obtain the customer using the franchise's own customer reference.
    /// Listen360API.ICustomer customer = franchise.FindCustomerByReference("C00236");
    ///
    /// // Create a new job for the customer.
    /// Listen360API.Job job = new Listen360API.Job(listen360, franchise.Id.Value, customer.Id.Value);
    ///
    /// // Add a unique reference, an invoice number in this case.
    /// job.Reference = "INV000234";
    ///
    /// // Set the value of the job.
    /// job.Value = 100;
    ///
    /// // The job was completed today.
    /// job.CompletedAt = DateTime.Now;
    ///
    /// // Set a custom attribute, in this case it's the type of service performed.
    /// job[1] = "Deep Clean"; // Custom attribute assignment.
    ///
    /// // Save the job. A survey will be sent automatically.
    /// job.Save();
    /// </code>
    /// </example>
    public class Listen360 : IListen360
    {
        private string _authorization;
        private IWebRequestFactory _webRequestFactory;

        private Listen360()
        {
        }

        /// <summary>
        /// Initializes a new instance for a given user.
        /// </summary>
        /// <param name="serviceUrl">The base URL for the remote service.</param>
        /// <param name="token">The user's API token.</param>
        /// <returns>The initialized instance.</returns>
        public static Listen360 GetInstance(string serviceUrl, string token)
        {
            return GetInstance(serviceUrl, token, new ProductionWebRequestFactory());
        }

        /// <exclude/>
        public static Listen360 GetInstance(string serviceUrl, string token, IWebRequestFactory factory)
        {
            ConfigureHttpClient();
            return new Listen360
            {
                ServiceURL = serviceUrl,
                Token = token,
                _authorization = GetBasicAuthentication(token),
                _webRequestFactory = factory
            };
        }

        /// <summary>
        /// Configure the HTTP stack for compatibility with the Listen360 API, which challenges clients to authenticate
        /// and requires the use of TLS.
        /// </summary>
        /// <remarks>
        /// Windows 7 changed the default HTTP client behaviour. This method ensures behaviour is consistent with previous versions of
        /// Windows. It is automatically called within <see cref="GetInstance(string, string)"/>.
        /// </remarks>
        public static void ConfigureHttpClient()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        /// <inheritdoc/>
        public string ServiceURL { get; private set; }

        /// <inheritdoc/>
        public string Token { get; private set; }

        /// <exclude/>
        public IWebRequestFactory WebRequestFactory { get; private set; }

        /// <inheritdoc/>
        public IOrganization[] GetEntryPoints()
        {
            return GetEntryPoints(1);
        }

        /// <inheritdoc/>
        public IOrganization[] GetEntryPoints(int page)
        {
            string requestPath = string.Format("organizations?page={0}", page);
            return (IOrganization[])((ArrayList)ModelBase.Deserialize(this, GetRequestResponseElement(requestPath))).ToArray(typeof(IOrganization));
        }

        /// <inheritdoc/>
        public IOrganization GetOrganizationById(long id)
        {
            IOrganization org = (IOrganization)ModelBase.Deserialize(this, GetRequestResponseElement(string.Format("organizations/{0}", id)));
            return org;
        }

        /// <inheritdoc/>
        public XmlElement GetRequestResponseElement(string requestPath)
        {
            return GetRequestResponseElement(requestPath, string.Empty);
        }

        /// <inheritdoc/>
        public XmlElement GetRequestResponseElement(string requestPath, string data)
        {
            return GetRequestResponseElement(requestPath, data, HttpVerb.Get);
        }

        /// <inheritdoc/>
        public XmlElement GetRequestResponseElement(string requestPath, string data, HttpVerb verb)
        {
            IWebRequest request = _webRequestFactory.CreateWebRequest(string.Format("{0}/{1}", ServiceURL, requestPath));
            request.Method = verb;
            request.BasicAuthorization = _authorization;
            if (verb != HttpVerb.Get)
            {
                request.RequestText = data;
            }
            return (request.Response == null) ? null : request.Response.DocumentElement;
        }

        /// <inheritdoc/>
        public string DeleteRequest(string requestPath)
        {
            IWebRequest request = _webRequestFactory.CreateWebRequest(string.Format("{0}/{1}", ServiceURL, requestPath));
            request.Method = HttpVerb.Delete;
            request.BasicAuthorization = _authorization;
            request.RequestText = string.Empty;
            return request.ResponseText;
        }

        /// <exclude/>
        public static string GetBasicAuthentication(string token)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("{0}:{1}", token, "X")));
        }
    }
}
