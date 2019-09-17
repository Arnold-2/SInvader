using System;

namespace SpaceInvaders
{
    // Used as a wrapper for linked list
    public class ImageHolder : DLink
    {
        public Image.Name name;
        public Image pImage;

        public ImageHolder()
        {
            this.name = Image.Name.Uninitialized;
        }

        public ImageHolder(Image img)
        {
            this.name = img.name;
            pImage = img;
        }

        // Not useful for now
        public override bool DerivedEqual(DLink node)
        {
            return true;
        }

        public override void dbgDerivedPrint()
        {
            throw new NotImplementedException();
        }
    }
}
