using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
   public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
