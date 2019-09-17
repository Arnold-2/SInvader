using System;

namespace SpaceInvaders
{
    public class SwapImgAnimation : Command
    {
        public float deltaTime; // Execution interval
        private GameObject privGameObject;

        public SwapImgAnimation(GameObject gObject, float dTime)
        {
            privGameObject = gObject;

            // set default speed and delta time
            this.deltaTime = dTime;
        }



        public void setStats(float dTime)
        {
            this.deltaTime = dTime;
        }



        public override void Execute(float deltaTime)
        {
            // Swap Image
            ((MoveProxy)(this.privGameObject.pProxy)).SwapImage();

            int alienShoot;

            if (GlobalPlayerStats.isPlayer1Playing)
                alienShoot = 55 - GlobalPlayerStats.Player1.alienLeft;
            else
                alienShoot = 55 - GlobalPlayerStats.Player2.alienLeft;

            TimerManager.getInstance().Add(TimerEvent.Name.SwapImageAnimation, this, this.deltaTime - 0.005f * alienShoot);
        }
    }

}
