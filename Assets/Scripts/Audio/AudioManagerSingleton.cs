using System.Collections;
using UnityEngine;

public class AudioManagerSingleton : AbstractSingleton<AudioManagerSingleton>
{

    [SerializeField]
    private AudioSource audioSourceSFX;

    [SerializeField]
    private AudioSource audioSourceBGM;

    public void Play(AudioClip clip, float delay, bool isSFX)
    {
        StartCoroutine(CoroutinePlay(clip, delay, isSFX));
    }

    //TODO ren: use UniTask or asyncs
    private IEnumerator CoroutinePlay(AudioClip clip, float delay, bool isSFX)
    {
        if (clip == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(delay);

        if (isSFX)
        {
            audioSourceSFX.PlayOneShot(clip);
        }
        else
        {
            audioSourceBGM.Stop();
            audioSourceBGM.clip = clip;
            audioSourceBGM.Play();
        }
    }

}