using System;
using GXPEngine;
using System.Xml.Serialization;
using System.IO;

 

public class MyGame : Game
{
	//
    private int _width = 1600;
    private int _height = 900;

    Map map;

    Level level;
    int[,] gIDArray;

    private string _level = "Level/test.tmx";

    public MyGame() : base(1600, 900, false)
    {
        //background

        //tiles
        level = new Level(this, _width, _height);
        generateLevel();
        // player.

        //forground

        //hud
    }

    static void Main()
    {
        new MyGame().Start();
    }

    public void Update()
    {

    }

    private void generateLevel()
    {
        ReadMap();
        ParseInnerDate();
        this.AddChild(level);
        level.DrawLevel(map, gIDArray);
    }

    public void ReadMap()
    {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));

            TextReader reader = new StreamReader(_level);
            map = serializer.Deserialize(reader) as Map;
            reader.Close();

            //Console.WriteLine(map);
    }

    private void ParseInnerDate()
    {
        gIDArray = new int[map.width, map.height];
        Layer layer = map.layers[0];
        gIDArray = layer.parseTile();
    }
}