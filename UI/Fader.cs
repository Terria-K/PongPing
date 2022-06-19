using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Teuria;

namespace PongPing;

public class Fader : Entity 
{
    private Sprite sprite;

    public override void EnterScene(Scene scene, ContentManager content)
    {
        var tex = content.Load<Texture2D>("pixel");
        sprite = new Sprite(tex, ProgramWidth, ProgramHeight);
        Modulate = Color.Black * 0.4f;
        ZIndex = 1;
        AddComponent(sprite);
        base.EnterScene(scene, content);
    }
}