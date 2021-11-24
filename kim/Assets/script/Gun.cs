using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

    float nextShotTime;

    public void Shoot(Text txt)
    {
        if(Time.time > nextShotTime && txt.GetComponent<BulletText>().n>0)
        {
            txt.GetComponent<BulletText>().n -= 1;
            GetComponent<AudioSource>().Play();
            nextShotTime = Time.time + msBetweenShots / 500;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation);
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
