using Teuria;
using Microsoft.Xna.Framework;

namespace PongPing;

public class PongPing : TeuriaEngine
{
    private Camera camera;

    public PongPing(int width, int height, int screenWidth, int screenHeight, string windowTitle, bool fullScreen) 
        : base(width, height, screenWidth, screenHeight, windowTitle, fullScreen)
    {
    }

    protected override SceneRenderer Init()
    {
        camera = new Camera();
        Scene = new MainMenu(Content, camera);
        return new SceneRenderer(Scene, camera, Color.Black);
    }
}