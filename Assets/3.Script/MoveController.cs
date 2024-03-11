using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Animator ani;
    UnicornController unicornController;

    bool isWalk;
    bool iscall;
    bool isRun;


    private void Start()
    {
        ani = GetComponent<Animator>();
        unicornController = FindObjectOfType<UnicornController>();
    }

    private void OnEnable()
    {
        ani.SetInteger("animation", (int)0);
        isWalk = false;
        iscall = false;
        isRun = false;
    }

    private void Update()
    {
        if (unicornController.isWalk && unicornController.States == 6 && !isWalk) 
        {
            iscall = false;
            isRun = false;

            isWalk = true;
            ani.SetInteger("animation", (int)0);
            ani.SetInteger("animation", (int)6);
        }

        else if (unicornController.isWalk && unicornController.States == 3 && !iscall)
        {
            isWalk = false;
            isRun = false;
            iscall = true;

            ani.SetInteger("animation", (int)0);
            ani.SetInteger("animation", (int)3);
            unicornController.States = 0;
            StartCoroutine(Call_co());
        }

        else if (unicornController.isWalk && unicornController.States == 5 && !isRun)
        {
            isWalk = false;
            iscall = false;
            isRun = true;

            ani.SetInteger("animation", (int)0);
            ani.SetInteger("animation", (int)5);
        }

    }

    IEnumerator Call_co()
    {
        yield return new WaitForSeconds(1.2f);

        ani.SetInteger("animation", (int)2);
    }
}
