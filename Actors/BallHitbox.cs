using Microsoft.Xna.Framework;
using Teuria;

namespace PongPing;

public class BallHitbox : Hitbox
{
    private Ball ball;

    public BallHitbox(float width, float height, Vector2 pos, Ball ball) : base(width, height, pos)
    {
        this.ball = ball;
    }


    public bool CollideLeft(Hitbox other) 
    {
        return GlobalLeft + ball.Velocity.X < other.GlobalRight && 
            GlobalRight > other.GlobalLeft 
            && GlobalBottom > other.GlobalTop 
            && GlobalTop < other.GlobalBottom
            && ball.Velocity.X != 1;
    }

    public bool CollideRight(Hitbox other) 
    {
        return GlobalLeft - ball.Velocity.X < other.GlobalRight && 
            GlobalRight > other.GlobalLeft 
            && GlobalBottom > other.GlobalTop 
            && GlobalTop < other.GlobalBottom
            && ball.Velocity.X != -1;
    }
}