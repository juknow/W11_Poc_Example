using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("Job필드")]
    [SerializeField] private Canvas firstTeacherJobCanvas;
    [SerializeField] private TMP_Dropdown firstTeacherJobDropDown;
    [SerializeField] private TMP_Dropdown firstBoyJobDropDown;
    [SerializeField] private Button firstJobButton;

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

    public void FirstSetJob()
    {
        GameManager.Instance.TeacherJob = firstTeacherJobDropDown.value;
        GameManager.Instance.BoyJob = firstBoyJobDropDown.value;
        firstTeacherJobCanvas.gameObject.SetActive(false);
    }
}
