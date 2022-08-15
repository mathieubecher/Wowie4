using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Menu_Custo_Manager : MonoBehaviour
{
    public GameObject Slot_List;
    public GameObject Behavior_List;
    public GameObject Button_Behavior_Prefab;

    private GameObject Behavior_Database;
    private GameObject Behavior_Child;
    private UI_Behavior_Manager Behavior_Manager;
    private GameObject Behavior_Button;
    private GameObject Slot_Button;
    private int Behavior_Number;
    public int Selected_Slot;
    private int Compare_ID;
    private int Slot_Behavior_ID;
    


    // Start is called before the first frame update
    void Start()
    {

        Behavior_Database = GameObject.Find("SaveDiskette");
        Behavior_Number = Behavior_Database.transform.childCount;

        for (int i = 1; i < Behavior_Number; i++)
        {
            
            Instantiate(Button_Behavior_Prefab, Behavior_List.transform);
            Behavior_Button = Behavior_List.transform.GetChild(i).gameObject;
            Behavior_Button.GetComponent<UI_Behavior_Button>().ID_Behavior = i;
            Behavior_Child = Behavior_Database.transform.GetChild(i).gameObject;
            Behavior_Manager = Behavior_Child.GetComponent<UI_Behavior_Manager>();
            if (Behavior_Manager.Is_Equiped == true)
            {
                Debug.Log("Behavior" + i + "isEquiped to " + Behavior_Manager.Priority_Order);
                Slot_Button = Slot_List.transform.GetChild(Behavior_Manager.Priority_Order - 1).gameObject;
                Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = false;
                Slot_Button.GetComponent<UI_Button_Slot>().Update_Behavior(i + 1);
                Behavior_Button.GetComponent<UI_Behavior_Button>().Update_Equip(true);
            }



        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit_Menu()

    {

        List<GunBehavior> List_Behavior = new List<GunBehavior>();
        for (int i = 0; i < 4; i++)
        {
            Slot_Button = Slot_List.transform.GetChild(i).gameObject;
            Slot_Behavior_ID = Slot_Button.GetComponent<UI_Button_Slot>().ID_Behavior;
            if (Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty == false)
            {
                
                Behavior_Child = Behavior_Database.transform.GetChild(Slot_Behavior_ID - 1).gameObject;
                Behavior_Manager = Behavior_Child.GetComponent<UI_Behavior_Manager>();
                Debug.Log("Adding " + Behavior_Manager.Behavior_Object);
                List_Behavior.Add(Behavior_Manager.Behavior_Object);
            }
            

        }

        Behavior_Database.GetComponent<SaveDiskette>().SetEquippedGunBehaviors(List_Behavior);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
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

    public void Set_Equiped(int i)
    {
        if (i != 0)
        {
            Behavior_Button = Behavior_List.transform.GetChild(i - 1).gameObject;
            Behavior_Button.GetComponent<UI_Behavior_Button>().Update_Equip(true);
        }
        


    }

    public void Set_Unequiped(int i)
    {
        Debug.Log("Unequip" + i);
        Behavior_Button = Behavior_List.transform.GetChild(i - 1).gameObject;
        Behavior_Button.GetComponent<UI_Behavior_Button>().Update_Equip(false);


    }

    public void Clean_Duplicates(int BehaviorID, int SlotID)

    {

        if (SlotID != 1 )
        {
            Slot_Button = Slot_List.transform.GetChild(0).gameObject;
            Compare_ID = Slot_Button.GetComponent<UI_Button_Slot>().ID_Behavior;
            if (Compare_ID == BehaviorID && Compare_ID != 0 && BehaviorID != 0)
            {
                Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = true;
                Slot_Button.GetComponent<UI_Button_Slot>().Update_Behavior(0);
                Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = true;
                Set_Equiped(BehaviorID);

            }
        }

        if (SlotID != 2)
        {
            Slot_Button = Slot_List.transform.GetChild(1).gameObject;
            Compare_ID = Slot_Button.GetComponent<UI_Button_Slot>().ID_Behavior;
            if (Compare_ID == BehaviorID && Compare_ID != 0 && BehaviorID != 0)
            {
                Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = true;
                Slot_Button.GetComponent<UI_Button_Slot>().Update_Behavior(0);
                Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = true;
                Set_Equiped(BehaviorID);

            }
        }

        if (SlotID != 3)
        {
            Slot_Button = Slot_List.transform.GetChild(2).gameObject;
            Compare_ID = Slot_Button.GetComponent<UI_Button_Slot>().ID_Behavior;
            if (Compare_ID == BehaviorID && Compare_ID != 0 && BehaviorID != 0)
            {
                Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = true;
                Slot_Button.GetComponent<UI_Button_Slot>().Update_Behavior(0);
                Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = true;
                Set_Equiped(BehaviorID);

            }
        }

        if (SlotID != 4)
        {
            Slot_Button = Slot_List.transform.GetChild(3).gameObject;
            Compare_ID = Slot_Button.GetComponent<UI_Button_Slot>().ID_Behavior;
            if (Compare_ID == BehaviorID && Compare_ID != 0 && BehaviorID != 0)
            {
                Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = true;
                Slot_Button.GetComponent<UI_Button_Slot>().Update_Behavior(0);
                Slot_Button.GetComponent<UI_Button_Slot>().IsEmpty = true;
                Set_Equiped(BehaviorID);

            }
        }


    }

}
