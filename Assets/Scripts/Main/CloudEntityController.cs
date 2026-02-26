/*
 * Description
 * -------------------------------------------------
 * This script moves cloud game object to left side.
 */

using UnityEngine;

public class CloudEntityController : MonoBehaviour
{
    [SerializeField] [Tooltip("Cloud object move speed.")] private float speed;

    void Update()
    {
        transform.Translate(Vector2.left * (Time.deltaTime * speed)); // left move with speed
    }
}
