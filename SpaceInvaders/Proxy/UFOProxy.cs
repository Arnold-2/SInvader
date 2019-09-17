using System;


namespace SpaceInvaders
{
    public class UFOProxy : Proxy
    {
        public UFOProxy(GameSprite s)
        {
            this.pSprite = s;
            this.y = 900f;
        }

        public void reset()
        {
            this.x = 1000f;
            ProjectileTracker.UFOFlying = false;
        }

        public override void Update()
        {
            if (this.x < -30f)
            {
                this.x = 1000f;
                ProjectileTracker.UFOFlying = false;
            }
                

            // Constantly trying to show up
            if (ProjectileTracker.UFOFlying)
            {
                this.x -= 2.0f;
            }
                
            

            this.pushToSprite();
            this.pSprite.Update();
        }

        public override void pushToSprite()
        {
            this.pSprite.setX(this.x);
            this.pSprite.setY(this.y);
        }
    }
}
