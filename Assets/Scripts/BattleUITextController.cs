using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUITextController : MonoBehaviour
{
    [Header("HP필드")]
    [SerializeField] private TextMeshProUGUI playerHp;
    [SerializeField] private TextMeshProUGUI boyHp;
    [SerializeField] private TextMeshProUGUI enemyHp;

    [Header("Attack필드")]
    [SerializeField] private TextMeshProUGUI playerAttack;
    [SerializeField] private TextMeshProUGUI boyAttack;
    [SerializeField] private TextMeshProUGUI enemyAttack;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerHp.text = "My Hp : " + GameManager.Instance.PlayerHp;
        boyHp.text = "boy Hp : " + GameManager.Instance.BoyHp;
        enemyHp.text = "Enemy Hp : " + GameManager.Instance.EnemyHp;
        playerAttack.text = "My Attack : " + GameManager.Instance.PlayerAttack;
        boyAttack.text = "boy Attack : " + GameManager.Instance.BoyAttack;
        enemyAttack.text = "Enemy Attack : " + GameManager.Instance.EnemyAttack;

    }
}
