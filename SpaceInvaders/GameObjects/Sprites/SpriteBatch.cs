using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatch : DLink
    {
        public SpriteBatch.Name name;
        public int priority;
        public SpriteBatchNodeManager poBatchNodeManager;

        // Constructor. Only be called when manager adding new reserved nodes.
        public SpriteBatch()
        {
            this.name = SpriteBatch.Name.Uninitialized;
            poBatchNodeManager = new SpriteBatchNodeManager();
        }

        public void AddSprite(SpriteBase sprite)
        {
            poBatchNodeManager.AddSpriteBatchNode(sprite);
        }

        // Change the priority and re-sort the list if necessary
        public void SetPriority (int newP)
        {
            this.priority = newP;

            // It's singleton, we only need to re-sort the only one instance
            SpriteBatchManager boss = SpriteBatchManager.getInstance();
            boss.ReSort(this);
        }

        // Derived Equal function, to be used in Find()
        public override bool DerivedEqual(DLink node)
        {
            if (this.name == ((SpriteBatch)node).name)
                return true;
            return false;
        }

        public void UpdateAllNodes()
        {
            this.poBatchNodeManager.UpdateAllActive();
        }

        public void RenderAllNodes()
        {
            this.poBatchNodeManager.RenderAllActive();
        }

        public SpriteBatch Wash()
        {
            this.name = Name.Uninitialized;
            this.priority = 0;
            this.poBatchNodeManager = new SpriteBatchNodeManager();
            return this;
        }

        // Recycle nodes
        public void RecycleNodes()
        {
            this.poBatchNodeManager.RecycleAllActive();
        }

        // Debugging print function
        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  SpriteBatch");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("Priority:  " + this.priority);
            Debug.WriteLine("Next.Name  " 
                + ((this.pNext == null)? Name.Uninitialized : ((SpriteBatch)this.pNext).name));
            Debug.WriteLine("--------------");
        }

        public enum Name
        {
            Instruction,
            ScoreBar,
            Player1,
            Player2,
            ToPlayer1,
            ToPlayer2,
            GameOver,
            Shared,
            Uninitialized
        }
    }


}

