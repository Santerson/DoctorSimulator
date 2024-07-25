using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MedicineCompilation : MonoBehaviour
{
    public List<float> MedicineOrdered = new List<float>(0);
    public List<float> MedicinePlayerInputted = new List<float>(0);
    [SerializeField] GameObject PrefabRed;
    [SerializeField] GameObject PrefabGreen;
    [SerializeField] GameObject PrefabBlue;
    [SerializeField] TextMeshProUGUI EqualText;
    OrdersList orders; 

    private void Awake()
    {
        if (PrefabRed == null)
        {
            Debug.LogError("Could not find AddRed button");
        }
        if (PrefabGreen == null)
        {
            Debug.LogError("Could not find AddGreen button");
        }
        if (PrefabBlue == null)
        {
            Debug.LogError("Could not find AddBlue button");
        }
        orders = FindObjectOfType<OrdersList>();
    }

    private void Start()
    {
        startOrder();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < MedicinePlayerInputted.Count; i++)
        {
            Debug.Log(MedicinePlayerInputted[i]);
        }

    }

    void DisplayMedicine()
    {
        float yOffset = -1.5f;
        for (int i = 0; i < MedicinePlayerInputted.Count; ++i)
        {
            GameObject medicine = null;
            if (MedicinePlayerInputted[i] == 0)
            {
                medicine = PrefabRed;
            }
            else if (MedicinePlayerInputted[i] == 1)
            {
                medicine = PrefabGreen;
            }
            else if (MedicinePlayerInputted[i] == 2)
            {
                medicine = PrefabBlue;
            }
            Instantiate(medicine, new Vector2(7, yOffset), Quaternion.identity);
            yOffset += 2;
        }
        if (MedicinePlayerInputted.Count == 3)
        {
            CheckEqual();
        }
    }

    void PrintOrder()
    {
        float Xoffset = 5.5f;
        for (int i = 0; i < MedicineOrdered.Count; ++i)
        {
            GameObject medicine = null;
            if (MedicineOrdered[i] == 0)
            {
                medicine = PrefabRed;
            }
            else if (MedicineOrdered[i] == 1)
            {
                medicine = PrefabGreen;
            }
            else if (MedicineOrdered[i] == 2)
            {
                medicine = PrefabBlue;
            }
            Instantiate(medicine, new Vector2(Xoffset, -3.84f), Quaternion.identity);
            Xoffset += 1.5f;
        }
    }

    void CheckEqual()
    {
        bool match = true;
        for (int i = 0; i < MedicineOrdered.Count; i++)
        {
            if (MedicineOrdered[i] != MedicinePlayerInputted[i])
            {
                EqualText.text = "NO MATCH!";
                match = false;
                break;
            }
        }
        if (match)
        {
            EqualText.text = "MATCH!";
            orders.gameObject.SetActive(true);
            orders.RemoveItem(0);
            orders.gameObject.SetActive(false);
        }
    }

    public void startOrder()
    {
        MedicinePlayerInputted.Clear();
        MedicineOrdered.Clear();
        int[] arr = orders.InternalOrders[0];
        foreach (int i in arr)
        {
            MedicineOrdered.Add(i);
        }
        orders.gameObject.SetActive(false);
        PrintOrder();
    }

    public void addRed()
    {
        MedicinePlayerInputted.Add(0);
        DisplayMedicine();
    }

    public void addGreen()
    {
        MedicinePlayerInputted.Add(1);
        DisplayMedicine();
    }

    public void addBlue()
    {
        MedicinePlayerInputted.Add(2);
        DisplayMedicine();
    }

    public void GoBack()
    {
        orders.gameObject.SetActive(true);
        SceneManager.LoadScene("Main");
    }
}
