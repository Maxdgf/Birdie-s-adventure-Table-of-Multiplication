using UnityEngine;

public class ParallaksBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Transform bgTransform;
    private float bgSize, bgPos;

    void Start()
    {
        bgTransform = gameObject.transform;
        SpriteRenderer bg = gameObject.GetComponent<SpriteRenderer>();
        bgSize = bg.bounds.size.x;
    }

    void Update()
    {
        MoveBackground(); // start moving
    }

    /// <summary>
    /// Moves background from right to left side.
    /// </summary>
    private void MoveBackground()
    {
        bgPos -= moveSpeed * Time.deltaTime;
        bgPos = Mathf.Repeat(bgPos, bgSize);
        bgTransform.position = new Vector2(bgPos, 0);
    }
}