using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGun : MonoBehaviour
{

    private void Start()
    {
        Tank.BulletGun = false;
    }
    public void Click()
    {
        Tank.BulletGun = true;
        Destroy(gameObject);
    }
}
