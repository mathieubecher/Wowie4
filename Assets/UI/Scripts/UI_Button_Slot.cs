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


    


    
    // Start is called before the first frame update
    void Start()
    {
        Update_Behavior(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update_Behavior(int New_ID)
    {
        Text_Name.text = "Prio" + ID_Slot + ": " + "Empty";
    }

    public void OnSelect(BaseEventData eventData)
    {
        Arrow.SetActive(true);
        Background.SetActive(true);
        Selection_Border.SetActive(true);
        Text_Name.color = Color.white;
        if (IsEmpty == true)
        {
            Icon.sprite = Empty_Selected;
        }
        Number.sprite = Number_Selected;
        




    }

    public void OnDeselect(BaseEventData eventData)
    {
        Arrow.SetActive(false);
        Background.SetActive(false);
        Selection_Border.SetActive(false);
        Text_Name.color = Color_Unselected;
        if (IsEmpty == true)
        {
            Icon.sprite = Empty_Unselect;
            
        }
        Number.sprite = Number_Unselect;

    }
}
