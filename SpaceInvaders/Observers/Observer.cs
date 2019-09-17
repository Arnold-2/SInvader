using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Observer : DLink
    {
        public Subject.Name name;

        public Observer()
        {
            this.name = Subject.Name.Uninitialized;
        }

        // method getting notification from the subject
        public virtual void Notify()
        {

        }

        public override bool DerivedEqual(DLink node)
        {
            if (((Observer)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  Observer");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }
    }
}
