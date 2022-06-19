using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Teuria;

namespace PongPing;

public class EnemyBat : Bat
{
    private Ball target;
    private Random rand;
    private bool isLeft;
    private float targetValue;
    private int currentRandom;
    private int[] listRandomLocation = new int[5] { 0, 195, 350, 600, ProgramHeight };
    private Coroutine coroutine;

    public EnemyBat(Texture2D texture, bool isFlipped, Ball ball, Random random, bool isLeft = false) : base(texture, isFlipped)
    {
        target = ball;
        rand = random;
        this.isLeft = isLeft;
        coroutine = new Coroutine();
        AddComponent(coroutine);
        coroutine.Run(RandMoveCoroutine());
    }

    private IEnumerator RandMoveCoroutine() 
    {
        while (true) 
        {
            currentRandom = listRandomLocation[rand.Next(listRandomLocation.Length)];
            targetValue = currentRandom;
            yield return 1.2f;
            
            yield return null;
        }
    }

    public override void Update()
    {
        Move();
        base.Update();
    }

    private void Move() 
    {
        if (target.Velocity.X == -1) 
        {
            CanMove = false;
        }
        if (target.Velocity.X == 1) 
        {
            CanMove = true;
        }
        var towards = MoveValue();
        Position.Y = MathHelper.Clamp(towards, 0, ProgramHeight - sprite.texture.Height);
    }

    private float MoveValue() 
    {
        // if (!CanMove) 
        // {
        //     if (Position.Y == currentRandom || currentRandom == 0 || targetValue == target.Position.Y) {
        //         targetValue = rand.Next(0, TeuriaEngine.ScreenHeight);   
        //         currentRandom = (int)targetValue;
        //         System.Console.WriteLine(currentRandom);
        //     } 
        //     else if (Position.Y != currentRandom) 
        //     {
        //         targetValue = currentRandom;
        //     }
        // }
        if (CanMove) 
        {
            targetValue = target.Position.Y;
        }
        if (targetValue <= Position.Y * 3f || targetValue >= Position.Y * 3f) 
        {
            return MathUtils.MoveTowards(Position.Y, targetValue - 32f, 200f * TeuriaEngine.DeltaTime);
        }
        return Position.Y;
    }
}