using System;
using GXPEngine;
using System.Xml.Serialization;
using System.IO;

 

public class MyGame : Game
{
    Map map;
    Level level = new Level();
    int[,] gIDArray;

    private string _level = "Level/test.tmx";

    public MyGame() : base(800, 600, false, false)
    {
        //background

        //tiles
        generateLevel();
        // player.
        Player _player = new Player(this, map);
        AddChild(_player);
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

            Console.WriteLine(map);
    }

    private void ParseInnerDate()
    {
        gIDArray = new int[map.width, map.height];
        Layer layer = map.layers[0];
        gIDArray = layer.parseTile();
    }
}