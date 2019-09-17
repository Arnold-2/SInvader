using System;

namespace SpaceInvaders
{
    public class AnimationSprite : Command
    {
        public float deltaTime; // Execution interval
        private GameObject privGameObject;
        
        public AnimationSprite(GameObject gObject, float dTime)
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

            TimerManager.getInstance().Add(TimerEvent.Name.SwapImageAnimation, this, deltaTime);
        }
    }
}
