using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;

    [SerializeField] AudioClip move_sound;
    [SerializeField] AudioClip close_sound;
    [SerializeField] AudioClip win_sound;
    [SerializeField] AudioClip goodClick_sound;
    [SerializeField] AudioClip badClick_sound;


    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        _instance = this;
    }

    AudioClip CheckClip(SoundTypes _)
    {
        switch (_)
        {
            case SoundTypes.MOVE:
                return move_sound;
            case SoundTypes.CLOSE:
                return close_sound;
            case SoundTypes.WIN:
                return win_sound;
            case SoundTypes.GOODCLICK:
                return goodClick_sound;
            case SoundTypes.BADCLICK:
                return badClick_sound;
            default:
                return null;
        }
    }

    public void PlayMoveSound(SoundTypes _)
    {
        audioSource.PlayOneShot(CheckClip(_));
    }
}
