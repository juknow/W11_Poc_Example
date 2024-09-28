using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextController : MonoBehaviour
{
    [Header("HP필드")]
    [SerializeField] private TextMeshProUGUI playerHp;
    [SerializeField] private TextMeshProUGUI boyHp;

    [Header("Attack필드")]
    [SerializeField] private TextMeshProUGUI playerAttack;
    [SerializeField] private TextMeshProUGUI boyAttack;

    [Header("Bonus필드")]
    [SerializeField] private TextMeshProUGUI bonusStat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerHp.text = "My Hp : " + GameManager.Instance.PlayerHp;
        boyHp.text = "boy Hp : " + GameManager.Instance.BoyHp;
        playerAttack.text = "My Attack : " + GameManager.Instance.PlayerAttack;
        boyAttack.text = "boy Attack : " + GameManager.Instance.BoyAttack;
        bonusStat.text = "bonus Stats : " + GameManager.Instance.BoyBonusStat;
    }
}
