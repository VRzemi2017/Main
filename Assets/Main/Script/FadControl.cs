using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public class FadControl : MonoBehaviour
{
    [SerializeField] AnimationClip fadinClip;
    [SerializeField] AnimationClip fadoutClip;

    Animation anim;

    private Subject<Unit> fadin = new Subject<Unit>();
    public IObservable<Unit> OnFadinEnd { get { return fadin; } }

    private Subject<Unit> fadout = new Subject<Unit>();
    public IObservable<Unit> OnFadoutEnd { get { return fadout; } }

    public bool Fading { get; private set; }


    void Start()
    {
        anim = GetComponent<Animation>();
    }

    void FadinEnd()
    {
        Fading = false;
        fadin.OnNext(Unit.Default);
    }

    void FadoutEnd()
    {
        Fading = false;
        fadout.OnNext(Unit.Default);
    }

    public void Fadin()
    {
        if (Fading)
        {
            return;
        }

        anim.clip = fadinClip;
        anim.Play();
        Fading = true;
        
    }

    public void Fadout()
    {
        if (Fading)
        {
            return;
        }

        anim.clip = fadoutClip;
        anim.Play();
        Fading = true;
    }
}
