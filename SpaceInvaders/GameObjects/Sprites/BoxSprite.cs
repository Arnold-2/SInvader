using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BoxSprite : SpriteBase
    {
        public Azul.SpriteBox poAzulSpriteBox;
        public Name name;
        public Azul.Color color = new Azul.Color(1,1,1);

        public BoxSprite(Name name, float ag, float sx, float sy, float x, float y,
            float red, float green, float blue, float posx, float posy, float w, float h)
        {
            this.name = name;
            this.poRect = new Azul.Rect(posx, posy, w, h);
            this.poAzulSpriteBox = new Azul.SpriteBox(new Azul.Rect(posx, posy, w, h), new Azul.Color(red, green, blue));
            this.poAzulSpriteBox.angle = ag;
            this.poAzulSpriteBox.sx = sx;
            this.poAzulSpriteBox.sy = sy;
            this.poAzulSpriteBox.x = x;
            this.poAzulSpriteBox.y = y;
        }

        public BoxSprite()
        {
            this.name = Name.Uninitialized;
        }

        public override void Render()
        {
            this.poAzulSpriteBox.Render();
        }

        public override void Update()
        {
            this.poAzulSpriteBox.Update();   
        }

        public void setX(float x)
        {
            this.poAzulSpriteBox.x = x;
        }

        public void setY(float y)
        {
            this.poAzulSpriteBox.y = y;
        }

        public void setAngle(float ag)
        {
            this.poAzulSpriteBox.angle = ag;
        }

        public void setSX(float sx)
        {
            this.poAzulSpriteBox.sx = sx;
        }

        public void setSY(float sy)
        {
            this.poAzulSpriteBox.sy = sy;
        }

        public override DLink Wash()
        {
            this.poAzulSpriteBox = null;
            this.name = Name.Uninitialized;
            this.color = null;

            return this;
        }

        public void setDimension(Azul.Rect r)
        {
            this.poRect.x = r.x;
            this.poRect.y = r.y;
            this.poRect.width = r.width;
            this.poRect.height = r.height;
            Azul.Rect tmpRect = new Azul.Rect(r.x, r.y, r.width, r.height);
            this.poAzulSpriteBox.SwapScreenRect(tmpRect);
        }

        public void SwapColor(float r, float g, float b)
        {
            this.poAzulSpriteBox.SwapColor(new Azul.Color(r, g, b));
            this.color = new Azul.Color(r, g, b);
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((BoxSprite)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  BoxSprite");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }

        public enum Name
        {
            CollisionRectBox,
            Uninitialized
        }

    }
}
