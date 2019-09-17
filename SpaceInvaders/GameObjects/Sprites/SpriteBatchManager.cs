using System;

namespace SpaceInvaders
{
    class SpriteBatchManager : Manager
    {
        private static SpriteBatchManager pInstance;

        public SpriteBatchManager() : base(3, 1)
        {

        }

        public SpriteBatch AddSpriteBatch (SpriteBatch.Name name, int priority)
        {
            SpriteBatch ret;

            // If exists, return without create
            if ((ret = this.FindSpriteBatchByName(name)) != null)
                return ret;

            ret = (SpriteBatch)this.PullFromReserved();
            ret.name = name;
            ret.priority = priority;
            this.Add(ret);
            return ret;
        }

        public override void Add(DLink node)
        {
            // If reserved is empty, replenish with nodes
            if (this.mNumReserved == 0)
                CreateReservedNodes(this.mGrowthSize);

            AddAndSort(ref this.poActiveHead, ref this.pActiveEnd, node);
            this.mNumActive++;
        }

        public SpriteBatch FindSpriteBatchByName (SpriteBatch.Name _name)
        {
            SpriteBatch current = (SpriteBatch)this.poActiveHead;

            while (current != null)
            {
                if (current.name == _name)
                {
                    return current;
                }

                current = (SpriteBatch)(current.pNext);
            }

            return null;
        }

        public void AddAndSort(ref DLink pHead, ref DLink pTail, DLink node)
        {
            node.Clear();

            // Case 1, First Node
            if (pHead == null)
            {
                pHead = node;
                pTail = node;
            }
            // Case 2: Not first node
            else
            {
                // Traverse the list to find first node have greater priority than node
                DLink current = pHead;
                while(current != null)
                {
                    if (((SpriteBatch)current).priority >= ((SpriteBatch)node).priority)
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

        public void ReSort(SpriteBatch node)
        {
            // Temporary remove node from the list
            // 4 Different Cases
            // Case 1: Only node
            if (node.pNext == null && node.pPrev == null && poActiveHead == node)
            {
                // no need to resort
                return;
            }
            // Case 2: First node
            else if (poActiveHead == node)
            {
                poActiveHead = poActiveHead.pNext;
                node.pNext.pPrev = null;
            }
            // Case 3: Last node
            else if (pActiveEnd == node)
            {
                this.pActiveEnd = this.pActiveEnd.pPrev;
                node.pPrev.pNext = null;
            }
            // Case 4: Middle node
            else
            {
                node.pPrev.pNext = node.pNext;
                node.pNext.pPrev = node.pPrev;
            }


            // Re-add it to the list
            AddAndSort(ref this.poActiveHead, ref this.pActiveEnd, node);
        }

        public void RenderAllBatches()
        {
            DLink current = this.poActiveHead;
            while (current != null)
            {
                ((SpriteBatch)current).RenderAllNodes();
                current = current.pNext;
            }
        }

        public static SpriteBatchManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new SpriteBatchManager();
            }

            return pInstance;
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new SpriteBatch();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new SpriteBatch();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }

    }
}
