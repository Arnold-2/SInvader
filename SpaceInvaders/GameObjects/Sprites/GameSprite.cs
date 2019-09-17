using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameSprite : SpriteBase
    {
        public Image pImage;
        public Image pExplosionImage;
        public ImageHolderMan pImageQueue;
        public ImageHolder pCrntImgHolder;
        public Name name;
        public Proxy pProxy;
         // Reference to wrapper object


        public Azul.Sprite poAzulSprite;

        public GameSprite(Name name, float ag, float sx, float sy, float x, float y, Image img
            , float posx, float posy, float w, float h)
        {
            this.name = name;
            this.pImage = img;
            this.poRect = new Azul.Rect(posx, posy, w, h);
            this.poAzulSprite = new Azul.Sprite(pImage.pAzulTexture, pImage.poRect, this.poRect);

            this.poAzulSprite.angle = ag;
            this.poAzulSprite.sx = sx;
            this.poAzulSprite.sy = sy;
            this.poAzulSprite.x = x;
            this.poAzulSprite.y = y;
        }

        public void addImage(Image.Name imgName)
        {
            if (this.pImageQueue == null)
                this.pImageQueue = new ImageHolderMan();

            Image img = ImageManager.getInstance().FindImageByName(imgName);

            this.pCrntImgHolder = this.pImageQueue.AddImageHolder(img);
        }


        public void addImage(Image img)
        {
            if (this.pImageQueue == null)
                this.pImageQueue = new ImageHolderMan();

            this.pCrntImgHolder = this.pImageQueue.AddImageHolder(img);
        }

        // Change image to use the next in the queue
        public void SwapImage()
        {
            if (pCrntImgHolder.pNext == null)
                pCrntImgHolder = (ImageHolder)pImageQueue.getActiveHead();
            else
                pCrntImgHolder = (ImageHolder)pCrntImgHolder.pNext;

            this.pImage = pCrntImgHolder.pImage;
            this.poAzulSprite.SwapTexture(this.pImage.pAzulTexture);
            this.poAzulSprite.SwapTextureRect(this.pImage.poRect);
        }

        public void ShowExplosion()
        {
            this.poAzulSprite.SwapTexture(this.pExplosionImage.pAzulTexture);
            this.poAzulSprite.SwapTextureRect(this.pExplosionImage.poRect);
        }

        public GameSprite()
        {
            this.name = Name.Uninitialized;
        }

        public override DLink Wash()
        {
            this.pImage = null;
            this.pImageQueue = null;
            this.pCrntImgHolder = null;
            this.name = Name.Uninitialized;
            this.pProxy = null;

            return this;
        }

        // Draw the shape
        public override void Render()
        {
            if (this.pGameObject.collidable)
                this.poAzulSprite.Render();
        }

        // Update shape
        public override void Update()
        {
            this.poAzulSprite.Update();
        }

        public void setX (float x)
        {
            this.poAzulSprite.x = x;
        }

        public void setY (float y)
        {
            this.poAzulSprite.y = y;
        }

        public void setAngle(float ag)
        {
            this.poAzulSprite.angle = ag;
        }

        public void setSX (float sx)
        {
            this.poAzulSprite.sx = sx;
        }

        public void setSY (float sy)
        {
            this.poAzulSprite.sy = sy;
        }

        public void SwapColor(float r, float g, float b)
        {
            this.poAzulSprite.SwapColor(new Azul.Color(r, g, b));
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((GameSprite)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  Sprite");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }

        public enum Name
        {
            Squid,
            Squid_1,
            Squid_2,
            Squid_3,
            Crab,
            Crab_1,
            Crab_2,
            Crab_3,
            Octopus,
            Octopus_1,
            Octopus_2,
            Octopus_3,
            Ship,
            Missile,
            Shield,
            UFO,
            ShieldBrick,
            DaggerBomb,
            ZigZagBomb,
            RollingBomb,
            LeftWall,
            RightWall,
            TopWall,
            BottomWall,
            NullObject,
            Uninitialized
        }
    }


}
