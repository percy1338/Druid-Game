using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRootAttribute("object")]
    public class TiledObject
    {
        [XmlAttribute("gid")]
        public int gid;

        [XmlAttribute("x")]
        public float x;

        [XmlAttribute("y")]
        public float y;

        [XmlElement("properties")]
        public Properties properties;
    }
}
