using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _playerSFX, _upgradeUseSFX, _upgradePickupSFX, _buttonSFX;

    [SerializeField] AudioClip _shoot, _healthUp, _nuke, _rapidFire, _shield, _speedUp, _buttonPressed;

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "Shoot":
                _playerSFX.PlayOneShot(_shoot);
                //Debug.Log("Playing shoot sound");
                break;
            case "HealthUp":
                _upgradePickupSFX.PlayOneShot(_healthUp);
                break;
            case "Nuke":
                _upgradeUseSFX.PlayOneShot(_nuke);
                break;
            case "RapidFire":
                _upgradePickupSFX.PlayOneShot(_rapidFire);
                break;
            case "Shield":
                _upgradePickupSFX.PlayOneShot(_shield);
                break;
            case "SpeedUp":
                _upgradePickupSFX.PlayOneShot(_speedUp);
                break;
            case "ButtonPressed":
                _buttonSFX.PlayOneShot(_buttonPressed);
                break;
        }
    }
}
