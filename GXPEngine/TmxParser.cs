using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace GXPEngine
{
    public class TmxParser
    {
        TMXMap map = new TMXMap();

        public TmxParser()
        {

        }

        public void ReadMap()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TMXMap));

            TextReader reader = new StreamReader("level name");
            map = serializer.Deserialize(reader) as TMXMap;
            reader.Close();

            Console.WriteLine(map);
        }

        private void ParseInnerData()
        {

        }



    }
}
