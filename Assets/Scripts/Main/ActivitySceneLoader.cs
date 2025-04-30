using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ActivitySceneLoader : MonoBehaviour
{
    public GameObject alert;

    public void ActivityButton(string sceneName)
    {
        if (PlayerStats.Instance.Health > 0 && PlayerStats.Instance.Hygiene > 0)
        {
            SceneManager.LoadScene(sceneName);
        } else
        {
            alert.gameObject.SetActive(true);
        }
    }
}
