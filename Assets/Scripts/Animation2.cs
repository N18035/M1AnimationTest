using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class Animation2 : MonoBehaviour
{
    private float m_start_time = 0.0f;
    private float m_start_value = 0.0f;
    private float m_end_time = 5.0f;
    private float m_end_value = 10.0f;
    PlayableGraph graph;

    void Awake()
    {
        graph = PlayableGraph.Create ();
    }

    void Start()
    {
        // AnimationClipの生成.
        AnimationClip clip = new AnimationClip();
        // AnimationCurveの生成.
        AnimationCurve curve = new AnimationCurve();
        // Keyframeの生成.
        Keyframe startKeyframe = new Keyframe(m_start_time, m_start_value);
        Keyframe endKeyframe = new Keyframe(m_end_time, m_end_value);
        // Keyframeの追加.
        curve.AddKey(startKeyframe);
        curve.AddKey(endKeyframe);
        // AnimationCurveの追加.
        clip.SetCurve("", typeof(Transform), "localPosition.x", curve);
        // AnimationClipの追加.
        // animation.AddClip(clip, "Move");
        // AnimationClipの再生.
        // animation.Play("Move");

        var clipPlayable = AnimationClipPlayable.Create (graph, clip);
        // outputを生成して、出力先を自身のAnimatorに設定
        var output = AnimationPlayableOutput.Create (graph, "output", GetComponent<Animator>());

        // // playableをoutputに流し込む
        output.SetSourcePlayable (clipPlayable);

        graph.Play ();
    }


}
