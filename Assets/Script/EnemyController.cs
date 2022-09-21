using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Alert,
    Following,
}

public class EnemyController : MonoBehaviour
{
    public EnemyState enemyState;

    public LayerMask LayerPlayer;
    int layerPlayer;

    public float AlertRage;   
    public float Velocity;

    //Sonidos
    public AudioClip SoundEnemy;  
    public AudioSource audioSourceEnemy;

    public GameObject player = null;


    //

    //Variables de animacion
    //public GameObject Enemy;
    public Animator EnemyAnimatorController;
    public bool BeAbleEnemy;
    public bool Attack;
    void Start()
    {
        enemyState = EnemyState.Idle;
        //Enemy = GameObject.Find("Enemy");
        EnemyAnimatorController = GetComponent<Animator>();
        audioSourceEnemy = GetComponent<AudioSource>();

        layerPlayer = LayerMask.GetMask("Player");
    }

    void Update()
    {
        enemyBehavior();
    }

    public void enemyBehavior()
    {
        if (enemyState == EnemyState.Idle)
        {
            if (player == null)
            {
                Waiting();
            }
        }
        else if (enemyState == EnemyState.Alert)
        {
            Alerting();
        }
        else if (enemyState == EnemyState.Following)
        {
            Following();
        }
        





        //if (player != null)
        //{

        //}
        //BeAler = Physics.CheckSphere(transform.position, AlertRage, LayerPlayer);

        //if (BeAler == true)
        //{
        //    BeAbleEnemy = true;
        //    Vector3 PosPlayer = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        //    EnemyAnimatorController.SetBool("BeAbleEnemy", true);
        //    audioSourceEnemy.Play();
        //    transform.LookAt(PosPlayer);

        //    player = GameObject.Find("Android");

        //}
        //else
        //{
        //    EnemyAnimatorController.SetBool("BeAbleEnemy", false);
        //    EnemyAnimatorController.SetBool("Attack", false);
        //}



        ////EnemyAnimatorController.SetBool("BeAbleEnemy", true);
        ////transform.position = Vector3.MoveTowards(transform.position, PosPlayer,Velocity*Time.deltaTime);


        //if (BeAler == true && Vector3.Distance(transform.position, Player.position) < 3)
        //{
        //    Vector3 PosPlayer = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        //    EnemyAnimatorController.SetBool("Attack", true);
        //    transform.LookAt(PosPlayer);
        //    transform.position = Vector3.MoveTowards(transform.position, PosPlayer, Velocity * Time.deltaTime);
        //}

    }

    public void Waiting()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AlertRage, layerPlayer);
        foreach (Collider hitCollider in hitColliders)
        {
            player = hitCollider.transform.gameObject;
            audioSourceEnemy.Play();
            EnemyAnimatorController.SetBool("BeAbleEnemy", true);
            enemyState = EnemyState.Alert;
            return;

        }       
    }

    public void Alerting()
    {
        transform.LookAt(new Vector3(player.transform.position.x, 0f, player.transform.position.z));

        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            enemyState = EnemyState.Following;
        }
    }

    public void Following()
    {
        Vector3 PosPlayer = player.transform.position;
        Vector3 dirTolook = new Vector3(player.transform.position.x, 0f ,player.transform.position.z);
        transform.LookAt(dirTolook);
        transform.position = Vector3.MoveTowards(transform.position, PosPlayer, Velocity * Time.deltaTime);

        EnemyAnimatorController.SetBool("Attack", true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AlertRage);
    }

}
