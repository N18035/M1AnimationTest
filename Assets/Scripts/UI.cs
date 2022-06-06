using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;

public class UI : MonoBehaviour
{
    [SerializeField] CreateAnimation createAnimation;

    [SerializeField] GameObject button;
    void Start()
    {
        createAnimation.OnEND
        .Subscribe(_ => button.SetActive(true))
        .AddTo(this);
    }

}
