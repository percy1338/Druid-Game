using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRootAttribute("objectgroup")]
    public class Objectgroup
    {
        [XmlAttribute("name")]
        public string name;

        [XmlElement("object")]
        public TiledObject[] TiledObject;
    }
}
