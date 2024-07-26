using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDepositOrders : MonoBehaviour
{
    public List<int> order = new List<int>();
    private List<GameObject> InstantiatedObjects = new List<GameObject>();
    [SerializeField] GameObject PrefabBlueMed;
    [SerializeField] GameObject PrefabGreenMed;
    [SerializeField] GameObject PrefabRedMed;
    [SerializeField] Vector2 MedPosition;
    public bool OrderTaken = false;

    private void OnDrawGizmosSelected()
    {
        DebugExtensions.DrawBox(MedPosition, new Vector2(MedPosition.x + 0.3f, MedPosition.y + 0.3f), Color.green);
    }

    public void TakeOrder()
    {
        OrderTaken = true;
        int rand = Random.Range(1, 4);
        for (int i = 0; i < rand; i++)
        {
            order.Add(Random.Range(0, 3));
        }
        PrintMedicine();
    }

    private void PrintMedicine()
    {
        float xOffset = 0;
        for (int i = 0; i < order.Count; i++)
        {
            if (order[i] == 0)
            {
                InstantiatedObjects.Add(Instantiate(PrefabRedMed, new Vector2(MedPosition.x + xOffset, MedPosition.y) , Quaternion.identity));
            }
            else if (order[i] == 1)
            {
                InstantiatedObjects.Add(Instantiate(PrefabGreenMed, new Vector2(MedPosition.x + xOffset, MedPosition.y), Quaternion.identity));
            }
            else if (order[i] == 2)
            {
                InstantiatedObjects.Add(Instantiate(PrefabBlueMed, new Vector2(MedPosition.x + xOffset, MedPosition.y), Quaternion.identity));
            }
            xOffset += 0.3f;
        }
    }

    public void CheckGivenMedicine(int medicine)
    {
        bool isInOrder = false;
        foreach (int item in order)
        {
            if (item == medicine) isInOrder = true;
        }
        if (isInOrder)
        {
            FindObjectOfType<PlayerCloseTo>().AppearingItem.text = "Press space to deposit";
            if (Input.GetKey(KeyCode.Space))
            {
                GaveMedicine(medicine);
            }
        }
        else
        {
            FindObjectOfType<PlayerCloseTo>().AppearingItem.text = "Not on the order!";
        }
    }

    void GaveMedicine(int medicine)
    {
        for (int i = 0; i < order.Count; i++)
        {
            if (order[i] == medicine)
            {
                order.Remove(i);
                Debug.Log("removed an object!");
                break;
            }
        }

        foreach(GameObject obj in InstantiatedObjects)
        {
            Destroy(obj);
        }

        PrintMedicine();
    }
}
