using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UniRx;

public class AddAnimation : MonoBehaviour
{
    [SerializeField] AnimationManager animationManager;
    PlayableGraph graph;
    AnimationMixerPlayable mixer;
    AnimationClipPlayable Playable1;
    AnimationClipPlayable Playable2;

    //ブレンドレート 0から1まで
    [SerializeField] private FloatReactiveProperty _blendRate = new FloatReactiveProperty(0.5f); 
    [SerializeField,Range(0f,1f)] private float _rate=0.5f;

    bool AnimationIsCreated;

    void Start(){
        graph = PlayableGraph.Create ();

        // _blendRate
        // .Where(_ => AnimationIsCreated)
        // .Subscribe(_ =>{
        //     _blendRate.Value = Mathf.Clamp(_blendRate.Value,0f,1f);
        //     // ブレンド率に応じてウェイトを設定
        //     mixer.SetInputWeight(0, 1.0f - _blendRate.Value);
        //     mixer.SetInputWeight(1, _blendRate.Value);
        // })
        // .AddTo(this);
    }

    public void SetAnimation(){
        // AnimationClipをMixerに登録
		Playable1 = AnimationClipPlayable.Create (graph, animationManager.clip);
        Playable2 = AnimationClipPlayable.Create (graph, animationManager.clip2);

        // ミキサーを生成して、Clip1とClip2を登録
		mixer = AnimationMixerPlayable.Create (graph, 2, true);
		mixer.ConnectInput (0, Playable1, 0);
        mixer.ConnectInput (1, Playable2, 0);
		// mixer.SetInputWeight (0, 1);//機能しない

        // outputにmixerとanimatorを登録して、再生
		var output = AnimationPlayableOutput.Create (graph, "output", GetComponent<Animator> ());
		// playableをoutputに流し込む
        output.SetSourcePlayable (mixer);

        //ウェイトを追加        
            mixer.SetInputWeight(0, 1.0f - _rate);
            mixer.SetInputWeight(1, _rate);


        graph.Play ();

        AnimationIsCreated=true;
    }

}
