using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Listen360API
{
    /// <exclude/>
    public class ProductionWebRequestFactory : IWebRequestFactory
    {
        /// <exclude/>
        public ProductionWebRequestFactory()
        {
            #if DEBUG
            if (ServicePointManager.ServerCertificateValidationCallback == null)
            {
                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(SkipValidateRemoteCertificate);
            }
            #endif
        }

        /// <exclude/>
        private static bool SkipValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }

        /// <exclude/>
        public IWebRequest CreateWebRequest(string url)
        {
            return ProductionWebRequest.GetInstance(url);
        }
    }
}
