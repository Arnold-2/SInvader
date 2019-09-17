using System;


namespace SpaceInvaders
{
    public abstract class Component : DLink
    {
        public Component pParent;
        public Component pChildHead;
        public Component pNextSibling;
        public Component pPrevSibling;
        public abstract void Update();
    }
}
