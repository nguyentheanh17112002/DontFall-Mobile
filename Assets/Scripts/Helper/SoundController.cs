using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {


    public static SoundController Inst;

    public AudioClip _buttonClick;
    public AudioClip _gameOver;
    public AudioClip _moveDone;
    public AudioClip _backGround;
    public AudioClip _coin;
    public AudioClip _keyActiveItems;



    public bool _isMute;

    private AudioSource _audioSource;

    public int soundState;

    void Awake()
    {
        Inst = this;
        _audioSource = GetComponent<AudioSource>();
        CheckVolume();
    }

    
    void Update()
    {
        CheckVolume();
    }
    private void CheckVolume()
    {
        if (PlayerPrefs.GetString(Config.Volume) == Volume.Mute)
        {
            _isMute = true;
            soundState = 0;
        }
        else
        {
            _isMute = false;
            soundState = 1;
        }
    }

    public void PlayButtonClick()
    {
        if (!_isMute)
        {
            _audioSource.PlayOneShot(_buttonClick);
        }
    }

    public void PlayKeyClick()
    {
        if (!_isMute)
        {
            _audioSource.PlayOneShot(_keyActiveItems);
        }
    }

    public void PlayMoveDone()
    {
        if (!_isMute)
        {
            _audioSource.PlayOneShot(_moveDone);
        }
    }

    public void PlayCoin()
    {
        if (!_isMute)
        {
            _audioSource.PlayOneShot(_coin);
        }
    }

    public void ChangeVolume()
    {
        soundState++;
        if (soundState % 2 == 0)
        {
            PlayerPrefs.SetString(Config.Volume, Volume.Mute);
            _isMute = true;
            StopGameBackGround();
        }
        else
        {
            PlayerPrefs.SetString(Config.Volume, Volume.NotMute);
            _isMute = false;
            PlayGameBackGround();
        }
    }


    public void PlayGameOver()
    {
        if (!_isMute)
        {
            _audioSource.PlayOneShot(_gameOver);
        }
    }


    public void PlayGameBackGround()
    {
        if (!_isMute)
        {
            _audioSource.clip = _backGround;
            _audioSource.Play();
            _audioSource.loop = true;
        }
    }

    public void StopGameBackGround()
    {
        _audioSource.clip = _backGround;
        _audioSource.Stop();
    }

}
