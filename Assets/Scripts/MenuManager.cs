using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI StarCount;
    private int starCount;
    void Start()
    {
        starCount = PlayerPrefs.GetInt("StarCount");
        StarCount.text = starCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
