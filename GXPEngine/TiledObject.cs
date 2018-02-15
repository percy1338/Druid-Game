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
        public float x = 0.0f;

        [XmlAttribute("y")]
        public float y = 0.0f;

        [XmlAttribute("width")]
        public float width = 0.0f;

        [XmlAttribute("height")]
        public float height = 0.0f;

        [XmlElement("properties")]
        public Properties properties;
    }
}
