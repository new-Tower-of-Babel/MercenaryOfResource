using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTab : MonoBehaviour
{
    public GameObject title;
    public Button[] tab;
    public GameObject[] content;

    void Start()
    {
        Initialize();
        TabClick(0);
    }

    public void Initialize()
    {
        for (int i = 0; i < tab.Length; i++)
        {
            int index = i;
            tab[i].onClick.AddListener(() => TabClick(index));
        }
    }

    public void TabClick(int index)
    {
        title.GetComponent<TextMeshProUGUI>().text = tab[index].name;

        for(int i = 0; i < content.Length; i++)
        {
            content[i].SetActive(i == index);
        }
    }
}
