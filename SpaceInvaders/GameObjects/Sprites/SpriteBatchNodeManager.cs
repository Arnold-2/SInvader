using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatchNodeManager : Manager
    {
        public SpriteBatchNodeManager() : base(5, 3)
        {

        }

        public SpriteBatchNode AddSpriteBatchNode (SpriteBase sprt)
        {
            SpriteBatchNode ret = (SpriteBatchNode)this.PullFromReserved();
            ret.pSprite = sprt;
            this.Add(ret);
            return ret;
        }

        public void UpdateAllActive()
        {
            SpriteBatchNode current = (SpriteBatchNode)this.poActiveHead;
            while (current != null)
            {
                //Debug.Print(current.pSprite.name.ToString());
                //Debug.Print("x: " + ((GameSprite)(current.pSprite)).poAzulSprite.x);
                //Debug.Print("y: " + ((GameSprite)(current.pSprite)).poAzulSprite.y);
                if (current.pSprite.pGameObject != null)
                    current.pSprite.pGameObject.Update();
                else
                    current.pSprite.Update();

                current = (SpriteBatchNode)current.pNext;
            }
        }

        public void RenderAllActive()
        {
            SpriteBatchNode current = (SpriteBatchNode)this.poActiveHead;
            while (current != null)
            {
                if (current.pSprite.pGameObject != null)
                    current.pSprite.pGameObject.Render();
                else
                    current.pSprite.Render();
                current = (SpriteBatchNode)current.pNext;
            }
        }

        public void RecycleAllActive()
        {
            SpriteBatchNode current = (SpriteBatchNode)this.poActiveHead;
            SpriteBatchNode pOld;
            while (current != null)
            {
                if (current.pSprite.pGameObject != null)
                    GameObjectManager.getInstance().Remove(current.pSprite.pGameObject.Wash());

                if (current.pSprite.GetType() == typeof(GameSprite))
                    GameSpriteManager.getInstance().Remove(current.pSprite.Wash());
                else if (current.pSprite.GetType() == typeof(BoxSprite))
                    BoxSpriteManager.getInstance().Remove(current.pSprite.Wash());

                pOld = current;
                current = (SpriteBatchNode)current.pNext;
                this.Remove(pOld);
            }
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new SpriteBatchNode();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new SpriteBatchNode();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
