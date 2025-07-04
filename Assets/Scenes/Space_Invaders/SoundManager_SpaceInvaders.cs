using UnityEngine;

// Defino los tipos de sonido que mi juego puede tener.
public enum SoundType { Fire, Explosion, Hit, GameOver, Edge, Button, Pickup }

public class SoundManager_SpaceInvaders : MonoBehaviour
{
    public static SoundManager_SpaceInvaders Instance { get; private set; }
    private AudioSource audioSource;

    [Header("Clips de Sonido")]
    public AudioClip fireSound;
    public AudioClip explosionSound;
    public AudioClip hitSound;
    public AudioClip gameOverSound;
    public AudioClip edgeSound;
    public AudioClip buttonSound;
    public AudioClip pickupSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Función única para reproducir cualquier sonido.
    public void Play(SoundType soundType)
    {
        if (!SettingsManager_SpaceInvaders.IsSoundOn) return;

        AudioClip clip = GetAudioClip(soundType);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Un switch para devolver el clip correcto según el tipo.
    private AudioClip GetAudioClip(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Fire: return fireSound;
            case SoundType.Explosion: return explosionSound;
            case SoundType.Hit: return hitSound;
            case SoundType.GameOver: return gameOverSound;
            case SoundType.Edge: return edgeSound;
            case SoundType.Button: return buttonSound;
            case SoundType.Pickup: return pickupSound;
            default: return null;
        }
    }
}