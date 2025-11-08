using UnityEngine;

public class Animationcontroller : MonoBehaviour
{
    public Animator ani;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // axe pose
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ani.SetFloat("Blend", 0f);
        }

        // bow pose
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ani.SetFloat("Blend", 0.5f);
        }

        // sword pose
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ani.SetFloat("Blend", 1f);
        }
    }
}
