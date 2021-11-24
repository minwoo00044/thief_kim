using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idle, chasing, Attacking };
    public bool isChasing = false;
    State currentState;

    NavMeshAgent pathfinder;
    Transform target;
    Material skinMaterial;
    LivingEntity targetEntity;

    Color originColor;

    float attackDistanceThreshold = 0.5f;
    float timeBetweenAttacks = 1;
    float damage = 1;

    float nextAttackTIme;
    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originColor = skinMaterial.color;

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;
        }

        if (isChasing)
        {
            hasTarget = true;
            currentState = State.chasing;
        }


        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());

    }
    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    void Update()
    {
        /*if (hasTarget)
        {
            if (Time.time > nextAttackTIme)
            {
                float sqrDstTarget = (target.position - transform.position).sqrMagnitude;

                if (sqrDstTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTIme = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }

            }

        }*/
        if (!isChasing)
        {
            hasTarget = false;
            currentState = State.Idle;
        }
        else
        {
            hasTarget = true;
            currentState = State.chasing;
            StartCoroutine(UpdatePath());
        }
    }
    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;


        while (percent <= 1)
        {
            if (percent >= 0.5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originPosition, attackPosition, interpolation);

            yield return null;
        }
        skinMaterial.color = originColor;
        currentState = State.chasing;
        pathfinder.enabled = true;
    }


    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while (hasTarget)
        {
            if (currentState == State.chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                if (!dead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }


            yield return new WaitForSeconds(refreshRate);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
    // Update is called once per frame

}
