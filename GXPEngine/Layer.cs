using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRoot("layer")]
    class Layer
    {
        [XmlAttribute("name")]
        public string name;

        [XmlAttribute("height")]
        public int height = 0;

        [XmlAttribute("width")]
        public int width = 0;

        [XmlElement("data")]
        public Data data;

        public Layer()
        {

        }

        public override string ToString()
        {
            //parseTile();
            string output = string.Format("Layer: {0}, width: {1}, height: {2} \n", name, width, height);
            output += data;
            return output;
        }
    }
}
