using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : BaseManager<MusicMgr>
{
    private AudioSource backgroundMusic;
    private float backgroundMusicValue = 1;

    private GameObject soundObj;
    private List<AudioSource> soundList =new List<AudioSource>();
    private float soundValue = 1;

    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }

    private void Update()
    {
        for (int i = soundList.Count - 1; i >= 0; --i)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    #region 背景音乐
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayBackgroundMusic(string name)
    {
        if(backgroundMusic == null)
        {
            GameObject obj = new GameObject();
            obj.name = "BackgroundMusic";
            backgroundMusic =obj.AddComponent<AudioSource>();
        }
        //异步加载背景音乐，加载完成后播放
        ResMgr.GetInstance().LoadAsync<AudioClip>("Resources/Music/BK/" + name, (clip) => 
        {
            backgroundMusic.clip = clip;
            backgroundMusic.loop = true;
            backgroundMusic.volume = backgroundMusicValue;
            backgroundMusic.Play();
        });
    }

    /// <summary>
    /// 改变背景音乐音量大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeBackgroundMusic(float value)
    {
        backgroundMusicValue = value;
        if(backgroundMusic == null) 
            return;
        backgroundMusic.volume = backgroundMusicValue;
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBackgroundMusic()
    {
        if (backgroundMusic == null)
            return;
        backgroundMusic.Pause();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void StopBackgroundMusic() 
    {
        if (backgroundMusic == null)
            return;
        backgroundMusic.Stop();
    }
    #endregion

    #region 音效
    /// <summary>
    /// 停止音效
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> callBack=null)
    {
        if(soundObj==null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }      
        //异步加载音效，加载完成后添加音效
        ResMgr.GetInstance().LoadAsync<AudioClip>("Resources/Music/Sound/" + name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundValue;
            source.Play();
            soundList.Add(source);
            if(callBack != null)
            {
                callBack(source);
            }
        });
    }

    /// <summary>
    /// 改变音效大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundValue(float value)
    {
        soundValue = value;
        for(int i = 0;i<soundList.Count;i++)
        {
            soundList[i].volume = soundValue;
        }
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    public void Stopsound(AudioSource source)
    {
        if(soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(soundObj);
        }
    }
    #endregion
}
