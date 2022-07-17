using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public GameObject googles;
    public GameObject keycard;
    public GameObject medkit;
    //public GameObject bezpiecznik;
    public GameObject gasMask;
    public GameObject batteries;
    public static GameObject inv;
    public static bool invOn;
    Texture m_OnTexture;
    Texture m_OffTexture;
    public RawImage m_Image;
    GameObject player;
    bool googlesSelected = false;
    bool keycardSelected = false;
    bool medkitSelected = false;
    //bool bezpiecznikSelected = false;
    bool gasMaskSelected = false;
    bool batteriesSelected = false;
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
                googles.transform.localScale = new Vector3(1f, 1f, 1f);
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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Goggles")
            {
                if (!googlesSelected)
                {
                    googles.transform.localScale = new Vector3(1f, 1f, 1f);
                    googlesSelected = true;
                }
                else
                {
                    googles.transform.localScale = new Vector3(0f, 0f, 0f);
                    googlesSelected = false;
                }
            }
            else if (hit.collider.tag == "Keycard")
            {
                if (!keycardSelected)
                {
                    keycard.transform.localScale = new Vector3(1f, 1f, 1f);
                    keycardSelected = true;
                }
                else
                {
                    keycard.transform.localScale = new Vector3(0f, 0f, 0f);
                    keycardSelected = false;
                }
            }
            else if (hit.collider.tag == "Medkit")
            {
                if (!medkitSelected)
                {
                    medkit.transform.localScale = new Vector3(1f, 1f, 1f);
                    medkitSelected = true;
                }
                else
                {
                    medkit.transform.localScale = new Vector3(0f, 0f, 0f);
                    medkitSelected = false;
                }
            }
            /*else if (hit.collider.tag == "Bezpiecznik")
            {
                if (!bezpiecznikSelected)
                {
                    bezpiecznik.transform.localScale = new Vector3(1f, 1f, 1f);
                    bezpiecznikSelected = true;
                }
                else
                {
                    bezpiecznik.transform.localScale = new Vector3(0f, 0f, 0f);
                    bezpiecznikSelected = false;
                }
            }*/
            else if (hit.collider.tag == "GasMask")
            {
                if (!gasMaskSelected)
                {
                    gasMask.transform.localScale = new Vector3(1f, 1f, 1f);
                    gasMaskSelected = true;
                }
                else
                {
                    gasMask.transform.localScale = new Vector3(0f, 0f, 0f);
                    gasMaskSelected = false;
                }
            }
            else if (hit.collider.tag == "Batteries")
            {
                if (!batteriesSelected)
                {
                    batteries.transform.localScale = new Vector3(1f, 1f, 1f);
                    batteriesSelected = true;
                }
                else
                {
                    batteries.transform.localScale = new Vector3(0f, 0f, 0f);
                    batteriesSelected = false;
                }
            }
        }
    }
    void Combine()
    {
        if (batteriesSelected && googlesSelected)
        {
            if(PlayerInventory.hasBatteries && Noktowizja.m_Battery != 100)
            {
                PlayerInventory.batteryCount -= 1;
                Noktowizja.m_Battery = 100;
            }
        }
    }
}
