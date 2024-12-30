using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    None,
    Bounce,
    Drop,
    Slide
}

public class UIBase : MonoBehaviour
{
    [SerializeField] private Transform animationRoot;


    public void Open()
    {
        Open(AnimationType.None);
    }

    public void Open(AnimationType type)
    {
        gameObject.SetActive(true);

        if (animationRoot == null)
        {
            OpenProcedure();
            return;
        }

        switch (type)
        {
            case AnimationType.Bounce:
                //animationRoot
                //    .DOScale(Vector3.one, 0.3f)
                //    .ChangeStartValue(Vector3.zero)
                //    .SetEase(Ease.OutBack)
                //    .OnComplete(OpenProcedure);
                break;

            default:
                OpenProcedure();
                break;
        }

    }

    public void Close()
    {
        Close(AnimationType.None);
    }

    public void Close(AnimationType type)
    {

        if (animationRoot == null)
        {
            CloseDone();
            return;
        }

        switch (type)
        {
            case AnimationType.Bounce:
                //animationRoot
                //    .DOScale(Vector3.zero, 0.3f)
                //    .SetEase(Ease.InBack)
                //    .OnComplete(CloseDone);
                break;

            default:
                CloseDone();
                break;

        }
    }

    void CloseDone()
    {
        gameObject.SetActive(false);
        CloseProcedure();
    }


    protected virtual void OpenProcedure()
    {

    }

    protected virtual void CloseProcedure()
    {

    }
}
