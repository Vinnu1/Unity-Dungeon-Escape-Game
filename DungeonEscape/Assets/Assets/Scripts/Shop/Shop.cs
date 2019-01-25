using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;

    private Player _player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if(_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    //public void SelectItem(int item)
    //{
    //    //0 = flame sword; 1 = boots; 2 = key
        
    //    //    switch(item)
    //    //    {
    //    //        case 0:
    //    //            UIManager.Instance.UpdateShopSelection(-56);
    //    //            currentSelectedItem = 0;
    //    //            currentItemCost = 200;
    //    //            break;
    //    //        case 1:
    //    //            UIManager.Instance.UpdateShopSelection(15);
    //    //            currentSelectedItem = 1;
    //    //            currentItemCost = 400;
    //    //            break;
    //    //        case 2:
    //    //            UIManager.Instance.UpdateShopSelection(-96);
    //    //            currentSelectedItem = 2;
    //    //            currentItemCost = 100;
    //    //            break;
    //    //    }
    //    //}
    //}

    public void BuyItem()
    {
        currentItemCost = 200;
        if (_player.diamonds >= currentItemCost)
        {
           
                GameManager.Instance.HasKey = true;
                SceneManager.LoadScene("Credits");
            
            _player.diamonds -= currentItemCost;
            UIManager.Instance.UpdateGemCount(_player.diamonds);
            shopPanel.SetActive(false);
        }
        else
        {
            shopPanel.SetActive(false);
        }
    }


}
 