using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    // Get ground
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    //Move ground
    private void Update()
    {
        float speed = GameManagerDino.Instance.Speed / transform.localScale.x;
        meshRenderer.material.mainTextureOffset +=  Vector2.right * speed * Time.deltaTime;
    }
}
