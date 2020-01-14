using Listen360API;

namespace TestListen360API
{
    public class FakeWebRequestFactory : IWebRequestFactory
    {
        public FakeWebRequestFactory()
        {
        }

        #region Implementation of IWebRequestFactory

        public IWebRequest CreateWebRequest(string url)
        {
            CreateWebRequestLastUrl = url;
            CreateWebRequestCalled = true;
            return CreateWebRequestFakeResult;
        }

        public bool CreateWebRequestCalled { get; private set; }
        public string CreateWebRequestLastUrl { get; private set; }
        public IWebRequest CreateWebRequestFakeResult { private get; set; }

        #endregion
    }
}