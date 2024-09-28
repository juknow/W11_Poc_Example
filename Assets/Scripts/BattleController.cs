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

    [Header("Button필드")]
    [SerializeField] private Button attackButton;


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
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHp <= 0)
        {
            GameManager.Instance.BoyBonusStat += 3;
            GameManager.Instance.PlayerHp -= 1;
            GameManager.Instance.PlayerAttack -= 1;
            SceneManager.LoadScene("Home");
        }

    }

    public void UpdateBattleLog(string message)
    {
        logText.text += message + "\n";
    }

    public void AttackButton()
    {
        // disable button
        attackButton.interactable = false;
        PlayerAttack();
        BoyAttack();

        if (enemyHp > 0)
        {
            EnemyAttack();
        }
    }

    public void PlayerAttack()
    {
        enemyHp -= playerAttack;
        UpdateBattleLog($"Player Attack! : {playerAttack} damage");

    }
    public void BoyAttack()
    {
        enemyHp -= boyAttack;
        UpdateBattleLog($"Boy Attack! : {boyAttack} damage");
    }
    public void EnemyAttack()
    {
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
}
