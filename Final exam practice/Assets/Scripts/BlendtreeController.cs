using UnityEngine;
using DitzelGames.FastIK;

public class Animationcontroller : MonoBehaviour
{
    public Animator ani;
    [SerializeField] FastIKFabric iKFab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
            
        iKFab.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // axe pose
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ani.SetLayerWeight(2, 1f);
            ani.SetFloat("Blend", 0f);
        }

        // bow pose
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ani.SetLayerWeight(2, 1f);
            ani.SetFloat("Blend", 0.5f);
        }

        // sword pose
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ani.SetLayerWeight(2, 1f);
            ani.SetFloat("Blend", 1f);
        }

        // toggle IK
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            iKFab.enabled = !iKFab.enabled;
            ani.SetLayerWeight(2, 0f);
        }
    }
}