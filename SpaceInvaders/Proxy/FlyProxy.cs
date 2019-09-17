using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class FlyProxy : Proxy
    {

        public readonly float direction;
        public readonly Random rnd1 = new Random(1);
        public readonly Random rnd2 = new Random(2);
        public readonly Random rnd3 = new Random(3);

        public FlyProxy(GameObject.Category cat, GameSprite s)
        {
            if (cat == GameObject.Category.Missile)
                this.direction = 1f; // upward
            else if (cat == GameObject.Category.Bomb)
                this.direction = -1f; // downward
            else
                this.direction = 0f; // howard

            this.category = Proxy.Category.FlyProxy;
            this.pSprite = s;
        }

        public override void Update()
        {
            // Constantly trying to fly
            if (this.direction == 1f && ProjectileTracker.MissileFlying)
                this.y += delta * direction;
            else if 
            (
                this.direction == -1f 
                && 
                (
                    (this.pSprite.name == GameSprite.Name.DaggerBomb && ProjectileTracker.DaggerFlying)
                    ||
                    (this.pSprite.name == GameSprite.Name.RollingBomb && ProjectileTracker.RollingFlying)
                    ||
                    (this.pSprite.name == GameSprite.Name.ZigZagBomb && ProjectileTracker.ZigzagFlying)
                )
            )
                this.y += delta * direction;

            this.pushToSprite();
            this.pSprite.Update();
        }

        // set movement speed
        public void setDelta(float d)
        {
            this.delta = d;
        }


        public override void pushToSprite()
        {
            this.pSprite.setX(this.x);
            this.pSprite.setY(this.y);
        }

        // shoot a missile
        public void Shoot()
        {
            // missile should be fast
            this.setDelta(10.0f);

            // for missile
            if (this.direction == 1f && !ProjectileTracker.MissileFlying)
                ProjectileTracker.MissileHandle();
        }

        // drop a bomb
        public void Drop(float x, float y, int level)
        {
            

            if (this.direction == -1f)
            {
                if (this.pSprite.name == GameSprite.Name.DaggerBomb && !ProjectileTracker.DaggerFlying)
                {
                    this.setDelta(7.0f + 0.2f * level);
                    // Final: Should be dropping from an alien pro
                    // PA6: get a random dropping point
                    this.x = x;
                    this.y = y;

                    ProjectileTracker.DaggerHandle();
                }
                else if (this.pSprite.name == GameSprite.Name.RollingBomb && !ProjectileTracker.RollingFlying)
                {
                    this.setDelta(5.0f + 0.2f * level);
                    // Final: Should be dropping from an alien pro
                    // PA6: get a random dropping point
                    this.x = x;
                    this.y = y;

                    ProjectileTracker.RollingHandle();
                }
                else if (this.pSprite.name == GameSprite.Name.ZigZagBomb && !ProjectileTracker.ZigzagFlying)
                {
                    this.setDelta(6.0f + 0.2f * level);
                    // Final: Should be dropping from an alien pro
                    // PA6: get a random dropping point
                    this.x = ProjectileTracker.pShip.pProxy.x;
                    this.y = y;

                    ProjectileTracker.ZigZagHandle();
                }
            }
        }
    }
}
