using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class SaveDiskette : MonoBehaviour
{
    [SerializeField] private List<GunBehavior> m_equippedGunBehaviors;

    private Robot m_robot;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {}

    void OnEnable()
    {
        SceneManager.sceneLoaded += InitRobotEquippedGunBehavior;
        GameManager.OnWin += Win;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= InitRobotEquippedGunBehavior;
        GameManager.OnWin -= Win;
    }

    public void InitRobotEquippedGunBehavior(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("InitRobotEquippedGunBehavior");
        m_robot = FindObjectOfType<Robot>();
        if(m_robot)
        {
            m_robot.SetGunBehaviors(m_equippedGunBehaviors);
        }
    }

    public void SetEquippedGunBehaviors(List<GunBehavior> _equippedGunBehaviors)
    {
        m_equippedGunBehaviors = _equippedGunBehaviors;
    }

    public void UnlockGunBehaviors(List<GunBehavior> _gunBehaviors)
    {
        int childCount = transform.childCount;

        for (int i = 1; i < childCount; i++)
        {
            GameObject Behavior_Button = transform.GetChild(i).gameObject;
            UI_Behavior_Manager behaviorManager = Behavior_Button.GetComponent<UI_Behavior_Manager>();
            
            if(_gunBehaviors.Contains(behaviorManager.Behavior_Object))
            {
                behaviorManager.Locked = false;
            }
        }
    }

    private void Win(GameManager _manager)
    {
        UnlockGunBehaviors(_manager.gunBehaviorsUnlock);
    }

}