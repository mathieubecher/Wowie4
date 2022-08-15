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
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= InitRobotEquippedGunBehavior;
    }

    public void InitRobotEquippedGunBehavior(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Robot Init with equipped gun behavior");
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
}