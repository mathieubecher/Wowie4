using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Menu_Description_Manager : MonoBehaviour
{
    public GameObject Stack_Horizontal;
    public GameObject Alternative_Text;
    public TMPro.TMP_Text Text_Effect;
    public TMPro.TMP_Text Text_Condition;
    public TMPro.TMP_Text Text_Empty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill_Classic_Desc(string effect, string condition)
    {
        Stack_Horizontal.SetActive(true);
        Alternative_Text.SetActive(false);
        Text_Effect.text = effect;
        Text_Condition.text = condition;
    }

    public void Fill_Empty_Slot_Desc()
    {
        Stack_Horizontal.SetActive(false);
        Alternative_Text.SetActive(true);
        Text_Empty.text = "Press Enter or click on a Behavior to place it on this Slot.";
    }

    public void Fill_Empty_Behavior_Desc()
    {
        Stack_Horizontal.SetActive(false);
        Alternative_Text.SetActive(true);
        Text_Empty.text = "Press Enter or click on a Behavior to place it on this Slot.";
    }
}
