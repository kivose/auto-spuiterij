using UnityEngine;

public class scrollingtexturescript : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public Vector2 scrollDirection = Vector2.right;

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        Vector2 offsetVector = offset * scrollDirection;
        _renderer.material.mainTextureOffset = offsetVector;
    }
}
    