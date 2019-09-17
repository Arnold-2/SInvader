using System;


namespace SpaceInvaders
{
    class GameObjectManager : Manager
    {
        private static GameObjectManager pInstance;

        public GameObjectManager() : base(11, 7)
        {

        }

        public static GameObjectManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new GameObjectManager();
            }

            return pInstance;
        }

        // Factory method, create and add GameObject into actives
        public GameObject Add(GameObject.Category cate, GameSprite gSprite)
        {
            // Create a new image
            GameObject ret = (GameObject)this.PullFromReserved();

            ret.category = cate;
            ret.pProxy = gSprite.pProxy;
            ret.mGameSprtName = gSprite.name;
            ret.mGameSprite = gSprite;
            ret.colliRect = new CollisionRect(gSprite.poRect);
            ret.colliRect.width = gSprite.poRect.width;
            ret.colliRect.height = gSprite.poRect.height;

            // Add it to the list
            this.Add(ret);

            // add this to gSprite's object reference
            gSprite.pGameObject = ret;

            return ret;
        }

        // Render all active objects
        public void Render()
        {
            GameObject current = (GameObject)this.poActiveHead;
            while (current != null)
            {
                current.Render();
                current = (GameObject)(current.pNext);
            }
        }

        // Update all active objects
        public void Update()
        {
            GameObject current = (GameObject)this.poActiveHead;
            while (current != null)
            {
                current.Update();
                current = (GameObject)(current.pNext);
            }
        }

        public override void Remove(DLink node)
        {
            GameObject go = (GameObject)node;

            base.Remove(node);

           
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new GameObject();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new GameObject();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }

    }
}
