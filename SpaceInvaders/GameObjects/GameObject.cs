using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameObject : Component
    {
        public Category category;
        public int id;
        public Proxy pProxy;
        public GameSprite.Name mGameSprtName;
        public GameSprite mGameSprite;
        public CollisionRect colliRect;
        public bool collidable = true;
        public bool isComposite = false;
        public CompositeType compositeType = CompositeType.NotComposite;
        public int dropID = 0;

        public enum Category
        {
            Squid,
            Crab,
            Octopus,
            Ship,
            Shield,
            Missile,
            Bomb,
            UFO,
            Wall,
            Alien,
            AlienGrid,
            AlienCol,
            Uninitialized
        }

        // Indicate if the object is a composite and what kind of composite it is
        public enum CompositeType
        {
            Grid,
            Column,
            Row,
            NotComposite
        }

        public GameObject(GameObject.Category cate, GameSprite s)
        {
            this.pProxy = s.pProxy;
            this.mGameSprtName = s.name;
            this.mGameSprite = s;
        }

        public GameObject()
        {
            this.category = GameObject.Category.Uninitialized;
        }

        // Update all proxies in the list
        public override void Update()
        {
            
            // recalculate composite rect
            if (this.isComposite)
            {
                this.UpdateCompositeBox();
            }
            // for leaf nodes, recalculate Collision rect based on current location
            else
            {
                this.colliRect.x = this.pProxy.x;
                this.colliRect.y = this.pProxy.y;
                this.colliRect.setDimension(this.colliRect);
            }


            this.pProxy.Update();
            
        }


        // Update collision box based on child
        public void UpdateCompositeBox()
        {
            if (!this.isComposite)
                return;

            
            GameObject current = (GameObject)this.pChildHead;
            CollisionRect cr = null;        

            while (current != null)
            {
                if (current.collidable || current.isComposite)
                {
                    // get box from first collidable child
                    if (cr == null)
                        cr = new CollisionRect(current.colliRect);
                    else
                        cr.Union(current.colliRect);
                }
                current = (GameObject)current.pNextSibling;
            }

            // no collidable child, shrink box to parent's center
            if (cr == null && this.pParent != null)
            {
                GameObject parent = (GameObject)this.pParent;
                cr = new CollisionRect(parent.colliRect.x, parent.colliRect.y, 0.1f, 0.1f);
            }
            else if (cr == null && this.pParent == null)
            {
                cr = new CollisionRect(this.colliRect.x, this.colliRect.y, 0.1f, 0.1f);
            }
               

            this.colliRect.setDimension(cr);
        }

        // Composite method, move composite and all its children
        public void CompositeMove()
        {
            MoveProxy mpx = (MoveProxy)this.pProxy;

            // move itself first
            mpx.x += mpx.delta;

            Component walk = this.pChildHead;

            // move all children (recursively until leaves)
            while (walk != null)
            {
                ((GameObject)walk).CompositeMove();
                walk = walk.pNextSibling;
            }
        }

        public void CompositeAdvance()
        {
            MoveProxy mpx = (MoveProxy)this.pProxy;

            // move itself first
            mpx.y -= 15f;

            Component walk = this.pChildHead;

            // move all children (recursively until leaves)
            while (walk != null)
            {
                ((GameObject)walk).CompositeAdvance();
                walk = walk.pNextSibling;
            }
        }

        // composite metho, set delta direction for all children
        public void CompositeSetDelta(float d)
        {
            MoveProxy mpx = (MoveProxy)this.pProxy;

            // set itself first
            mpx.delta = d;

            Component walk = this.pChildHead;

            // move all children (recursively until leaves)
            while (walk != null)
            {
                ((GameObject)walk).CompositeSetDelta(d);
                walk = walk.pNextSibling;
            }
        }


        // Visitor logic
        // TODO: can be simplified using strategy 
        public override void Accept(Visitor v)
        {
            if (this.category == Category.Wall)
            {
                if (this.mGameSprtName == GameSprite.Name.LeftWall)
                    v.VisitLeftWall();
                else if (this.mGameSprtName == GameSprite.Name.RightWall)
                    v.VisitRightWall();
                else
                    v.VisitWall();
            }
            else if (this.category == Category.Alien)
            {
                v.VisitAlien();
            }
            else if (this.category == Category.Shield)
            {
                v.VisitShield();
            }
            else if (this.category == Category.Missile)
            {
                if (ProjectileTracker.MissileFlying)
                {
                    v.VisitMissile();

                    // Reset location
                    this.pProxy.x = 0f;
                    this.pProxy.y = -10f;
                    this.pProxy.Update();
                    // Flip Missile Handle
                    ProjectileTracker.MissileHandle();
                }
            }
            else if (this.category == Category.Ship)
            {
                v.VisitShip();
            }

            // If hit by missile, start self-destruction process
            if (((GameObject)v).category == Category.Missile || (((GameObject)v).category == Category.Bomb && this.category != Category.Missile))
            {
                // Play explosion animation
                Debug.Print(this.category + " hit by" + ((GameObject)v).category);

                // If ship, trigger next round
                if (this.category == Category.Ship)
                {
                    SoundUtility.getInstance().playExplosion();
                    SpaceInvaders.currentState.Handle();
                    return;
                }

                // If Alien, incremente score
                if (this.mGameSprtName == GameSprite.Name.Octopus)
                    GlobalPlayerStats.incrementScore(10);
                else if (this.mGameSprtName == GameSprite.Name.Crab)
                    GlobalPlayerStats.incrementScore(20);
                else if (this.mGameSprtName == GameSprite.Name.Squid)
                    GlobalPlayerStats.incrementScore(20);

                // set itself as not collidable, if it's not wall
                if (this.category != Category.Wall)
                {
                    if (this.category == Category.Alien)
                    {
                        SoundUtility.getInstance().killed();
                        this.mGameSprite.ShowExplosion();
                    }
                    else if (this.category == Category.UFO)
                    {
                        SoundUtility.getInstance().killed();
                        ((UFOProxy)this.pProxy).reset();
                        GlobalPlayerStats.incrementScore(300);
                        return;
                    }
                    else
                    {
                        SoundUtility.getInstance().playExplosion();
                    }


                    
                    this.collidable = false;
                    // Remove itself from active
                    GameObjectManager.getInstance().Remove(this);
                }
            }
        }

        public override void VisitShip()
        {
           
            Debug.Print(this.pProxy.pSprite.name + " hits ship!");

            // Explode
            // ReDrop immediately
            if (this.pProxy.pSprite.name == GameSprite.Name.DaggerBomb)
            {
                this.dropID = (new Random().Next(0, 10));
         
                ProjectileTracker.DaggerHandle();
            }
            else if (this.pProxy.pSprite.name == GameSprite.Name.RollingBomb)
            {
                this.dropID = (new Random().Next(0, 10));
                ProjectileTracker.RollingHandle();
            }
            else if (this.pProxy.pSprite.name == GameSprite.Name.ZigZagBomb)
            {
                this.dropID = (new Random().Next(0, 10));
                ProjectileTracker.ZigZagHandle();
            }

            // reset bomb location
            this.pProxy.x = -10f;
            this.pProxy.y = -10f;
            this.Update();
        }

        // For Missile object use
        // When missile hits Alien
        public override void VisitAlien()
        {
            if (this.category == Category.Missile)
            {
                // Explode
                //Debug.Print("Missile exloded!");
                // Reset location
                this.pProxy.x = 0f;
                this.pProxy.y = -10f;
                this.pProxy.Update();
                // Flip Missile Handle
                ProjectileTracker.MissileHandle();
            }
        }

        // When projectile hits Wall
        public override void VisitWall()
        {
            if (this.category == Category.Missile) {
                // Explode
                // Reset location
                this.pProxy.x = -10f;
                this.pProxy.y = -10f;
                this.pProxy.Update();
                // Flip Missile Handle
                ProjectileTracker.MissileHandle();
            }
            else if (this.category == Category.Bomb)
            {
                // reset bomb location
                this.pProxy.x = -10f;
                this.pProxy.y = -10f;
                this.pProxy.Update();

                // Explode
                // ReDrop immediately
                if (this.pProxy.pSprite.name == GameSprite.Name.DaggerBomb)
                {
                    this.dropID = (new Random().Next(0, 10));
                    //Debug.Print("Bomb Drop ID: " + this.dropID.ToString());
                    ProjectileTracker.DaggerHandle();
                }
                else if (this.pProxy.pSprite.name == GameSprite.Name.RollingBomb)
                {
                    this.dropID = (new Random().Next(0, 10));
                    ProjectileTracker.RollingHandle();
                }
                else if (this.pProxy.pSprite.name == GameSprite.Name.ZigZagBomb)
                {
                    this.dropID = (new Random().Next(0, 10));
                    ProjectileTracker.ZigZagHandle();
                }
            }
            else if (this.category == Category.AlienGrid)
            {
                //Debug.Print("Alien Grid collides Wall");

                float oldDelta = this.pProxy.delta;

                this.CompositeSetDelta(oldDelta * -1f);
            }
        }

        public override void VisitLeftWall()
        {
            //Debug.Print("Alien Grid collides left Wall!");

            float oldDelta = this.pProxy.delta;

            if (oldDelta < 0)
            {
                this.CompositeSetDelta(oldDelta * -1f);
                this.CompositeAdvance();
            }
                
        }

        public override void VisitRightWall()
        {
            //Debug.Print("Alien Grid collides right Wall!");

            float oldDelta = this.pProxy.delta;

            if (oldDelta > 0)
            {
                this.CompositeSetDelta(oldDelta * -1f);
                this.CompositeAdvance();
            }
                
            
        }



        // When missile hits Shield
        public override void VisitShield()
        {
            if (this.category == Category.Missile)
            {
                // Explode
                // Reset location
                this.pProxy.x = 0f;
                this.pProxy.y = -10f;
                this.pProxy.Update();
                // Flip Missile Handle
                ProjectileTracker.MissileHandle();
            }
            else if (this.category == Category.Bomb)
            {
                // reset bomb location
                this.pProxy.x = -10f;
                this.pProxy.y = -10f;
                this.pProxy.Update();

                // Explode
                // ReDrop immediately
                if (this.pProxy.pSprite.name == GameSprite.Name.DaggerBomb)
                {
                    this.dropID = (new Random().Next(0, 10));
                    ProjectileTracker.DaggerHandle();
                }
                else if (this.pProxy.pSprite.name == GameSprite.Name.RollingBomb)
                {
                    this.dropID = (new Random().Next(0, 10));
                    ProjectileTracker.RollingHandle();
                }
                else if (this.pProxy.pSprite.name == GameSprite.Name.ZigZagBomb)
                {
                    this.dropID = (new Random().Next(0, 10));
                    ProjectileTracker.ZigZagHandle();
                }
            }
        }


        // when bomb hits missile
        public override void VisitMissile()
        {
            if (this.category == Category.Bomb)
            {
                // reset bomb location
                ProjectileTracker.ResetProjectile(this);

                // Explode
                if (this.pProxy.pSprite.name == GameSprite.Name.DaggerBomb)
                {
                    this.dropID = (new Random().Next(0, 10));
                    ProjectileTracker.DaggerHandle();
                }
                else if (this.pProxy.pSprite.name == GameSprite.Name.RollingBomb)
                {
                    this.dropID = (new Random().Next(0, 10));
                    ProjectileTracker.RollingHandle();
                }
                else if (this.pProxy.pSprite.name == GameSprite.Name.ZigZagBomb)
                {
                    this.dropID = (new Random().Next(0, 10));
                    ProjectileTracker.ZigZagHandle();
                }
            }
        }

        // render all proxies in the list
        public void Render()
        {
            if (this.category != Category.Shield || GlobalConfiguration.showShieldFilling)
                this.pProxy.Render();

            //if (this.compositeType == CompositeType.Column)
            if (GlobalConfiguration.showCollisionRect && (GlobalConfiguration.showLeafRect || this.isComposite))
                this.colliRect.Render();
        }


        // Composite Methods
        public void AddChild(GameObject childToAdd)
        {


            // no first child yet
            if (this.pChildHead == null)
            {
                this.pChildHead = childToAdd;
                childToAdd.pParent = this;
            }
            else // add to the front
            {
                this.pChildHead.pPrevSibling = childToAdd;
                childToAdd.pNextSibling = this.pChildHead;
                this.pChildHead = childToAdd;
                childToAdd.pParent = this;
            }
        }

        public GameObject Wash()
        {
            category = Category.Uninitialized;
            id = 0;
            pProxy = null;
            mGameSprtName = GameSprite.Name.Uninitialized;
            mGameSprite = null;
            colliRect = null;
            collidable = true;
            isComposite = false;
            compositeType = CompositeType.NotComposite;
            dropID = 0;

            this.pChildHead = null;
            this.pNextSibling = null;
            this.pPrevSibling = null;
            this.pParent = null;


            return this;
        }

        // Derived Methods
        public override bool DerivedEqual(DLink node)
        {
            if (((GameObject)node).category == this.category)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  GameObject");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Category:      " + this.category);
            Debug.WriteLine("GameSpriteName:" + this.mGameSprtName);
            Debug.WriteLine("--------------");
        }
    }
}

