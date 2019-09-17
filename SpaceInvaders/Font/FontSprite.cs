using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class FontSprite : SpriteBase
    {
        public Font.Name name;
        private Azul.Sprite pAzulSprite;
        private Azul.Rect pScreenRect;
        private Azul.Color pColor;   // this color is multplied by the texture

        private String pMessage;
        public Glyph.Name glyphName;

        public float x;
        public float y;

        public FontSprite()
        {
            this.pAzulSprite = new Azul.Sprite();
            this.pScreenRect = new Azul.Rect();
            this.pColor = new Azul.Color(1.0f, 1.0f, 1.0f);

            this.pMessage = null;
            this.glyphName = Glyph.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
        }

        public void Set(Font.Name name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);
            this.pMessage = pMessage;

            this.x = xStart;
            this.y = yStart;

            this.name = name;

            // TODO: for wash... this should be a nullGlyph
            this.glyphName = glyphName;

            // Force color to white
            Debug.Assert(this.pColor != null);
            this.pColor.Set(1.0f, 1.0f, 1.0f);
        }

        public void UpdateMessage(String msg)
        {
            this.pMessage = msg;
        }

        public override void Render()
        {
            Debug.Assert(this.pAzulSprite != null);
            Debug.Assert(this.pColor != null);
            Debug.Assert(this.pScreenRect != null);
            Debug.Assert(this.pMessage != null);
            Debug.Assert(this.pMessage.Length > 0);

            float xTmp = this.x;
            float yTmp = this.y;

            float xEnd = this.x;

            for (int i = 0; i < this.pMessage.Length; i++)
            {
                int key = Convert.ToByte(pMessage[i]);

                Glyph pGlyph = GlyphManager.getInstance().FindGlyph(this.glyphName, key);
                Debug.Assert(pGlyph != null);

                xTmp = xEnd + pGlyph.GetAzulSubRect().width / 2;
                this.pScreenRect.Set(xTmp, yTmp, pGlyph.GetAzulSubRect().width, pGlyph.GetAzulSubRect().height);

                pAzulSprite.Swap(pGlyph.GetAzulTexture(), pGlyph.GetAzulSubRect(), this.pScreenRect, this.pColor);

                pAzulSprite.Update();
                pAzulSprite.Render();

                // move the starting to the next character
                xEnd = pGlyph.GetAzulSubRect().width / 2 + xTmp;

            }
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((FontSprite)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  Sprite");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }
    }
}
