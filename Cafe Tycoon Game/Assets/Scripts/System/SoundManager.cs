using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public enum SoundType
    {
        PressButton,
        Error,
        RecieveMoney,
        LevelUp
    }
    [SerializeField]
    private AudioSource UIAudioSource;
    [SerializeField]
    private AudioClip pressButtonAudioClip;
    [SerializeField]
    private AudioClip errorAudioClip;
    [SerializeField]
    private AudioClip recieveMoneyAudioClip;
    [SerializeField]
    private AudioClip levelUpAudioClip;

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.PressButton:
                UIAudioSource.clip = pressButtonAudioClip;
                UIAudioSource.Play(); 
                break;
            case SoundType.Error:
                UIAudioSource.clip = errorAudioClip;
                UIAudioSource.Play();
                break;
            case SoundType.RecieveMoney:
                UIAudioSource.clip = recieveMoneyAudioClip;
                UIAudioSource.Play();
                break;
            case SoundType.LevelUp:
                UIAudioSource.clip = levelUpAudioClip;
                UIAudioSource.Play();
                break;
            default:
                UIAudioSource.clip = null; 
                break;
        }
    }

}
