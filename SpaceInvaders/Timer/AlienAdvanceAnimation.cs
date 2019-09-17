using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienAdvanceAnimation : Command
    {
        public float deltaTime;
        private GameObject privGameObject;

        // constructor
        public AlienAdvanceAnimation(GameObject go, float delta)
        {
            this.privGameObject = go;
            this.deltaTime = delta;
        }

        public void setDelta(float dTime)
        {
            this.deltaTime = dTime;
        }


        public override void Execute(float deltaTime)
        {
            this.privGameObject.CompositeMove();
            //Debug.Print("(" + this.privGameObject.pProxy.x.ToString() + ", " + this.privGameObject.pProxy.y.ToString() + ")");

            SoundUtility.getInstance().Advancing();

            int alienShoot;

            if (GlobalPlayerStats.isPlayer1Playing)
                alienShoot = 55 - GlobalPlayerStats.Player1.alienLeft;
            else
                alienShoot = 55 - GlobalPlayerStats.Player2.alienLeft;


            TimerManager.getInstance().Add(TimerEvent.Name.SwapImageAnimation, this, this.deltaTime - 0.005f * alienShoot);
        }

    }
}
