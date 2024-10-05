using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Representation of individual Resource Gameobject - Attached on Each Individual GO
public class Resource : MonoBehaviour, IInteractable
{
    public int MaxHealth = 3;
    private int health = 3;
    public int resourcesPerHit = 1;
    public float regrowthTime = 5f;
    public ResourceType Type;
    public GameObject ResourceModel; //  for resource when regrown

    private bool isCut = false;

    public void HitResource()
    {
        health--;
        if (health <= 0 && !isCut)
        {
            isCut = true;
            CollectResource();
            ResourceModel.SetActive(false);
            StartCoroutine(RegrowResource());
        }
        else
        {
            isCut = false;
            GetComponent<ShakeObject>().TriggerShake(1f,30);
            CollectResource();
        }
    }

    public void interact()
    {
        HitResource();
    }

    private void CollectResource()
    {
        Debug.Log($"Collected {resourcesPerHit} resources");
        // Add resources to player inventory (implement inventory system)
        Inventory.instance.AddResource(Type.ToString(), resourcesPerHit);

    }

    #region ienumerators
    private IEnumerator RegrowResource()
    {
        yield return new WaitForSeconds(regrowthTime);
        health = MaxHealth;
        ResourceModel.SetActive(true);
        isCut = false;
        // Instantiate(resourcePrefab, transform.position, Quaternion.identity);
    }
    IEnumerator Farm(Collider player)
    {
        while (isCut == false)
        {
            interact();
            player.gameObject.GetComponent<PlayerManager>().animator.SetTrigger("attack_1");
            yield return new WaitForSeconds(1);
        }
    }

    #endregion

    #region triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(Farm(other));

        }
    }
    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(Farm(other));
    }
    #endregion
   

    public enum ResourceType
    {
        Stone,
        Wood,
        Gold
    }

}
