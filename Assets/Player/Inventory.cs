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
    public GameObject filter;
    public static GameObject inv;
    public static bool invOn;
    public Sprite m_OnTexture;
    public Sprite m_OffTexture;
    Image m_Image;
    GameObject player;
    bool gogglesSelected = false;
    bool filterSelected = false;
    bool gasMaskSelected = false;
    bool batterySelected = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inv = GameObject.Find("Inventory");
        m_Image = GameObject.Find("Canvas/Inventory/NVIcon").GetComponent<Image>();
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
                filter.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasGoggles)
            {
                goggles.transform.localScale = new Vector3(1f, 1f, 1f);
                if (Noktowizja.canBe)
                {
                    m_Image.sprite = m_OnTexture;
                }
                else
                {
                    m_Image.sprite = m_OffTexture;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !Pause.inWork && invOn)
        {
            inv.transform.localScale = new Vector3(0f, 0f, 0f);
            goggles.gameObject.transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
            gasMask.gameObject.transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
            filter.gameObject.transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
            battery.gameObject.transform.GetChild(0).transform.localScale = new Vector3(0f, 0f, 0f);
            gogglesSelected = false;
            filterSelected = false;
            gasMaskSelected = false;
            batterySelected = false;
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
        if (results[0].gameObject.name == "FilterIcon")
        {
            if(!filterSelected)
            {
                filterSelected = true;
                filter.gameObject.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                filterSelected = false;
                filter.gameObject.transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
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
        if (batterySelected && gogglesSelected && PlayerInventory.hasBatteries && Noktowizja.m_Battery != 100)
        {
            PlayerInventory.batteryCount -= 1;
            PlayerInventory.BatteryCount();
            Noktowizja.m_Battery = 100;
        }
        if (gasMaskSelected && filterSelected && PlayerInventory.hasFilter)
        {
            PlayerInventory.filterCount -= 1;
            PlayerInventory.FilterCount();
            GasMask.filterTime = 100;
        }
    }
}
