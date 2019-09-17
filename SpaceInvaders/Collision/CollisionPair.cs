using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionPair : DLink
    {
        public Name name;
        public GameObject treeHost;
        public GameObject treeVisitor;

        // Host priority: Wall -> Ship (UFO)-> Shield -> Missile = Bomb
        public enum Name
        {
            WallShip,
            WallAlien,
            WallMissile,
            WallBomb,
            ShipAlien,
            ShipBomb,
            UFOMissile,
            AlienMissile,
            ShieldAlien,
            ShieldMissile,
            ShieldBomb,
            MissileBomb,
            Uninitialized
        }

        public CollisionPair ()
        {
            this.name = Name.Uninitialized;
            this.treeHost = null;
            this.treeVisitor = null;
        }

        // called by manager factory method, to set value for newly pulled object
        public void set(CollisionPair.Name _name, GameObject host, GameObject visitor)
        {
            this.name = _name;
            this.treeHost = host;
            this.treeVisitor = visitor;
        }

        // will be called in every refresh, to check if the pair has collided
        // if so, start the visting
        public void Process()
        {
            this.Collide();
        }

        public void Collide()
        {
            GameObject currentHost = this.treeHost;
            GameObject currentVisitor = this.treeVisitor;

            // It is not possible to hit column before grid
            //  There's no standalone column

            if (currentHost.isComposite)
            {
                // if current host collide with the current visitor, go next level
                if (CollisionRect.Intersect(currentHost.colliRect, currentVisitor.colliRect))
                {
                    // Go to column level
                    currentHost = (GameObject)currentHost.pChildHead;
                    while (currentHost != null)
                    {
                        if (CollisionRect.Intersect(currentHost.colliRect, currentVisitor.colliRect))
                        {
                            // Go to leaf level
                            currentHost = (GameObject)currentHost.pChildHead;
                            while (currentHost != null)
                            {
                                if (currentHost.collidable && CollisionRect.Intersect(currentHost.colliRect, currentVisitor.colliRect))
                                {
                                    Debug.Print("<CollisionPair>: Collided!");

                                    // Visitor visits host
                                    currentHost.Accept(this.treeVisitor);
                                    return;
                                }
                                currentHost = (GameObject)currentHost.pNextSibling;
                            }
                        }
                        if (currentHost != null)
                            currentHost = (GameObject)currentHost.pNextSibling;
                        else
                            break;
                    }
                }
            }
            else
            {
                if (this.treeHost.collidable && (CollisionRect.Intersect(currentHost.colliRect, currentVisitor.colliRect)))
                    this.treeHost.Accept(this.treeVisitor);
            }
           
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((CollisionPair)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  CollisionPair");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Category:      " + this.name);
            Debug.WriteLine("--------------");
        }
    }
}
