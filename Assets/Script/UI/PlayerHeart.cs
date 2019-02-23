using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : MonoBehaviour
{

    private int maxHP;
    private int currentHP;

    private int quotient; // HP를 2로 나눈 몫.
    private int remainder; // HP를 2로 나눈 나머지.

    private int countHeart; // 활성화된 체력 수.

    private Transform transform;

    private PlayerController playerController;

    [SerializeField]
    private Sprite heart;
    [SerializeField]
    private Sprite half_Heart;
    [SerializeField]
    private Sprite empty_Heart;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        transform = GetComponent<Transform>();

        HeartReset();
    }

    public void HeartReset()
    {
        currentHP = playerController.currentHp;
        maxHP = playerController.maxHp;

        quotient = currentHP / 2;
        remainder = currentHP % 2;

        for (int i = 0; i < quotient; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).gameObject.GetComponent<Image>().sprite = heart;
            countHeart = i;
        }

        if (remainder == 1)
        {
            countHeart += 1;
            transform.GetChild(countHeart).gameObject.SetActive(true);
            transform.GetChild(countHeart).gameObject.GetComponent<Image>().sprite = half_Heart;
            countHeart += 1;
        }
        else
        {
            countHeart += 1;
        }

        for (int i = countHeart; i < maxHP / 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).gameObject.GetComponent<Image>().sprite = empty_Heart;
        }
    }
}
