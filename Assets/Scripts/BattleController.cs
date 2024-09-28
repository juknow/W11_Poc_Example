using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    public int playerHp;
    public int boyHp;
    public int enemyHp;

    public int playerAttack;
    public int boyAttack;
    public int enemyAttack;
    // Start is called before the first frame update
    void Start()
    {
        playerHp = GameManager.Instance.PlayerHp;
        boyHp = GameManager.Instance.BoyHp;
        enemyHp = GameManager.Instance.EnemyHp;

        playerAttack = GameManager.Instance.PlayerAttack;
        boyAttack = GameManager.Instance.BoyAttack;
        enemyAttack = GameManager.Instance.EnemyAttack;
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

    public void AttackButton()
    {
        enemyHp -= playerAttack;
    }

}
