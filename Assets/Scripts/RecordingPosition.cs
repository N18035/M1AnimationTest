using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

using Cysharp.Threading.Tasks;

public class RecordingPosition : MonoBehaviour
{
    public IReadOnlyReactiveProperty<Vector3> pos => _pos;
    private readonly ReactiveProperty<Vector3> _pos = new ReactiveProperty<Vector3>();

    public IObservable<Unit> OnEND => _star;
    private Subject<Unit> _star = new Subject<Unit>();

    public IntReactiveProperty Time => time;
    [SerializeField] private IntReactiveProperty time = new IntReactiveProperty();

    [SerializeField] int Delaytime=2000;

    void Start(){
    }

    public void PRecord(){
        Record().Forget();
    }

    private async UniTask Record(){
        time.Value = 0;
        
        await UniTask.Delay(Delaytime);
        time.Value++;
        _pos.Value =  this.gameObject.GetComponent<Transform>().position;

        await UniTask.Delay(Delaytime);
        time.Value++;
        _pos.Value =  this.gameObject.GetComponent<Transform>().position;

        await UniTask.Delay(Delaytime);
        time.Value++;
        _pos.Value =  this.gameObject.GetComponent<Transform>().position;

        await UniTask.Delay(Delaytime);
        time.Value++;
        _pos.Value =  this.gameObject.GetComponent<Transform>().position;

        _star.OnNext(Unit.Default);
    }
}
