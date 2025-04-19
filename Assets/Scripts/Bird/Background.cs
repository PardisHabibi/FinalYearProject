using UnityEngine;

public class Background : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float speed = 1f;

    // Get background
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    //Move Background
    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
