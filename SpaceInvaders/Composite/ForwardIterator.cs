using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ForwardIterator : Iterator
    {
        public ForwardIterator(Component c)
        {
            this.pRoot = c;
            this.pCurrent = this.pRoot;
        }

        public override bool IsDone()
        {
            return this.pCurrent == null;
        }
    
        // DFS. traverse the tree
        public override Component Next()
        {
            // Always return current node, and move current node to the next position
            Component ret = this.pCurrent;

            // Procede to next node
            // if has child, return next child
            if (this.pCurrent.pChildHead != null)
            {
                this.pCurrent = this.pCurrent.pChildHead;
            }
            // no child, but has sibling
            else if (this.pCurrent.pNextSibling != null) 
            {
                this.pCurrent = this.pCurrent.pNextSibling;
            }
            // no child, no sibling, has parent
            else
            {
                Component walk = this.pCurrent.pParent;
                // Recursively checking parent's siblings
                while (walk != null)
                {
                    if (walk.pNextSibling != null)
                    {
                        this.pCurrent = walk.pNextSibling;
                    }
                    walk = walk.pParent;
                }
                
                // if walk come to the root node
                if (walk == null)
                {
                    this.pCurrent = null;
                }
            }

            return ret;
        }

    }
}
