using UnityEngine;
using UnityEngine.UI;

public class SoundStep : MonoBehaviour
{
    [SerializeField] private Button _buttonSound;
    [SerializeField] private AudioClip _soundStep;
    private AudioSource _audioSource;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _muteSprite;
    private Image _imageButton;


    private void OnEnable()
    {
        Setting.onActivatedSettingsGame += SwitchActiveButton;
        Distributor.onWasMadeMove += PlaySound;
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _soundStep;
        _imageButton = _buttonSound.GetComponent<Image>();
    }
    private void SwitchActiveButton()
    {
        if (_buttonSound.gameObject.activeInHierarchy == true)
            _buttonSound.gameObject.SetActive(false);
        else
            _buttonSound.gameObject.SetActive(true);
    }

    private void PlaySound()
    {
        _audioSource.pitch = Random.Range(1f, 3f);
        _audioSource.Play();
    }
    public void ToggleSound()
    {
        if (_audioSource.mute == true)
        {
            _audioSource.mute = false;
            _imageButton.sprite = _normalSprite;
        }
        else
        {
            _audioSource.mute = true;
            _imageButton.sprite = _muteSprite;
        }
    }

}
