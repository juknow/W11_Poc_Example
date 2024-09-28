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
        playerHp.text = "My Hp : " + battleController.playerHp;
        boyHp.text = "boy Hp : " + battleController.boyHp;
        enemyHp.text = "Enemy Hp : " + battleController.enemyHp;
        playerAttack.text = "My Attack : " + battleController.playerAttack;
        boyAttack.text = "boy Attack : " + battleController.boyAttack;
        enemyAttack.text = "Enemy Attack : " + battleController.enemyAttack;

    }
}
