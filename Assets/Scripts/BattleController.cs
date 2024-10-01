using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    [Header("Hp필드")]
    public int playerHp;
    public int boyHp;
    public int enemyHp;

    [Header("Attack필드")]
    public int playerAttack;
    public int boyAttack;
    public int enemyAttack;

    [Header("log필드")]
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private TextMeshProUGUI turnText;

    [Header("Button필드")]
    [SerializeField] private Button attackButton;

    // field end

    public enum PlayerState
    {
        Young,
        Middle,
        Old
    }

    private PlayerState playerState;

    private bool isAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        playerHp = GameManager.Instance.PlayerHp;
        boyHp = GameManager.Instance.BoyHp;
        enemyHp = GameManager.Instance.EnemyHp;

        playerAttack = GameManager.Instance.PlayerAttack;
        boyAttack = GameManager.Instance.BoyAttack;
        enemyAttack = GameManager.Instance.EnemyAttack;
        logText.text = "";

        UpdateStatus();

        switch (playerState)
        {
            case PlayerState.Old:
                PlayerAttack();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHp <= 0)
        {
            GameManager.Instance.BoyBonusStat += 5;
            GameManager.Instance.PlayerHp -= 5;
            GameManager.Instance.PlayerAttack -= 1;

            isAttacking = false;
            SceneManager.LoadScene("Home");
        }
    }

    public void UpdateStatus()
    {
        int playerTotal = GameManager.Instance.PlayerHp + GameManager.Instance.PlayerAttack;
        int boyTotal = GameManager.Instance.BoyHp + GameManager.Instance.BoyAttack;

        // state determination
        if (playerTotal > boyTotal * 1.3f)
        {
            playerState = PlayerState.Young;
        }
        else if (playerTotal < boyTotal * 0.7f)
        {
            playerState = PlayerState.Old;
        }
        else
        {
            playerState = PlayerState.Middle;
        }
    }

    public void UpdateBattleLog(string message)
    {
        logText.text += message + "\n";
    }

    public void AttackButton()
    {
        if (isAttacking)
        {
            attackButton.interactable = false;
            return;
        }

        isAttacking = true;
        attackButton.interactable = false;

        switch (playerState)
        {
            case PlayerState.Young:
                StartCoroutine(YoungAttackSequence());
                break;

            case PlayerState.Middle:
                StartCoroutine(MiddleAttackSequence());
                break;

            case PlayerState.Old:
                StartCoroutine(OldAttackSequence());
                break;

        }
    }

    public void PlayerAttack()
    {
        turnText.text = "스승 턴";
        enemyHp -= playerAttack;
        UpdateBattleLog($"Player Attack! : {playerAttack} damage");

    }
    public void BoyAttack()
    {
        turnText.text = "제자 턴";
        enemyHp -= boyAttack;
        UpdateBattleLog($"Boy Attack! : {boyAttack} damage");
    }
    public void EnemyAttack()
    {
        turnText.text = "적 턴";
        int target = Random.Range(0, 2);

        if (target == 0)
        {
            playerHp -= enemyAttack;
            UpdateBattleLog($"Enemyy Attacked Player! : {enemyAttack} damage");
        }
        else if (target == 1)
        {
            boyHp -= enemyAttack;
            UpdateBattleLog($"Enemyy Attacked Boy! : {enemyAttack} damage");
        }

    }

    private IEnumerator YoungAttackSequence()
    {
        PlayerAttack();
        yield return new WaitForSeconds(1f);
        BoyAttack();
        yield return new WaitForSeconds(1f);
        if (enemyHp > 0)
        {
            EnemyAttack();
        }
        attackButton.interactable = true;
        isAttacking = false;
    }
    private IEnumerator MiddleAttackSequence()
    {
        PlayerAttack();
        yield return new WaitForSeconds(1f);
        attackButton.interactable = true;
        yield return new WaitUntil(() => attackButton.interactable == false);
        BoyAttack();
        yield return new WaitForSeconds(1f);
        if (enemyHp > 0)
        {
            EnemyAttack();
        }


        attackButton.interactable = true;
        isAttacking = false;
    }

    private IEnumerator OldAttackSequence()
    {
        BoyAttack();
        yield return new WaitForSeconds(1f);
        if (enemyHp > 0)
        {
            EnemyAttack();
        }
        yield return new WaitForSeconds(1f);
        PlayerAttack();
        yield return new WaitForSeconds(1f);
        attackButton.interactable = true;
        isAttacking = false;
    }
}
