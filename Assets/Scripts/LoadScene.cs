using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MiniGame");
    }
}
