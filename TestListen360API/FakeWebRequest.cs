using System.Xml;
using Listen360API;

namespace TestListen360API
{
    class FakeWebRequest : IWebRequest
    {
        public FakeWebRequest()
        {
        }

        #region Implementation of IWebRequest

        public HttpVerb Method
        {
            set { MethodSetCalled = true; MethodSetLastValue = value; }
        }
        public HttpVerb MethodSetLastValue { get; private set; }
        public bool MethodSetCalled { get; private set; }

        public string BasicAuthorization
        {
            set { BasicAuthorizationSetCalled = true; BasicAuthorizationSetLastValue = value; }
        }
        public string BasicAuthorizationSetLastValue { get; private set; }
        public bool BasicAuthorizationSetCalled { get; private set; }

        public string RequestText
        {
            set { RequestSetCalled = true; RequestSetLastValue = value; }
        }
        public bool RequestSetCalled { get; private set; }
        public string RequestSetLastValue { get; private set; }

        public XmlDocument Response
        {
            get { ResponseGetCalled = true; return ResponseGetFakeResult; }
        }
        public bool ResponseGetCalled { get; private set; }
        public XmlDocument ResponseGetFakeResult { private get; set; }

        public string ResponseText
        {
            get { ResponseTextGetCalled = true; return ResponseTextGetFakeResult; }
        }
        public bool ResponseTextGetCalled { get; private set; }
        public string ResponseTextGetFakeResult { private get; set; }

        public string Location { get; set; }

        #endregion
    }
}