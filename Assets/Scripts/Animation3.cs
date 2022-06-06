using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class Animation3 : MonoBehaviour
{
    private float m_start_time = 0.0f;
    private float m_start_value = 0.0f;
    private float m_end_time = 5.0f;
    private float m_end_value = 10.0f;
    PlayableGraph graph;
    [SerializeField, Range(0.0f, 1.0f)]
    private float _blendRate;
    AnimationMixerPlayable mixer;
    AnimationClipPlayable prePlayable, currentPlayable;

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
        // clip.wrapMode = WrapMode.Loop;//機能しない

        // AnimationClipの生成.２
        AnimationClip clip2 = new AnimationClip();
        // AnimationCurveの生成.
        AnimationCurve curve2 = new AnimationCurve();
        // Keyframeの生成.
        Keyframe startKeyframe2 = new Keyframe(m_start_time, m_start_value);
        Keyframe endKeyframe2 = new Keyframe(1, 10);
        // Keyframeの追加.
        curve2.AddKey(startKeyframe2);
        curve2.AddKey(endKeyframe2);
        // AnimationCurveの追加.
        clip2.SetCurve("", typeof(Transform), "localPosition.x", curve2);
        clip2.wrapMode = WrapMode.Loop;

		// AnimationClipをMixerに登録
		currentPlayable = AnimationClipPlayable.Create (graph, clip);
		mixer = AnimationMixerPlayable.Create (graph, 2, true);
		mixer.ConnectInput (0, currentPlayable, 0);
		mixer.SetInputWeight (0, 1);//機能しない

        // outputにmixerとanimatorを登録して、再生
		var output = AnimationPlayableOutput.Create (graph, "output", GetComponent<Animator> ());
		output.SetSourcePlayable (mixer);

        graph.Play ();
    }

    private void Update()
    {
        // ブレンド率に応じてウェイトを設定
        mixer.SetInputWeight(0, 1.0f - _blendRate);
        mixer.SetInputWeight(1, _blendRate);
    }
}
