using System.Collections;
using UnityEngine;

public class playerFightController : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private BossFightDataServer bossFightDataServer;
    [SerializeField] private float speed;
    [SerializeField] private Sprite[] playerStates;

    private SpriteRenderer spriteRenderer;
    private Coroutine wingsRoutine;
    private Vector3 startPosition;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        wingsRoutine = StartCoroutine(MoveWings(0.5f));
        startPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (bossFightDataServer.isPlayerAttackNow) AttackBoss();
        else
            if (transform.position != startPosition) MakeMoveToStartPosition();
    }

    private void AttackBoss()
    {
        Vector2 bossPosition = boss.transform.position;
        gameObject.transform.Translate(bossPosition * (Time.deltaTime * speed));
    }

    private IEnumerator ReturnToStartPosition()
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = new Vector2(-3.9f, -0.22f);
        bossFightDataServer.ManagePlayerAttackState(false);
        wingsRoutine = StartCoroutine(MoveWings(0.5f));
    }

    public void PrepareToAttack()
    {
        StopCoroutine(wingsRoutine);
        StartCoroutine(ReturnToStartPosition());
        spriteRenderer.sprite = playerStates[0];
        bossFightDataServer.DecreaseBossHealth();
    }

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

    private void MakeMoveToStartPosition()
    {
        gameObject.transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * speed);
    }
}
