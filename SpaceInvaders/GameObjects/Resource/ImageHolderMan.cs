using System;

namespace SpaceInvaders
{
    public class ImageHolderMan : Manager
    {

        public ImageHolderMan() : base(3, 1)
        {

        }

        public ImageHolder AddImageHolder(Image img)
        {
            ImageHolder ret = (ImageHolder)this.PullFromReserved();
            ret.pImage = img;
            ret.name = img.name;

            this.Add(ret);
            return ret;
        }


        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new ImageHolder();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new ImageHolder();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
