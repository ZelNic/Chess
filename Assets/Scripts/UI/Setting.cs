using System;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public static Action onActivatedSettingsGame;
    public void CustomizeGame()
    {
        onActivatedSettingsGame?.Invoke();
    } 

}
