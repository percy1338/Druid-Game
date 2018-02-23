using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace GXPEngine
{
	//
    public class TmxParser
    {
        public Map map = new Map();
        private string _level = "test.tmx";

        public TmxParser()
        {

        }

        public void ReadMap()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));

            TextReader reader = new StreamReader(_level);
            map = serializer.Deserialize(reader) as Map;
            reader.Close();
        }

        private void ParseInnerData()
        {

        }



    }
}
