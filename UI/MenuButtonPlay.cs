using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Teuria;

namespace PongPing;

public class MenuButtonPlay : KeyboardButton
{
    private Coroutine coroutine;
    private RefCoroutine refCoroutine;

    public MenuButtonPlay(SpriteFont fontText, string text, bool firstSelected = false) : base(fontText, text, firstSelected)
    {
        coroutine = new Coroutine();
        AddComponent(coroutine);
    }

    public override void Update()
    {
        if (refCoroutine.Corou != null) 
        {
            base.Update();
            return;
        }
        if (!refCoroutine.IsRunning) 
        {
            base.Update();
            return;
        }
    }

    public override void OnFocus()
    {
        Modulate = Color.Yellow;
        base.OnFocus();
    }

    public override void OnRelease()
    {
        Modulate = Color.White;
        base.OnRelease();
    }

    public override void OnEnter()
    {
        refCoroutine = coroutine.Run(Start());
    }

    private IEnumerator Start() 
    {
        Modulate = Color.Green;
        yield return 0.1f;
        OnConfirm?.Invoke();
    }
}