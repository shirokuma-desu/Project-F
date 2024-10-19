using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Vector3 dir;
    public void Setup(Vector3 direction)
    {
        this.dir = direction;
        Destroy(this.gameObject,5f);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * bulletSpeed * Time.deltaTime;
    }


}
