using UnityEngine;

public class SoundStep : MonoBehaviour
{
    [SerializeField] private AudioClip _soundStep;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        Distributor.onWasMadeMove += PlaySound;
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _soundStep;
    }

    private void PlaySound()
    {
        _audioSource.pitch = Random.Range(1f, 3f);
        _audioSource.Play();
    }
}
