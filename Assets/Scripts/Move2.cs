using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    void Update()
    {
        //座標を書き換える
        this.gameObject.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
    }
}
