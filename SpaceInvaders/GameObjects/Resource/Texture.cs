using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Texture : DLink
    {
        public Name name;
        public Azul.Texture poAzulTexture;

        public void Set (Texture.Name name, String source)
        {
            this.name = name;
            this.poAzulTexture = new Azul.Texture(source);
        }

        public Texture()
        {
            this.name = Texture.Name.Uninitialized;
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((Texture)node).name == this.name)
                return true;
            else
                return false;
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.poAzulTexture != null);
            return this.poAzulTexture;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  Texture");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }

        public enum Name
        {
            Alien,
            Bird,
            Font,
            Shield,
            Uninitialized
        }
    }


}
