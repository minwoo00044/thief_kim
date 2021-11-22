using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotlight : MonoBehaviour
{
    public float offsetX;
    public float offsetY;
    public float offsetZ;


    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Vector3 FixPos = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);
        transform.position = FixPos;
    }

}
