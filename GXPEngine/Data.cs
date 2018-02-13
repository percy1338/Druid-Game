using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRoot("data")]
    public class Data
    {
        [XmlAttribute("encoding")]
        public string encoding;

        [XmlText]
        public string innnerXML;

        public Data()
        {
        }

        public override string ToString()
        {
            return innnerXML + "\n";
        }
    }
}
