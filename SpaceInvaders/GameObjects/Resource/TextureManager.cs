using System;

namespace SpaceInvaders
{
    public class TextureManager : Manager
    {
        private static TextureManager pInstance;
        
        public TextureManager() : base(5, 3)
        {
        }

        public static TextureManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new TextureManager();
            }

            return pInstance;
        }

        public Texture FindTextureByName(Texture.Name tName)
        {
            DLink current = poActiveHead;
            while (current != null)
            {
                if (((Texture)current).name == tName)
                {
                    return (Texture)current;
                }

                current = current.pNext;
            }

            return null;
        }


        // PA2: Factory Method
        public Texture AddTexture(Texture.Name name, String source)
        {
            // Create new texture
            Texture ret = (Texture)this.PullFromReserved();

            // Set Data
            ret.Set(name, source);

            // Add to the list
            this.Add(ret);

            return ret;
            
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new Texture();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new Texture();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }

    }
}
