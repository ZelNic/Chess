using Unity.VisualScripting;
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
        _audioSource.Play();
    }
}
