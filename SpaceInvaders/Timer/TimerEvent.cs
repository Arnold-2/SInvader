using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TimerEvent : DLink
    {
        public Name name;
        public Command pCommand;
        public float triggerTime;
        public float deltaTime;

        public TimerEvent () : base()
        {
            this.name = Name.Uninitialized;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        public TimerEvent (Name name, Command cmd, float deltaTimeToTrigger)
        {
            this.pCommand = cmd;
            this.deltaTime = deltaTimeToTrigger;

            this.triggerTime = GlobalTimer.currentTime + deltaTimeToTrigger;
            
        }

        // Execute command based on delta time
        public void process()
        {
            this.pCommand.Execute(deltaTime);
        }


        public override void dbgDerivedPrint()
        {
            //Debug.Print("Timer Event -------------------");
            //Debug.Print("Trigger Time: " + this.triggerTime.ToString());
        }

        public override bool DerivedEqual(DLink node)
        {
            return ((TimerEvent)node).name == this.name;
        }

        public enum Name
        {
            SwapImageAnimation,
            AlienAdvanceAnimation,
            DropBombCommand,
            ReleaseUFOCommand,
            Uninitialized
        }

    }
}
