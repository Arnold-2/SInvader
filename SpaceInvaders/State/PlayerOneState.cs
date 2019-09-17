using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // <singleton><state>
    public class PlayerOneState : GameState
    {
        private static PlayerOneState privInstance;
        public static bool isGameOver = false;

        // constructor
        public PlayerOneState()
        {

        } 
        
        // singleton method
        public static PlayerOneState getInstance()
        {
            if (privInstance == null)
            {
                privInstance = new PlayerOneState();
            }

            return privInstance;
        }

        // switch state
        public override void Handle()
        {
            // if not initialied, do nothing
            if (privInstance == null)
                return;

            GlobalPlayerStats.Player1.life--;
            GlobalPlayerStats.reduceLife();
            ProjectileTracker.Reset();

            // if player two is not over, switch to player two
            if (GlobalConfiguration.isTwoPlayers && GlobalPlayerStats.Player2.life > 0)
            {
                TimerManager.SwapInstance();
                ColliPairManager.SwapInstance();
                SpaceInvaders.currentState = ToPlayer2State.getInstance();
            }
            else if (GlobalPlayerStats.Player1.life > 0) // continue play
            {
                SpaceInvaders.currentState = ToPlayer1State.getInstance();
            }
            else // switch to score screen
                SpaceInvaders.currentState = GameOverState.getInstance();
        }

        // Up the level
        // --rebuild collision and timer manager
        // --reload alient grid and shield grid
        // --regroup sprites into spritebatch
        public void LevelUp()
        {
            TimerManager.DiscardInstance();
            ColliPairManager.DiscardInstance();
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Player1).RecycleNodes();
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Player1).Wash();
            

            this.load();
            GlobalPlayerStats.Player1.alienLeft = 55;
            ProjectileTracker.Reset();

            SpaceInvaders.currentState = ToPlayer1State.getInstance();
        }


        // game methods

        public override void load()
        {
            //---------------------------------------------------------------------------------------------------------
            // Create sprite batch
            //---------------------------------------------------------------------------------------------------------
            SpriteBatchManager.getInstance().AddSpriteBatch(SpriteBatch.Name.Player1, 2);

            //---------------------------------------------------------------------------------------------------------
            // Create game sprites
            //---------------------------------------------------------------------------------------------------------
            GameObject alienGrid = GameSpriteFactory.CreateAlienGrid(SpriteBatch.Name.Player1, 100f, 800f - GlobalPlayerStats.Player1.currentLevel * 20f);

            TimerManager.getInstance().Add(new TimerEvent(TimerEvent.Name.ReleaseUFOCommand, new ReleaseUFOCommand(), 0.5f));

            // PA6 Create shield
            ShieldFactory shieldFactory = new ShieldFactory();

            GameObject shieldGrid_1 = shieldFactory.CreateShield(SpriteBatch.Name.Player1, 150f, 300f, 15f, 7.5f);
            GameObject shieldGrid_2 = shieldFactory.CreateShield(SpriteBatch.Name.Player1, 350f, 300f, 15f, 7.5f);
            GameObject shieldGrid_3 = shieldFactory.CreateShield(SpriteBatch.Name.Player1, 550f, 300f, 15f, 7.5f);
            GameObject shieldGrid_4 = shieldFactory.CreateShield(SpriteBatch.Name.Player1, 750f, 300f, 15f, 7.5f);
   
            // Collision Pair setup
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.WallAlien, ProjectileTracker.pLeftWall, alienGrid);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.WallAlien, ProjectileTracker.pRightWall, alienGrid);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.WallMissile, ProjectileTracker.pTopWall, ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.AlienMissile, alienGrid, ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldMissile, shieldGrid_1, ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldMissile, shieldGrid_2,ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldMissile, shieldGrid_3,ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldMissile, shieldGrid_4, ProjectileTracker.pMissile);


            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_1, ProjectileTracker.pDaggerBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_1, ProjectileTracker.pZigZagBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_1, ProjectileTracker.pRollingBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_2, ProjectileTracker.pDaggerBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_2, ProjectileTracker.pZigZagBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_2, ProjectileTracker.pRollingBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_3, ProjectileTracker.pDaggerBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_3, ProjectileTracker.pZigZagBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_3, ProjectileTracker.pRollingBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_4, ProjectileTracker.pDaggerBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_4, ProjectileTracker.pZigZagBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldBomb, shieldGrid_4, ProjectileTracker.pRollingBomb);

            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.WallBomb, ProjectileTracker.pBottomWall, ProjectileTracker.pDaggerBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.WallBomb, ProjectileTracker.pBottomWall, ProjectileTracker.pZigZagBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.WallBomb, ProjectileTracker.pBottomWall, ProjectileTracker.pRollingBomb);

            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.MissileBomb, ProjectileTracker.pMissile, ProjectileTracker.pDaggerBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.MissileBomb, ProjectileTracker.pMissile, ProjectileTracker.pZigZagBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.MissileBomb, ProjectileTracker.pMissile, ProjectileTracker.pRollingBomb);

            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShipBomb, ProjectileTracker.pShip, ProjectileTracker.pDaggerBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShipBomb, ProjectileTracker.pShip, ProjectileTracker.pZigZagBomb);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShipBomb, ProjectileTracker.pShip, ProjectileTracker.pRollingBomb);

            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.UFOMissile, ProjectileTracker.pUFO, ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldAlien, alienGrid, ProjectileTracker.pMissile);




        }

        public override void update()
        {
            if (GlobalTimer.player1 == -1f)
                GlobalTimer.player1Delta = GlobalTimer.currentTime;

            GlobalTimer.player1 = GlobalTimer.currentTime - GlobalTimer.player1Delta;
           
            // update shared objects
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Shared).UpdateAllNodes();
            // update score bar
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).UpdateAllNodes();
            // update current player
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Player1).UpdateAllNodes();
            
            // update timer events
            TimerManager.getInstance().Update(GlobalTimer.currentTime);
            // update observers
            SubjectManager.getInstance().UpdateSubjects();
            // update collision
            ColliPairManager.getInstance().Process();

            // level up logic
            if (GlobalPlayerStats.Player1.alienLeft == 0)
            {
                this.LevelUp();
            }
        }

        public override void draw()
        {
            // render player 1 sprites
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Player1).RenderAllNodes();

            // render shared sprites
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Shared).RenderAllNodes();

            // render score bar
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).RenderAllNodes();

        }

        

    }
}
