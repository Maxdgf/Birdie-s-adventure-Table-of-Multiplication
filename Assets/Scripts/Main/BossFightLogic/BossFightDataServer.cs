using UnityEngine;

public class BossFightDataServer : MonoBehaviour
{
    public int playerHealth, bossHealth;
    [HideInInspector] public string gameState;
    [HideInInspector] public bool isBossAttackNow, isPlayerAttackNow;
    public float autoBossAttackTimer;

    public void DecreasePlayerHealth()
    {
        playerHealth--;

    }

    public void DecreaseBossHealth()
    {
        bossHealth--;
    }

    public void ManageBossAttackState(bool state)
    {
        isBossAttackNow = state;
    }

    public void ManagePlayerAttackState(bool state)
    {
        isPlayerAttackNow = state;
    }

    public void UpdateAutoBossAttackTimer(float time)
    {
        autoBossAttackTimer = time;
    }

    public void SetPlayerWin()
    {
        gameState = "PLAYER_WIN";
    }

    public void SetBossWin()
    {
        gameState = "BOSS_WIN";
    }
}
