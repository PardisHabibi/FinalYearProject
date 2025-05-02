using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ActivitySceneLoader : MonoBehaviour
{
    public GameObject alert;
    public GameObject activity;

    //Load scenes if health and hygiene are above a certain limit
    public void ActivityButton(string sceneName)
    {
        if (PlayerStats.Instance.Health > 4 && PlayerStats.Instance.Hygiene > 4)
        {
            SceneManager.LoadScene(sceneName);
        } else
        {
            activity.SetActive(false);
            alert.SetActive(true);
        }
    }
}
