using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class TimerManager : Manager
    {
        private static TimerManager pInstance;
        private static TimerManager pInactiveInstance;
        private float currentTime;
        private float currentTimeDelta;

        public TimerManager() : base(3, 1)
        {

        }

        public static TimerManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new TimerManager();
            }

            return pInstance;
        }

        public static void SwapInstance()
        {
            TimerManager temp;

            temp = pInactiveInstance;
            pInactiveInstance = pInstance;
            pInstance = temp;

            if (pInstance != null)
                pInstance.RefreshTimer();

            Debug.Print("TimerManager Swapped!");
        }

        // called when up the level
        public static void DiscardInstance()
        {
            pInstance = null;
        }

        // PA2: Factory method, create and add image into actives
        public TimerEvent Add(TimerEvent.Name name, Command cmd, float deltaTimeToTrigger)
        {
            // Create a new image
            TimerEvent ret = (TimerEvent)this.PullFromReserved();
            ret.name = name;
            ret.pCommand = cmd;
            ret.deltaTime = deltaTimeToTrigger;
            ret.triggerTime = this.currentTime + deltaTimeToTrigger;

            // Add it to the list
            this.AddAndSort(ref this.poActiveHead, ref this.pActiveEnd, ret);

            return ret;
        }

        public void AddAndSort(ref DLink pHead, ref DLink pTail, DLink node)
        {
            node.Clear();

            // If reserved is empty, replenish with nodes
            if (this.mNumReserved == 0)
                CreateReservedNodes(this.mGrowthSize);

            // Case 1, First Node
            if (pHead == null)
            {
                pHead = node;
                pTail = node;
            }
            // Case 2: Not first node
            else
            {
                // Traverse the list to find first node have greater trigger time than node
                DLink current = pHead;
                while (current != null)
                {
                    if (((TimerEvent)current).triggerTime >= ((TimerEvent)node).triggerTime)
                        break;

                    current = current.pNext;
                }
                if (current != null)
                    this.AddBeforeNode(current, node);
                else
                {
                    // Add to end
                    if (this.poActiveHead == null)
                    {
                        // no node in the list
                        this.poActiveHead = node;
                        this.pActiveEnd = node;
                    }
                    else
                    {
                        // there are nodes, Add to end
                        this.pActiveEnd.pNext = node;
                        node.pNext = null;
                        node.pPrev = this.pActiveEnd;
                        this.pActiveEnd = node;
                    }
                }
            }
        }

        public void Update(float time)
        {
            TimerEvent crntEvent = (TimerEvent)this.poActiveHead;
            TimerEvent nextEvent;
            currentTime = time;

            //this.dbgPrintLists();
            
            while (crntEvent != null && crntEvent.triggerTime <= time)
            {
                nextEvent = (TimerEvent)crntEvent.pNext;
                // Process the event
                crntEvent.process();
                // Remove processed event
                Remove(crntEvent);
                // move crnt to next
                crntEvent = nextEvent;
            }
        }

        public void RefreshTimer()
        {
            TimerEvent walk = (TimerEvent)this.poActiveHead;

            while (walk != null)
            {
                walk.triggerTime = GlobalTimer.currentTime + walk.deltaTime;
                walk = (TimerEvent)walk.pNext;
            }
        }

        private float getCurrentTime()
        {
            return this.currentTime;
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new TimerEvent();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new TimerEvent();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }

    }
}
