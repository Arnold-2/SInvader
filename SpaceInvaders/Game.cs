using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        SoundUtility soundUtil = SoundUtility.getInstance();
        public static GameState currentState;

        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("SpaceInvaders - SE456 Final Project");
            this.SetWidthHeight(896, 1024);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            // Initialize current game state
            currentState = StartScreenState.getInstance();


            GlobalConfiguration.showCollisionRect = true;
            GlobalConfiguration.showShieldFilling = true;
            GlobalConfiguration.showLeafRect = false;
            GlobalConfiguration.isTwoPlayers = true;
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------

        public override void LoadContent()
        {
            // Load Shared Elements
            TextureManager texMgr = TextureManager.getInstance();
            ImageManager imgMgr = ImageManager.getInstance();

            // Load Textures
            texMgr.AddTexture(Texture.Name.Alien, "Invaders_0.tga");
            texMgr.AddTexture(Texture.Name.Shield, "birds_N_shield.tga");
            texMgr.AddTexture(Texture.Name.Font, "Consolas36pt.tga");

            // Load Glyphs
            GlyphManager.getInstance().AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Font);

            // Load Images
            imgMgr.AddImage(Image.Name.Squid_1, Texture.Name.Alien, 610.0f, 25.0f, 119.0f, 122.0f);
            imgMgr.AddImage(Image.Name.Squid_2, Texture.Name.Alien, 610.0f, 173.0f, 119.0f, 122.0f);
            imgMgr.AddImage(Image.Name.Crab_1, Texture.Name.Alien, 316.0f, 25.0f, 166.0f, 119.0f);
            imgMgr.AddImage(Image.Name.Crab_2, Texture.Name.Alien, 316.0f, 180.0f, 166.0f, 119.0f);
            imgMgr.AddImage(Image.Name.Octopus_1, Texture.Name.Alien, 57.0f, 16.0f, 197.0f, 132.0f);
            imgMgr.AddImage(Image.Name.Octopus_2, Texture.Name.Alien, 57.0f, 170.0f, 197.0f, 132.0f);
            imgMgr.AddImage(Image.Name.Ship, Texture.Name.Alien, 50f, 331f, 193f, 126f);
            imgMgr.AddImage(Image.Name.Missile, Texture.Name.Alien, 417f, 698f, 19f, 60f);
            imgMgr.AddImage(Image.Name.Nothing, Texture.Name.Alien, 21f, 480f, 341, 14f);
            imgMgr.AddImage(Image.Name.Brick, Texture.Name.Shield, 20, 210, 10, 5);
            imgMgr.AddImage(Image.Name.BrickLeft_Top0, Texture.Name.Shield, 15, 180, 10, 5);
            imgMgr.AddImage(Image.Name.BrickLeft_Top1, Texture.Name.Shield, 15, 185, 10, 5);
            imgMgr.AddImage(Image.Name.BrickLeft_Bottom, Texture.Name.Shield, 35, 215, 10, 5);
            imgMgr.AddImage(Image.Name.BrickRight_Top0, Texture.Name.Shield, 75, 180, 10, 5);
            imgMgr.AddImage(Image.Name.BrickRight_Top1, Texture.Name.Shield, 75, 185, 10, 5);
            imgMgr.AddImage(Image.Name.BrickRight_Bottom, Texture.Name.Shield, 55, 215, 10, 5);
            imgMgr.AddImage(Image.Name.DaggerBomb, Texture.Name.Alien, 275, 795, 43, 88);
            imgMgr.AddImage(Image.Name.RollingBomb, Texture.Name.Alien, 486, 639, 50, 106);
            imgMgr.AddImage(Image.Name.ZigzagBomb, Texture.Name.Alien, 570, 639, 50, 106);
            imgMgr.AddImage(Image.Name.AlienExplosion, Texture.Name.Alien, 567, 484, 197, 125);
            imgMgr.AddImage(Image.Name.ShipExplosion, Texture.Name.Alien, 558, 334, 228, 120);
            imgMgr.AddImage(Image.Name.MissileExplosion, Texture.Name.Alien, 698, 796, 94, 121);
            imgMgr.AddImage(Image.Name.UFOExplosion, Texture.Name.Alien, 40, 643, 295, 112);
            imgMgr.AddImage(Image.Name.UFO, Texture.Name.Alien, 76, 501, 240, 107);

            
            SpriteBatchManager.getInstance().AddSpriteBatch(SpriteBatch.Name.Shared, 2);

            GameSprite Ship = GameSpriteFactory.CreateGameSprite(GameObject.Category.Ship, GameSprite.Name.Ship, SpriteBatch.Name.Shared, 400f, 150f, 40.0f, 25.0f, Image.Name.Ship);
            GameSprite Missile = GameSpriteFactory.CreateGameSprite(GameObject.Category.Missile, GameSprite.Name.Missile, SpriteBatch.Name.Shared, 400f, -40f, 4f, 10f, Image.Name.Missile);
            GameSprite WallTop = GameSpriteFactory.CreateGameSprite(GameObject.Category.Wall, GameSprite.Name.TopWall, SpriteBatch.Name.Shared, 448f, 940f, 896f, 0.1f, Image.Name.Nothing);
            GameSprite WallBottom = GameSpriteFactory.CreateGameSprite(GameObject.Category.Wall, GameSprite.Name.BottomWall, SpriteBatch.Name.Shared, 448f, 137.5f, 1000f, 0.1f, Image.Name.Nothing);
            GameSprite WallLeft = GameSpriteFactory.CreateGameSprite(GameObject.Category.Wall, GameSprite.Name.LeftWall, SpriteBatch.Name.Shared, 30f, 512f, 0.1f, 1024f, Image.Name.Nothing);
            GameSprite WallRight = GameSpriteFactory.CreateGameSprite(GameObject.Category.Wall, GameSprite.Name.RightWall, SpriteBatch.Name.Shared, 866f, 512f, 0.1f, 1024f, Image.Name.Nothing);

            GameSprite DaggerBomb = GameSpriteFactory.CreateGameSprite(GameObject.Category.Bomb, GameSprite.Name.DaggerBomb, SpriteBatch.Name.Shared, 900f, 800f, 7f, 15f, Image.Name.DaggerBomb);
            GameSprite ZigzagBomb = GameSpriteFactory.CreateGameSprite(GameObject.Category.Bomb, GameSprite.Name.ZigZagBomb, SpriteBatch.Name.Shared, 900f, 800f, 7f, 15f, Image.Name.ZigzagBomb);
            GameSprite RollingBomb = GameSpriteFactory.CreateGameSprite(GameObject.Category.Bomb, GameSprite.Name.RollingBomb, SpriteBatch.Name.Shared, 900f, 800f, 7f, 15f, Image.Name.RollingBomb);
            GameSprite UFO = GameSpriteFactory.CreateGameSprite(GameObject.Category.UFO, GameSprite.Name.UFO, SpriteBatch.Name.Shared, 960f, 900f, 50f, 20f, Image.Name.UFO);

            ProjectileTracker.setMissile(Missile.pGameObject, Ship.pGameObject);
            ProjectileTracker.setBombs(DaggerBomb.pGameObject, ZigzagBomb.pGameObject, RollingBomb.pGameObject);
            ProjectileTracker.setWalls(WallLeft.pGameObject, WallRight.pGameObject, WallTop.pGameObject, WallBottom.pGameObject);
            ProjectileTracker.pUFO = UFO.pGameObject;

            // Bind keys events
            SubjectManager subjectMgr = SubjectManager.getInstance();
            subjectMgr.CreateAllSubjects();
            subjectMgr.FindSubjectByName(Subject.Name.LeftKey).Subscribe(Ship.pGameObject);
            subjectMgr.FindSubjectByName(Subject.Name.RightKey).Subscribe(Ship.pGameObject);
            subjectMgr.FindSubjectByName(Subject.Name.SpaceKey).Subscribe(Missile.pGameObject);


            // Load Start and score screen
            StartScreenState.getInstance().load();
            ToPlayer1State.getInstance().load();
            ToPlayer2State.getInstance().load();
            GameOverState.getInstance().load();

            // Load player1
            PlayerOneState.getInstance().load();
            
            // Swap timer instance to load player2
            TimerManager.SwapInstance();
            ColliPairManager.SwapInstance();

            // Load Player2
            PlayerTwoState.getInstance().load();

            //Swap back to start with player 1
            ColliPairManager.SwapInstance();
            TimerManager.SwapInstance();

            
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------

        public override void Update()
        {
            //InputTest.KeyboardTest();
            //--------------------------------------------------------
            // Change Texture, TextureRect, Color
            //--------------------------------------------------------
            // keep sound engine running
            soundUtil.Update();
            GlobalTimer.currentTime = this.GetTime();

            if (!GlobalConfiguration.modeChoosed)
            {
                if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1) == true)
                {
                    GlobalConfiguration.isTwoPlayers = false;
                    // Always starts at player1
                    currentState = ToPlayer1State.getInstance();

                    GlobalConfiguration.modeChoosed = true;
                }
                else if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2) == true)
                {
                    GlobalConfiguration.isTwoPlayers = true;
                    // Always starts at player1
                    currentState = ToPlayer1State.getInstance();

                    GlobalConfiguration.modeChoosed = true;
                }
            }


            currentState.update();
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            currentState.draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {

        }

    }
}

