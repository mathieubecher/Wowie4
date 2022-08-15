using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu_Custo_Manager : MonoBehaviour
{
    public GameObject Slot_List;
    public GameObject Behavior_List;
    public GameObject Button_Behavior_Prefab;

    private GameObject Behavior_Database;
    private GameObject Behavior_Button;
    private int Behavior_Number;


    // Start is called before the first frame update
    void Start()
    {
        Behavior_Database = GameObject.Find("Behavior_Database");
        Behavior_Number = Behavior_Database.transform.childCount;

        for (int i = 1; i < Behavior_Number; i++)
        {
            Instantiate(Button_Behavior_Prefab, Behavior_List.transform);
            Behavior_Button = Behavior_List.transform.GetChild(i).gameObject;
            Behavior_Button.GetComponent<UI_Behavior_Button>().ID_Behavior = i;



        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
