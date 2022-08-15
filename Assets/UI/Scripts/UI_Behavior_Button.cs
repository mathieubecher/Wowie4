using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Behavior_Button : MonoBehaviour, IDeselectHandler, ISelectHandler
{

    public int ID_Behavior;
    
    public GameObject Arrow;
    public GameObject Background;
    public GameObject Selection_Border;
    public GameObject New_Tag;
    public GameObject Equip_Icon;
    public Image Icon;
    public TMPro.TMP_Text Text_Name;

    public Color Color_Unselected;

    public GameObject Behavior_Database;
    public GameObject Behavior_Child;
    public UI_Behavior_Manager Behavior_Manager;

    private bool IsSelected;

    private GameObject Menu_Manager;
    private GameObject Description;




    // Start is called before the first frame update
    void Start()
    {
        Menu_Manager = GameObject.Find("Viewport");
        Description = GameObject.Find("Description");
        Behavior_Database = GameObject.Find("SaveDiskette");
        Behavior_Child = Behavior_Database.transform.GetChild(ID_Behavior).gameObject;
        Behavior_Manager = Behavior_Child.GetComponent<UI_Behavior_Manager>();

        if (Behavior_Manager.Locked == true)
        {
            Icon.color = Color.black;
            Text_Name.text = "???";
            GetComponent<Button>().interactable = false;
            Equip_Icon.SetActive(false);
            New_Tag.SetActive(false);



        } else
        {
            if (IsSelected == true)
            {
                Icon.sprite = Behavior_Manager.Behavior_Icon_Selected;
            }
            else
            {
                Icon.sprite = Behavior_Manager.Behavior_Icon;
            }
            Text_Name.text = Behavior_Manager.Behavior_Name;
            if (Behavior_Manager.Is_Equiped == true && ID_Behavior != 0)
            {
                Equip_Icon.SetActive(true);


            }
            else
            {
                Equip_Icon.SetActive(false);
            }
        }

        if (ID_Behavior == 0)
        {
            New_Tag.SetActive(false);
        }
  
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update_Equip(bool IsEquiped)
    {
        Behavior_Database = GameObject.Find("SaveDiskette");
        Behavior_Child = Behavior_Database.transform.GetChild(ID_Behavior).gameObject;
        Behavior_Manager = Behavior_Child.GetComponent<UI_Behavior_Manager>();

        if (IsEquiped == true)
        {
            
            if (ID_Behavior != 0)
            {
                Equip_Icon.SetActive(true);
                Behavior_Manager.Is_Equiped = true;

            }
            


        } else
        {
            Equip_Icon.SetActive(false);
            Behavior_Manager.Is_Equiped = false;
        }
        New_Tag.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        IsSelected = true;
        Arrow.SetActive(true);
        Background.SetActive(true);
        Selection_Border.SetActive(true);
        Text_Name.color = Color.white;
        Icon.sprite = Behavior_Manager.Behavior_Icon_Selected;
        if (ID_Behavior == 0)
        {
            Description.GetComponent<UI_Menu_Description_Manager>().Fill_Empty_Behavior_Desc();

        } else
        {
            Description.GetComponent<UI_Menu_Description_Manager>().Fill_Classic_Desc(Behavior_Manager.Effect_Text, Behavior_Manager.Condition_Text);
        }
 

    }


    public void OnDeselect(BaseEventData eventData)
    {
        IsSelected = false;
        Arrow.SetActive(false);
        Background.SetActive(false);
        Selection_Border.SetActive(false);
        Text_Name.color = Color_Unselected;
        Icon.sprite = Behavior_Manager.Behavior_Icon;


    }

    public void Equip()
    {
        Menu_Manager.GetComponent<UI_Menu_Custo_Manager>().Equip_to_Selected_Slot(ID_Behavior);
    }
}
