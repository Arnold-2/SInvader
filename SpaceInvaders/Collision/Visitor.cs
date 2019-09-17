using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Visitor
    {
        public virtual void Accept(Visitor v)
        {
            Debug.Print("Default Accept Triggered!");
        }
        public virtual void VisitAlien()
        {
            Debug.Print("Default VisitAlien Triggered!");
        }
        public virtual void VisitWall()
        {
            Debug.Print("Default VisitWall Triggered!");
        }

        public virtual void VisitShield()
        {
            Debug.Print("Default VisitShield Triggered!");
        }

        public virtual void VisitRightWall()
        {
            Debug.Print("Default VisitRightWall Triggered!");
        }

        public virtual void VisitLeftWall()
        {
            Debug.Print("Default VisitLeftWall Triggered!");
        }

        public virtual void VisitMissile()
        {
            Debug.Print("Default VisitMissile Triggered!");
        }

        public virtual void VisitShip()
        {
            Debug.Print("Default VisitShip Triggered!");
        }

        public virtual void VisitUFO()
        {
            Debug.Print("Default VisitUFO Triggered!");
        }

    }
}
