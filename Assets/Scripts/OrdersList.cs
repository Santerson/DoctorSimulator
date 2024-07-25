using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrdersList : MonoBehaviour
{
    List<string> Orders = new List<string>();
    public List<int[]> InternalOrders { get; set; } = new List<int[]>();
    TextMeshProUGUI text = null;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            Debug.LogError("No tmpro text added to component");
        }
        DisplayList();
    }

    public void AddItem()
    {
        AddItem(new int[] { Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3)});
    }

    public void AddItem(int[] Items)
    {
        if (Items.Length != 3)
        {
            Debug.LogError("Inserted Lists can only be 3 items long!");
            return;
        }
        InternalOrders.Add(Items);
        DisplayList();
    }

    public void RemoveItem(int index)
    {
        if (index > InternalOrders.Count)
        {
            Debug.LogError("Index out of bounds exception");
            return;
        }
        InternalOrders.RemoveAt(index);
        DisplayList();
    }

    void DisplayList()
    {
        string ordersText = "ORDERS: \n";
        for (int i = 0; i < InternalOrders.Count; ++i)
        {
            for (int j = 0; j < InternalOrders[i].Length; ++j)
            {
                ordersText += ConvertIntToString(InternalOrders[i][j]) + ", ";
            }
            ordersText += "\n";
        }
        text.SetText(ordersText);
    }

    string ConvertIntToString(int input)
    {
        if (input == 0) return "red";
        if (input == 1) return "green";
        if (input == 2) return "blue";
        Debug.LogError("Unknown integer passed into convertString function. Returning an empty string");
        return "";
    }
}
