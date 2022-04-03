using UnityEngine.Audio;
using System;
using UnityEngine;

/// <summary>
/// En klass som skapar en meny för ljud, varav man kan justera dessa i Editor
/// </summary>
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.clip = s.clip;
            s.source.loop = s.loop;

        }
    }

    /// <summary>
    /// Spelar vald låt när projektet kör 
    /// </summary>
    /// <param name="name">Namn som ljudet har i AudioManager-menyn</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();

    }

    /// <summary>
    /// Stoppar vald låt 
    /// </summary>
    /// <param name="name">Namn som ljudet har i AudioManager-menyn</param>
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Stop();

    }
}
