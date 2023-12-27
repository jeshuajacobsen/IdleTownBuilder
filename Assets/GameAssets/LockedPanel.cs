using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockedPanel : MonoBehaviour
{
    private bool usePrestigeToUnlock = false;
    [SerializeField] private Unlockable parent;
    [SerializeField] private MonoBehaviour parentMonoBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        parent = parentMonoBehaviour as Unlockable;
        transform.Find("BuildButton").GetComponent<Button>().onClick.AddListener(Unlock);
        transform.Find("BuildButton").Find("Text").GetComponent<TextMeshProUGUI>().text = "$" + parent.GetUnlockCost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUsePrestige(bool usePrestige)
    {
        usePrestigeToUnlock = usePrestige;
    }

    public void Unlock()
    {
        if (!usePrestigeToUnlock)
        {
            if (GameManager.instance.HasEnoughCoin(parent.GetUnlockCost()))
            {
                gameObject.SetActive(false);
                parent.Unlock();
                GameManager.instance.SubtractCoins(parent.GetUnlockCost());
            }
        } else {
            if (GameManager.instance.HasEnoughPrestige(parent.GetUnlockCost()))
            {
                gameObject.SetActive(false);
                parent.Unlock();
                GameManager.instance.SubtractCollectedPrestige(parent.GetUnlockCost());
            }
        }
    }
}
