using System;


namespace SpaceInvaders
{
    // <state>
    public abstract class GameState : IState
    {
        // switch context state reference between concrete states
        public abstract void Handle();

        // load content
        public abstract void load();

        // update content 
        public abstract void update();

        // draw content
        public abstract void draw();


    }
}
