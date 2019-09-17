using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    public class SpriteBatchNode : DLink
    {
        public SpriteBatchNode.Name name;
        public SpriteBase pSprite;

        public SpriteBatchNode()
        {
            this.name = SpriteBatchNode.Name.Uninitialized;
            this.pSprite = new GameSprite();
        }

        // Derived Equal function, to be used in Find()
        public override bool DerivedEqual(DLink node)
        {
            if (this.name == ((SpriteBatchNode)node).name)
                return true;
            return false;
        }

        // Debugging print function
        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  SpriteBatchNode");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }
        
        public enum Name
        {
            Uninitialized
        }
    }

   
}
