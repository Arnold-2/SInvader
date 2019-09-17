using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Proxy : DLink
    {
        public Category category;
        public GameSprite pSprite;
        public float x;
        public float y;
        public float delta = 1.0f;

        public enum Category
        {
            MoveProxy,
            SwapImageProxy,
            FlyProxy,
            Uninitialized
        }

        // Draw the shape
        public void Render()
        {
            this.pSprite.Render();
        }

        // Update shape
        public virtual void Update()
        {
            //Debug.Print(this.name.ToString());
            //Debug.Print("(" + this.x.ToString() + ", " + this.y.ToString() + ")");
            this.pushToSprite();
            this.pSprite.Update();
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((Proxy)node).category == this.category)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  Proxy");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("--------------");
        }

        // push to data from proxy to real object
        public abstract void pushToSprite();
    }
}
