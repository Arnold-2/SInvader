using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ProxySprite : DLink
    {
        public Name name;
        public GameSprite pSprite;
        public float x;
        public float y;
        public float delta = 2.0f;

        public enum Name
        {
            ProxySquid_1,
            ProxySquid_2,
            ProxyCrab_1,
            ProxyCrab_2,
            ProxyOctopus_1,
            ProxyOctopus_2,
            Uninitialized
        }

        public ProxySprite(ProxySprite.Name name, GameSprite gSprite)
        {
            this.name = name;
            this.x = 0.0f;
            this.y = 0.0f;
            this.pSprite = gSprite;
        }

        public ProxySprite()
        {
            this.name = ProxySprite.Name.Uninitialized;
        }

        // Draw the shape
        public void Render()
        {
            this.pSprite.Render();
        }

        // Update shape
        public void Update()
        {
            //Debug.Print(this.name.ToString());
            //Debug.Print("(" + this.x.ToString() + ", " + this.y.ToString() + ")");
            this.pushToSprite();
            this.pSprite.Update();
        }

        public void SwapImage()
        {
            this.pSprite.SwapImage();
        }

        // push to data from proxy to real object
        public void pushToSprite()
        {
            this.pSprite.setX(this.x);
            this.pSprite.setY(this.y);
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((ProxySprite)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  ProxySprite");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }
    }
}
