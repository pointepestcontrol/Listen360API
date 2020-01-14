using System;
using System.Collections.Generic;
using System.Text;

namespace Listen360API
{
    /// <exclude/>
    public interface IWebRequestFactory
    {
        /// <exclude/>
        IWebRequest CreateWebRequest(string url);
    }
}
