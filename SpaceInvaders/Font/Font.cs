using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Font : DLink
    {
        public Name name;
        public FontSprite pFontSprite;
        static private String pNullString = "null";

        public enum Name
        {
            Title,
            ScoreBanner,
            P1_Score,
            P2_Score,
            HighScore,
            Uninitialized,
            NullObject

        }

        public Font()
        {
            this.name = Name.Uninitialized;
            this.pFontSprite = new FontSprite();
        }

        public void Set(Font.Name name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);

            this.name = name;
            this.pFontSprite.Set(name, pMessage, glyphName, xStart, yStart);
        }

        public void UpdateMessage(String msg)
        {
            this.pFontSprite.UpdateMessage(msg);
        }

        public void Update()
        {
            this.pFontSprite.Update();
        }

        public void Render()
        {
            this.pFontSprite.Render();
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((Font)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  Texture");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }
    }
}
