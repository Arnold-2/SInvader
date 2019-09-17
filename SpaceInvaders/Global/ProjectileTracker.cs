using System;


namespace SpaceInvaders
{
    // Tracking projectile states
    // Paring Shooter objects with projectiles (missile, bomb)
    static class ProjectileTracker
    {
        public static GameObject pMissile;
        public static GameObject pDaggerBomb;
        public static GameObject pZigZagBomb;
        public static GameObject pRollingBomb;
        public static GameObject pShip;
        public static GameObject pLeftWall;
        public static GameObject pRightWall;
        public static GameObject pTopWall;
        public static GameObject pBottomWall;
        public static GameObject pUFO;
        public static bool MissileFlying = false;
        public static bool DaggerFlying = false;
        public static bool ZigzagFlying = false;
        public static bool RollingFlying = false;
        public static bool UFOFlying = false;

        public static void setMissile(GameObject missile, GameObject ship)
        {
            pMissile = missile;
            pShip = ship;
        }

        public static void setBombs(GameObject dagger, GameObject zigzag, GameObject rolling)
        {
            pDaggerBomb = dagger;
            pZigZagBomb = zigzag;
            pRollingBomb = rolling;
        }

        public static void setWalls(GameObject left, GameObject right, GameObject top, GameObject bottom)
        {
            pLeftWall = left;
            pRightWall = right;
            pTopWall = top;
            pBottomWall = bottom;

        }

        public static void Reset()
        {
            ResetProjectile(pMissile);
            ResetProjectile(pDaggerBomb);
            ResetProjectile(pZigZagBomb);
            ResetProjectile(pRollingBomb);

            MissileFlying = false;
            DaggerFlying = false;
            ZigzagFlying = false;
            RollingFlying = false;
        }

        public static void ResetProjectile(GameObject p)
        {
            if (p.category == GameObject.Category.Missile)
            {
                p.pProxy.x = 0;
                p.pProxy.y = 1000f;
                p.Update();
            }
            else if (p.category == GameObject.Category.Bomb)
            {
                p.pProxy.x = 900f;
                p.pProxy.y = 1000f;
                p.Update();
            }

            
        }

        public static void MissileHandle()
        {
            MissileFlying = !MissileFlying;
        }

        public static void DaggerHandle()
        {
            DaggerFlying = !DaggerFlying;
        }

        public static void ZigZagHandle()
        {
            ZigzagFlying = !ZigzagFlying;
        }

        public static void RollingHandle()
        {
            RollingFlying = !RollingFlying;
        }
    }
}
