using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Teuria;

namespace PongPing;

public class Arena : Scene
{
#region Arena
    private Entity workFlowEntity;
    private PlayerBat player;
    private EnemyBat enemy;
    private Ball ball;
    private Random random = new Random();
    private Vector2 BallPosition;
    private Vector2 max;
    private Score scorePlayer;
    private Score scoreEnemy;
    private ShakerHandler shaker;
    private bool resetting;
#endregion

#region User Interface
    private Label labelText;
    private KeyboardButton continueButton;
    private KeyboardButton mainMenuButton;
    private KeyboardButton resetButton;
    private KeyboardButton exitButton;
    private FontText fontText;
    private Fader fader;
    private Coroutine wildCoroutine;
#endregion

    public Arena(ContentManager content, Camera camera) : base(content, camera)
    {
    }

    public override void Ready(GraphicsDevice device)
    {
        var batTexture = Content.Load<Texture2D>("bat");
        var ballTexture = Content.Load<Texture2D>("ball");
        workFlowEntity = new Entity();
        OnPause += Paused;
        var turnRandom = random.Next(0, 2);


        BallPosition = new Vector2(
            (ProgramWidth / 2f) - ballTexture.Width, 
            (ProgramHeight / 2) - ballTexture.Height);
        ball = new Ball(ballTexture) 
        { Position = BallPosition, Velocity = new Vector2(turnRandom == 1 ? -1 : 1, 0) };
        player = new PlayerBat(batTexture);
        enemy = new EnemyBat(batTexture, true, ball, random);
        shaker = new ShakerHandler();

        player.Position = new Vector2(0, (ProgramHeight / 2 - batTexture.Height));
        enemy.Position = new Vector2(
            (ProgramWidth - batTexture.Width), 
            player.Position.Y);
        max = new Vector2(ProgramWidth + batTexture.Width, ProgramHeight - ballTexture.Height);

        Add(shaker);
        Add(player);
        Add(enemy);
        Add(ball);
        UserInterface(device);
        base.Ready(device);
    }

    private new void Paused() 
    {
        labelText.Active = true;
        mainMenuButton.Active = true;
        continueButton.Active = true;
        exitButton.Active = true;
        resetButton.Active = true;
        fader.Active = true;
    }

    private void UnPaused() 
    {
        labelText.Active = false;
        mainMenuButton.Active = false;
        continueButton.Active = false;
        exitButton.Active = false;
        resetButton.Active = false;
        fader.Active = false;
    }

    private void UserInterface(GraphicsDevice device) 
    {
        var playButtonTex = TextureImporter.LoadImage(device, "ui/play-button.png");
        var quitButtonTex = TextureImporter.LoadImage(device, "ui/quit-button.png");
        var mainButtonTex = TextureImporter.LoadImage(device, "ui/menu-button.png");
        var scoreEnemyFontText = FontText.Create("Rubik-Regular", Content);
        var scorePlayerFontText = FontText.Create("Rubik-Regular", Content);
        fontText = FontText.Create("Rubik-Regular", Content);
        var spriteFont = fontText.ShareSpriteFont();
        fader = new Fader();
        wildCoroutine = new Coroutine();
        workFlowEntity.AddComponent(wildCoroutine);
        
        scorePlayer = new Score(scorePlayerFontText);
        scoreEnemy = new Score(scoreEnemyFontText, 100);
        scorePlayer.ScoreOf = 0;
        scoreEnemy.ScoreOf = 0;

        labelText = new Label(fontText);
        continueButton = new MenuButtonPlay(spriteFont, "Continue", true);
        continueButton.Position = new Vector2(spriteFont.MeasureScreenString("Continue", ProgramWidth), 200);
        resetButton = new MenuButtonPlay(spriteFont, "Reset");
        resetButton.Position = new Vector2(spriteFont.MeasureScreenString("Reset", ProgramWidth), continueButton.Position.Y + 60);
        mainMenuButton = new MenuButtonPlay(spriteFont, "Main Menu");
        mainMenuButton.Position = new Vector2
            (spriteFont.MeasureScreenString("Main Menu", ProgramWidth), resetButton.Position.Y + 60);
        exitButton = new MenuButtonPlay(spriteFont, "Exit");
        exitButton.Position = new Vector2
            (spriteFont.MeasureScreenString("Exit", ProgramWidth), mainMenuButton.Position.Y + 60);
        continueButton.DownFocus = resetButton;
        resetButton.UpFocus = continueButton;
        resetButton.DownFocus = mainMenuButton;
        mainMenuButton.UpFocus = resetButton;
        mainMenuButton.DownFocus = exitButton;
        exitButton.UpFocus = mainMenuButton;

        continueButton.OnConfirm = () => 
        {
            base.Paused = false;
            UnPaused();
        };

        mainMenuButton.OnConfirm = () => 
        {
            var mainScene = new MainMenu(Content, Camera);
            SceneRenderer.ChangeScene(mainScene);
        };

        resetButton.OnConfirm = () => 
        {
            wildCoroutine.Run(ResetCoroutine());
        };

        exitButton.OnConfirm = () => 
        {
            TeuriaEngine.Instance.ExitGame();
        };

        labelText.Text = "Paused";
        labelText.Position = new Vector2((ProgramWidth / 2 - fontText.MeasureStringHalf()), 100);

        Add(scoreEnemy);
        Add(scorePlayer);
        Add(fader, PauseMode.Single);
        Add(labelText, PauseMode.Single);
        Add(mainMenuButton, PauseMode.Single);
        Add(resetButton, PauseMode.Single);
        Add(continueButton, PauseMode.Single);
        Add(exitButton, PauseMode.Single);
        Add(workFlowEntity, PauseMode.Single);
        labelText.Active = false;
        mainMenuButton.Active = false;
        continueButton.Active = false;
        exitButton.Active = false;
        resetButton.Active = false;
        fader.Active = false;
    }

    private IEnumerator ResetCoroutine() 
    {
        ball.Position = BallPosition;
        UnPaused();
        resetting = true;
        while (scoreEnemy.ScoreOf > 0 || scorePlayer.ScoreOf > 0) 
        {
            if (scoreEnemy.ScoreOf > 0) 
            {
                scoreEnemy.ScoreOf--;
            }
            if (scorePlayer.ScoreOf > 0) 
            {
                scorePlayer.ScoreOf--;
            }
            yield return null;
        }
        resetting = false;
        base.Paused = false;
    }

    private void AddBallPhysics(Ball ball) 
    {
        if (ball.Position.Y < 0) 
        {
            ball.Position.Y = 1;
            ball.Velocity.Y *= -(1 + random.Next(-50, 150) * 0.01f);
        }
        if (ball.Position.Y > max.Y) 
        {
            ball.Position.Y = max.Y - 1;
            ball.Velocity.Y *= -(1 + random.Next(-50, 150) * 0.01f);
        }
    }

    public override void Update()
    {
        if (TInput.Keyboard.JustPressed(Keys.Escape) && !resetting) 
        {
            base.Paused = true;
        }
        BatIntersectingBall();

        Camera.Offset = shaker.GetValue();

        AddBallPhysics(ball);

        if (ball.Position.X < -100) 
        {
            shaker.ShakeFor(0.1f);
            scoreEnemy.ScoreOf = scoreEnemy.ScoreOf + 1;
            ball.Position = BallPosition;
            ball.Velocity = new Vector2(-1, 0);
        }
        if (ball.Position.X > max.X) 
        {
            shaker.ShakeFor(0.1f);
            scorePlayer.ScoreOf = scorePlayer.ScoreOf + 1;
            ball.Position = BallPosition;
            ball.Velocity = new Vector2(1, 0);
        }
        base.Update();
    }

    private void BatIntersectingBall() 
    {
        if (ball.CollideLeft(player.Hitbox)) 
        {
            BallIntersected();
        }
        if (ball.CollideRight(enemy.Hitbox)) 
        {
            BallIntersected();
        }

        void BallIntersected() 
        {
            ball.Velocity.X *= -1;
            ball.Velocity.Y += 100 * 0.005f;
        }
    }

    public override void Exit()
    {
        OnPause -= Paused;
        base.Exit();
    }
}