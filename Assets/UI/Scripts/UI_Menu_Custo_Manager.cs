using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Menu_Custo_Manager : MonoBehaviour
{
    public GameObject Slot_List;
    public GameObject Behavior_List;
    public GameObject Button_Behavior_Prefab;

    private GameObject Behavior_Database;
    private GameObject Behavior_Button;
    private GameObject Slot_Button;
    private int Behavior_Number;
    public int Selected_Slot;


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

    public void Focus_Side_List(int ID_to_Focus)
    {
        if (ID_to_Focus == 0)
        {
            Behavior_Button = Behavior_List.transform.GetChild(0).gameObject;
        } else
        {
            Behavior_Button = Behavior_List.transform.GetChild(ID_to_Focus - 1).gameObject;
        }
        
        EventSystem.current.SetSelectedGameObject(Behavior_Button);
    }

    public void Select_New_Slot(int New_ID_to_Focus)
    {
        if (Selected_Slot == New_ID_to_Focus)
        {

        } else
        {
            if (Selected_Slot != 0)
            {
                Slot_Button = Slot_List.transform.GetChild(Selected_Slot - 1).gameObject;
                Slot_Button.GetComponent<UI_Button_Slot>().Force_Deselect();
            }
            
            Selected_Slot = New_ID_to_Focus;
            
            
            Debug.Log("on change");
        }
    }

    public void Equip_to_Selected_Slot(int ID_Behavior)
    {
        Slot_Button = Slot_List.transform.GetChild(Selected_Slot - 1).gameObject;
        if (ID_Behavior != 0)
        {
            Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = false;
        } else
        {
            Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = true;
        }
        Slot_Button.GetComponent<UI_Button_Slot>().Update_Behavior(ID_Behavior + 1);
        
    }

}
