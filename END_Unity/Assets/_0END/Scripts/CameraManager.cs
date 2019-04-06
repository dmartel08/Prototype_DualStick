using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameManager gM;
    public Vector3 cam_offset;
    public Vector3 p1_offset;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
        cam_offset = new Vector3(0.0f, 5.75f, -4.25f);
        //p1_offset = gM.player1GO.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        p1_offset = gM.player1GO.transform.position;
        this.transform.position = new Vector3(p1_offset.x + cam_offset.x, p1_offset.y + cam_offset.y, p1_offset.z + cam_offset.z);
    }
}
