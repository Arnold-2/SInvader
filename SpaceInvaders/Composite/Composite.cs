using System;

namespace SpaceInvaders
{
    public class Composite : Component
    {
        public Component poHead;

        // Add component to head
        public void AddComponent(Component c)
        {
            // if head is empty
            if (poHead == null)
                this.poHead = c;
            else
            {
                c.pNext = poHead;
                poHead.pPrev = c;
                c.pPrev = null;
                poHead = c;
            }
        }

        // Update status for all components
        public override void Update()
        {
            Component crntP = poHead;
            while(crntP != null)
            {
                crntP.Update();
                crntP = (Component)crntP.pNext;
            }
        }

        // This method is not needed in Composite
        public override bool DerivedEqual(DLink target)
        {
            return true;
        }

        public override void dbgDerivedPrint()
        {
        }
    }
}
