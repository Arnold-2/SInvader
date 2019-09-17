using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class ImageManager : Manager
    {
        private static ImageManager pInstance;

        public ImageManager() : base(20, 5)
        {

        }

        public static ImageManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new ImageManager();
            }

            return pInstance;
        }

        // PA2: Factory method, create and add image into actives
        public Image AddImage(Image.Name name, Texture.Name tName, float x, float y, float w, float h)
        {
            Texture texture = TextureManager.getInstance().FindTextureByName(tName);
            Debug.Assert(texture != null);

            // Create a new image
            Image ret = new Image(name, texture.poAzulTexture, x, y, w, h);

            // Add it to the list
            this.Add(ret);

            return ret;
        }

        // PA6 Find Image by Name
        public Image FindImageByName(Image.Name n)
        {
            DLink current = poActiveHead;
            while (current != null)
            {
                if (((Image)current).name == n)
                {
                    return (Image)current;
                }

                current = current.pNext;
            }

            return null;
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new Image();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new Image();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
