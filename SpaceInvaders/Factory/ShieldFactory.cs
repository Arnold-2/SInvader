using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {
        public GameSpriteManager pGameSpriteManager;

        public ShieldFactory()
        {
            this.pGameSpriteManager = GameSpriteManager.getInstance();
        }

        public GameObject CreateShield(SpriteBatch.Name sbName, float center_x, float center_y, float brickWidth, float brickHeight) {
            // create a grid first
            GameSprite gridSprite = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.Shield
                , GameObject.CompositeType.Grid, 400f, 400f, Image.Name.Nothing);
            GameObject grid = gridSprite.pGameObject;



            // Create column and bricks
            // Set parameter
            float start_x = center_x - 3.5f * brickWidth;
            float start_y = center_y - 5.0f * brickHeight;

            // Load Images
            Image imgBrick = ImageManager.getInstance().FindImageByName(Image.Name.Brick);
            Image imgBrickLeft_Top0 = ImageManager.getInstance().FindImageByName(Image.Name.BrickLeft_Top0);
            Image imgBrickLeft_Top1 = ImageManager.getInstance().FindImageByName(Image.Name.BrickLeft_Top1);
            Image imgBrickLeft_Bottom = ImageManager.getInstance().FindImageByName(Image.Name.BrickLeft_Bottom);
            Image imgBrickRight_Top0 = ImageManager.getInstance().FindImageByName(Image.Name.BrickRight_Top0);
            Image imgBrickRight_Top1 = ImageManager.getInstance().FindImageByName(Image.Name.BrickRight_Top1);
            Image imgBrickRight_Bottom = ImageManager.getInstance().FindImageByName(Image.Name.BrickRight_Bottom);


            // Create column 1/7 (bricks created from bottom to top)
            GameSprite col_1_Sprite = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.Shield
                , GameObject.CompositeType.Column, 400f, 400f, Image.Name.Nothing);
            GameObject col_1 = col_1_Sprite.pGameObject;

            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 0 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 1 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 2 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 3 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 4 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 5 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 6 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 7 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 8 * brickHeight, brickWidth, brickHeight, imgBrickLeft_Top1).pGameObject);
            col_1.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x, start_y + 9 * brickHeight, brickWidth, brickHeight, imgBrickLeft_Top0).pGameObject);

            // Create column 2/7
            GameSprite col_2_Sprite = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.Shield
                , GameObject.CompositeType.Column, 400f, 400f, Image.Name.Nothing);
            GameObject col_2 = col_2_Sprite.pGameObject;

            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 0 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 1 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 2 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 3 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 4 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 5 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 6 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 7 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 8 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_2.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 1 * brickWidth, start_y + 9 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);

            // Create column 3/7
            GameSprite col_3_Sprite = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.Shield
                , GameObject.CompositeType.Column, 400f, 400f, Image.Name.Nothing);
            GameObject col_3 = col_3_Sprite.pGameObject;

            col_3.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 2 * brickWidth, start_y + 2 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_3.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 2 * brickWidth, start_y + 3 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_3.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 2 * brickWidth, start_y + 4 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_3.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 2 * brickWidth, start_y + 5 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_3.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 2 * brickWidth, start_y + 6 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_3.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 2 * brickWidth, start_y + 7 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_3.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 2 * brickWidth, start_y + 8 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_3.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 2 * brickWidth, start_y + 9 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);

            // Create column 4/7
            GameSprite col_4_Sprite = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.Shield
                , GameObject.CompositeType.Column, 400f, 400f, Image.Name.Nothing);
            GameObject col_4 = col_4_Sprite.pGameObject;

            col_4.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 3 * brickWidth, start_y + 3 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_4.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 3 * brickWidth, start_y + 4 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_4.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 3 * brickWidth, start_y + 5 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_4.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 3 * brickWidth, start_y + 6 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_4.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 3 * brickWidth, start_y + 7 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_4.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 3 * brickWidth, start_y + 8 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_4.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 3 * brickWidth, start_y + 9 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);

            // Create column 5/7
            GameSprite col_5_Sprite = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.Shield
                , GameObject.CompositeType.Column, 400f, 400f, Image.Name.Nothing);
            GameObject col_5 = col_5_Sprite.pGameObject;

            col_5.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 4 * brickWidth, start_y + 2 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_5.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 4 * brickWidth, start_y + 3 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_5.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 4 * brickWidth, start_y + 4 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_5.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 4 * brickWidth, start_y + 5 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_5.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 4 * brickWidth, start_y + 6 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_5.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 4 * brickWidth, start_y + 7 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_5.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 4 * brickWidth, start_y + 8 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_5.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 4 * brickWidth, start_y + 9 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);

            // Create column 6/7
            GameSprite col_6_Sprite = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.Shield
                , GameObject.CompositeType.Column, 400f, 400f, Image.Name.Nothing);
            GameObject col_6 = col_6_Sprite.pGameObject;

            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 0 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 1 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 2 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 3 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 4 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 5 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 6 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 7 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 8 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_6.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 5 * brickWidth, start_y + 9 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);

            // Create column 7/7
            GameSprite col_7_Sprite = GameSpriteFactory.CreateCompositeSprite(GameObject.Category.Shield
                , GameObject.CompositeType.Column, 400f, 400f, Image.Name.Nothing);
            GameObject col_7 = col_7_Sprite.pGameObject;

            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 0 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 1 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 2 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 3 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 4 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 5 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 6 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 7 * brickHeight, brickWidth, brickHeight, imgBrick).pGameObject);
            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 8 * brickHeight, brickWidth, brickHeight, imgBrickRight_Top1).pGameObject);
            col_7.AddChild(GameSpriteFactory.CreateShieldSprite(sbName, start_x + 6 * brickWidth, start_y + 9 * brickHeight, brickWidth, brickHeight, imgBrickRight_Top0).pGameObject);


            // Add Columns to Grid
            grid.AddChild(col_1);
            grid.AddChild(col_2);
            grid.AddChild(col_3);
            grid.AddChild(col_4);
            grid.AddChild(col_5);
            grid.AddChild(col_6);
            grid.AddChild(col_7);

            // Add to sprite batch
            SpriteBatch sb = SpriteBatchManager.getInstance().FindSpriteBatchByName(sbName);
            sb.AddSprite(gridSprite);
            sb.AddSprite(col_1_Sprite);
            sb.AddSprite(col_2_Sprite);
            sb.AddSprite(col_3_Sprite);
            sb.AddSprite(col_4_Sprite);
            sb.AddSprite(col_5_Sprite);
            sb.AddSprite(col_6_Sprite);
            sb.AddSprite(col_7_Sprite);

            return grid;
        }
    }
}
