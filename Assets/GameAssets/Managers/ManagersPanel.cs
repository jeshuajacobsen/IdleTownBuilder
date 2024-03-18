using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagersPanel : MonoBehaviour
{

    public Building selectedBuilding;
    private Manager selectedManager;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
        transform.Find("BuyManagerButton").GetComponent<Button>().onClick.AddListener(PurchaseManager);
        transform.Find("CloseManagerPanelButton").GetComponent<Button>().onClick.AddListener(() => {Close(null);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PurchaseManager()
    {
        Manager manager = GameManager.instance.PurchaseManager();
        if (selectedManager && manager.Name == selectedManager.Name)
        {
            selectedManager.Level = manager.Level;
        }
    }
    public void AddManagerToView(Manager manager)
    {
        manager.transform.SetParent(transform.Find("Scroll View").Find("Viewport").Find("ManagerContent"), false);
    }

    public void Open(Building building)
    {
        selectedBuilding = building;
        transform.Find("BuildingPanel").Find("BuildingName").GetComponent<TextMeshProUGUI>().text = building.buildingName;
        transform.Find("BuildingPanel").Find("BuildingImage").GetComponent<Image>().sprite = SpriteManager.instance.GetBuildingSprite(building.buildingName);
        if (building.Manager != null)
        {
            Manager manager = Instantiate(building.Manager).GetComponent<Manager>();
            selectedManager = manager;
            manager.transform.SetParent(transform.Find("BuildingPanel").Find("AssignedManagerPanel"), false);  
            manager.transform.localPosition = Vector3.zero;
            transform.Find("BuildingPanel").Find("AssignedManagerPanel").GetComponent<Image>().sprite = null;
        }
        else
        {
            transform.Find("BuildingPanel").Find("AssignedManagerPanel").GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ManagerButton");
        }
        transform.gameObject.SetActive(true);
    }

    public void Close(Manager manager)
    {
        if (manager == null)
        {
            transform.gameObject.SetActive(false);
            DestroyAttachedManager();
            return;
        }
        if (selectedBuilding.Manager && selectedBuilding.Manager.Name == manager.Name)
        {
            selectedBuilding.Manager.AssignedBuilding = null;
            selectedBuilding.Manager = null;
            transform.Find("BuildingPanel").Find("AssignedManagerPanel").GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ManagerButton");
            DestroyAttachedManager();
            return;
        }
        DestroyAttachedManager();
        if (selectedBuilding != null && manager != null)
        {
            GameManager.instance.UnassignManagerFromOtherBuilding(manager);
            if (selectedBuilding.Manager != null)
            {
                GameManager.instance.UnassignManagerFromOtherBuilding(selectedBuilding.Manager);
            }
            selectedBuilding.Manager = manager;
            manager.AssignedBuilding = selectedBuilding;
        }
        transform.gameObject.SetActive(false);
    }

    private void DestroyAttachedManager()
    {
        foreach(Transform child in transform.Find("BuildingPanel").Find("AssignedManagerPanel").transform)
        {
            Destroy(child.gameObject);
        }
    }
}
