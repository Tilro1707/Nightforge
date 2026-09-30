using UnityEngine;

public class AudioComposer : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource steamSource;

    [Header("Spell Sounds")]
    [SerializeField] private AudioClip[] jetLanceSounds;
    [SerializeField] private AudioClip[] firestormSounds;

    [Header("Enemy Sounds")]
    [SerializeField] private AudioClip[] enemyHitSounds;
    [SerializeField] private AudioClip[] enemyDeathSounds;

    [Header("Player Sounds")]
    [SerializeField] private AudioClip[] playerHitSounds;
    [SerializeField] private AudioClip[] playerDeathSounds;

    [Header("Steam")]
    [SerializeField] private AudioClip steamLoop;

    private void Awake()
    {
        steamSource.clip = steamLoop;
        steamSource.loop = true;
        steamSource.playOnAwake = false;
    }

    public void PlayJetLance()
    {
        PlayRandomSound(jetLanceSounds);
    }

    public void PlayFirestorm()
    {
        PlayRandomSound(firestormSounds);
    }

    public void PlayEnemyHit()
    {
        PlayRandomSound(enemyHitSounds);
    }

    public void PlayEnemyDeath()
    {
        PlayRandomSound(enemyDeathSounds);
    }

    public void PlayPlayerHit()
    {
        PlayRandomSound(enemyHitSounds);
    }

    public void PlayPlayerDeath()
    {
        PlayRandomSound(enemyDeathSounds);
    }

    public void StartSteam()
    {
        if (!steamSource.isPlaying)
        {
            steamSource.Play();
        }
    }

    public void StopSteam()
    {
        if (steamSource.isPlaying)
        {
            steamSource.Stop();
        }
    }

    private void PlayRandomSound(AudioClip[] sounds)
    {
        if (sounds == null || sounds.Length == 0)
            return;

        int randomIndex = Random.Range(0, sounds.Length);

        sfxSource.PlayOneShot(sounds[randomIndex]);
    }
}