using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemPurchaseButton : MonoBehaviour
{

    [SerializeField] private int quantity;
    [SerializeField] private int cost;

    // Start is called before the first frame update
    void Start()
    {
        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(PurchaseGems);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PurchaseGems()
    {
        GameManager.instance.PurchaseGems(quantity, cost);
    }
}
