using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PrimeTween;


[Serializable]
public class Item
{
    public Cell Cell { get; private set; }

    public Transform View { get; protected set; }

    public GetItemTexture itemTexture;

    public virtual void SetView()
    {
        itemTexture = Resources.Load<GetItemTexture>("FishTexture");
        string prefabname = GetPrefabName();

        if (!string.IsNullOrEmpty(prefabname))
        {
            GameObject prefab = Resources.Load<GameObject>(prefabname);
            if (prefab)
            {
                View = GameObject.Instantiate(prefab).transform;

            }
        }
    }

    protected virtual string GetPrefabName() { return string.Empty; }

    public virtual void SetCell(Cell cell)
    {
        Cell = cell;
    }

    internal void AnimationMoveToPosition()
    {
        if (View == null) return;

        Tween.Position(View, Cell.transform.position, 0.2f);
    }

    public void SetViewPosition(Vector3 pos)
    {
        if (View)
        {
            View.position = pos;
        }
    }

    public void SetViewRoot(Transform root)
    {
        if (View)
        {
            View.SetParent(root);
        }
    }

    public void SetSortingLayerHigher()
    {
        if (View == null) return;

        SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 1;
        }
    }


    public void SetSortingLayerLower()
    {
        if (View == null) return;

        SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 0;
        }

    }

    internal void ShowAppearAnimation()
    {
        if (View == null) return;

        Vector3 scale = View.localScale;
        View.localScale = Vector3.one * 0.1f;
        Tween.Scale(View, scale, 0.1f);
      
    }

    internal virtual bool IsSameType(Item other)
    {
        return false;
    }

    internal virtual void ExplodeView()
    {
        if (View)
        {

            Tween.Scale(View, 0.1f, 0.1f).OnComplete(
                () =>
                {
                    GameObject.Destroy(View.gameObject);
                    View = null;
                }
                );
        }
    }



    internal void AnimateForHint()
    {
        if (View)
        {
            Tween.Scale(View, 1f, 0.5f,1,Ease.Default,-1);
          //  View.DOPunchScale(View.localScale * 0.1f, 0.1f).SetLoops(-1);
        }
    }

    internal void StopAnimateForHint()
    {
        if (View)
        {
            Tween.StopAll(View);
            
        }
    }

    internal void Clear()
    {
        Cell = null;

        if (View)
        {
            GameObject.Destroy(View.gameObject);
            View = null;
        }
    }
}
