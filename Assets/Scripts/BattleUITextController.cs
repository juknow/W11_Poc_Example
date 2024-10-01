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


    [Header("Scripts필드")]
    [SerializeField] private BattleController battleController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerHp.text = "내 체력 : " + battleController.playerHp;
        boyHp.text = "제자 체력 : " + battleController.boyHp;
        enemyHp.text = "적 체력 : " + battleController.enemyHp;
        playerAttack.text = "공격력 : " + battleController.playerAttack;
        boyAttack.text = "공격력 : " + battleController.boyAttack;
        enemyAttack.text = "공격력 : " + battleController.enemyAttack;

    }
}
