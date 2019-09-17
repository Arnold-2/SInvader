using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombDropCommand : Command
    {
        public float deltaTime;
        private GameObject privGameObject;
        private int level;

        // constructor
        public BombDropCommand(GameObject go, int level)
        {
            this.privGameObject = go;
            this.level = level;
            this.deltaTime = 3.0f - 0.2f * level;
        }

        public void setDelta(float dTime)
        {
            this.deltaTime = dTime;
        }


        public override void Execute(float deltaTime)
        {
            float newtime = deltaTime;

            if (!ProjectileTracker.DaggerFlying && privGameObject.dropID == ProjectileTracker.pDaggerBomb.dropID)
            {
                float x = privGameObject.colliRect.x;
                float y = privGameObject.colliRect.y - privGameObject.colliRect.height / 2f;

                if (y < 150f)
                    y = 160f;

                //Debug.Print("Dagger Drop ID: " + privGameObject.dropID.ToString());

                ((FlyProxy)(ProjectileTracker.pDaggerBomb.pProxy)).Drop(x, y, level);
            }
            else if (!ProjectileTracker.RollingFlying && privGameObject.dropID == ProjectileTracker.pRollingBomb.dropID)
            {
                float x = privGameObject.colliRect.x;
                float y = privGameObject.colliRect.y - privGameObject.colliRect.height / 2f;

                if (y < 150f)
                    y = 160f;

                //Debug.Print("Rolling Drop ID: " + privGameObject.dropID.ToString());

                ((FlyProxy)(ProjectileTracker.pRollingBomb.pProxy)).Drop(x, y, level);

                newtime *= 0.98f;
            }
            else if (!ProjectileTracker.ZigzagFlying && privGameObject.dropID == ProjectileTracker.pZigZagBomb.dropID)
            {
                float x = privGameObject.colliRect.x;
                float y = privGameObject.colliRect.y - privGameObject.colliRect.height / 2f;

                if (y < 150f)
                    y = 160f;

                //Debug.Print("ZigZag Drop ID: " + privGameObject.dropID.ToString());

                ((FlyProxy)(ProjectileTracker.pZigZagBomb.pProxy)).Drop(x, y, level);

                newtime *= 0.96f;
            }



            //Debug.Print("(" + this.privGameObject.pProxy.x.ToString() + ", " + this.privGameObject.pProxy.y.ToString() + ")");

            TimerManager.getInstance().Add(TimerEvent.Name.SwapImageAnimation, this, newtime);
        }
    }
}
