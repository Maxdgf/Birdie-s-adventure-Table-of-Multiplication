/*
 * Description
 * -------------------------------------------------
 * This script controls player game object on scene.
 */

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private const float COLLISION_REACTION_DELAY = 1.5f; // player(birdy) collision reaction animation delay
    private const float WINGS_MOVE_DELAY = 0.3f; // player(birdy) wings animation delay
    private const int FORCE_REDUCTION_FACTOR = 2; // y-axis force player(birdy) reduction factor

    [SerializeField] [Tooltip("Player object force and smooth rotation speed.")] private float forcePower, smoothRotationSpeed; // player force and smooth rotation speed
    [SerializeField] [Tooltip("Audio source for sound effects.")] private AudioSource soundSource; // player AudioSource for sound effects
    [SerializeField] [Tooltip("Data server object.")] private PlayerDataServer dataServer; // player data server object
    [SerializeField] [Tooltip("Rotation angle.")] private int upAngle, fallAngle; // player rotation angles
    [SerializeField] [Tooltip("Danger zones tags.")] private string upperZoneTag, lowerZoneTag; // danger zones tags
    [SerializeField] [Tooltip("Audio effects clips.")] private AudioClip upperZoneCollision, wingsSound, fallSound; // required sound effects
    [SerializeField] [Tooltip("Player state sprites.")] private Sprite actionState, normalState, confusedState; // state sprites

    private Rigidbody2D rigidBody2D; // player rigidbody2D
    private SpriteRenderer spriteRenderer; // player spriterenderer
    private AudioPlayer audioPlayer; // audio player
    private float initialPlayerXSize;
    private float initialPlayerYSize;

    void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioPlayer = gameObject.AddComponent<AudioPlayer>();
        audioPlayer.SetAudioSource(soundSource); // setup audio source

        initialPlayerXSize = gameObject.transform.localScale.x; // get initial player x scale
        initialPlayerYSize = gameObject.transform.localScale.y; // get initial player y scale

        AddStartYForceToPlayer(); // add start force to player rigid body
    }

    void Update()
    {
        // player rotation when he falling or moving up.
        if (rigidBody2D.linearVelocityY < 0)
        {
            Quaternion angle = Quaternion.Euler(0, 0, fallAngle); // target angle
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * smoothRotationSpeed); // fall
        }
        else
        {
            Quaternion angle = Quaternion.Euler(0, 0, upAngle); // target angle
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * smoothRotationSpeed); // move up
        }
    }

    // player collision detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(upperZoneTag))
        {
            Handheld.Vibrate(); // vibration

            audioPlayer.PlayAudio(upperZoneCollision);
            StartCoroutine(CollisionReaction());

            AddYForceToPlayer(false);
        }
    }

    // player trigger enter detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(lowerZoneTag))
        {
            Handheld.Vibrate(); // vibration
            audioPlayer.PlayAudio(fallSound); // play fall sound
            dataServer.UpdateIsPlayerLostState();
        }
    }

    /// <summary>
    /// Adds a start y-axis force to player(birdy) RigidBody2D.
    /// </summary>
    private void AddStartYForceToPlayer()
    {
        rigidBody2D.AddForce(Vector2.up * forcePower / 2); // add half of force
    }

    /// <summary>
    /// Adds y-axis positive or negative force to player(birdy) RigidBody2D.
    /// </summary>
    /// <param name="isPositive">Positive or negative force.</param>
    public void AddYForceToPlayer(bool isPositive)
    {
        if (isPositive) rigidBody2D.AddForce(Vector2.up * forcePower); // positive y-axis force
        else rigidBody2D.AddForce(Vector2.down * forcePower / FORCE_REDUCTION_FACTOR); // negative y-axis force (with reduction factor)
    }

    /// <summary>
    /// Changes player(birdy) sprite, sets 'birdy with raised wings' sprite. Then, after delay, sets regular sprite.
    /// </summary>
    public IEnumerator MoveWings()
    {
        audioPlayer.PlayAudio(wingsSound); // play sound effect
        spriteRenderer.sprite = actionState; // set sprite with raised wings
        yield return new WaitForSeconds(WINGS_MOVE_DELAY); // delay
        spriteRenderer.sprite = normalState; // set regular sprite
    }

    /// <summary>
    /// Changes player(birdy) sprite, sets 'confused birdy' sprite. Then, after delay, sets regular sprite.
    /// </summary>
    private IEnumerator CollisionReaction()
    {
        spriteRenderer.sprite = confusedState; // set state sprite
        gameObject.transform.localScale = new Vector2(initialPlayerXSize, 0.4f); // flatten player a little
        yield return new WaitForSeconds(COLLISION_REACTION_DELAY); // delay
        spriteRenderer.sprite = normalState; // set normal state sprite
        gameObject.transform.localScale = new Vector2(initialPlayerXSize, initialPlayerYSize); // return player to initial size
    }
}