using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletText : MonoBehaviour
{
    public Text txt;
    public int n = 8;
    // Start is called before the first frame update
    void Start()
    {
        txt.text = "bullet : " + n;
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "bullet : " + n;
    }
}
