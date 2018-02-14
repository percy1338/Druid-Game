using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Objects : GameObject //: AnimSprite
    {
        string propertie = "";
        string propertieValue = "";

        int Id = 0;

        public Objects(int frame, Map map) : base() //: base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
        {
            //  Id = frame;
            // GetType(Id);
            //SetFrame(frame - map.tileSet.firstGid);

            switch (frame)
            {
                case 97:
                    Pickup pickup = new Pickup(frame,map);
                    AddChild(pickup);
                    break;

                case 17:
                    SolidObjects solid = new SolidObjects(frame, map);
                    AddChild(solid);
                    break;
            }
        }

        void GetType(int ID)
        {

        }
    }
}
