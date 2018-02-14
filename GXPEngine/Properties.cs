using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRootAttribute("properties")]
    public class Properties
    {
        [XmlElement("property")]
        public Property[] property;
    }

    [XmlRootAttribute("property")]
    public class Property
    {
        [XmlAttribute("name")]
        public string name;
        [XmlAttribute("value")]
        public string value;
    }
}
