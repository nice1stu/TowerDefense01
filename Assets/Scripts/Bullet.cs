using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    
    public float bulletSpeed = 70f;
    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);
        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
