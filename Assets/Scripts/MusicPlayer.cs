using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            print("Duplicate music player self-destructing!");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }

    void OnLevelWasLoaded(int level)
    {
        music.Stop();

        switch (level)
        {
            case 0:
                music.clip = startClip;
                Debug.Log("MusicPlayer loaded StartMenu");
                break;
            case 1:
                music.clip = gameClip;
                Debug.Log("MusicPlayer loaded GameScene");
                break;
            case 2:
                music.clip = endClip;
                Debug.Log("MusicPlayer loaded WinScreen");
                break;
        }

        music.Play();
        music.loop = true;
    }
}
