using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRoot("map")]
    public class Map
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
        public int tileHeight = 0;

        [XmlElement("tileset")]
        public TileSet tileSet;

        [XmlElement("layer")]
        public Layer[] layers;

        [XmlElement("objectgroup")]
        public Objectgroup objGroup;

        [XmlElement("imagelayer")]
        public Imagelayer[] background;


        public Map()
        {
        }

        public override string ToString()
        {
            string output = string.Format("version: {0}, orientation: {1}, renderorder: {2}, Map width: {3}, height: {4}, tileWidth {5}, tileHeight {6}\n\n", version, orientation, renderOrder, width, height, tileWidth, tileHeight);

            output += tileSet;
            foreach (Layer layer in layers)
            {
                output += layer;
            }
            return output;
        }
    }
    [XmlRoot("layer")]
    public class Layer
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
            parseTile();
            string output = string.Format("Layer: {0}, width: {1}, height: {2} \n", name, width, height);
            output += data;
            return output;
        }

        public int[,] parseTile()
        {
            int x = 0;
            int[,] dataArray = new int[width, height];
            string layerData = data.ToString();

            layerData = layerData.Replace(Environment.NewLine, "\n");
            string[] rows = layerData.Split('\n');
            for (int i = 0; i < rows.Count(); i++)
            {
                if (rows[i].ToString() != "")
                {
                    string[] row = rows[i].Split(',');
                    for (int y = 0; y < width; y++)
                    {
                        dataArray[x, y] = int.Parse(row[y]);

                    }
                    x++;
                }
            }
            return dataArray;
        }
    }

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

        [XmlElement("tile")]
        public Tile tile;

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

    [XmlRootAttribute("objectgroup")]
    public class Objectgroup
    {
        [XmlAttribute("name")]
        public string name;

        [XmlElement("object")]
        public TiledObject[] TiledObject;
    }

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

    [XmlRoot("image")]
    public class Image
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

    [XmlRoot("<imagelayer ")]
    public class Imagelayer
    {
        [XmlAttribute("offsetx")]
        public float offsetx = 0.0f;

        [XmlAttribute("offsety")]
        public float offsety = 0.0f;

        [XmlElement("image")]
        public Image background;

    }
}
