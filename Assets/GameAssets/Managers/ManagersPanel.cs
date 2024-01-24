using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagersPanel : MonoBehaviour
{

    public Building selectedBuilding;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
        transform.Find("BuyManagerButton").GetComponent<Button>().onClick.AddListener(GameManager.instance.PurchaseManager);
        transform.Find("CloseManagerPanelButton").GetComponent<Button>().onClick.AddListener(() => {Close(null);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddManagerToView(Manager manager)
    {
        manager.transform.SetParent(transform.Find("Scroll View").Find("Viewport").Find("ManagerContent"), false);
    }

    public void Open(Building building)
    {
        selectedBuilding = building;
        transform.gameObject.SetActive(true);
    }

    public void Close(Manager manager)
    {
        if (selectedBuilding != null && manager != null)
        {
            GameManager.instance.UnassignManagerFromOtherBuilding(manager);
            selectedBuilding.Manager = manager;
        }
        transform.gameObject.SetActive(false);
    }
}
