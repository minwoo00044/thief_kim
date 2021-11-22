using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Takehit(float damage, RaycastHit hit);
    void TakeDamage(float damage);
}
