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

    #region ��������
    /// <summary>
    /// ���ű�������
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
        //�첽���ر������֣�������ɺ󲥷�
        ResMgr.GetInstance().LoadAsync<AudioClip>("Resources/Music/BK/" + name, (clip) => 
        {
            backgroundMusic.clip = clip;
            backgroundMusic.loop = true;
            backgroundMusic.volume = backgroundMusicValue;
            backgroundMusic.Play();
        });
    }

    /// <summary>
    /// �ı䱳������������С
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
    /// ��ͣ��������
    /// </summary>
    public void PauseBackgroundMusic()
    {
        if (backgroundMusic == null)
            return;
        backgroundMusic.Pause();
    }

    /// <summary>
    /// ֹͣ��������
    /// </summary>
    /// <param name="name"></param>
    public void StopBackgroundMusic() 
    {
        if (backgroundMusic == null)
            return;
        backgroundMusic.Stop();
    }
    #endregion

    #region ��Ч
    /// <summary>
    /// ֹͣ��Ч
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> callBack=null)
    {
        if(soundObj==null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }      
        //�첽������Ч��������ɺ������Ч
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
    /// �ı���Ч��С
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
    /// ֹͣ��Ч
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
