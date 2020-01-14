using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Listen360API
{
    /// <summary>
    /// An abstract base class for all model implementations. Provides facilities for loading data, tracking and persisting changes.
    /// </summary>
    public abstract class ModelBase : IModel
    {
        /// <exclude/>
        protected Hashtable _attributes = new Hashtable();

        /// <exclude/>
        protected Hashtable _changedAttributes = new Hashtable();

        /// <exclude/>
        protected string[] _errors = new string[0];

        /// <summary>
        /// Initializes a new model instance.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        public ModelBase(IListen360 listen360)
        {
            Listen360 = listen360;
        }

        /// <summary>
        /// Initializes an existing model instance and loads data from the specified XML fragment.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="node">An XML fragment obtained from the remote service.</param>
        public ModelBase(IListen360 listen360, XmlNode node)
		{
            Listen360 = listen360;
            LoadXml(node);
		}

        /// <summary>
        /// Deserializes an XML graph obtained from the remote service into the corresponding model classes.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="node">An XML fragment obtained from the remote service.</param>
        /// <returns>An object graph represenation.</returns>
        public static object Deserialize(IListen360 listen360, XmlNode node)
        {
            object instance = null;
            string typeName = null;

            if (node.Attributes["type"] != null)
            {
                typeName = node.Attributes["type"].Value;
            }
            else
            {
                XmlNode typeNode = node.SelectSingleNode("type");
                typeName = (typeNode == null) ? node.Name : typeNode.InnerText;
            }

            switch (typeName.ToLower())
            {
                case "array":
                    ArrayList container = new ArrayList(node.ChildNodes.Count);
                    Deserialize(listen360, node, container);
                    instance = container;
                    break;
                case "custom-attribute-definition":
                    instance = new CustomAttributeDefinition(listen360, node);
                    break;
                case "customer":
                    instance = new Customer(listen360, node);
                    break;
                case "division":
                    instance = new Division(listen360, node);
                    break;
                case "franchise":
                    instance = new Franchise(listen360, node);
                    break;
                case "franchisor":
                    instance = new Franchisor(listen360, node);
                    break;
                case "job":
                    instance = new Job(listen360, node);
                    break;
                case "membership":
                    instance = new Membership(listen360, node);
                    break;
                case "technician":
                    instance = new Technician(listen360, node);
                    break;
            }

            return instance;
        }

        /// <summary>
        /// Deserializes an "array" element obtained from the remote service into a collection of model objects.
        /// </summary>
        /// <param name="listen360">The remote service.</param>
        /// <param name="node">An XML fragment obtained from the remote service.</param>
        /// <param name="container">The collection that will contain the model objects/</param>
        /// <returns>An list of model objects.</returns>
        public static object Deserialize(IListen360 listen360, XmlNode node, ArrayList container)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType != XmlNodeType.Element) { continue; }
                container.Add(Deserialize(listen360, childNode));
            }
            return container;
        }

        /// <summary>
        /// Loads an XML fragment obtained from the remote service into the model instance.
        /// </summary>
        /// <param name="node">The XML fragment representing the model instance.</param>
        protected void LoadXml(XmlNode node)
        {
            ClearErrors();

            if (node == null) { return; }

            if (node.Name == "errors")
            {
                ArrayList errors = new ArrayList(node.ChildNodes.Count);

                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.NodeType != XmlNodeType.Element) continue;
                    errors.Add(childNode.InnerText);
                }

                _errors = (string[])errors.ToArray(typeof(string));
                return;
            }

            _attributes.Clear();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType != XmlNodeType.Element) continue;

                if (childNode.Attributes["nil"] != null && childNode.Attributes["nil"].Value == "true") { continue; }

                string rubyType = (childNode.Attributes["type"] == null) ? null : childNode.Attributes["type"].Value;
                string stringValue = childNode.InnerText;
                object nativeValue = null;

                switch (rubyType)
                {
                    case "integer":
                        var name = childNode.Name.ToLower();
                        if (name =="id" || name == "parent-id" || name =="organization-id" || name == "customer-id")
                        {
                          long tempLong;
                          nativeValue = Int64.TryParse(stringValue, out tempLong) ? (long?)tempLong : (long?)null;
                        }
                        else
                        {
                          int tempInt;
                          nativeValue = Int32.TryParse(stringValue, out tempInt) ? (int?)tempInt : (int?)null;
                        }
                        break;
                    case "decimal":
                        decimal tempDecimal;
                        nativeValue = Decimal.TryParse(stringValue, out tempDecimal) ? (decimal?)tempDecimal : (decimal?)null;
                        break;
                    case "datetime":
                        DateTime tempDateTime;
                        nativeValue = DateTime.TryParse(stringValue, out tempDateTime) ? (DateTime?)tempDateTime : (DateTime?)null;
                        break;
                    case "boolean":
                        bool tempBool;
                        nativeValue = bool.TryParse(stringValue, out tempBool) ? (bool?)tempBool : (bool?)null;
                        break;
                    case "array":
                        ArrayList list = new ArrayList();
                        ModelBase.Deserialize(Listen360, childNode, list);
                        nativeValue = list;
                        break;
                    case null:
                        nativeValue = stringValue;
                        break;
                    default:
                        ModelBase.Deserialize(Listen360, childNode);
                        break;
                }

                _attributes[childNode.Name] = nativeValue;
            }
        }

        /// <summary>
        /// Gets or sets the remote service.
        /// </summary>
		protected IListen360 Listen360 { get; set; }

        /// <inheritdoc/>
        public bool IsValid
        {
            get
            {
                return _errors.Length == 0;
            }
        }

        /// <inheritdoc/>
        public string[] Errors
        {
            get { return (string[])_errors.Clone(); }
        }

        /// <inheritdoc/>
        protected void ClearErrors()
        {
            _errors = new string[0];
        }

        /// <inheritdoc/>
        public bool HasChanged
        {
            get
            {
                return _changedAttributes.Count != 0;
            }
        }

        /// <inheritdoc/>
        public Hashtable Changes
        {
            get
            {
                return (Hashtable)_changedAttributes.Clone();
            }
        }

        /// <summary>
        /// Reads an attribute value.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>The corresponding value.</returns>
        protected object ReadAttribute(string attributeName)
        {
            return _attributes[attributeName];
        }

        /// <summary>
        /// Writes a value for an attribute and tracks the change.
        /// </summary>
        /// <param name="attributeName">The name of the attribute to write.</param>
        /// <param name="value">The desired value.</param>
        protected void WriteAttribute(string attributeName, object value)
        {
            _attributes[attributeName] = value;
            _changedAttributes[attributeName] = value;
        }

        /// <summary>
        /// Discards tracked changes.
        /// </summary>
        /// <remarks>Note that this method does not revert model attributes to their initial state.</remarks>
        protected void ClearChanges()
        {
            _changedAttributes.Clear();
        }

        /// <inheritdoc/>
        public string SerializeChanges()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement(GetType().Name.ToLower());
            SerializeChanges(doc, root);
            doc.AppendChild(root);
            return doc.OuterXml;
        }

        /// <exclude/>
        public void SerializeChanges(XmlDocument doc, XmlNode root)
        {
            if (this.Id.HasValue)
            {
                XmlElement idElement = doc.CreateElement("id");
                idElement.InnerText = Id.ToString();
                root.AppendChild(idElement);
            }

            foreach (string key in _changedAttributes.Keys)
            {
                XmlElement element = doc.CreateElement(key);
                object attrValue = _changedAttributes[key];

                if (attrValue == null)
                {
                    element.SetAttribute("nil", "true");
                }
                else
                {
                    if (typeof(IModel).IsAssignableFrom(attrValue.GetType()))
                    {
                        ((IModel)attrValue).SerializeChanges(doc, element);
                    }
                    else
                    {
                        element.InnerText = attrValue.ToString();
                    }
                }

                root.AppendChild(element);
            }

            FinalizeSerialization(doc, root);
        }

        /// <summary>
        /// Provides a hook to manipulate serialized XML in derived classes.
        /// </summary>
        /// <param name="doc">The XML document being constructed.</param>
        /// <param name="root">The root node representing the model.</param>
        protected virtual void FinalizeSerialization(XmlDocument doc, XmlNode root)
        {
        }

        /// <inheritdoc/>
        public long? Id
        {
            get { return (long?)ReadAttribute("id"); }
        }

        /// <inheritdoc/>
        public DateTime? CreatedAt
        {
            get { return (DateTime?)ReadAttribute("created-at"); }
        }

        /// <inheritdoc/>
        public DateTime? UpdatedAt
        {
            get { return (DateTime?)ReadAttribute("updated-at"); }
        }

        /// <inheritdoc/>
        public virtual string this[int ordinal]
        {
            get
            {
                return (string)ReadAttribute(CustomChoiceLabelAttributeName(ordinal));
            }
            set
            {
                WriteAttribute(CustomChoiceLabelAttributeName(ordinal), value);
            }
        }

        /// <inheritdoc/>
        public string this[ICustomAttributeDefinition attributeDefinition]
        {
            get
            {
                return this[attributeDefinition.Ordinal];
            }
            set
            {
                this[attributeDefinition.Ordinal] = value;
            }
        }

        /// <summary>
        /// Gets the name of the attribute used by the remote service to represent a specific custom choice label.
        /// </summary>
        /// <param name="ordinal">The ordinal of the custom attribute.</param>
        /// <returns>The label used by the remote service.</returns>
        protected string CustomChoiceLabelAttributeName(int ordinal)
        {
            if (ordinal < 1 || ordinal > 10) { throw new ArgumentOutOfRangeException("index"); }
            return string.Format("custom-choice-{0}-label", ordinal);
        }

        /// <inheritdoc/>
        public bool IsNewRecord
        {
            get { return !Id.HasValue; }
        }

        /// <summary>
        /// Gets the path representing the model at the remote service.
        /// </summary>
        public string Path
        {
            get { return IsNewRecord ? null : string.Format("{0}/{1}", CollectionPath, Id); }
        }

        /// <summary>
        /// Gets the path used to create new models at the remote service.
        /// </summary>
        public virtual string CreatePath
        {
            get { return CollectionPath; }
        }

        /// <summary>
        /// Gets the path used to update the model at the remote service.
        /// </summary>
        public virtual string UpdatePath
        {
            get { return Path; }
        }

        /// <summary>
        /// Gets the path used to delete the model at the remote service.
        /// </summary>
        public virtual string DeletePath
        {
            get { return Path; }
        }

        /// <summary>
        /// Gets the base path used to represent a collection of models at the remote service.
        /// </summary>
        public abstract string CollectionPath
        {
            get;
        }

        /// <inheritdoc/>
        public virtual bool Save()
        {
            string diff = SerializeChanges();
            ClearErrors();

            if (IsNewRecord)
            {
                XmlNode node = Listen360.GetRequestResponseElement(CreatePath, diff, HttpVerb.Post);
                LoadXml(node);
            }
            else
            {
                XmlNode node = Listen360.GetRequestResponseElement(UpdatePath, diff, HttpVerb.Put);
                if (node != null)
                {
                    LoadXml(node);
                }
            }

            if (IsValid)
            {
                ClearChanges();
            }

            return IsValid;
        }

        /// <inheritdoc/>
        public virtual void Reload()
        {
            ClearChanges();
            LoadXml(Listen360.GetRequestResponseElement(Path));
        }

        /// <inheritdoc/>
        public virtual void Delete()
        {
            Listen360.DeleteRequest(DeletePath);
        }
    }
}
