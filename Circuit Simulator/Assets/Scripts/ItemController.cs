using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
    public int ID;
    public int quantity;
    public TextMeshProUGUI quantityText;
    public bool Clicked = false;
    private EditorManager editor;

    void Start()
    {
        quantityText.text = quantity.ToString();
        editor = GameObject.FindGameObjectWithTag("EditorManager").GetComponent<EditorManager>();
    }

    public void ButtonClicked()
    {
        if (quantity > 0)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            Clicked = true;
            Instantiate(editor.ItemImage[ID], worldPosition, Quaternion.identity);
            quantity--;
            quantityText.text = quantity.ToString();
            editor.CurrentButtonPressed = ID;
        }
    }

}
