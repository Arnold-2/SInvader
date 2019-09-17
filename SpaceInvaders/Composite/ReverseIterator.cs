using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ReverseIterator : Iterator
    {
        public ReverseIterator(Component c)
        {
            this.pRoot = c;

            // Find the last leaf node
            this.pCurrent = this.pRoot;
            while (pCurrent != null)
            {
                // Has child
                if (this.pCurrent.pChildHead != null)
                {
                    this.pCurrent = this.pCurrent.pChildHead;
                }
                // no child, has sibling
                else if (this.pCurrent.pNextSibling != null)
                {
                    this.pCurrent = this.pCurrent.pNextSibling;
                }
                // no child, no sibling, has parent, parent has sibling
                else if (this.pCurrent.pParent != null && this.pCurrent.pParent.pNextSibling != null)
                {
                    this.pCurrent = this.pCurrent.pParent.pNextSibling;
                }
                // no child, no sibling, has parent, no parent's sibling -- Last node
                else if (this.pCurrent.pParent != null)
                {
                    break;
                }
                // no parent, only node in the tree
                else
                {
                    break;
                }
            }
        }

        public override bool IsDone()
        {
            return this.pCurrent == null;
        }

        // Return current node
        // set current node to next availble position
        public override Component Next()
        {
            Component ret = this.pCurrent;

            // don't go above root (for column object has parent)
            if (this.pCurrent == this.pRoot)
            {
                this.pCurrent = null;
            }
            // has prev sibling
            else if (this.pCurrent.pPrevSibling != null)
            {
                this.pCurrent = this.pCurrent.pPrevSibling;
            }
            // no prev sib, has parent
            else if (this.pCurrent.pParent != null)
            {
                this.pCurrent = this.pCurrent.pParent;
            }
            // no parent, root, next node will be null
            else
            {
                this.pCurrent = null;
            }

            return ret;
        }
    }
}
