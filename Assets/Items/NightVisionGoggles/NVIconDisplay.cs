using UnityEngine;
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
}
