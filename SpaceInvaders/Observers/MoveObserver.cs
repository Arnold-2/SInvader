using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveObserver : Observer
    {
        private MoveProxy privMoveProxy;
        private float direction = 0f;

        // set observer fields based on name
        public void set(Subject.Name name, MoveProxy mProxy)
        {
            if (name == Subject.Name.LeftKey)
                this.direction = -2f;
            else if (name == Subject.Name.RightKey)
                this.direction = 2f;

            this.privMoveProxy = mProxy;
            this.name = name;
        }

        public override void Notify()
        {
            if ((direction > 0 && privMoveProxy.x >= 876) || (direction < 0 && privMoveProxy.x <= 20))
                return;

            privMoveProxy.Move(direction);
        }
    }
}
