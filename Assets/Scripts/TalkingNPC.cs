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
    Object clone1;
    Object clone2;
    Object clone3;

    void Start()
    {
        Text.GetComponent<TextMesh>().text = Say;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Human_Prefab")
        {
            TextBox.transform.localScale += new Vector3(0.25f * Say.Length, 0, 0);
            clone1 = Instantiate(TextBox, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, 1), Quaternion.identity);
            clone2 = Instantiate(CharFollower, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f, 1), Quaternion.AngleAxis(45, Vector3.forward));
            clone3 = Instantiate(Text, new Vector3(gameObject.transform.position.x - (Say.Length / 8), gameObject.transform.position.y + 2.1f, 0.5f), Quaternion.identity);
        }
        }

    void OnTriggerExit(Collider other)
    {
            if (other.name == "Human_Prefab")
            {
                Destroy(clone1);
                Destroy(clone2);
                Destroy(clone3);
                TextBox.transform.localScale = new Vector3(1, 1, 0.0001f);
            }
    }
}
