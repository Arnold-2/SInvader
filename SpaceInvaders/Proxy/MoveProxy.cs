using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MoveProxy : Proxy
    {
        public MoveProxy(GameSprite gSprite)
        {
            this.x = gSprite.poAzulSprite.x;
            this.y = gSprite.poAzulSprite.y;
            this.pSprite = gSprite;
        }

        // action methods controls the horizontal movement
        public void Move(float direction)
        {
            this.x += this.delta * direction;
        }

        // action methods let aliens move closer to ship
        public void Advance()
        {
            this.y -= this.delta;
        }

        // set speed of movement
        public void setDelta(float d)
        {
            this.delta = d;
        }

        public void SwapImage()
        {
            this.pSprite.SwapImage();
        }

        public override void pushToSprite()
        {
            this.pSprite.setX(this.x);
            this.pSprite.setY(this.y);
        }




    }
}
