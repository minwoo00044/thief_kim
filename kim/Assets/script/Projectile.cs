using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    float damage = 1;
    public LayerMask collisionMask;
    public float lifetime = 3;
    float skinWidth = 0.1f;

    // Start is called before the first frame update


    void Start()
    {
        Destroy(gameObject, lifetime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if(initialCollisions.Length>0)
        {
            OnHitObject(initialCollisions[0]);
        }


    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    // Update is called once per frame
    void Update()
    {

        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Destroy(gameObject, 3);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }

    }
    void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if(damageableObject != null)
        {
            damageableObject.Takehit(damage, hit);
        }

        GameObject.Destroy(gameObject);
    }

    void OnHitObject(Collider c)
    {
        if(c.gameObject.layer== LayerMask.NameToLayer("Enemy"))
        {
            IDamageable damageableObject = c.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damage);
            }

            GameObject.Destroy(gameObject);
        }

    }


}
