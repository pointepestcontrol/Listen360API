using System;
using System.Xml;

namespace Listen360API
{
    /// <exclude/>
    public enum HttpVerb
    {
        /// <exclude/>        
        Get,
        /// <exclude/>
        Post,
        /// <exclude/>
        Put,
        /// <exclude/>
        Delete
    };

    /// <exclude/>
	public interface IWebRequest
	{
        /// <exclude/>
		HttpVerb Method { set; }

        /// <exclude/>
		string BasicAuthorization { set; }

        /// <exclude/>
		string RequestText { set;  }

        /// <exclude/>
		XmlDocument Response { get; }

        /// <exclude/>
		string ResponseText { get; }

        /// <exclude/>
		string Location { get; }
	}
}
