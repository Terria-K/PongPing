using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Teuria;

namespace PongPing;

public class Bat : Entity
{
    protected bool CanMove = true;
    public Sprite sprite;
    public float Speed = 5;
    public Vector2 BatScale = Vector2.One;
    private Hitbox hitbox;

    public Hitbox Hitbox 
    {
        get => hitbox;
    }


    public Bat(Texture2D texture, bool isFlipped = false) 
    {
        sprite = new Sprite(texture);
        sprite.FlipH = isFlipped;
        hitbox = new Hitbox(sprite.texture.Width, sprite.texture.Height, Position);
        AddComponent(sprite);
        AddComponent(hitbox);
    }

    public void UpdateScale(Vector2 scale) 
    {
        sprite.Scale = scale;
        CanMove = false;
    } 

    public override void Update()
    {
        if (sprite.Scale.X >= Vector2.One.X - 0.2f) 
        {
            CanMove = true;
        }
        if (sprite.Scale != Vector2.One) 
        {
            sprite.Scale.X = MathHelper.Lerp(sprite.Scale.X, BatScale.X, 0.5f * TeuriaEngine.DeltaTime);
            sprite.Scale.Y = MathHelper.Lerp(sprite.Scale.Y, BatScale.Y, 0.5f * TeuriaEngine.DeltaTime);
        }
        base.Update();
    }
}