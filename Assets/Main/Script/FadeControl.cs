using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public class FadeControl : MonoBehaviour
{
    [SerializeField] AnimationClip fadeinClip;
    [SerializeField] AnimationClip fadeoutClip;

    Animation anim;

    private Subject<Unit> fadein = new Subject<Unit>();
    public IObservable<Unit> OnFadeInEnd { get { return fadein; } }

    private Subject<Unit> fadeout = new Subject<Unit>();
    public IObservable<Unit> OnFadeOutEnd { get { return fadeout; } }

    public bool Fading { get; private set; }


    void Start()
    {
        anim = GetComponent<Animation>();
    }

    void FadeInEnd()
    {
        Fading = false;
        fadein.OnNext(Unit.Default);
    }

    void FadeOutEnd()
    {
        Fading = false;
        fadeout.OnNext(Unit.Default);
    }

    public void FadeIn()
    {
        if (Fading)
        {
            return;
        }

        anim.clip = fadeinClip;
        anim.Play();
        Fading = true;
        
    }

    public void FadeOut()
    {
        if (Fading)
        {
            return;
        }

        anim.clip = fadeoutClip;
        anim.Play();
        Fading = true;
    }
}
