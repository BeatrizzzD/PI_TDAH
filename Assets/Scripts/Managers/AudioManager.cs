using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip successClip;
    [SerializeField] private AudioClip failureClip;
    [SerializeField] private AudioClip phaseMusicClip;

    private void Start()
    {
        AudioListener.volume = SettingsPanel.GetSavedVolume();

        if (musicSource && phaseMusicClip)
        {
            musicSource.clip = phaseMusicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySuccess() => sfxSource?.PlayOneShot(successClip);
    public void PlayFailure() => sfxSource?.PlayOneShot(failureClip);
    public void SetVolume(float volume) => AudioListener.volume = volume;
}
