using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public static class GlobalPlayerStats
    {
        public static playerstats Player1 = new playerstats();
        public static playerstats Player2 = new playerstats();

        public static Font P1_Score;
        public static Font P2_Score;
        public static Font P1_Life;
        public static Font P2_Life;


        public static Font HighestScore;
        public static bool isPlayer1Playing = true;

        public class playerstats
        {
            public int score = 0;
            public int life = 2;
            public int alienLeft = 55;
            public int currentLevel = 0;
            public Font LevelFont;

            public void IncrementScore(int s)
            {
                this.score += s;
                
                // we don't want to record UFO
                if (s < 100)
                    this.alienLeft--;

                if (this.alienLeft == 0)
                {
                    // up the level
                    this.currentLevel++;

                    this.LevelFont.UpdateMessage("LEVEL - " + this.currentLevel.ToString());
                }
            }

            public void reset()
            {
                this.score = 0;
                this.life = 2;
                this.alienLeft = 55;
                this.currentLevel = 0;
            }
        }

        public static void reduceLife()
        {
            if (isPlayer1Playing)
            {
                P1_Life.UpdateMessage("P1 HP = " + Player1.life.ToString());
            }
            else
            {
                P2_Life.UpdateMessage("P2 HP = " + Player2.life.ToString());
            }
        }

        public static void incrementScore(int s)
        {
            if (isPlayer1Playing)
            {
                Player1.IncrementScore(s);

                P1_Score.UpdateMessage(getFormattedScore(Player1.score));
            }
            else
            {
                Player2.IncrementScore(s);

                P2_Score.UpdateMessage(getFormattedScore(Player2.score));
            }

            if (Player1.score >= Player2.score)
            {
                HighestScore.UpdateMessage(getFormattedScore(Player1.score));
            }
            else
            {
                HighestScore.UpdateMessage(getFormattedScore(Player2.score));
            }

        }

        public static void reset()
        {
            Player1.reset();
            Player2.reset();
            P1_Score.UpdateMessage("0000");
            P2_Score.UpdateMessage("0000");
            P1_Life.UpdateMessage("P1 HP = " + Player1.life.ToString());
            P2_Life.UpdateMessage("P2 HP = " + Player2.life.ToString());
        }

        public static string getFormattedScore(int s)
        {
            string s0 = (s % 10).ToString();
            string s1 = ((s / 10) % 10).ToString();
            string s2 = ((s / 100) % 10).ToString();
            string s3 = ((s / 1000) % 10).ToString() ;

            // 4 digits, prepend by 0
            return s3 + s2 + s1 + s0;
        }
    }
}
