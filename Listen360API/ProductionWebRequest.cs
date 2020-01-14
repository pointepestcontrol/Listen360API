using System.IO;
using System.Net;
using System.Xml;

namespace Listen360API
{
    class ProductionWebRequest : IWebRequest
    {
        private HttpWebRequest _request;
        private XmlDocument _response;

        public static ProductionWebRequest GetInstance(string url)
        {
            return new ProductionWebRequest(url);
        }

        private ProductionWebRequest(string url)
        {
            _request = (HttpWebRequest)WebRequest.Create(url);
            _request.ContentType = "text/xml";
            _request.Accept = "text/xml";
            _request.ServicePoint.Expect100Continue = false;
        }

        #region Implementation of IWebRequest
        public HttpVerb Method
        {
            set { _request.Method = value.ToString().ToUpper(); }
        }

        public string BasicAuthorization
        {
            set { _request.Headers.Add("Authorization", string.Format("Basic {0}", value)); }
        }

        public string RequestText
        {
            set
            {
                using (StreamWriter writer = new StreamWriter(_request.GetRequestStream()))
                {
                    writer.WriteLine(value);
                }
            }
        }

        public XmlDocument Response
        {
            get
            {
                if (_response == null && ResponseText.Trim() != string.Empty)
                {
                    _response = new XmlDocument();
                    _response.LoadXml(ResponseText);
                }
                return _response;
            }
        }

        public string Location
        {
            get
            {
                if (_location == null)
                {
                    GetResponse();
                    if (_location == null)
                    {
                        _location = string.Empty;
                    }
                }
                return _location;
            }
        }

        #endregion

        public string ResponseText
        {
            get
            {
                if (_responseText == null)
                {
                    GetResponse();
                }
                return _responseText;
            }
        }

        private void GetResponse()
        {
            WebResponse response = null;
            try
            {
                response = _request.GetResponse();
            }
            catch (WebException e)
            {
                HttpWebResponse errorResponse = e.Response as HttpWebResponse;
                if (errorResponse != null && (int)errorResponse.StatusCode == 422)
                {
                    response = errorResponse;
                }
                else
                {
                    throw e;
                }
            }
            _location = response.Headers["Location"];
            if (_location != null)
            {
                _location = (new System.Uri(_location)).AbsolutePath;
            }
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                _responseText = reader.ReadToEnd();
            }
        }

        private string _responseText;
        private string _location;
    }
}