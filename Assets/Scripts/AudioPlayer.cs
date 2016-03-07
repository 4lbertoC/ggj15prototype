using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour
{

    private static AudioPlayer _instance;

    public List<AudioSource> victorySounds;
    public List<AudioSource> caricatoreSounds;
    public List<AudioSource> escapeSounds;
    public List<AudioSource> deadSounds;
    public List<AudioSource> gunSounds;
    public List<AudioSource> voiceSounds;
    public List<AudioSource> blossomSounds;
    public List<AudioSource> musics;
    private Dictionary<string, List<AudioSource>> audioSources = new Dictionary<string, List<AudioSource>>();
    private int playingMusicId = 0;

    void Awake()
    {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        audioSources.Add("Victory", victorySounds);
        audioSources.Add("Caricatore", caricatoreSounds);
        audioSources.Add("Escape", escapeSounds);
        audioSources.Add("Dead", deadSounds);
        audioSources.Add("Gun", gunSounds);
        audioSources.Add("Voice", voiceSounds);
        audioSources.Add("Blossom", blossomSounds);
    }

    public static AudioPlayer GetInstance()
    {
        return _instance;
    }

    public void PlaySound(string id)
    {
        int randSoundIdx = (int)Mathf.Floor(Random.value * audioSources[id].Count);
        audioSources[id][randSoundIdx].Play();
    }

    public void StopSound(string id)
    {
        foreach (AudioSource a in audioSources[id])
        {
            a.Stop();
        }
    }

    public void PlayMusic(int musicId, bool overlapping = false, float delay = 0f)
    {
        if (musicId == playingMusicId)
        {
            return;
        }
        playingMusicId = musicId;
        if (!overlapping)
        {
            foreach (AudioSource s in musics)
            {
                s.Stop();
            }
        }
        musics[musicId].PlayDelayed(delay);
        Debug.Log("Playing " + musics[musicId].name + (overlapping ? " in overlapping mode" : ""));
    }
}
