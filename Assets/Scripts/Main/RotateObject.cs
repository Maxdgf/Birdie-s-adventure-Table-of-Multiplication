/*
 * Description
 * --------------------------------------------------
 * This script rotates game object by specific speed.
 */

using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] [Tooltip("Game object rotation speed.")] private float rotation_speed; // object rotation speed

    void Update()
    {
        transform.Rotate(0, 0, rotation_speed * Time.deltaTime); // rotate with speed
    }
}
