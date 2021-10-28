using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuitLink : MonoBehaviour
{
    public void Quit() {
        ApplicationQuitController.Instance.Quit();
    }
}
