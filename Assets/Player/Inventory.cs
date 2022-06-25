using UnityEngine;
public class Inventory : MonoBehaviour
{
    public GameObject googles;
    public GameObject keycard;
    public GameObject medkit;
    //public GameObject bezpiecznik;
    //public GameObject gasMask;
    //public GameObject batteries;
    public static GameObject inv;
    public static bool invOn;
    void Start()
    {
        inv = GameObject.Find("Inventory");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !Pause.inWork && !invOn)
        {
            inv.transform.localScale = new Vector3(1f, 1f, 1f);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            Pause.enemy.GetComponent<AudioSource>().volume = 0;
            invOn = true;
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
                googles.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !Pause.inWork && invOn)
        {
            inv.transform.localScale = new Vector3(0f, 0f, 0f);
            Time.timeScale = 1;
            Pause.enemy.GetComponent<AudioSource>().volume = 1;
            medkit.transform.localScale = new Vector3(0f, 0f, 0f);
            keycard.transform.localScale = new Vector3(0f, 0f, 0f);
            googles.transform.localScale = new Vector3(0f, 0f, 0f);
            invOn = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //bezpiecznik.transform.localScale = new Vector3(0f, 0f, 0f);
            //gasMask.transform.localScale = new Vector3(0f, 0f, 0f);
            //batteries.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
/*  public Texture m_OnTexture;
    public Texture m_OffTexture;

    private bool m_LastState = false; 

    void Start()
    {
        m_Image = GetComponent<RawImage>();
    }

    void Update()
    {
        transform.localScale = new Vector3(PlayerInventory.hasGoggles ? 1f : 0f, 1f, 1f);

        if(Noktowizja.m_TurnedOn != m_LastState)
            m_Image.texture = Noktowizja.m_TurnedOn ? m_OnTexture : m_OffTexture;

        m_LastState = Noktowizja.m_TurnedOn;
    }*/
}
