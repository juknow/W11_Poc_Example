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
    [SerializeField] private Button healButton;

    // field end

    public enum PlayerState
    {
        Young,
        Middle,
        Old
    }

    private PlayerState playerState;

    private bool isPlayerChecking = false;
    private bool isboyAttacking = false;
    private bool isboyHealing = false;


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
            case PlayerState.Young:
                turnText.text = "스승 턴";
                UpdateBattleLog($"스승 생각 중... ");
                break;
            case PlayerState.Middle:
                turnText.text = "스승 턴";
                UpdateBattleLog($"스승 생각 중... ");
                break;
            case PlayerState.Old:
                StartCoroutine(OldStateTeacherRoutine());
                break;
        }
    }



    // Update Method
    void Update()
    {
        if (enemyHp <= 0)
        {
            GameManager.Instance.BoyBonusStat += 5;
            GameManager.Instance.PlayerHp -= 5;
            GameManager.Instance.PlayerAttack -= 1;

            isPlayerChecking = false;
            isboyAttacking = false; isboyHealing = false;
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



    //Button Method

    public void AttackButton()
    {
        if (isPlayerChecking)
        {
            isboyAttacking = true;
            attackButton.interactable = false;
            healButton.interactable = false;
            return;
        }

        isPlayerChecking = true;
        attackButton.interactable = false;
        healButton.interactable = false;

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

    public void HealButton()
    {
        if (isPlayerChecking)
        {
            isboyHealing = true;
            attackButton.interactable = false;
            healButton.interactable = false;
            return;
        }

        isPlayerChecking = true;
        attackButton.interactable = false;
        healButton.interactable = false;

        switch (playerState)
        {
            case PlayerState.Young:
                StartCoroutine(YoungHealSequence());
                break;

            case PlayerState.Middle:
                StartCoroutine(MiddleHealSequence());
                break;

            case PlayerState.Old:
                StartCoroutine(OldHealSequence());
                break;

        }
    }

    //Attack Method

    public void TeacherAttack()
    {
        enemyHp -= playerAttack;
        UpdateBattleLog($"스승은 공격을 선택했다! : {playerAttack} 데미지 !");

    }
    public void BoyAttack()
    {
        enemyHp -= boyAttack;
        UpdateBattleLog($"제자는 공격을 선택했다! : {boyAttack} 데미지 !");
    }
    public void EnemyAttack()
    {
        turnText.text = "적 턴";
        int target = Random.Range(0, 2);

        if (target == 0)
        {
            playerHp -= enemyAttack;
            UpdateBattleLog($"적은 스승을 공격했다! : {enemyAttack} 데미지 !");
        }
        else if (target == 1)
        {
            boyHp -= enemyAttack;
            UpdateBattleLog($"적은 제자를 공격했다! : {enemyAttack} 데미지 !");
        }

    }

    //Heal Method

    public void TeacherHeal()
    {
        turnText.text = "스승 턴";
        playerHp += playerAttack;
        UpdateBattleLog($"스승은 치료를 선택했다! : {playerAttack} 체력 회복 ! ");

    }

    public void BoyHeal()
    {
        boyHp += boyAttack;
        UpdateBattleLog($"제자는 치료를 선택했다! : {boyAttack} 체력 회복 ! ");
    }

    // Attack Sequence Method

    private IEnumerator YoungAttackSequence()
    {
        TeacherAttack();
        yield return new WaitForSeconds(1f);
        turnText.text = "제자 턴";
        UpdateBattleLog($"제자 생각 중... ");
        yield return new WaitForSeconds(1f);
        int randomAction = Random.Range(0, 2);
        if (randomAction == 0)
        {
            BoyAttack();
        }
        else
        {
            BoyHeal();
        }
        yield return new WaitForSeconds(1f);
        if (enemyHp > 0)
        {
            turnText.text = "적 턴";
            UpdateBattleLog($"적 생각 중... ");
            yield return new WaitForSeconds(1f);
            EnemyAttack();
        }
        attackButton.interactable = true;
        healButton.interactable = true;
        isPlayerChecking = false;
        isboyAttacking = false;
        isboyHealing = false;
    }
    private IEnumerator MiddleAttackSequence()
    {
        TeacherAttack();
        yield return new WaitForSeconds(1f);
        turnText.text = "제자 턴";
        UpdateBattleLog($"제자 생각 중... ");
        attackButton.interactable = true;
        healButton.interactable = true;
        yield return new WaitUntil(() => attackButton.interactable == false || healButton.interactable == false);
        if (isboyAttacking = true)
        {
            BoyAttack();
        }
        else if (isboyHealing = true)
        {
            BoyHeal();

        }
        yield return new WaitForSeconds(1f);
        if (enemyHp > 0)
        {
            EnemyAttack();
        }


        attackButton.interactable = true;
        isPlayerChecking = false;
        isboyAttacking = false;
        isboyHealing = false;
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
        TeacherAttack();
        yield return new WaitForSeconds(1f);
        attackButton.interactable = true;
        healButton.interactable = true;
        isPlayerChecking = false;
        isboyAttacking = false;
        isboyHealing = false;
    }

    // Heal Sequence Method

    private IEnumerator YoungHealSequence()
    {
        TeacherHeal();
        yield return new WaitForSeconds(1f);
        turnText.text = "제자 턴";
        UpdateBattleLog($"제자 생각 중... ");
        yield return new WaitForSeconds(1f);
        int randomAction = Random.Range(0, 2);
        if (randomAction == 0)
        {
            BoyAttack();
        }
        else
        {
            BoyHeal();
        }
        yield return new WaitForSeconds(1f);
        if (enemyHp > 0)
        {
            turnText.text = "적 턴";
            UpdateBattleLog($"적 생각 중... ");
            yield return new WaitForSeconds(1f);
            EnemyAttack();
        }
        attackButton.interactable = true;
        healButton.interactable = true;
        isPlayerChecking = false;
        isboyAttacking = false;
        isboyHealing = false;
    }

    private IEnumerator MiddleHealSequence()
    {
        TeacherHeal();
        yield return new WaitForSeconds(1f);
        turnText.text = "제자 턴";
        UpdateBattleLog($"제자 생각 중... ");
        attackButton.interactable = true;
        healButton.interactable = true;
        yield return new WaitUntil(() => attackButton.interactable == false || healButton.interactable == false);
        if (isboyAttacking = true)
        {
            BoyAttack();
        }
        else if (isboyHealing = true)
        {
            BoyHeal();

        }
        yield return new WaitForSeconds(1f);
        if (enemyHp > 0)
        {
            turnText.text = "제자 턴";
            UpdateBattleLog($"제자 생각 중... ");
            yield return new WaitForSeconds(1f);
            EnemyAttack();
        }

        attackButton.interactable = true;
        healButton.interactable = true;
        isPlayerChecking = false;
        isboyAttacking = false;
        isboyHealing = false;
    }


    // Old 상태에서의 Heal 시퀀스
    private IEnumerator OldHealSequence()
    {
        BoyAttack(); // 제자가 먼저 공격
        yield return new WaitForSeconds(1f);
        if (enemyHp > 0)
        {
            EnemyAttack(); // 적이 공격
        }
        yield return new WaitForSeconds(1f);
        TeacherHeal(); // 스승이 힐을 선택
        yield return new WaitForSeconds(1f);
        attackButton.interactable = true;
        healButton.interactable = true;
        isPlayerChecking = false;
        isboyAttacking = false;
        isboyHealing = false;
    }


    // Old State First Routine
    private IEnumerator OldStateTeacherRoutine()
    {
        if (Random.Range(0, 2) == 0)
        {
            TeacherAttack();
        }
        else
        {
            TeacherHeal();
        }
        yield return new WaitForSeconds(1f);

        turnText.text = "제자 턴";
        UpdateBattleLog($"제자 생각 중... ");
    }
}
