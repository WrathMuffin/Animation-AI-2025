using System.Collections.Generic;
using UnityEngine;

public class SwitchView : MonoBehaviour
{
    public List<GameObject> myCams;
    public KeyCode switchCam = KeyCode.Space;

    private int camNum = 0;
    private bool[] isActive;

    // Temp script to switch view, may implenet multiple cams (more than two) later
    void Start()
    {
        myCams[camNum].SetActive(true);

        //isActive = new bool[myCams.Count]; 

        //isActive[0] = true;
        //isActive[1] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchCam))
        {
            if (camNum < myCams.Count)
            {
                camNum++;
            }

            if (camNum >= myCams.Count)
            {
                camNum = 0;
            }

            // if cam num != curr num, then set active to false
            for (int i = 0; i < myCams.Count; i++)
            {
                if (i != camNum)
                {
                    myCams[camNum].SetActive(false);
                }

                //    isActive[i] = !isActive[i];
                //    myCams[i].SetActive(isActive[i]);
            }

            myCams[camNum].SetActive(true);
        }
    }
}