using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Si_parts : MonoBehaviour
{
    HP hp;
    public GameObject Player;
    public GameObject Enemy;

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

        //if Enemy isn's 50 meters to the Player then teleport Enemy 10 meters to the Player
        if (Vector3.Distance(Enemy.transform.position, Player.transform.position) > 50)
        {
            Enemy.transform.position = Player.transform.position + new Vector3(10, 0, 0);
        }
        //Enemy can't see through any objects
        Enemy.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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