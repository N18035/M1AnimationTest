using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    void Update()
    {
        //座標を書き換える
        this.gameObject.transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
    }
}
