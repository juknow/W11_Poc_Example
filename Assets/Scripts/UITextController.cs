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

    [Header("Job필드")]
    [SerializeField] private TextMeshProUGUI playerJob;
    [SerializeField] private TextMeshProUGUI boyJob;

    [Header("Bonus필드")]
    [SerializeField] private TextMeshProUGUI bonusStat;

    [Header("JobSelection필드")]
    [SerializeField] private Canvas firstTeacherJobCanvas;
    [SerializeField] private TMP_Dropdown firstTeacherJobDropDown;
    [SerializeField] private TMP_Dropdown firstBoyJobDropDown;
    [SerializeField] private Button firstJobButton;

    [Header("BattleOver필드")]
    [SerializeField] private Canvas boyJobCanvas;
    [SerializeField] private TMP_Dropdown boyJobDropDown;
    [SerializeField] private Button boyJobButton;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.GameSet == true)
        {
            firstTeacherJobCanvas.gameObject.SetActive(false);
        }
        if (GameManager.Instance.BattleFinish == true)
        {
            boyJobCanvas.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.BattleFinish == false)
        {
            boyJobCanvas.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHp.text = "내 체력 : " + GameManager.Instance.PlayerHp;
        boyHp.text = "제자 체력 : " + GameManager.Instance.BoyHp;
        playerAttack.text = "내 공격력 : " + GameManager.Instance.PlayerAttack;
        boyAttack.text = "제자 공격력 : " + GameManager.Instance.BoyAttack;
        playerJob.text = "스승 직업 : " + GetJobName(GameManager.Instance.TeacherJob);
        boyJob.text = "제자 직업 : " + GetJobName(GameManager.Instance.BoyJob);
    }

    public void FirstSetJob()
    {
        GameManager.Instance.TeacherJob = firstTeacherJobDropDown.value;
        GameManager.Instance.BoyJob = firstBoyJobDropDown.value;
        GameManager.Instance.GameSet = true;
        firstTeacherJobCanvas.gameObject.SetActive(false);
    }

    private string GetJobName(int jobValue)
    {
        switch (jobValue)
        {
            case 0:
                return "전사";
            case 1:
                return "힐러";
            case 2:
                return "마법사";
            default:
                return "알 수 없음";
        }
    }

    public void SetJob()
    {
        GameManager.Instance.TeacherJob = GameManager.Instance.BoyJob;
        GameManager.Instance.BoyJob = boyJobDropDown.value;
        GameManager.Instance.BattleFinish = false;
        boyJobCanvas.gameObject.SetActive(false);
    }

}
