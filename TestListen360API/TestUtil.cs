using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Listen360API;

namespace TestListen360API
{
    class TestUtil
    {
        public class TestContext
        {
            public string Url;
            public string Token;
            public string Authentication;
            public IListen360 Listen360;
            public FakeWebRequestFactory WebRequestFactory;
            public FakeWebRequest WebRequest;

            public TestContext()
            {
                this.Url = "https://foo.bar";
                this.Token = "abc123";
                this.Authentication = "YWJjMTIzOlg=";
                this.WebRequest = new FakeWebRequest();
                this.WebRequestFactory = new FakeWebRequestFactory();
                this.WebRequestFactory.CreateWebRequestFakeResult = this.WebRequest;
                this.Listen360 = Listen360API.Listen360.GetInstance(Url, Token, WebRequestFactory);
            }

            public void SetResponseXml(string xml)
            {
                XmlDocument response = new XmlDocument();
                response.LoadXml(xml);
                this.WebRequest.ResponseTextGetFakeResult = xml;
                this.WebRequest.ResponseGetFakeResult = response;
            }
        }

        public static XmlElement GetElementForXml(string xml)
        {
            return GetXmlDocument(xml).DocumentElement;
        }

        public static XmlDocument GetXmlDocument(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            return document;
        }

        public static void SetupResponseXml(FakeWebRequest webRequest, string xml)
        {
            XmlDocument response = new XmlDocument();
            response.LoadXml(xml);
            webRequest.ResponseTextGetFakeResult = xml;
            webRequest.ResponseGetFakeResult = response;
        }
    }
}
