using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button_Slot : MonoBehaviour, IDeselectHandler, ISelectHandler
{
    public bool IsEmpty;
    public int ID_Behavior;
    public int ID_Slot;

    public GameObject Arrow;
    public GameObject Background;
    public GameObject Selection_Border;
    public Image Icon;
    public Image Number;
    public TMPro.TMP_Text Text_Name;

    public Sprite Number_Unselect;
    public Sprite Number_Selected;
    public Sprite Empty_Unselect;
    public Sprite Empty_Selected;

    public Color Color_Unselected;

    private GameObject Behavior_Database;
    private GameObject Behavior_Child;
    private UI_Behavior_Manager Behavior_Manager;

    private GameObject Menu_Manager;

    private bool IsSelected;



    


    
    // Start is called before the first frame update
    void Start()
    {
        Menu_Manager = GameObject.Find("Viewport");
        Behavior_Database = GameObject.Find("SaveDiskette");
        Behavior_Child = Behavior_Database.transform.GetChild(ID_Behavior + 1).gameObject;
        Behavior_Manager = Behavior_Child.GetComponent<UI_Behavior_Manager>();
        Update_Behavior(ID_Behavior);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Update_Behavior(int New_ID)
    {
        ID_Behavior = New_ID;

        if (ID_Behavior == 0)
        {
            IsEmpty = true;
        } else
        {
            Behavior_Child = Behavior_Database.transform.GetChild(ID_Behavior - 1).gameObject;
            Behavior_Manager = Behavior_Child.GetComponent<UI_Behavior_Manager>();
        }
        

        if (IsEmpty == true)
        {
            Text_Name.text = "Prio" + ID_Slot + ": " + "Empty";

            if (IsSelected == true)
            {
                Icon.sprite = Empty_Selected;
            }
            else
            {
                Icon.sprite = Empty_Unselect;
            }
        }
        else
        {
            Text_Name.text = "Prio" + ID_Slot + ": " + Behavior_Manager.Behavior_Name;

            if (IsSelected == true)
            {
                Icon.sprite = Behavior_Manager.Behavior_Icon_Selected;
            }
            else
            {
                Icon.sprite = Behavior_Manager.Behavior_Icon;
            }
        }

        


        
    }

    public void OnSelect(BaseEventData eventData)
    {
        IsSelected = true;
        Arrow.SetActive(true);
        Background.SetActive(true);
        Selection_Border.SetActive(true);
        Text_Name.color = Color.white;
        if (IsEmpty == true)
        {
            Icon.sprite = Empty_Selected;
        }
        else
        {
            Icon.sprite = Behavior_Manager.Behavior_Icon_Selected;
        }
        Number.sprite = Number_Selected;
        Menu_Manager.GetComponent<UI_Menu_Custo_Manager>().Select_New_Slot(ID_Slot);





    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (Menu_Manager.GetComponent<UI_Menu_Custo_Manager>().Selected_Slot == ID_Slot)
        {

        } else
        {
            IsSelected = false;
            Arrow.SetActive(false);
            Background.SetActive(false);
            Selection_Border.SetActive(false);
            Text_Name.color = Color_Unselected;
            if (IsEmpty == true)
            {
                Icon.sprite = Empty_Unselect;

            }
            else
            {
                Icon.sprite = Behavior_Manager.Behavior_Icon;
            }
            Number.sprite = Number_Unselect;
        }
        

    }

    public void Click_on_Button()
    {
        Menu_Manager.GetComponent<UI_Menu_Custo_Manager>().Focus_Side_List(ID_Behavior);
    }

    public void Force_Deselect()
    {
        IsSelected = false;
        Arrow.SetActive(false);
        Background.SetActive(false);
        Selection_Border.SetActive(false);
        Text_Name.color = Color_Unselected;
        if (IsEmpty == true)
        {
            Icon.sprite = Empty_Unselect;

        }
        else
        {
            Icon.sprite = Behavior_Manager.Behavior_Icon;
        }
        Number.sprite = Number_Unselect;
    }
}
