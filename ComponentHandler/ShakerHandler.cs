using Microsoft.Xna.Framework;
using Teuria;

namespace PongPing;

public class ShakerHandler : Entity 
{
    private Shaker shaker;

    public ShakerHandler()
    {
        shaker = new Shaker();
        shaker.Intensity = 2f;
        AddComponent(shaker);
    }

    public void ShakeFor(float timer) 
    {
        shaker.ShakeFor(timer);
    }

    public Vector2 GetValue() 
    {
        return shaker.Value;
    }
}