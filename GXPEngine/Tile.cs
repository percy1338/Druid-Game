using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRoot("tile")]
    public class Tile
    {
        [XmlAttribute("id")]
        public int id = 0;

        [XmlElement("properties")]
        public Properties properties;

        public Tile()
        {
            
        }
    }
}
