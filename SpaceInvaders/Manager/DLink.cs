using System;

namespace SpaceInvaders
{
    public abstract class DLink : Visitor
    {
        public DLink pNext;
        public DLink pPrev;

        protected DLink()
        {
            Clear();
        }

        public void Clear()
        {
            pNext = null;
            pPrev = null;
        }

        // To compare if the nodes are equal. To be used by Find()
        public abstract bool DerivedEqual(DLink node);

        // To Print the list that node exits
        public abstract void dbgDerivedPrint();

    }
}
