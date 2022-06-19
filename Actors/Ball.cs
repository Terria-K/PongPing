using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Teuria;

namespace PongPing;

public class Ball : Entity 
{
    protected Sprite Sprite;
    private const float SpeedLimit = 1.0f;
    private BallHitbox hitbox;
    public Vector2 Velocity;
    public float Speed = 5.0f;

    public Ball(Texture2D texture) 
    {
        Sprite = new Sprite(texture);
        hitbox = new BallHitbox(Sprite.texture.Width, Sprite.texture.Height, Position, this);
        AddComponent(Sprite);
        AddComponent(hitbox);
    }

    public override void Update()
    {
        if (Velocity.X >SpeedLimit) { Velocity.X =SpeedLimit; }
        else if (Velocity.X < -SpeedLimit) { Velocity.X = -SpeedLimit; }        
        if (Velocity.Y >SpeedLimit) { Velocity.Y =SpeedLimit; }
        else if (Velocity.Y < -SpeedLimit) { Velocity.Y = -SpeedLimit; }        
        Position.X += Velocity.X * Speed;
        Position.Y += Velocity.Y * Speed;
        base.Update();
    }

    public bool CollideLeft(Hitbox other) 
    {
        return hitbox.CollideLeft(other);
    }

    public bool CollideRight(Hitbox other) 
    {
        return hitbox.CollideRight(other);
    }
}