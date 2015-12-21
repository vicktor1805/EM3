using UnityEngine;
using UnityEngine.UI;

public class MessageDiseccion : MonoBehaviour {


    public GameObject Message;
    private GameObject MyMessage;

    void OnMouseEnter()
    {
        print("Se entro");
        var x = Input.mousePosition.x;
        var y = Input.mousePosition.y;
        MyMessage = Instantiate(Message) as GameObject;
        MyMessage.transform.SetParent(GameObject.Find("Canvas").transform, false);
        MyMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        MyMessage.GetComponentInChildren<Text>().text = SearchName(this.name);
    }

    void OnMouseExit()
    {
        if (MyMessage != null)
            Destroy(MyMessage);
    }

    string SearchName(string name)
    {
        //BUscar en xml y poner nomvbre
        return "";
    }
}
