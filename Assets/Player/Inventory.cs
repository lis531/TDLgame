using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class Inventory : MonoBehaviour
{
    [SerializeField]  GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;
    public GameObject goggles;
    public GameObject keycard1;
    public GameObject keycard2;
    public GameObject keycard3;
    public GameObject medkit;
    public GameObject bezpiecznik;
    public GameObject gasMask;
    public GameObject battery;
    public static GameObject inv;
    public static bool invOn;
    Texture m_OnTexture;
    Texture m_OffTexture;
    public RawImage m_Image;
    GameObject player;
    bool gogglesSelected = false;
    bool gasMaskFilterSelected = false;
    bool gasMaskSelected = false;
    bool batterySelected = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inv = GameObject.Find("Inventory");
        m_OnTexture = Resources.Load<Texture>("Assets/Items/NightVisionGoggles/Resources/NVIconOn");
        m_OffTexture = Resources.Load<Texture>("Assets/Items/NightVisionGoggles/Resources/NVIconOff");
        m_Image = GetComponent<RawImage>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && invOn)
        {
            CombineRay();
            Combine();
        }
        if(Input.GetKeyDown(KeyCode.Tab) && !Pause.inWork && !invOn)
        {
            inv.transform.localScale = new Vector3(1f, 1f, 1f);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            invOn = true;
            if (Noktowizja.canBe)
            {
                player.GetComponent<Noktowizja>().SwitchState(false);
            }
            if(PlayerInventory.hasBatteries)
                battery.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasGasMask)
                gasMask.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasBezpiecznik)
                bezpiecznik.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasMedkit)
                medkit.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasKeycard1)
                keycard1.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasKeycard2)
                keycard2.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasKeycard3)
                keycard3.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasFilter)
                goggles.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasGoggles)
            {
                goggles.transform.localScale = new Vector3(1f, 1f, 1f);
                m_Image.texture = Noktowizja.m_TurnedOn ? m_OnTexture : m_OffTexture;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !Pause.inWork && invOn)
        {
            inv.transform.localScale = new Vector3(0f, 0f, 0f);
            Time.timeScale = 1;
            invOn = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Noktowizja.canBe)
            {
                player.GetComponent<Noktowizja>().SwitchState(true);
            }
        }
    }
    void CombineRay()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        if(results.Count > 0) Debug.Log("Hit " + results[0].gameObject.name);
        if (results[0].gameObject.name == "NVIcon")
        {
            if (!gogglesSelected)
            {
                gogglesSelected = true;
                goggles.gameObject.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                gogglesSelected = false;
                goggles.gameObject.transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
            }
        }
        if (results[0].gameObject.name == "GasMaskIcon")
        {
            if (!gasMaskSelected)
            {
                gasMaskSelected = true;
                gasMask.gameObject.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                gasMaskSelected = false;
                gasMask.gameObject.transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
            }
        }
        if (results[0].gameObject.name == "GasMaskFilterIcon")
        {
            if(!gasMaskFilterSelected)
            {
                gasMaskFilterSelected = true;
                gasMask.gameObject.transform.GetChild(1).localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                gasMaskFilterSelected = false;
                gasMask.gameObject.transform.GetChild(1).localScale = new Vector3(0f, 0f, 0f);
            }
        }
        if (results[0].gameObject.name == "BatteryIcon")
        {
            if (!batterySelected)
            {
                batterySelected = true;
                battery.gameObject.transform.GetChild(0).transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                batterySelected = false;
                battery.gameObject.transform.GetChild(0).transform.localScale = new Vector3(0f, 0f, 0f);
            }
        }
    }
    void Combine()
    {
        if (batterySelected && gogglesSelected)
        {
            if(PlayerInventory.hasBatteries && Noktowizja.m_Battery != 100)
            {
                PlayerInventory.batteryCount -= 1;
                Noktowizja.m_Battery = 100;
            }
        }
    }
}
