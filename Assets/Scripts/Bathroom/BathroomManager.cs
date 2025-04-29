using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BathroomManager : MonoBehaviour
{
    public float showerProgress = 0f;
    public float increaseRate = 5f;
    public float bonusIncrease = 5f;
    public float maxProgress = 100f;
    private bool isShowering = false;

    public RectTransform soapCloud;
    public Vector2 startPos;
    public Vector2 endPos;

    public GameObject play;
    public TextMeshProUGUI complete;

    public Slider bathTime;

    private void Start()
    {
        Time.timeScale = 0f;

        if (soapCloud != null)
            soapCloud.anchoredPosition = startPos;
    }

    private void Update()
    {
        if (isShowering)
        {
            showerProgress += increaseRate * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                showerProgress += bonusIncrease;
            }

            showerProgress = Mathf.Clamp(showerProgress, 0f, maxProgress);

            if (bathTime != null)
            {
                bathTime.value = showerProgress;
            }

            if (soapCloud != null)
            {
                float t = showerProgress / maxProgress;
                soapCloud.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            }

            if (showerProgress >= maxProgress)
            {
                isShowering = false;
                complete.gameObject.SetActive(true);
                PlayerStats.Instance.Hygiene += maxProgress;
            }
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
        isShowering = true;
        play.SetActive(false);
    }
}
