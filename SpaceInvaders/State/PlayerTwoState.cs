using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // <singleton><state>
    class PlayerTwoState : GameState
    {
        private static PlayerTwoState privInstance;
        public static bool isGameOver = false;

        // constructor
        public PlayerTwoState()
        {
            
        } 
        
        // singleton method
        public static PlayerTwoState getInstance()
        {
            if (privInstance == null)
            {
                privInstance = new PlayerTwoState();
            }

            return privInstance;
        }

        // switch state
        public override void Handle()
        {
            // if not initialied, do nothing
            if (privInstance == null)
                return;

            GlobalPlayerStats.Player2.life--;
            GlobalPlayerStats.reduceLife();
            ProjectileTracker.Reset();

            // if player two is not over, switch to player two
            if (GlobalConfiguration.isTwoPlayers && GlobalPlayerStats.Player1.life > 0)
            {
                TimerManager.SwapInstance();
                ColliPairManager.SwapInstance();
                SpaceInvaders.currentState = ToPlayer1State.getInstance();
            }
            else if (GlobalPlayerStats.Player2.life > 0) // continue play
            {
                SpaceInvaders.currentState = ToPlayer2State.getInstance();
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
            // Clean up the managers
            TimerManager.DiscardInstance();
            ColliPairManager.DiscardInstance();
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Player2).RecycleNodes();
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Player2).Wash();


            this.load();

            GlobalPlayerStats.Player2.alienLeft = 55;
            ProjectileTracker.Reset();

            SpaceInvaders.currentState = ToPlayer2State.getInstance();
        }

        // game methods
        public override void load()
        {

            //---------------------------------------------------------------------------------------------------------
            // Create sprite batch
            //---------------------------------------------------------------------------------------------------------
            SpriteBatchManager.getInstance().AddSpriteBatch(SpriteBatch.Name.Player2, 2);

            //---------------------------------------------------------------------------------------------------------
            // Create game sprites
            //---------------------------------------------------------------------------------------------------------
            GameObject alienGrid = GameSpriteFactory.CreateAlienGrid(SpriteBatch.Name.Player2, 50f, 800f);

            TimerManager.getInstance().Add(new TimerEvent(TimerEvent.Name.ReleaseUFOCommand, new ReleaseUFOCommand(), 0.5f));

            // PA6 Create shield
            ShieldFactory shieldFactory = new ShieldFactory();

            GameObject shieldGrid_1 = shieldFactory.CreateShield(SpriteBatch.Name.Player2, 150f, 300f, 15f, 7.5f);
            GameObject shieldGrid_2 = shieldFactory.CreateShield(SpriteBatch.Name.Player2, 350f, 300f, 15f, 7.5f);
            GameObject shieldGrid_3 = shieldFactory.CreateShield(SpriteBatch.Name.Player2, 550f, 300f, 15f, 7.5f);
            GameObject shieldGrid_4 = shieldFactory.CreateShield(SpriteBatch.Name.Player2, 750f, 300f, 15f, 7.5f);

            // Collision Pair setup
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.WallAlien, ProjectileTracker.pLeftWall, alienGrid);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.WallAlien, ProjectileTracker.pRightWall, alienGrid);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.WallMissile, ProjectileTracker.pTopWall, ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.AlienMissile, alienGrid, ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldMissile, shieldGrid_1, ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldMissile, shieldGrid_2, ProjectileTracker.pMissile);
            ColliPairManager.getInstance().AddCollisionPair(CollisionPair.Name.ShieldMissile, shieldGrid_3, ProjectileTracker.pMissile);
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
        }

        public override void update()
        {
            if (GlobalTimer.player2 == -1f)
                GlobalTimer.player2Delta = GlobalTimer.currentTime;

            GlobalTimer.player2 = GlobalTimer.currentTime - GlobalTimer.player1Delta;


            // update shared objects
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Shared).UpdateAllNodes();

            // update score bar
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).UpdateAllNodes();
            // update current player
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Player2).UpdateAllNodes();
            
            // update timer events
            TimerManager.getInstance().Update(GlobalTimer.currentTime);
            // update observers
            SubjectManager.getInstance().UpdateSubjects();
            // update collision
            ColliPairManager.getInstance().Process();

            // level up logic

            if (GlobalPlayerStats.Player2.alienLeft == 0)
            {
                this.LevelUp();
            }

        }

        public override void draw()
        {
            // render player2 sprites
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Player2).RenderAllNodes();

            // render shared sprites
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.Shared).RenderAllNodes();

            // render score bar
            SpriteBatchManager.getInstance().FindSpriteBatchByName(SpriteBatch.Name.ScoreBar).RenderAllNodes();
        }
    }
}
