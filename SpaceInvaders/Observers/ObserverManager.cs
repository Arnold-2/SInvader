using System;

namespace SpaceInvaders
{
    class ObserverManager : Manager
    {
        public ObserverManager() : base(5, 3)
        {

        }

        // method adding observer to a game object
        public void AddObserver(Subject.Name name, GameObject go)
        {
            Observer raw = (Observer)this.PullFromReserved();

            // adding derived observer based on name
            if (name == Subject.Name.LeftKey || name == Subject.Name.RightKey)
            {
                // for left key and right key event
                // create a move observer to listen
                MoveObserver mo = (MoveObserver)raw;
                mo.set(name, (MoveProxy)go.pProxy); // downcasting dangerous

                // Add new observer to the list
                this.Add(mo);
            }
        }

        public void Notify()
        {
            Observer current = (Observer)this.poActiveHead;

            while(current != null)
            {
                current.Notify();
            }
        }


        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new Observer();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new Observer();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
