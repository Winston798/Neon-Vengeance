using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : SingletonBase<AudioService>
{
    public AudioSource BKAudio;
    public AudioSource EffectAudio;
    public float BKVolume => BKAudio.volume;
    public float EffectVolume => EffectAudio.volume;
    public override void Init()
    {
        base.Init();
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="ClipName">音乐名字</param>
    public void PlayBK(string ClipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Music/" + ClipName);
        BKAudio.clip = clip;
        BKAudio.Play();
    }
    public void StopBK()
    {
        BKAudio.Stop();
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="ClipName">音效名字</param>
    public void PlayEffect(string ClipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Music/" + ClipName);
        EffectAudio.PlayOneShot(clip);
    }

    //改变背景音乐大小
    public void ChangeBKVolume(float value)
    {
        BKAudio.volume = value * 0.2f;
    }
    //改变音效大小
    public void ChangeEffectVolume(float value)
    {
        EffectAudio.volume = value;
    }

}
