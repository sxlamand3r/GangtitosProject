using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<itemType> inventoryList;
    public int selectedItem;
    public float playerReach;
    [SerializeField] GameObject throwItem_gameobject;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode pickItemKey;

    [Space(20)]
    [Header("Item gameobjects")]
    [SerializeField] GameObject flashlight_item;
    [SerializeField] GameObject bigFlaslight_item;

    [Space(20)]
    [Header("Item prefabs")]
    [SerializeField] GameObject flashlight_prefab;
    [SerializeField] GameObject bigFlaslight_prefab;

    //[Space(20)]
    //[Header("UI")]
    //[SerializeField] Image[] inventorySlotImage = new Image[4];
    //[SerializeField] Image[] inventoryBackgroundImage = new Image[4];
    //[SerializeField] Sprite emptySloteSprite;

    [SerializeField] Camera cam;
    [SerializeField] GameObject pickUpItem_gameobject;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>(){};
    private Dictionary<itemType, GameObject> itemInstantiate = new Dictionary<itemType, GameObject>() { };
    void Start()
    {
        itemSetActive.Add(itemType.Flashlight, flashlight_item);
        itemSetActive.Add(itemType.bigFlashlight, bigFlaslight_item);

        itemInstantiate.Add(itemType.Flashlight, flashlight_prefab);
        itemInstantiate.Add(itemType.bigFlashlight, bigFlaslight_prefab);


        NewItemSelected();
    }

    void Update()
    {
        //Items Pickup
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, playerReach))
        {
            if (inventoryList.Count < 4)
            {
                IPickable item = hitInfo.collider.GetComponent<IPickable>();
                if (item != null)
                {
                    pickUpItem_gameobject.SetActive(true);
                    if (Input.GetKey(pickItemKey))
                    {
                        inventoryList.Add(hitInfo.collider.GetComponent<ItemPickable>().itemScriptableObject.item_type);
                        item.PickItem();
                    }
                       
                }
                else
                {
                    pickUpItem_gameobject.SetActive(false);
                }
            }
        }
        else
        {
            pickUpItem_gameobject.SetActive(false);
        }

        //Items Throw
        if (Input.GetKeyDown(throwItemKey) && inventoryList.Count > 1)
        {
            Instantiate(itemInstantiate[inventoryList[selectedItem]], position: throwItem_gameobject.transform.position, new Quaternion());
            inventoryList.RemoveAt(selectedItem);

            if (selectedItem != 0)
            {
                selectedItem -= 1;
            }
            NewItemSelected();
        }

        //UI

        //for (int i = 0; i < 3; i++)
        //{
        //    if (i < inventoryList.Count)
        //    {
        //        inventorySlotImage[i].sprite = itemSetActive[inventoryList[i]].GetComponent<Item>().itemScriptableObject.item_sprite;
        //    }
        //    else
        //    {
        //        inventorySlotImage[i].sprite = emptySloteSprite;
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 0)
        {
            selectedItem = 0;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inventoryList.Count > 1)
        {
            selectedItem = 1;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && inventoryList.Count > 2)
        {
            selectedItem = 2;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && inventoryList.Count > 3)
        {
            selectedItem = 3;
            NewItemSelected();
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha5) && inventoryList.Count > 4)
        //{
        //    selectedItem = 4;
        //    NewItemSelected();
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha6) && inventoryList.Count > 5)
        //{
        //    selectedItem = 5;
        //    NewItemSelected();
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha7) && inventoryList.Count > 6)
        //{
        //    selectedItem = 6;
        //    NewItemSelected();
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha8) && inventoryList.Count > 7)
        //{
        //    selectedItem = 7;
        //    NewItemSelected();
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha9) && inventoryList.Count > 8)
        //{
        //    selectedItem = 8;
        //    NewItemSelected();
        //}
    }


    private void NewItemSelected()
    {
        flashlight_item.SetActive(false);
        bigFlaslight_item.SetActive(false);
        // mace_item.SetActive(false);
        //club_item.SetActive(false);


        GameObject selectedItemGameobject = itemSetActive[inventoryList[selectedItem]];
        selectedItemGameobject.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * playerReach);
    }

}
public interface IPickable
{
    void PickItem();
}





