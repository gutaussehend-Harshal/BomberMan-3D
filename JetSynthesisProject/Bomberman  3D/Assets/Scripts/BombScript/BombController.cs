using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BombController : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float bombBlastTimer;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private float bombStrength = 10;
    public LayerMask levelMask;

    // private void Start()
    // {
    //     Invoke("Explode", 3f);
    // }    

    private void Update()
    {
        TimeForNextBombPlaced();
    }
    private void TimeForNextBombPlaced()
    {
        bombBlastTimer -= Time.deltaTime;
        if (bombBlastTimer <= 0.1f)
        {
            Explode();
            Destroy(this.gameObject);
        }
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));

        // GetComponent<MeshRenderer>().enabled = false; //2
        // transform.Find("Collider").gameObject.SetActive(false); //3
        // Destroy(gameObject, .3f); //4
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i < bombStrength; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), direction, out hit, i, levelMask);

            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), Quaternion.identity);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null)
        {
            sphereCollider.isTrigger = false;
        }
    }
}
