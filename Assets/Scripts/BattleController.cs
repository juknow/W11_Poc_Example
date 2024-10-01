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

    [Header("Job필드")]
    public int teacherJob;
    public int boyJob;

    [Header("log필드")]
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private TextMeshProUGUI turnText;

    [Header("Button필드")]
    [SerializeField] private Button firstButton;
    [SerializeField] private Button secondButton;


    // field end

    public enum TeacherState
    {
        Fighter,
        Healer,
        Magician
    }

    public enum BoyState
    {
        Fighter,
        Healer,
        Magician
    }

    private TeacherState teacherState;
    private BoyState boyState;
    void Start()
    {
        playerHp = GameManager.Instance.PlayerHp;
        boyHp = GameManager.Instance.BoyHp;
        enemyHp = GameManager.Instance.EnemyHp;

        playerAttack = GameManager.Instance.PlayerAttack;
        boyAttack = GameManager.Instance.BoyAttack;
        enemyAttack = GameManager.Instance.EnemyAttack;

        teacherJob = GameManager.Instance.TeacherJob;
        boyJob = GameManager.Instance.BoyJob;

        UpdateStatus();
        UpdateButton();


        turnText.text = "나의 턴";
        logText.text = "나 생각 중..." + "\n";
    }

    void Update()
    {
        if (enemyHp <= 0)
        {
            GameManager.Instance.BattleFinish = true;
            SceneManager.LoadScene("Home");
        }
    }

    public void UpdateStatus()
    {

        // state determination
        if (GameManager.Instance.TeacherJob == 0)
        {
            teacherState = TeacherState.Fighter;
        }

        else if (GameManager.Instance.TeacherJob == 1)
        {
            teacherState = TeacherState.Healer;
        }
        else if (GameManager.Instance.TeacherJob == 2)
        {
            teacherState = TeacherState.Magician;
        }

        if (GameManager.Instance.BoyJob == 0)
        {
            boyState = BoyState.Fighter;
        }

        else if (GameManager.Instance.BoyJob == 1)
        {
            boyState = BoyState.Healer;
        }
        else if (GameManager.Instance.BoyJob == 2)
        {
            boyState = BoyState.Magician;
        }
    }

    public void UpdateButton()
    {
        TextMeshProUGUI firstButtonText = firstButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI secondButtonText = secondButton.GetComponentInChildren<TextMeshProUGUI>();

        switch (teacherState)
        {
            case TeacherState.Fighter:
                firstButtonText.text = "검 공격";
                secondButtonText.text = "엄호";
                break;

            case TeacherState.Healer:
                firstButtonText.text = "힐";
                secondButtonText.text = "약한 공격";
                break;

            case TeacherState.Magician:
                firstButtonText.text = "마력 발사";
                secondButtonText.text = "전역 마력공격";
                break;

        }
    }

    public void UpdateBattleLog(string message)
    {
        logText.text += message + "\n";
    }

    //Button Method

    public void FirstButton()
    {

        firstButton.interactable = false;
        secondButton.interactable = false;

        switch (teacherState)
        {
            case TeacherState.Fighter:
                FighterFirstAction();
                break;

            case TeacherState.Healer:
                HealerFirstAction();
                break;

            case TeacherState.Magician:
                MagicianFirstAction();
                break;

        }

        StartCoroutine(TurnEndSequence());
    }

    public void SecondButton()
    {
        firstButton.interactable = false;
        secondButton.interactable = false;

        switch (teacherState)
        {
            case TeacherState.Fighter:
                FighterSecondAction();
                break;

            case TeacherState.Healer:
                HealerSecondAction();
                break;

            case TeacherState.Magician:
                MagicianSecondAction();
                break;

        }
        StartCoroutine(TurnEndSequence());
    }


    //Action Method

    public void FighterFirstAction()
    {
        enemyHp -= playerAttack;
        UpdateBattleLog($"나는 검 공격을 선택했다! : {playerAttack} 데미지 !");

    }
    public void FighterSecondAction()
    {
        UpdateBattleLog($"나는 엄호를 선택했다! : 제자의 피해를 대신 받는다!");

    }
    public void HealerFirstAction()
    {
        int target = Random.Range(0, 2);

        if (target == 0)
        {
            boyHp += playerAttack;
            UpdateBattleLog($"나는 제자 힐을 선택했다! : {playerAttack} 회복 !");
        }
        else if (target == 1)
        {
            playerHp += playerAttack;
            UpdateBattleLog($"나는 나의 힐을 선택했다! : {playerAttack} 회복 !");
        }

    }
    public void HealerSecondAction()
    {
        enemyHp -= playerAttack / 2;
        UpdateBattleLog($"나는 약한 공격을 선택했다! : {playerAttack / 2} 데미지 !");

    }
    public void MagicianFirstAction()
    {
        enemyHp -= 2 * playerAttack;
        UpdateBattleLog($"나는 마력 발사를 선택했다! : {2 * playerAttack} 데미지 !");

    }
    public void MagicianSecondAction()
    {
        enemyHp -= playerAttack;
        UpdateBattleLog($"나는 전역 마력 공격을 선택했다! : {playerAttack} 데미지 !");

    }
    public void BoyAttack()
    {
        switch (boyState)
        {
            case BoyState.Fighter:
                int target1 = Random.Range(0, 2);

                if (target1 == 0)
                {
                    enemyHp -= boyAttack;
                    UpdateBattleLog($"제자는 검 공격을 선택했다! : {boyAttack} 데미지 !");
                }
                else if (target1 == 1)
                {
                    UpdateBattleLog($"제자는 엄호를 선택했다! : 나의 피해를 대신 받는다!");
                }
                break;

            case BoyState.Healer:
                int target2 = Random.Range(0, 2);

                if (target2 == 0)
                {
                    int target = Random.Range(0, 2);
                    if (target == 0)
                    {
                        boyHp += boyAttack;
                        UpdateBattleLog($"제자는 제자 힐을 선택했다! : {boyAttack} 회복 !");
                    }
                    else if (target == 1)
                    {
                        enemyHp -= boyAttack / 2;
                        UpdateBattleLog($"제자는 약한 공격을 선택했다! : {boyAttack / 2} 데미지 !");
                    }
                }
                else if (target2 == 1)
                {
                    boyHp += boyAttack / 2;
                    playerHp += boyAttack / 2;
                    UpdateBattleLog($"제자는 전체 힐을 선택했다! : {boyAttack / 2} 회복 !");
                }
                break;

            case BoyState.Magician:
                int target3 = Random.Range(0, 2);

                if (target3 == 0)
                {
                    enemyHp -= 2 * boyAttack;
                    UpdateBattleLog($"제자는 마력 발사를 선택했다! : {2 * boyAttack} 데미지 !");
                }
                else if (target3 == 1)
                {
                    enemyHp -= boyAttack;
                    UpdateBattleLog($"제자는 전역 마력 공격을 선택했다! :{boyAttack} 데미지 !");
                }
                break;

        }
    }
    public void EnemyAttack()
    {
        int target = Random.Range(0, 2);

        if (target == 0)
        {
            playerHp -= enemyAttack;
            UpdateBattleLog($"적은 나를 공격했다! : {enemyAttack} 데미지 !");
        }
        else if (target == 1)
        {
            boyHp -= enemyAttack;
            UpdateBattleLog($"적은 제자를 공격했다! : {enemyAttack} 데미지 !");
        }

    }

    //Sequence Method

    private IEnumerator TurnEndSequence()
    {
        yield return new WaitForSeconds(1.5f);
        turnText.text = "제자 턴";
        UpdateBattleLog($"제자 생각 중... ");
        yield return new WaitForSeconds(1.5f);
        BoyAttack();
        yield return new WaitForSeconds(1.5f);
        if (enemyHp > 0)
        {
            turnText.text = "적의 턴";
            UpdateBattleLog($"적 생각 중... ");
            yield return new WaitForSeconds(1.5f);
            EnemyAttack(); // 적이 공격
        }
        yield return new WaitForSeconds(1.5f);
        turnText.text = "나의 턴";
        UpdateBattleLog($"나 생각 중... ");
        firstButton.interactable = true;
        secondButton.interactable = true;
    }


    /*
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
    */
}
