using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ReleaseUFOCommand : Command
    {
        public float deltaTime = 10f;

        // constructor
        public ReleaseUFOCommand()
        {
        }


        public override void Execute(float deltaTime)
        {

            ProjectileTracker.UFOFlying = true;

            float nextDelta = this.deltaTime + new Random().Next(5, 20);


            //Debug.Print("(" + this.privGameObject.pProxy.x.ToString() + ", " + this.privGameObject.pProxy.y.ToString() + ")");

            TimerManager.getInstance().Add(TimerEvent.Name.SwapImageAnimation, this, nextDelta);
        }
    }
}
