using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

public class AssetManager
{
    protected ContentManager contentManager;
    public float volume = .5f;

    public AssetManager(ContentManager content)
    {
        contentManager = content;
    }

    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
        {
            return null;
        }
        return contentManager.Load<Texture2D>(assetName);
    }

    public void PlaySound(string assetName)
    {
        SoundEffect snd = contentManager.Load<SoundEffect>(assetName);
        snd.Play();
    }

    public void PlayOnce(SoundEffectInstance SEI)
    {
        SEI.Volume = volume;
        SEI.Play();
    }

    public void PlaySound(SoundEffectInstance SEI)
    {
        SEI.Volume = volume;
        if (SEI.State == SoundState.Playing)
        {
            SEI.Stop();
        }
        SEI.Play();
    }

    public void StopSound(SoundEffectInstance SEI)
    {
        SEI.Stop();
    }

    public void PlayMusic(string assetName, bool repeat = true)
    {
        string songFileName = @"Content/" + assetName + ".ogg";
        var uri = new Uri(songFileName, UriKind.Relative);
        var song = Song.FromUri(assetName, uri);
        Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = repeat;
        Microsoft.Xna.Framework.Media.MediaPlayer.Play(song);//.Load<Song>(assetName));
    }

    public ContentManager Content
    {
        get { return contentManager; }
    }
}