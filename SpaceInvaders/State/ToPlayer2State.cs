using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // <singleton><state>
    class ToPlayer2State : GameState
    {
        private static ToPlayer2State privInstance;

        public static bool isGameOver = false;
        private int hitcount = 0;

        // constructor
        public ToPlayer2State()
        {
         
        } 
        
        // singleton method
        public static GameState getInstance()
        {
            if (privInstance == null)
            {
                privInstance = new ToPlayer2State();
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
                GlobalPlayerStats.isPlayer1Playing = false;
                TimerManager.getInstance().RefreshTimer();
                ProjectileTracker.Reset();
                SpaceInvaders.currentState = PlayerTwoState.getInstance();
            }
                
        }

        // game methods
        public override void load()
        {
            SpriteBatch sb = SpriteBatchManager.getInstance().AddSpriteBatch(SpriteBatch.Name.ToPlayer2, 2);

            

            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ToPlayer2, "PLAYER 2", Glyph.Name.Consolas36pt, 350, 700);

            string level = "LEVEL - " + GlobalPlayerStats.Player2.currentLevel.ToString();
            GlobalPlayerStats.Player2.LevelFont = FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ToPlayer2, level, Glyph.Name.Consolas36pt, 350, 600);
        }

        public override void update()
        {
            // update score bar
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).UpdateAllNodes();
            // update current scene
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ToPlayer2).UpdateAllNodes();
        }

        public override void draw()
        {
            hitcount++;
            
            // render current scene
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ToPlayer2).RenderAllNodes();
            // render score bar
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).RenderAllNodes();
            if (hitcount > 200f)
            {
                this.Handle();
            }
        }
    }
}
