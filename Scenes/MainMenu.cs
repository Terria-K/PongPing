using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Teuria;

namespace PongPing;

public class MainMenu : Scene
{
    public MainMenu(ContentManager content, Camera camera) : base(content, camera) {}

    public override void Ready(GraphicsDevice device)
    {
        var font = FontText.Create("Rubik-Regular", Content);
        var spriteFont = Content.Load<SpriteFont>("Rubik-Regular");

        var title = new Label(font);
        title.Text = "Pong Ping";
        title.Position = new Vector2(ProgramWidth / 2 - font.MeasureStringHalf(), 100);
        Add(title);

        var play = new MenuButtonPlay(spriteFont, "Play", true);
        play.Position = new Vector2(spriteFont.MeasureScreenString("Play", ProgramWidth), 200);
        play.OnConfirm = ChangeScene;
        var exitButton = new MenuButtonPlay(spriteFont, "Exit");
        exitButton.OnConfirm = TeuriaEngine.Instance.ExitGame;
        play.DownFocus = exitButton;
        exitButton.UpFocus = play;
        exitButton.Position = 
            new Vector2(spriteFont.MeasureScreenString("Exit", ProgramWidth), play.Position.Y + 45);
        Add(play);
        Add(exitButton);
        base.Ready(device);
    }

    public override void Update()
    {
        base.Update();
    }

    private void ChangeScene() 
    {
        var scene = new Arena(Content, Camera);
        SceneRenderer.ChangeScene(scene);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public override void Exit()
    {
        base.Exit();
    }
}