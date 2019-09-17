using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    static class GameSpriteFactory
    {
        public static GameSpriteManager pGameSpriteManager = GameSpriteManager.getInstance();

        public static GameSprite Create(GameObject.Category cate, GameSprite.Name name, float x, float y, Image.Name imgName)
        {
            Image img = ImageManager.getInstance().FindImageByName(imgName);
            Debug.Assert(img != null);

            float w;
            float h;

            // For missile and bomb, we don't need a square
            if (cate == GameObject.Category.Missile)
            {
                w = 2.5f;
                h = 10.0f;
            }
            else if (cate == GameObject.Category.Bomb)
            {
                w = 5f;
                h = 20f;
            }
            else if (cate == GameObject.Category.Wall)
            {
                w = 896f;
                h = 1f;
            }
            else
            {
                w = 50f;
                h = 50f;
            }


            return pGameSpriteManager.AddGameSprite(cate, name, 0f, 1.0f, 1.0f, x, y, img, 400f, 500f, w, h);
        }


        public static GameSprite CreateGameSprite(GameObject.Category cate, GameSprite.Name name, SpriteBatch.Name sbName
            , float x, float y, float w, float h, Image.Name imgName)
        {
            Image img = ImageManager.getInstance().FindImageByName(imgName);
            Debug.Assert(img != null);

            GameSprite ret = pGameSpriteManager.AddGameSprite(cate, name, 0f, 1.0f, 1.0f, x, y, img, 400f, 500f, w, h);

            // Add to sprite batch
            SpriteBatch sb = SpriteBatchManager.getInstance().FindSpriteBatchByName(sbName);
            Debug.Assert(sb != null);
            sb.AddSprite(ret);
            // When Show collision box toggled on, add collision box to the batch as well
            if (GlobalConfiguration.showCollisionRect == true)
                sb.AddSprite(ret.pGameObject.colliRect.box);

            return ret;
        }

        // create 5 * 11 alien grid for given player at given location
        public static GameObject CreateAlienGrid(SpriteBatch.Name sbName, float x, float y)
        {
            // squid
            // crab
            // octopus
            GameSprite alienGridSprite = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.AlienGrid, GameObject.CompositeType.Grid
                , x, y, Image.Name.Nothing);

            Image explosionImage = ImageManager.getInstance().FindImageByName(Image.Name.AlienExplosion);


            GlobalPlayerStats.playerstats pStats;

            if (sbName == SpriteBatch.Name.Player1) pStats = GlobalPlayerStats.Player1;
            else pStats = GlobalPlayerStats.Player2;

            GameObject alienGrid = alienGridSprite.pGameObject;

            // 11 columns
            for (int i = 0; i < 11; i++)
            {
                GameSprite.Name gName;
                Image.Name imgName;
                Image.Name altImgName;

                GameSprite gs;
                SwapImgAnimation swapAni;

                GameSprite alienCol = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.AlienCol, GameObject.CompositeType.Column
                , x, y, Image.Name.Nothing);

                // 5 rows
                for (int j = 0; j < 5; j++)
                {
                    if (j == 0)
                    {
                        gName = GameSprite.Name.Squid;
                        imgName = Image.Name.Squid_1;
                        altImgName = Image.Name.Squid_2;
                    }
                    else if (j < 3)
                    {
                        gName = GameSprite.Name.Crab;
                        imgName = Image.Name.Crab_1;
                        altImgName = Image.Name.Crab_2;
                    }
                    else
                    {
                        gName = GameSprite.Name.Octopus;
                        imgName = Image.Name.Octopus_1;
                        altImgName = Image.Name.Octopus_2;
                    }

                    gs = CreateGameSprite(GameObject.Category.Alien, gName, sbName, x + i * 60f, y - j * 55f, 40.0f, 40.0f, imgName);
                    gs.addImage(altImgName);
                    gs.pExplosionImage = explosionImage;

                    // Add SwapImage animations
                    swapAni = new SwapImgAnimation(gs.pGameObject, 1.0f - 0.05f * pStats.currentLevel);
                    TimerManager.getInstance().Add(TimerEvent.Name.SwapImageAnimation, swapAni, 1.0f - 0.05f * pStats.currentLevel);

                    // Add Sprite to column
                    alienCol.pGameObject.AddChild(gs.pGameObject);
                }

                // Set Bomb Dropping logic
                alienCol.pGameObject.dropID = i;
                BombDropCommand dropCmd = new BombDropCommand(alienCol.pGameObject, pStats.currentLevel);
                TimerManager.getInstance().Add(TimerEvent.Name.DropBombCommand, dropCmd, 3.0f - 0.2f * pStats.currentLevel);

                // --add col to the batch
                SpriteBatchManager.getInstance().FindSpriteBatchByName(sbName).AddSprite(alienCol);

                alienGrid.AddChild(alienCol.pGameObject);
            }

            // Configure alien advance animation
            alienGrid.CompositeSetDelta(20.0f);
            AlienAdvanceAnimation alienAdv = new AlienAdvanceAnimation(alienGrid, 1.0f - 0.05f * pStats.currentLevel);
            TimerManager.getInstance().Add(TimerEvent.Name.AlienAdvanceAnimation, alienAdv, 1.0f - 0.1f * pStats.currentLevel);
            // -- add grid to the batch
            if (GlobalConfiguration.showCollisionRect)
                SpriteBatchManager.getInstance().FindSpriteBatchByName(sbName).AddSprite(alienGrid.colliRect.box);

            SpriteBatchManager.getInstance().FindSpriteBatchByName(sbName).AddSprite(alienGridSprite);

            return alienGrid;
        }

        // Factory method for creating composite type
        public static GameSprite CreateCompositeSprite(GameObject.Category cate, GameObject.CompositeType cType, float x, float y, Image.Name imgName)
        {
            Image img = ImageManager.getInstance().FindImageByName(imgName);
            Debug.Assert(img != null);

            GameSprite ret = pGameSpriteManager.AddGameSprite(cate
                , GameSprite.Name.NullObject, 0f, 1.0f, 1.0f, x, y, img, 400f, 700f, 1f, 1f);
            // give it the composite type specified
            ret.pGameObject.compositeType = cType;
            ret.pGameObject.collidable = false;
            ret.pGameObject.isComposite = true;

            // show red box for grid
            if (cType == GameObject.CompositeType.Grid)
                ret.pGameObject.colliRect.setColor(0, 255, 0);
            // show green box for column
            else if (cType == GameObject.CompositeType.Column)
                ret.pGameObject.colliRect.setColor(255, 0, 0);

            return ret;
        }

        // Factory method for creating shield
        public static GameSprite CreateShieldSprite(SpriteBatch.Name sbName, float x, float y, float w, float h, Image img)
        {
            GameSprite ret = pGameSpriteManager.AddGameSprite(GameObject.Category.Shield
                , GameSprite.Name.ShieldBrick, 0f, 1.0f, 1.0f, x, y, img, x, y, w, h);

            SpriteBatchManager.getInstance().FindSpriteBatchByName(sbName).AddSprite(ret);

            if (GlobalConfiguration.showCollisionRect)
                SpriteBatchManager.getInstance().FindSpriteBatchByName(sbName).AddSprite(ret.pGameObject.colliRect.box);

            return ret;
        }
    }
}
