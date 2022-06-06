using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class AnimationTest : MonoBehaviour
{
    PlayableGraph graph;

    // [SerializeField] string clipName = "RobotBoyRun";

    void Awake()
    {
        graph = PlayableGraph.Create ();
    }

    void Start()
    {
        //アニメーションクリップのインスタンス
        AnimationClip clip = new AnimationClip();

        //アニメーションコントローラーではなくアニメーションクリップ単体で操作する為の設定
        // clip.legacy = true;

        //値の変化を設定
        //Linear⇒直線的な変化
        //引数（開始時間, 開始値, 終了時間, 終了値）
        AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 3f, 0f);

        //キーフレームの設定
        //引数（時間, 値）
        //1.5秒の時点でz軸の値を5fにしている
        Keyframe key = new Keyframe(1.5f, 5f);

        //アニメーションカーブにキーフレームを追加
        curve.AddKey(key);

        //アニメーションクリップにアニメーションカーブをセット
        //引数（パスの指定, タイプ, 操作項目名, アニメーションカーブ）
        clip.SetCurve("", typeof(Transform), "localPosition.z", curve);

        //ラップモードの設定
        //ループに設定
        clip.wrapMode = WrapMode.Loop;

        // //アニメーションインスタンス
        // Animation animation = GetComponent<Animation>();

        // //アニメーションにアニメーションクリップを組み込む
        // //引数（アニメーションクリップ, 名前）
        // animation.AddClip(clip, "clip");

        // //アニメーションを再生
        // animation.Play("clip");

        //以下playable
        
        // アニメーションをResourcesから取得し
        // AnimationClipPlayableを構築
        // var clip = Resources.Load<AnimationClip> (clipName);

        var clipPlayable = AnimationClipPlayable.Create (graph, clip);

        // outputを生成して、出力先を自身のAnimatorに設定
        var output = AnimationPlayableOutput.Create (graph, "output", GetComponent<Animator>());

        // // playableをoutputに流し込む
        // output.SetSourcePlayable (clipPlayable);

        // graph.Play ();
        
    }

    void OnDestroy()
    {
        graph.Destroy ();
    }
}
