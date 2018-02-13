using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GXPEngine
{
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
            //int x = 0;
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
}
