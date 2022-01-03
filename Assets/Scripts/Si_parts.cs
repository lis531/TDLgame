using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Si_parts : MonoBehaviour
{
    public GameObject Player;
    HP hp;

    public GameObject Wall;
    public GameObject Door;

    private bool canAttack = true;
    public float attackCooldown = 1.0f;
    public float attackDistance = 2.0f;
    public float attackDamage = 10.0f;

    public float movementSpeed = 10.0f;

    IEnumerator DealDamage()
    {
        // Deal Damage
        hp.DealDamage(attackDamage);

        // Cooldown Begin
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        // Cooldown End
    }

    void Start()
    {
        hp = Player.GetComponent<HP>();

        //if Enemy isn's near to the Player then teleport Enemy near to the Player
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 100)
            transform.position = GameObject.Find("Player").transform.position + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
        
    }
    //Enemy can't see throught door 
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Door")
            transform.position = new Vector3(Player.transform.position.x + 5, Player.transform.position.y, Player.transform.position.z);
        
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, movementSpeed * Time.deltaTime);

        if (canAttack)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) < attackDistance)
                StartCoroutine(DealDamage());
        }
    }
}