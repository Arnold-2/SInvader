using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // <singleton><state>
    class GameOverState : GameState
    {
        private static GameOverState privInstance;

        public static bool isGameOver = false;

        private int hitcount = 0;

        // constructor
        public GameOverState()
        {
         
        } 
        
        // singleton method
        public static GameState getInstance()
        {
            if (privInstance == null)
            {
                privInstance = new GameOverState();
            }

            return privInstance;
        }

        // switch state
        public override void Handle()
        {
            this.hitcount = 0;

            // if not initialied, do nothing
            if (privInstance == null)
                return;
            else
            {
                GlobalPlayerStats.reset();
                if (GlobalPlayerStats.isPlayer1Playing)
                {
                    PlayerOneState.getInstance().LevelUp();

                    TimerManager.SwapInstance();
                    ColliPairManager.SwapInstance();

                    PlayerTwoState.getInstance().LevelUp();

                    TimerManager.SwapInstance();
                    ColliPairManager.SwapInstance();
                }
                else
                {
                    PlayerTwoState.getInstance().LevelUp();

                    TimerManager.SwapInstance();
                    ColliPairManager.SwapInstance();

                    PlayerOneState.getInstance().LevelUp();

                }
                
                SpaceInvaders.currentState = StartScreenState.getInstance();
                GlobalConfiguration.modeChoosed = false;
            }
                
        }

        // game methods
        public override void load()
        {
            SpriteBatch sb = SpriteBatchManager.getInstance().AddSpriteBatch(SpriteBatch.Name.GameOver, 2);

            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.GameOver, "GAME OVER!", Glyph.Name.Consolas36pt, 350, 700);
        }

        public override void update()
        {
            // update score bar
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).UpdateAllNodes();
            // update current scene
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.GameOver).UpdateAllNodes();
        }

        public override void draw()
        {
            hitcount++;

            // render current scene
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.GameOver).RenderAllNodes();
            // render score bar
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).RenderAllNodes();

            if (hitcount > 200f)
            {
                this.Handle();
            }
        }
    }
}
