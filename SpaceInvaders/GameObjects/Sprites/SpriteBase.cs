using System;


namespace SpaceInvaders
{
    public abstract class SpriteBase : DLink
    {
        public Azul.Rect poRect;
        public GameObject pGameObject;

        // Virtual method to render the sprite
        public virtual void Render() { }

        // Virutal method to update the sprite
        public virtual void Update() { }

        public virtual DLink Wash() { return null; }
    }
}
