using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    //一定要创建audioSource组件，赋予脚本里的变量！
    public AudioSource soundAudio; // 1.特效音
    public AudioSource musicAudio; //2.背景音乐
    public AudioSource dialogAudio;//3.对话音乐

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // 1.特效音
    public void PlaySound(string soundPath)
    {
        soundAudio.PlayOneShot(Resources.Load<AudioClip>("AudioClips/Sound/" + soundPath)); // 播放一次
    }

    //2.背景音乐
    public void PlayMusic(string musicPath, bool loop = true)
    {
        musicAudio.loop = loop;
        musicAudio.clip = Resources.Load<AudioClip>("AudioClips/Music/" + musicPath);
        musicAudio.Play(); // 一直播放
    }

    //2.背景音乐停止
    public void StopMusic()
    {
        musicAudio.Stop();
    }

    //3.对话音乐
    public void PlayDialog(string dialogPath)
    {
        dialogAudio.clip = Resources.Load<AudioClip>("AudioClips/Dialogue/" + dialogPath);
        dialogAudio.Play();
    }

    public void StopDialogMusic()
    {
        dialogAudio.Stop();
    }



}
