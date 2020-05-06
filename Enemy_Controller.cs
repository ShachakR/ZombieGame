using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float Health;
    public float DropRate;
    public float damage;

    [Header("Enemy Objectives")]
    // Static allows values of the gameeobjects to update on all enemy instances 
    public static GameObject objective; 
    public static GameObject player;

    private float n;
    private Animator anim;
    private float distance; 
    private GameObject currentTargert;
    private NavMeshAgent navMeshAgent; 

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponentInChildren<NavMeshAgent>(); 
        anim = GetComponent<Animator>();
        player = GameManager.instance.player;
        objective = GameManager.instance.objective; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            anim.SetTrigger("Die");
        }

        SetTarget();
    }

    #region setTarget
    private void SetTarget(){
        distance = Vector3.Distance(transform.position, player.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward + Vector3.down * 0.3f, out hit, 15f))
        {
            if (hit.transform.gameObject.CompareTag("Defence"))
            {
                EnemyNavigation(hit.transform.gameObject);
                currentTargert = hit.transform.gameObject;
            }
            else
            {
                if (distance < 3)
                {
                    EnemyNavigation(player);
                    currentTargert = player;
                }

                else
                {
                    EnemyNavigation(objective);
                    currentTargert = objective;
                }
            }
        }
        else
        {
            if (distance < 3)
            {
                EnemyNavigation(player);
                currentTargert = player;
            }

            else
            {
                EnemyNavigation(objective);
                currentTargert = objective;
            }
        }
    }
    #endregion

    #region EnemyDeath-ForAnimation
    private void EnemyDeath()
    {
        n = Random.Range(0, 100);
        if (n <= DropRate)
        {
            int n = Random.Range(0, GameManager.instance.PrefabWeapons.Length);
            GameObject obj = Instantiate(GameManager.instance.PrefabWeapons[n], transform.position, transform.rotation);
            obj.name = obj.name.Replace("(Clone)", "");
        }

        GameManager.instance.player.GetComponent<Player_Controller>().money += 15;
        GameManager.instance.GetComponent<Enemy_Spawner>().killCount++;
        GameManager.instance.totalKills++; 
        Debug.Log(GameManager.instance.GetComponent<Enemy_Spawner>().killCount);

        Destroy(gameObject); 
    }
    #endregion

    #region EnemyAttack - ForAnimation
    private void EnemyAttack()
    {
        if(currentTargert == player)
        {
            currentTargert.GetComponent<Player_Controller>().health -= damage;
        }
        else if(currentTargert.name == "Barricade_1" || currentTargert.name == "Barricade_2" || currentTargert.name == "Barricade_3" || currentTargert.name == "Barricade_4")
        {
            currentTargert.GetComponent<Barricade>().health -= damage;
        }
        else if (currentTargert == objective)
        {
            Vector3 tempValue = objective.transform.GetChild(0).transform.localScale;
            tempValue[2] -= damage / 200;

            if(tempValue[2] <= 0)
            {
                tempValue[2] = 0; 
            }

            objective.transform.GetChild(0).transform.localScale = tempValue; 
            Debug.Log("Damaged Objective"); 
        }
    }
    #endregion

    #region EnemyNavigationSystem
    private void EnemyNavigation(GameObject target)
    {
        Vector3 targetV3 = target.transform.position; 
        navMeshAgent.SetDestination(targetV3); 

        if(navMeshAgent.remainingDistance <= 2.3f)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("Attack", false);
        }
    }
    #endregion 
}
