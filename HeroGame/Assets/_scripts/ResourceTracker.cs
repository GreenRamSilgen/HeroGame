using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTracker : MonoBehaviour
{
    public static ResourceTracker resourceTracker;
    private double currency;
    private int fans;
    public Text currencyTEXT;
    public Text fansTEXT;
    // Start is called before the first frame update
    void Start()
    {
        resourceTracker = this;
        UpdateUI();
    }
    
    public void IncreaseCurrency(double amount)
    {
        currency += amount;
        UpdateUI();
    }
    public void DecreaseCurrency(double amount)
    {
        currency -= amount;
        UpdateUI();
    }
    public bool hasCurrrencyAmount(double amount)
    {
        if (currency >= amount)
        {
            return true;
        }
        return false;
    }

    public void IncreaseFans(int amount)
    {
        fans += amount;
        UpdateUI();
    }
    public void DecreaseFans(int amount)
    {
        fans -= amount;
        UpdateUI();
    }
    public bool hasFansAmount(int amount)
    {
        if (fans >= amount)
        {
            return true;
        }
        return false;
    }




    void UpdateUI()
    {
        currencyTEXT.text = "$" + currency.ToString("N2");
        fansTEXT.text = "Fame:" + fans;
    }
}
