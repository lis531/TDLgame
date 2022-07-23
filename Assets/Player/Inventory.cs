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
    public GameObject keycard;
    public GameObject medkit;
    //public GameObject bezpiecznik;
    public GameObject gasMask;
    public GameObject batteries;
    public static GameObject inv;
    public static bool invOn;
    Texture m_OnTexture;
    Texture m_OffTexture;
    //public RawImage m_Image;
    GameObject player;
    bool gogglesSelected = false;
    bool keycardSelected = false;
    bool medkitSelected = false;
    //bool bezpiecznikSelected = false;
    bool gasMaskSelected = false;
    bool batteriesSelected = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inv = GameObject.Find("Inventory");
        //m_OnTexture = Resources.Load<Texture>("Assets/Items/NightVisionGoggles/Resources/NVIconOn");
        //m_OffTexture = Resources.Load<Texture>("Assets/Items/NightVisionGoggles/Resources/NVIconOff");
        //m_Image = GetComponent<RawImage>();
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
            //if(PlayerInventory.hasBatteries)
            //    batteries.transform.localScale = new Vector3(1f, 1f, 1f);
            //if(PlayerInventory.hasGasMask)
            //    gasMask.transform.localScale = new Vector3(1f, 1f, 1f);
            //if(PlayerInventory.hasBezpiecznik)
            //    bezpiecznik.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasMedkit)
                medkit.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasKeycard)
                keycard.transform.localScale = new Vector3(1f, 1f, 1f);
            if(PlayerInventory.hasGoggles)
            {
                goggles.transform.localScale = new Vector3(1f, 1f, 1f);
                //m_Image.texture = Noktowizja.m_TurnedOn ? m_OnTexture : m_OffTexture;
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
//selecting items
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
        if (results[0].gameObject.name == "KeycardIcon")
        {
            if (!keycardSelected)
            {
                keycardSelected = true;
                keycard.gameObject.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                keycardSelected = false;
                keycard.gameObject.transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
            }
        }
        if (results[0].gameObject.name == "MedkitIcon")
        {
            if (!medkitSelected)
            {
                medkitSelected = true;
                medkit.gameObject.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                medkitSelected = false;
                medkit.gameObject.transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
            }
        }
        /*if (results[0].gameObject.name == "Bezpiecznik")
        {
            if (!bezpiecznikSelected)
            {
                bezpiecznikSelected = true;
                bezpiecznik.gameObject.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                bezpiecznikSelected = false;
                bezpiecznik.gameObject.transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
            }
        }*/
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
        if (results[0].gameObject.name == "BatteriesIcon")
        {
            if (!batteriesSelected)
            {
                batteriesSelected = true;
                batteries.gameObject.transform.GetChild(0).transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                batteriesSelected = false;
                batteries.gameObject.transform.GetChild(0).transform.localScale = new Vector3(0f, 0f, 0f);
            }
        }
    }
    void Combine()
    {
        if (batteriesSelected && gogglesSelected)
        {
            if(PlayerInventory.hasBatteries && Noktowizja.m_Battery != 100)
            {
                PlayerInventory.batteryCount -= 1;
                Noktowizja.m_Battery = 100;
            }
        }
    }
}
