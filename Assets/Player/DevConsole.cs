using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class DevConsole : MonoBehaviour
{
    // OPEN WITH TAB+Q

    static public bool m_IsOpen = false;

    public uint m_MaxCharacters = 86;

    private TMP_Text m_InputText;
    private TMP_Text m_OutputText;

    private List<string> m_Arguments;

    #region Handled Commands

    void help()
    {
        PrintOutput(
            "help - Displays this help message\n" +
            "hasitem [medkit/nv/keycard] [on/off] - Gives/Takes the item\n" +
            "mrwhite [on/off] - Enables/Disables Mr. Mhite's AI\n" +
            "stamina [on/off] - Enables/Disables stamina\n" +
            "dealdamage [int] - Deal damage to the player\n" +
            "heal [int] - Heal the player\n" +
            "sethealth [int] - Set the player's health\n" +
            "setstamina [float] - Set the player's stamina\n" +
            "removestamina [float] - Remove stamina from the player\n" +
            "addstamina [float] - Add stamina to the player\n"
            );
    }
    void dealdamage()
    {
        Health.health -= int.Parse(m_Arguments[0]);
    }
    void heal()
    {
        Health.health += int.Parse(m_Arguments[0]);
    }
    void sethealth()
    {
        Health.health = int.Parse(m_Arguments[0]);
    }
    void setstamina()
    {
        PlayerStamina.stamina = float.Parse(m_Arguments[0]);
    }
    void removestamina()
    {
        PlayerStamina.stamina -= float.Parse(m_Arguments[0]);
    }
    void addstamina()
    {
        PlayerStamina.stamina += float.Parse(m_Arguments[0]);
    }
    void hasitem()
    {
        string item = m_Arguments[0];
        bool state = m_Arguments[1].Equals("on");
        
        if(item.Equals("medkit"))
            PlayerInventory.hasMedkit = state;
        else if(item.Equals("nv"))
            PlayerInventory.hasGoggles = state;
        else if(item.Equals("keycard"))
            PlayerInventory.hasKeycard = state;
        else
            PrintOutput("Unknown item!", 2f);
    }
    void mrwhite()
    {
        AICore.m_Enabled = m_Arguments[0].Equals("on");
    }
    void stamina()
    {
        PlayerStamina.staminaEnabled = m_Arguments[0].Equals("on");
    }

    #endregion

    #region Console Related Methods
    void OpenConsole()
    {
        m_IsOpen = true;
        transform.localScale = new Vector3(1f,1f,1f);
    }
    void CloseConsole(bool unpause = true)
    {
        m_IsOpen = false;
        transform.localScale = new Vector3(0f,0f,0f);
    }

    void ExecuteCommand()
    {
        if(m_InputText.text.Length <= 1)
            return;

        m_Arguments.Clear();

        string trimmed = m_InputText.text.Substring(0, m_InputText.text.Length - 1);
        string[] args = trimmed.Split(' ');

        for(int i = 1; i < args.Length; i++)
        {
            if(args[i].Length > 0)
                m_Arguments.Add(args[i]);
        }

        Invoke(args[0], 0f);

        Clear();
    }

    void PrintOutput(string output, float showTime = 8f)
    {
        m_OutputText.text += output;

        Invoke("HideOutput", showTime);
    }
    void HideOutput()
    {
        m_OutputText.text = "";
    }

    #endregion

    void Start()
    {
        m_InputText = transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        m_OutputText = transform.GetChild(2).gameObject.GetComponent<TMP_Text>();

        m_Arguments = new List<string>();

        Clear();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Tab) && Input.GetKeyDown(KeyCode.Q) && Time.timeScale != 0)
        {
            m_IsOpen = !m_IsOpen;

            if(m_IsOpen)
                OpenConsole();
            else
                CloseConsole();
        }
        else if(m_IsOpen && Time.timeScale == 0)
            CloseConsole(false);

        if(m_IsOpen && !Input.GetKey(KeyCode.Tab))
        {
            foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key) && IsValidInput(key))
                    InputText(key);
            }

            if(Input.GetKeyDown(KeyCode.Return))
                ExecuteCommand();

            if(Input.GetKeyDown(KeyCode.Space))
                Space();

            if(Input.GetKeyDown(KeyCode.Backspace))
                Backspace();

            if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Backspace))
                Clear();
        }
    }

    #region Input Related Methods
    bool IsValidInput(KeyCode key)
    {
        return ((key >= KeyCode.A && key <= KeyCode.Z) || (key >= KeyCode.Keypad0 && key <= KeyCode.Keypad9) || (key >= KeyCode.Alpha0 && key <= KeyCode.Alpha9));
    }
    void InputText(KeyCode key)
    {
        if(m_InputText.text.Length + 1 > m_MaxCharacters) return;

        string keyString = key.ToString().ToLower();

        char character = keyString[keyString.Length-1];

        m_InputText.text = m_InputText.text.Substring(0, m_InputText.text.Length - 1);
        m_InputText.text += character + "_";
    }
    void Backspace()
    {
        if(m_InputText.text.Length <= 1)
            return;

        // Erase the last character in text
        m_InputText.text = m_InputText.text.Substring(0, m_InputText.text.Length - 2);
        m_InputText.text += "_";
    }
    void Clear()
    {
        m_InputText.text = "_";
    }
    void Space()
    {
        m_InputText.text = m_InputText.text.Substring(0, m_InputText.text.Length - 1);
        m_InputText.text += " _";
    }
    #endregion
}
