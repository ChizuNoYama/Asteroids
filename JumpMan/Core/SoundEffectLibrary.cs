using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace JumpMan.Core;

public class SoundEffectLibrary
{
    public SoundEffectLibrary()
    {
        _library = new Dictionary<string, SoundEffect>();
    }

    private Dictionary<string, SoundEffect> _library;

    public static SoundEffectLibrary Instance { get; private set; }
   
    public static void Initialize()
    {
        if (Instance == null)
        {
            Instance = new SoundEffectLibrary();
        }
        else
        {
            Debug.WriteLine("WARNING:: Instance of SoundEffectLibrary already exists");
        }
        
    }
    
    public void AddToLibrary(string name, SoundEffect effect)
    {
        if (_library.ContainsKey(name))
        {
            //TODO: Either throw an exception for dev to change the name, or replace the sound.
            Debug.WriteLine($"WARNING:: SoundEffect ({name}) is being replaced in the SoundEffectLibrary");// Currently replacing the soundEffect
        }
        else
        {
            _library.Add(name, effect);
        }
    }

    public void Play(string name)
    {
        if (_library.TryGetValue(name, out var value))
        {
            value.Play();
        }
        else
        {
            Debug.WriteLine($"WARNING:: SoundEffect ({name}) does not exist in SoundEffectLibrary");
        }
    }

    public void ClearSoundEffect()
    {
        _library.Clear();
    }

    public void DisposeInstance()
    {
        _library.Clear();
        Instance = null;
    }
}