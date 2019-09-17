using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // <singleton><state>
    class StartScreenState : GameState
    {
        private static StartScreenState privInstance;
        public static bool isGameOver = false;

        // constructor
        public StartScreenState()
        {
        } 
        
        // singleton method
        public static GameState getInstance()
        {
            if (privInstance == null)
            {
                privInstance = new StartScreenState();
            }

            return privInstance;
        }

        // switch state
        public override void Handle()
        {
            // if not initialied, do nothing
            if (privInstance == null)
                return;
            else
                SpaceInvaders.currentState = PlayerOneState.getInstance();
        }

        // game methods
        public override void load()
        {
            // Score Title
            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ScoreBar, "SCORE(P1)", Glyph.Name.Consolas36pt, 20, 1000);
            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ScoreBar, "HIGH SCORE", Glyph.Name.Consolas36pt, 350, 1000);
            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ScoreBar, "SCORE(P2)", Glyph.Name.Consolas36pt, 700, 1000);
            // Score Number
            GlobalPlayerStats.P1_Score = FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ScoreBar, "0000", Glyph.Name.Consolas36pt, 30, 965);
            GlobalPlayerStats.HighestScore = FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ScoreBar, "0000", Glyph.Name.Consolas36pt, 390, 965);
            GlobalPlayerStats.P2_Score = FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ScoreBar, "0000", Glyph.Name.Consolas36pt, 750, 965);

            GlobalPlayerStats.P1_Life = FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ScoreBar, "P1 HP = " + GlobalPlayerStats.Player1.life.ToString(), Glyph.Name.Consolas36pt, 25f, 50f);
            GlobalPlayerStats.P2_Life = FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.ScoreBar, "P2 HP = " + GlobalPlayerStats.Player2.life.ToString(), Glyph.Name.Consolas36pt, 696f, 50f);


            // Instructions
            // Game Title
            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.Instruction, "SPACE INVADERS", Glyph.Name.Consolas36pt, 320, 675);
            // Score Table
            GameSprite instrSquid = GameSpriteFactory.Create(GameObject.Category.Squid, GameSprite.Name.Squid_1, 370, 550, Image.Name.Squid_1);
            GameSprite instrCrab = GameSpriteFactory.Create(GameObject.Category.Crab, GameSprite.Name.Crab_1, 370, 450, Image.Name.Crab_1);
            GameSprite instrOctopus = GameSpriteFactory.Create(GameObject.Category.Octopus, GameSprite.Name.Octopus_1, 370, 350, Image.Name.Octopus_1);
            
            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.Instruction, "= 30 Pts", Glyph.Name.Consolas36pt, 420, 550);
            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.Instruction, "= 20 Pts", Glyph.Name.Consolas36pt, 420, 450);
            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.Instruction, "= 10 Pts", Glyph.Name.Consolas36pt, 420, 350);

            // Player Choice
            FontManager.getInstance().AddFont(Font.Name.Title, SpriteBatch.Name.Instruction, "1 - 1 Player   2 - 2 Players", Glyph.Name.Consolas36pt, 220, 150);

            // Add GameSprite to SpriteBatch
            SpriteBatch sbInstr = SpriteBatchManager.getInstance().AddSpriteBatch(SpriteBatch.Name.Instruction, 1);
            sbInstr.AddSprite(instrSquid);
            sbInstr.AddSprite(instrCrab);
            sbInstr.AddSprite(instrOctopus);
        }

        public override void update()
        {
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).UpdateAllNodes();
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Instruction).UpdateAllNodes();
        }

        public override void draw()
        {
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).RenderAllNodes();
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Instruction).RenderAllNodes();
        }
    }
}
