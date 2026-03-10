using System.Collections;
using UnityEngine;

public class playerFightController : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private BossFightDataServer bossFightDataServer;
    [SerializeField] private GameUiManager gameUiManager;
    [SerializeField] private float speed;
    [SerializeField] private Sprite[] playerStates;

    private SpriteRenderer spriteRenderer;
    private Coroutine wingsRoutine;
    private Vector3 startPosition;
    private Vector2 bossPosition;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        wingsRoutine = StartCoroutine(MoveWings(0.5f));
        startPosition = gameObject.transform.position;
        bossPosition = boss.transform.position;
    }

    void Update()
    {
        if (bossFightDataServer.playerHealth > 0) {
            if (bossFightDataServer.isPlayerAttackNow)
            {
                AttackBoss();
                gameUiManager.MoveAnswersPanel(false); // hide answers panel
            }
            else
                if (transform.position != startPosition)
                {
                    MakeMoveToStartPosition();
                    gameUiManager.MoveAnswersPanel(true); // show answers panel
                }
        }
        else
        {
            bossFightDataServer.SetBossWin();
            gameUiManager.HideAllImmediately();
            FallDown();
        }
    }

    /// <summary>
    /// Moves player to boss.
    /// </summary>
    private void AttackBoss()
    {
        gameObject.transform.Translate(bossPosition * (Time.deltaTime * speed));
    }

    /// <summary>
    /// Returns player to start position.
    /// </summary>
    private IEnumerator ReturnToStartPosition()
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = new Vector2(-3.9f, -0.22f); // move outside the camera for a while
        bossFightDataServer.ManagePlayerAttackState(false);
        wingsRoutine = StartCoroutine(MoveWings(0.5f)); // start wings animation
    }

    /// <summary>
    /// Prepares player to attack.
    /// </summary>
    public void PrepareToAttack()
    {
        StopCoroutine(wingsRoutine); // stop wings animation
        StartCoroutine(gameUiManager.DisableBlockPanelAfterDelay(2f)); // enable block panel on answers panel
        StartCoroutine(ReturnToStartPosition()); // start return to start position player routine

        spriteRenderer.sprite = playerStates[0]; // set attack sprite
        bossFightDataServer.DecreaseBossHealth(); // decrease boss health
    }

    /// <summary>
    /// Animates player wings.
    /// </summary>
    /// <param name="delay">Animation delay.</param>
    private IEnumerator MoveWings(float delay)
    {
        while (true)
        {
            spriteRenderer.sprite = playerStates[2];
            yield return new WaitForSeconds(delay);
            spriteRenderer.sprite = playerStates[1];
            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// Moves player to start position finally.
    /// </summary>
    private void MakeMoveToStartPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * speed);
    }

    /// <summary>
    /// Moves player down with rotation(fall effect).
    /// </summary>
    private void FallDown()
    {
        transform.Translate(Vector2.down * (Time.deltaTime * 5f), Space.World);
        transform.Rotate(0, 0, 360f * Time.deltaTime); // rotate player
    }
}
