using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class ShootObserver : Observer
    {
        public MoveProxy pShipProxy;
        public FlyProxy pMissileProxy;

        public ShootObserver()
        {
            this.pMissileProxy = (FlyProxy)ProjectileTracker.pMissile.pProxy;
            this.pShipProxy = (MoveProxy)ProjectileTracker.pShip.pProxy;
        }

        public override void Notify()
        {
            // If there's a missile flying, we don't want to do anything
            if (!ProjectileTracker.MissileFlying)
            {
                // Adjust missile position
                this.pMissileProxy.x = this.pShipProxy.x;
                this.pMissileProxy.y = this.pShipProxy.y;

                //Debug.Print("Shoot");

                SoundUtility.getInstance().shoot();
                pMissileProxy.Shoot();
                
            }
            
        }
    }
}
