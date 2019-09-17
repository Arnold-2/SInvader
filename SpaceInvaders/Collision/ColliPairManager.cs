using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // Collision Pair Mananger
    class ColliPairManager : Manager
    {
        private static ColliPairManager pInstance;
        private static ColliPairManager pIncativeInstance;

        public ColliPairManager() : base(5, 3)
        {

        }

        // Singleton method
        public static ColliPairManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new ColliPairManager();
            }

            return pInstance;
        }

        public static void SwapInstance()
        {
            
            ColliPairManager temp;

            temp = pInstance;
            pInstance = pIncativeInstance;
            pIncativeInstance = temp;

            Debug.Print("Collision Pair Swapped!");
            
        }

        // Called when up the levels
        public static void DiscardInstance()
        {
            pInstance = null;
        }

        // Factory method, create and add Collision Pair into actives
        public CollisionPair AddCollisionPair(CollisionPair.Name _name, GameObject _host, GameObject _visitor)
        {
            // get a new object
            CollisionPair ret = (CollisionPair)this.PullFromReserved();

            // set values
            ret.set(_name, _host, _visitor);

            // add to the active list
            this.Add(ret);

            return ret;
        }

        // Process all active pair
        public void Process()
        {
            CollisionPair current = (CollisionPair)this.poActiveHead;

            while(current != null)
            {
                current.Process();
                current = (CollisionPair)current.pNext;
            }
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new CollisionPair();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new CollisionPair();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
