using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTextColor : MonoBehaviour
{
    TMP_Text text;
    [SerializeField] Color color = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.color = color;
    }

}
