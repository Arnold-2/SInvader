using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Manager
    {
        protected DLink poActiveHead;
        protected DLink pActiveEnd;
        protected DLink poReservedHead;
        protected int mNumActive;
        protected int mNumReserved;
        protected int mGrowthSize;

        public Manager(int initSize, int growthSize)
        {
            CreateReservedNodes(initSize);
            this.mGrowthSize = growthSize;
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public abstract void CreateReservedNodes(int numofNodes);

        // Add Node method
        public virtual void Add(DLink node)
        {
            // If reserved is empty, replenish with nodes
            if (this.mNumReserved == 0)
                CreateReservedNodes(this.mGrowthSize);

            AddToFront(ref this.poActiveHead, ref this.pActiveEnd, node);
            this.mNumActive++;
        }

        public DLink getActiveHead()
        {
            return this.poActiveHead;
        }


        // Remove DataNode mothod
        public virtual void Remove(DLink node)
        {
            RemoveNode(ref this.poActiveHead, ref this.pActiveEnd, node);
            this.mNumActive--;
        }

        // Add a node to the front of the active list
        public static void AddToFront (ref DLink pHead, ref DLink pTail, DLink node)
        {
            Debug.Assert(node != null);

            node.Clear();

            // Case 1: First node
            if (pHead == null)
            {
                pHead = node;
                pTail = node;
            }
            // Case 2: Not first node
            else
            {
                pHead.pPrev = node;
                node.pNext = pHead;
                pHead = node;
            }
        }

        // Add a node to the end of the active list
        public static void AddToEnd (ref DLink pHead, ref DLink pTail, DLink node)
        {
            Debug.Assert(node != null);

            node.Clear();

            // Case 1: First node
            if (pTail == null)
            {
                pHead = node;
                pTail = node;
            }
            // Case 2: not first node
            else 
            {
                pTail.pNext = node;
                node.pPrev = pTail;
                pTail = node;
            }
        }
        
        // Recycle a node to the reserve list, internally
        private void AddToReserve(DLink node)
        {
            Debug.Assert(node != null);

            node.Clear();

            // Add to the front of the reserved list
            this.poReservedHead.pPrev = node;
            node.pNext = this.poReservedHead;
            this.poReservedHead = node;

            this.mNumReserved++;
        }

        public void RemoveNode (ref DLink pHead, ref DLink pTail, DLink node)
        {
            Debug.Assert(node != null);

            // 4 Different Cases
            // Case 1: Only node
            if (node.pNext == null && node.pPrev == null && pHead == node)
            {
                AddToReserve(node);
                pHead = null;
                pTail = null;
            }
                
            // Case 2: First node
            else if (pHead == node)
            {
                pHead = pHead.pNext;
                node.pNext.pPrev = null;
                AddToReserve(node);
            }
            // Case 3: Last node
            else if (pActiveEnd == node)
            {
                this.pActiveEnd = this.pActiveEnd.pPrev;
                node.pPrev.pNext = null;
                AddToReserve(node);
            }
            // Case 4: Middle node
            else
            {
                node.pPrev.pNext = node.pNext;
                node.pNext.pPrev = node.pPrev;
                AddToReserve(node);
            }
        }

        // Pull one reserved node from the reserved list
        // For the caller to configure the node and add to the active list
        public DLink PullFromReserved()
        {
            DLink ret = this.poReservedHead;

            // If there's more than 1 node in the list
            if (this.poReservedHead.pNext != null)
            {
                this.poReservedHead.pNext.pPrev = null;
                this.poReservedHead = this.poReservedHead.pNext;
                this.mNumReserved--;
            }
            // only 1 left
            else
            {
                this.poReservedHead = null;
                this.mNumReserved--;
            }

            return ret;
        }

        // Add a node one slot before the target node
        public void AddBeforeNode(DLink target, DLink node)
        {
            Debug.Assert(target != null);
            Debug.Assert(node != null);

            node.Clear();

            // Case 1: target is the head
            if (target == this.poActiveHead)
            {
                Debug.Assert(target.pPrev == null);

                target.pPrev = node;
                node.pNext = target;
                this.poActiveHead = node;
            }
            // Case 2: target is in the middle
            else
            {
                Debug.Assert(target.pPrev != null);

                target.pPrev.pNext = node;
                node.pPrev = target.pPrev;
                target.pPrev = node;
                node.pNext = target;
            }

            mNumActive++;
        }

        // Add a node one slot after the target node
        public void AddAfterNode(DLink target, DLink node)
        {
            Debug.Assert(target != null);
            Debug.Assert(node != null);

            node.Clear();

            // Case 1: target is the tail
            if (target == this.pActiveEnd)
            {
                Debug.Assert(target.pNext == null);

                target.pNext = node;
                node.pPrev = target;
            }
            // Case 2: target is in the middle
            else
            {
                Debug.Assert(target.pNext != null);

                target.pNext.pPrev = node;
                node.pNext = target.pNext;
                target.pNext = node;
                node.pPrev = target;
            }

            mNumActive++;
        }

        // Find a node in the active list by comparing nodes with sampleNode
        // Lower level DLink class create and pass in the sampleNode
        // Lower level DLink class defines derivedEqual
        public virtual DLink Find(DLink sampleNode)
        {
            DLink current = this.poActiveHead;
            
            // Iterate through the list
            while (current != null)
            {
                // Call derived Equal function. If match, return the current
                if (sampleNode.DerivedEqual(current))
                {
                    return current;
                }

                current = current.pNext;
            }
           
            return null;
        }

        // Debugging code: Print the list
        public void dbgPrintLists()
        {
            DLink current = this.poActiveHead;

            Debug.WriteLine("---------ACTIVE----------");
            
            while (current != null)
            {
                current.dbgDerivedPrint();
                current = current.pNext;
            }

            Debug.WriteLine("--------RESERVED---------");

            current = this.poReservedHead;
            while (current != null)
            {
                current.dbgDerivedPrint();
                current = current.pNext;
            }
        }

        // Get Stats
        public int getNumberActive()
        {
            return this.mNumActive;
        }

        public int getNumberReserved()
        {
            return this.mNumReserved;
        }
 
    }
}
