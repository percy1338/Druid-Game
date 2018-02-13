using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRoot("map")]
    class TMXMap
    {
        [XmlAttribute("version")]
        public string version = "";

        [XmlAttribute("orientation")]
        public string orientation = "";

        [XmlAttribute("renderorder")]
        public string renderOrder = "";

        [XmlAttribute("height")]
        public int height = 0;

        [XmlAttribute("width")]
        public int width = 0;

        [XmlAttribute("tilewidth")]
        public int tileWidth = 0;

        [XmlAttribute("tileheight")]
        public int tileHeight;

        [XmlElement("tileset")]
        public Tileset tileset;

        [XmlElement("layer")]
        public Layer[] layers;

        public TMXMap()
        {

        }

        public override string ToString()
        {
            string output = string.Format("version: {0}, orientation: {1}, renderorder: {2}, Map width: {3}, height: {4}, tileWidth {5}, tileHeight {6}\n\n", version, orientation, renderOrder, width, height, tileWidth, tileHeight);

            output += tileset;
            foreach (Layer layer in layers)
            {
                output += layer;
            }
            return output;
        }
    }
}
