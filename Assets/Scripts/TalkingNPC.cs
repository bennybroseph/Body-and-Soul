using UnityEngine;
using System.Collections;

public class TalkingNPC : MonoBehaviour {

    [SerializeField]
    GameObject Text;
    [SerializeField]
    GameObject TextBox;
    [SerializeField]
    GameObject CharFollower;
    [SerializeField]
    string Say;
    GameObject clone1;
    GameObject clone2;
    GameObject clone3;

    void Start()
    {
        Text.GetComponent<TextMesh>().text = Say;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Human_Prefab" && (clone1 == null && clone2 == null && clone3 == null))
        {
            TextBox.transform.localScale += new Vector3(0.25f * Say.Length, 0, 0);
            clone1 = Instantiate(TextBox, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, 1), Quaternion.identity) as GameObject;
            clone2 = Instantiate(CharFollower, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f, 1), Quaternion.AngleAxis(45, Vector3.forward)) as GameObject;
            clone3 = Instantiate(Text, new Vector3(gameObject.transform.position.x - (Say.Length / 8), gameObject.transform.position.y + 2.1f, 0.5f), Quaternion.identity) as GameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
            if (other.name == "Human_Prefab")
            {
                Destroy(clone1.gameObject);
                Destroy(clone2.gameObject);
                Destroy(clone3.gameObject);
                TextBox.transform.localScale = new Vector3(1, 1, 0.0001f);
            }
    }
}
