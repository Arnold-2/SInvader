using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class Glyph : DLink
    {
        public Name name;
        public int key;
        private Azul.Rect pSubRect;
        private Texture pTexture;

        public enum Name
        {
            Consolas36pt,
            NullObject,
            Uninitialized
        }


        public Glyph()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.pSubRect = new Azul.Rect();
            this.key = 0;
        }

        public void Set(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            Debug.Assert(this.pSubRect != null);
            this.name = name;

            this.pTexture = TextureManager.getInstance().FindTextureByName(textName);
            Debug.Assert(this.pTexture != null);

            this.pSubRect.Set(x, y, width, height);

            this.key = key;

        }

        public Azul.Rect GetAzulSubRect()
        {
            Debug.Assert(this.pSubRect != null);
            return this.pSubRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.pTexture != null);
            return this.pTexture.GetAzulTexture();
        }


        public override bool DerivedEqual(DLink node)
        {
            if (((Glyph)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  Glyph");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }


        
    }
}
