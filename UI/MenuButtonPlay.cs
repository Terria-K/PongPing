using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Teuria;
using Microsoft.Xna.Framework.Content;

namespace PongPing;

public class MenuButtonPlay : KeyboardButton
{
    private Coroutine coroutine;
    private RefCoroutine refCoroutine;
    private SoundEffect blip;
    private SoundEffect enter;

    public MenuButtonPlay(SpriteFont fontText, string text, bool firstSelected = false) : base(fontText, text, firstSelected)
    {
        coroutine = new Coroutine();
        AddComponent(coroutine);
    }

    public override void EnterScene(Scene scene, ContentManager content)
    {
        blip = content.Load<SoundEffect>("sfx/blip");
        enter = content.Load<SoundEffect>("sfx/selected");
        base.EnterScene(scene, content);
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
        blip.Play();
        Modulate = Color.White;
        base.OnRelease();
    }

    public override void OnEnter()
    {
        refCoroutine = coroutine.Run(Start());
        enter.Play();
    }

    private IEnumerator Start() 
    {
        Modulate = Color.Green;
        yield return 0.1f;
        OnConfirm?.Invoke();
    }
}