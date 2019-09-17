using System;

namespace SpaceInvaders
{
    public abstract class Iterator
    {
        protected Component pRoot;
        protected Component pCurrent;

        // if there's next node
        public abstract bool IsDone();

        // get next node
        public abstract Component Next();

        // get current node
        public virtual Component Current()
        {
            return pCurrent;
        }

        // get first node
        public virtual Component First()
        {
            return pRoot;
        }
    }
}
