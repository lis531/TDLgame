using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.AI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    static private bool m_QueueLoad = false;

    void Start()
    {
        Instance = this;

        if(m_QueueLoad)
            LoadPlayer();
    }

    public void QueueLoad()
    {
        m_QueueLoad = true;
    }

    public void SavePlayer()
    {
        string path = Application.persistentDataPath + "/player.save";

        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        Transform enemy = GameObject.FindGameObjectWithTag("Enemy").transform;

        PlayerData data = new PlayerData
        (
            Health.health,
            PlayerStamina.stamina,
            new float[3]{player.position.x, player.position.y, player.position.z},
            player.eulerAngles.y,
            Latarka.m_Enabled,
            PlayerInventory.medkitCount,
            PlayerInventory.hasGoggles,
            PlayerInventory.hasKeycard,
            new float[3]{enemy.position.x, enemy.position.y, enemy.position.z},
            enemy.eulerAngles.y,
            Noktowizja.m_TurnedOn
        );

        formatter.Serialize(stream, data);
        stream.Close();
    }

    private void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("Loaded player data:");
            Debug.Log("Health: " + data.health);
            Debug.Log("Position: " + data.position[0] + ", " + data.position[1] + ", " + data.position[2]);
            Debug.Log("Y Rotation: " + data.yRotation);
            Debug.Log("Latarka: " + data.latarkaOn);
            Debug.Log("Medkit count: " + data.medkitCount);
            Debug.Log("Has NV Goggles: " + data.hasNVGoggles);
            Debug.Log("Has keycard: " + data.hasKeycard);

            Debug.Log("Enemy position: " + data.enemyPosition[0] + ", " + data.enemyPosition[1] + ", " + data.enemyPosition[2]);
            Debug.Log("Enemy Y Rotation: " + data.enemyYRotation);

            Debug.Log("NV Goggles enabled: " + data.nvGogglesEnabled);

            Health.health = data.health;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            CharacterController controller = player.GetComponent<CharacterController>();

            controller.enabled = false;
            player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            player.transform.eulerAngles = new Vector3(0, data.yRotation, 0);
            controller.enabled = true;

            Latarka.m_Enabled = data.latarkaOn;

            player.GetComponent<Noktowizja>().SwitchState(data.nvGogglesEnabled);

            PlayerInventory.medkitCount = data.medkitCount;
            PlayerInventory.hasGoggles = data.hasNVGoggles;
            PlayerInventory.hasKeycard = data.hasKeycard;

            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

            agent.Warp(new Vector3(data.enemyPosition[0], data.enemyPosition[1], data.enemyPosition[2]));
            enemy.transform.eulerAngles = new Vector3(0, data.enemyYRotation, 0);
        }
        else
            Debug.LogError("Save file not found in " + path);

        m_QueueLoad = false;
    }
}