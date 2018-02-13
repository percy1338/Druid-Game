using System;
using GXPEngine;
using System.Xml.Serialization;
using System.IO;

public class MyGame : Game
{
<<<<<<< HEAD
    Map map;
    Level level = new Level();
    int[,] gIDArray;


    private string _level = "test.tmx";

    public MyGame() : base(800, 600, false)
    {
        generateLevel();
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
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));

            TextReader reader = new StreamReader(_level);
            map = serializer.Deserialize(reader) as Map;
            reader.Close();

            Console.WriteLine(map);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void ParseInnerDate()
    {
        gIDArray = new int[map.width, map.height];
        Layer layer = map.layers[0];
        gIDArray = layer.parseTile();
    }
=======
	public MyGame() : base(800, 600, false, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		Player player = new Player();
		AddChild(player);
	}

	void Update()
	{
		// Empty
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
>>>>>>> 74acca0c7c94970cd508bc836f32301a456299f3
}