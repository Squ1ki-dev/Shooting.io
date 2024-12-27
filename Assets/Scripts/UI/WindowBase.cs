using System.Collections;
using UnityEngine;

public abstract class WindowBase : MonoBehaviour
{
    public virtual void Close()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void Open()
    {
        this.gameObject.SetActive(true);
    }
}

