using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRoot("image")]
    class Image
    {
        [XmlAttribute("source")]
        public string source = "";

        public Image()
        {

        }

        public override string ToString()
        {
            string output = string.Format("tileset source: {0} \n\n", source);

            return output;
        }
    }
}
