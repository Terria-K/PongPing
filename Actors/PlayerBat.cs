using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Teuria;

namespace PongPing;

public class PlayerBat : Bat
{

    public PlayerBat(Texture2D texture) : base(texture) {}

    public override void Update()
    {
        Move();
        base.Update();
    }

    public void Move() 
    {
        if (!CanMove) return;
        var axis = TInput.Keyboard.GetAxis(Keys.Up, Keys.Down);
        Position.Y = MathHelper.Clamp(Position.Y + (Speed * axis), 0, ProgramHeight - sprite.texture.Height);
    }
}