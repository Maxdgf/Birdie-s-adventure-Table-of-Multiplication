using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private Sprite[] bossStates;
    [SerializeField] private GameObject player;
    [SerializeField] private BossFightDataServer bossFightDataServer;
    [SerializeField] private GameUiManager gameUiManager;
    [SerializeField] private float speed, answersPanelMoveSpeed;
    [SerializeField] private float autoBossAttackInterval;

    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private Coroutine wingsRoutine;
    private AudioSource soundSorce;
    private float autoBossAttackTime;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Transform sound = transform.Find("Sound Source");
        soundSorce = sound.gameObject.GetComponent<AudioSource>();

        startPosition = gameObject.transform.position;
        wingsRoutine = StartCoroutine(MoveWings(0.5f));
        autoBossAttackTime = autoBossAttackInterval;
    }

    void Update()
    {
        if (bossFightDataServer.bossHealth > 0)
        {
            /*
             * ---------------------------------------------------------
             * If the player is inactive for a certain amount of time, 
             * the boss will automatically attack him, 
             * also causing damage to the player.
             * ---------------------------------------------------------
             */
            if (autoBossAttackTime > 0)
            {
                autoBossAttackTime -= Time.deltaTime; // decrease time
                if (bossFightDataServer.isPlayerAttackNow)
                    autoBossAttackTime = autoBossAttackInterval;
            }
            else
            {
                PrepareToAttack(); // prepare boss to attack
                bossFightDataServer.ManageBossAttackState(true); // set boss attck state to true
            }
            bossFightDataServer.UpdateAutoBossAttackTimer(autoBossAttackTime); // update timer state


            if (bossFightDataServer.isBossAttackNow)
            {
                MakeMoveToPlayer();
                gameUiManager.MoveAnswersPanel(false); // hide answers panel
            }
            else
                if (transform.position != startPosition)
                {
                    MakeMoveToStartPosition(); // return to start position
                    gameUiManager.MoveAnswersPanel(true); // show answers panel
                }
        } else FallDown(); // fall down
    }

    /// <summary>
    /// Plays boss wings animation.
    /// </summary>
    /// <param name="delay">Move delay.</param>
    private IEnumerator MoveWings(float delay)
    {
        while (true)
        {
            spriteRenderer.sprite = bossStates[2];
            yield return new WaitForSeconds(delay);
            spriteRenderer.sprite = bossStates[1];
            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// Returns boss to start position.
    /// </summary>
    private IEnumerator ReturnToStartPosition()
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = new Vector2(5f, -0.14f);
        bossFightDataServer.ManageBossAttackState(false);
        wingsRoutine = StartCoroutine(MoveWings(0.5f));
    }

    /// <summary>
    /// Prepares boss to attack player.
    /// </summary>
    public void PrepareToAttack()
    {
        autoBossAttackTime = autoBossAttackInterval; // reset auto boss attack timer
        soundSorce.Play(); // play boss attack sound effect

        StopCoroutine(wingsRoutine); // stop wings move coroutine
        StartCoroutine(ReturnToStartPosition()); // start return boss to start position coroutine

        spriteRenderer.sprite = bossStates[0]; // set attack sprite to boss
        bossFightDataServer.DecreasePlayerHealth(); // player attacked by boss, decrease player health
    }

    /// <summary>
    /// Moves boss to player position.
    /// </summary>
    private void MakeMoveToPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        gameObject.transform.Translate(playerPosition * (Time.deltaTime * speed));
    }

    /// <summary>
    /// Moves boss to start position.
    /// </summary>
    private void MakeMoveToStartPosition()
    {
        gameObject.transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * speed);
    }

    /// <summary>
    /// Moves boss down when your health equals 0.
    /// </summary>
    private void FallDown()
    {
        gameObject.transform.Translate(Vector2.down * (Time.deltaTime * 5f));
    }
}
