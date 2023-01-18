using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip menuMusic;
    // Start is called before the first frame update

    static MusicManager instance = null;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += (scene, mode) => Play(scene);
        Play(SceneManager.GetActiveScene());

    }








    void Play(Scene scene)
    {

        switch (scene.name)
        {

            case "Level1": PlayMusic(level1Music); break;
            case "Level2": PlayMusic(level2Music); break;
            case "Menu": PlayMusic(menuMusic); break;
            case "WinScreen": PlayMusic(menuMusic); break;
            default: audioSource.Stop(); break;
        }
    }





    void PlayMusic(AudioClip music)
    {
        if (!audioSource.isPlaying || audioSource.clip != music)
        {
            audioSource.Stop();
            audioSource.clip = music;
            audioSource.Play();
        }
    }







}
