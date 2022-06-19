using UnityEngine;
public class Inventory : MonoBehaviour
{
    public GameObject googles;
    public GameObject keycard;
    public GameObject medkit;
    //public GameObject bezpiecznik;
    //public GameObject gasMask;
    //public GameObject batteries;
    public GameObject inv;
    public static bool on;
    void Start()
    {
        inv.transform.localScale = new Vector3(0, 0, 0);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !Pause.Paus)
        {
            Pause.Paused();
            Time.timeScale = 0;
            inv.transform.localScale = new Vector3(1f, 1f, 1f);
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
        else if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && Pause.Paus)
        {
            Pause.UnPaused();
            Time.timeScale = 1;
            inv.transform.localScale = new Vector3(0f, 0f, 0f);
            medkit.transform.localScale = new Vector3(0f, 0f, 0f);
            keycard.transform.localScale = new Vector3(0f, 0f, 0f);
            googles.transform.localScale = new Vector3(0f, 0f, 0f);
            //bezpiecznik.transform.localScale = new Vector3(0f, 0f, 0f);
            //gasMask.transform.localScale = new Vector3(0f, 0f, 0f);
            //batteries.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
/*using UnityEngine;
using UnityEngine.UI;

public class NVIconDisplay : MonoBehaviour
{
    RawImage m_Image;

    public Texture m_OnTexture;
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
    }
}*/
}
