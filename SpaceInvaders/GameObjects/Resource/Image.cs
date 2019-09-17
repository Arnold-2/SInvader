using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Image : DLink
    {
        public Name name;
        public Azul.Texture pAzulTexture;
        public Azul.Rect poRect;

        public Image(Image.Name name, Azul.Texture texture, float x, float y, float w, float h)
        {
            this.name = name;
            this.pAzulTexture = texture;
            this.poRect = new Azul.Rect(x, y, w, h);
        }

        public Image()
        {
            this.name = Image.Name.Uninitialized;
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((Image)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  Image");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }

        public enum Name
        {
            Squid_1,
            Squid_2,
            Crab_1,
            Crab_2,
            Octopus_1,
            Octopus_2,
            Ship,
            UFO,
            Missile,
            AlienExplosion,
            ShipExplosion,
            UFOExplosion,
            MissileExplosion,
            Brick,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,
            DaggerBomb,
            RollingBomb,
            ZigzagBomb,
            Nothing,
            Uninitialized
        }
    }

    
}
