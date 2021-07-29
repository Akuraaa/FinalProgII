using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRange : IMovable
{
    private Rigidbody _rb;

    public List<GameObject> targets;
    public GameObject bullet;
    public Transform bulletSpawn;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Attack(GetTarget(targets));
    }

    public void Move()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            _rb.velocity = ((Vector3.right * Input.GetAxisRaw("Horizontal")) + (Vector3.forward * Input.GetAxisRaw("Vertical"))).normalized * speed * Time.deltaTime;
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    public void Attack(GameObject tg)
    {
        if (tg != null)
        {
            transform.LookAt(tg.transform);
            var g = Instantiate(bullet, bulletSpawn.position, bullet.transform.rotation);
            g.transform.up = (tg.transform.position - g.transform.position).normalized;
        }
    }

    public GameObject GetTarget(List<GameObject> tg)
    {
        if (tg.Count > 0)
        {
            GameObject g = tg[0];
            float distance = (tg[0].transform.position - transform.position).magnitude;
            foreach (var item in tg)
            {
                if (item == tg[0])
                {
                    continue;
                }
                else
                {
                    float currentDistance = (item.transform.position - transform.position).magnitude;
                    if (currentDistance < distance)
                    {
                        RaycastHit t;
                        if (Physics.Raycast(transform.position, g.transform.position, out t))
                        {
                            if (t.transform.gameObject.tag == "Enemy")
                            {
                                distance = currentDistance;
                                g = item;
                            }
                        }
                    }
                }
            }
            if (g == tg[0])
            {
                RaycastHit t;
                if (Physics.Raycast(transform.position, g.transform.position, out t))
                {
                    if (t.transform.gameObject == tg[0])
                    {
                        return g;
                    }
                }
            }
            else
            {
                return g;
            }
        }
        return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            targets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            targets.Remove(other.gameObject);
        }
    }
}
