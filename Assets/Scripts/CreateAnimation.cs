using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public class CreateAnimation : MonoBehaviour
{
    [SerializeField] AnimationManager animationManager;
    [SerializeField] RecordingPosition recordingPosition;
    [SerializeField] RecordingPosition2 recordingPosition2;

    public IObservable<Unit> OnEND => CreateEnd;
    private Subject<Unit> CreateEnd = new Subject<Unit>();

    private AnimationCurve curve;
    private AnimationCurve curve2;

    bool Created=false;
    bool Created2=false;

    void Start(){
        recordingPosition.Time
        .Where(t => t>0)
        .Subscribe(t => AddKey(t))
        .AddTo(this);

        recordingPosition.OnEND
        .Subscribe(_ => CreateCurve())
        .AddTo(this);

        recordingPosition2.Time
        .Where(t => t>0)
        .Subscribe(t => AddKey2(t))
        .AddTo(this);

        recordingPosition2.OnEND
        .Subscribe(_ => CreateCurve2())
        .AddTo(this);

        // AnimationCurveの生成.
        curve = new AnimationCurve();
        curve2 = new AnimationCurve();

        // Keyframeの生成.
        Keyframe startKeyframe = new Keyframe(0f, 0f);
        curve.AddKey(startKeyframe);
        curve2.AddKey(startKeyframe);

        end().Forget();
    }

    async UniTask end(){
        await UniTask.WaitUntil(() => Created);
        await UniTask.WaitUntil(()=>Created2);
        CreateEnd.OnNext(Unit.Default);
    }

    private void CreateCurve(){
        // AnimationCurveの追加.
        animationManager.clip.SetCurve("", typeof(Transform), "localPosition.x", curve);
        Created= true;

    }

    private void CreateCurve2(){
        // AnimationCurveの追加.
        animationManager.clip2.SetCurve("", typeof(Transform), "localPosition.y", curve2);
        Created2= true;
    }

    private void AddKey(int time){
        float _pos = recordingPosition.pos.Value.x;
        // Keyframeの生成.
        // Keyframe Keyframe = new Keyframe(time, recordingPosition.pos.y.Value);
        Keyframe Keyframe = new Keyframe(time, _pos);

        //Keyframeの追加.
        curve.AddKey(Keyframe);
    }

    private void AddKey2(int time){
        float _pos = recordingPosition2.pos.Value.y;
        // Keyframeの生成.
        Keyframe Keyframe = new Keyframe(time, _pos);

        //Keyframeの追加.
        curve2.AddKey(Keyframe);
    }
}
