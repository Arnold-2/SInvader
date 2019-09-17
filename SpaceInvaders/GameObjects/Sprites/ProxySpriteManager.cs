using System;

namespace SpaceInvaders
{
    class ProxySpriteManager : Manager
    {
        private static ProxySpriteManager pInstance;
        static float currentTime;

        public ProxySpriteManager() : base(3, 1)
        {

        }

        public static ProxySpriteManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new ProxySpriteManager();
            }

            return pInstance;
        }

        // PA2: Factory method, create and add image into actives
        public ProxySprite Add(ProxySprite.Name name, GameSprite gSprite, float initX, float initY)
        {
            // Create a new Proxy
            ProxySprite ret = (ProxySprite)this.PullFromReserved();

            ret.name = name;
            ret.pSprite = gSprite;
            ret.x = initX;
            ret.y = initY;
            

            // Add it to the list
            this.Add(ret);

            return ret;
        }


        public static float getCurrentTime()
        {
            return currentTime;
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new ProxySprite();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new ProxySprite();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }

    }

}

