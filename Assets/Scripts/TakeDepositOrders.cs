using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDepositOrders : MonoBehaviour
{
    public List<int> order = new List<int>();
    [SerializeField] GameObject PrefabBlueMed;
    [SerializeField] GameObject PrefabGreenMed;
    [SerializeField] GameObject PrefabRedMed;
    [SerializeField] Vector2 MedPosition;

    private void OnDrawGizmosSelected()
    {
        DebugExtensions.DrawBox(MedPosition, new Vector2(MedPosition.x + 0.3f, MedPosition.y + 0.3f), Color.green);
    }

    public void TakeOrder()
    {
        int rand = Random.Range(1, 4);
        for (int i = 0; i < rand; i++)
        {
            order.Add(Random.Range(0, 3));
        }
        PrintMedicine();
    }

    private void PrintMedicine()
    {

        for (int i = 0; i < order.Count; i++)
        {
            if (order[i] > 0)
            {

            }
        }
    }
}
