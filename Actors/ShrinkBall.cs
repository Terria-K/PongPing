using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongPing;

public class ShrinkBall : Ball
{
    public ShrinkBall(Texture2D texture) : base(texture)
    {
    }

    public override void Ready()
    {
        Sprite.Modulate = Color.Green;
        base.Ready();
    }

}