using System;


namespace SpaceInvaders
{
    class GameSpriteManager : Manager
    {
        private static GameSpriteManager pInstance;

        public GameSpriteManager () : base (5, 3)
        {

        }

        public static GameSpriteManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new GameSpriteManager();
            }

            return pInstance;
        }

        // PA2 Factory method, create node and add to the list
        public GameSprite AddGameSprite (GameObject.Category category, GameSprite.Name name, float ag, float sx, float sy, float x, float y, Image img
            , float posx, float posy, float w, float h)
        {
            // pull one reserved node
            GameSprite ret = (GameSprite)this.PullFromReserved();

            // Modify the field
            ret.name = name;
            ret.pImage = img;

            // Add first image to queue
            ret.addImage(img);

            ret.poRect = new Azul.Rect(posx, posy, w, h);
            ret.poAzulSprite = new Azul.Sprite(img.pAzulTexture, img.poRect, ret.poRect);
            ret.poAzulSprite.angle = ag;
            ret.poAzulSprite.sx = sx;
            ret.poAzulSprite.sy = sy;
            ret.poAzulSprite.x = x;
            ret.poAzulSprite.y = y;

            // for projectile sprite, add flyproxy
            if (category == GameObject.Category.Missile || category == GameObject.Category.Bomb)
                ret.pProxy = new FlyProxy(category, ret);
            else if (category == GameObject.Category.UFO)
                ret.pProxy = new UFOProxy(ret);
            else
                ret.pProxy = new MoveProxy(ret);

            // Add to the active list
            this.Add(ret);

            // Create and Add GameObject to GameObjectManager
            GameObjectManager.getInstance().Add(category, ret);

            return ret;
        }

        public void RenderAllActive()
        {
            GameSprite current = (GameSprite)this.poActiveHead;
            while (current != null)
            {
                current.Render();
                current = (GameSprite)(current.pNext);
            }
        }

        public void UpdateAllActive()
        {
            GameSprite current = (GameSprite)this.poActiveHead;
            while (current != null)
            {
                current.Update();
                current = (GameSprite)(current.pNext);
            }
        }

        public GameSprite GetSpriteByName (GameSprite.Name n)
        {
            GameSprite current = (GameSprite)this.poActiveHead;
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
            DLink tmpNode = new GameSprite();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new GameSprite();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
