using System;


namespace SpaceInvaders
{
    class BoxSpriteManager : Manager
    {
        private static BoxSpriteManager pInstance;

        public BoxSpriteManager() : base (5, 3)
        {

        }

        public static BoxSpriteManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new BoxSpriteManager();
            }

            return pInstance;
        }

        // PA2 Factory method, create node and add to the list
        public BoxSprite AddBoxSprite (BoxSprite.Name name, float ag, float sx, float sy, float x, float y,
            float red, float green, float blue, float posx, float posy, float w, float h)
        {
            // pull one reserved node
            BoxSprite ret = (BoxSprite)this.PullFromReserved();

            // Modify the field
            ret.name = name;
            ret.poRect = new Azul.Rect(posx, posy, w, h);
            ret.poAzulSpriteBox = new Azul.SpriteBox(ret.poRect, new Azul.Color(red, green, blue));
            ret.poAzulSpriteBox.angle = ag;
            ret.poAzulSpriteBox.sx = sx;
            ret.poAzulSpriteBox.sy = sy;
            ret.poAzulSpriteBox.x = x;
            ret.poAzulSpriteBox.y = y;

            // Add to the active list
            this.Add(ret);

            return ret;
        }

        public BoxSprite AddBoxSprite (CollisionRect rect)
        {
            BoxSprite ret = (BoxSprite)this.PullFromReserved();

            // Modify the field
            ret.name = BoxSprite.Name.CollisionRectBox;
            ret.poRect = rect;
            ret.color = new Azul.Color(1, 1, 1);
            ret.poAzulSpriteBox = new Azul.SpriteBox(ret.poRect, ret.color);
            
            this.Add(ret);

            return ret;
        }

        public void RenderAllActive()
        {
            BoxSprite current = (BoxSprite)this.poActiveHead;
            while (current != null)
            {
                current.Render();
                current = (BoxSprite)(current.pNext);
            }
        }

        public void UpdateAllActive()
        {
            BoxSprite current = (BoxSprite)this.poActiveHead;
            while (current != null)
            {
                current.Update();
                current = (BoxSprite)(current.pNext);
            }
        }

        public BoxSprite GetSpriteByName (BoxSprite.Name n)
        {
            BoxSprite current = (BoxSprite)this.poActiveHead;
            while(current != null)
            {
                if (current.name == n)
                    return current;
            }

            return null;
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new BoxSprite();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new BoxSprite();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
