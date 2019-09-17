using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FontManager : Manager
    {
        private static FontManager pInstance;

        public FontManager() : base(5, 3)
        {
        }

        public static FontManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new FontManager();
            }

            return pInstance;
        }



        // PA2: Factory Method
        public Font AddFont(Font.Name name, SpriteBatch.Name SB_Name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {

            Font pNode = (Font)this.PullFromReserved();
            Debug.Assert(pNode != null);

            pNode.Set(name, pMessage, glyphName, xStart, yStart);

            // Add to sprite batch
            SpriteBatch sb = SpriteBatchManager.getInstance().AddSpriteBatch(SB_Name, 1);
            sb.AddSprite(pNode.pFontSprite);

            Debug.Assert(pNode.pFontSprite != null);

            this.Add(pNode);


            return pNode;
        }

        public Font FindFontByName (Font.Name name)
        {
            DLink current = poActiveHead;
            while (current != null)
            {
                if (((Font)current).name == name)
                {
                    return (Font)current;
                }

                current = current.pNext;
            }

            return null;
        }

        public void Update()
        {
            Font current = (Font)this.poActiveHead;
            while (current != null)
            {
                current.Update();
                current = (Font)(current.pNext);
            }
        }

        public void Render()
        {
            Font current = (Font)this.poActiveHead;
            while (current != null)
            {
                current.Render();
                current = (Font)(current.pNext);
            }
        }


        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new Font();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new Font();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
