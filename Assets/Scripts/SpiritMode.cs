using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpiritMode : MonoBehaviour
{

    [SerializeField]
    private Sprite Hell1;
    [SerializeField]
    private Sprite Hell2;
    [SerializeField]
    private Sprite Hell3;
    [SerializeField]
    private Sprite Hell4;
    [SerializeField]
    private Sprite Hell5;
    [SerializeField]
    private Sprite Hell6;
    [SerializeField]
    private Sprite Hell7;
    [SerializeField]
    private Sprite Hell8;

    private Sprite[] Hell = new Sprite[8];
    private int i = 0;
    private int Count = 0;

    void Start()
    {
        Hell[0] = Hell1;
        Hell[1] = Hell2;
        Hell[2] = Hell3;
        Hell[3] = Hell4;
        Hell[4] = Hell5;
        Hell[5] = Hell6;
        Hell[6] = Hell7;
        Hell[7] = Hell8;
    }

    void Update()
    {
        if (Count % 4 == 0)
        {
            gameObject.GetComponent<Image>().sprite = Hell[i];
            i++;
            if (i > 7)
            {
                i = 0;
            }
        }
        Count++;
    }
}