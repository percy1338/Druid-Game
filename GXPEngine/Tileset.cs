using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRoot("tileset")]
    public class TileSet
    {
        [XmlAttribute("firstgid")]
        public int firstGid = 0;

        [XmlAttribute("name")]
        public string name = "";

        [XmlAttribute("tilewidth")]
        public int tileWidth = 0;

        [XmlAttribute("tileheight")]
        public int tileHeight = 0;

        [XmlAttribute("tilecount")]
        public int tilecount = 0;

        [XmlAttribute("columns")]
        public int columns = 0;

        [XmlElement("image")]
        public Image image;


        public TileSet()
        {
        }

        public override string ToString()
        {
            string output = string.Format("first gid: {0} name: {1} tilewidth: {2} tileheight: {3} tilecount: {4} columns: {5} \n\n", firstGid, name, tileWidth, tileHeight, tilecount, columns);
            output += image;

            return output;
        }
    }
}
