using System;

namespace PongPing;

public static class Program
{
    [STAThread]
    static void Main()
    {
        using (var game = new PongPing(ProgramWidth/2, ProgramHeight/2, ProgramWidth, ProgramHeight, "Pong Ping", false))
            game.Run();
    }
}

