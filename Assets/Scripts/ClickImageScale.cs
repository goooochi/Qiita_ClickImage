using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //これ大事！


public class ClickImageScale : MonoBehaviour
{
    Canvas canvas;
    RectTransform canvasRect;
    RectTransform photoSize;
    Vector2 targetPosition;
    bool isZoomScale;

    void Start()
    {
        photoSize = GetComponent<RectTransform>();
        canvas = transform.parent.GetComponent<Canvas>();
        canvasRect = transform.parent.GetComponent<RectTransform>();

    }

    public void ScaleUp()
    {
        // クリックしたスクリーン座標
        var screenPoint = Input.mousePosition;
        var camera = canvas.worldCamera;

        // Overlayの場合はScreenPointToLocalPointInRectangleにnullを渡さないといけない
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            camera = null;
        }

        // クリック位置に対応するRectTransformのlocalPositionを計算する
        targetPosition = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, screenPoint, camera, out targetPosition);

        Debug.Log("クリック座標 : " + targetPosition);

        ScaleControl();
        
    }

    void ScaleControl()
    {
        if (!isZoomScale)
        {
            photoSize.DOAnchorPos(-targetPosition, 2f);

            photoSize.DOScale(2.5f, 2f);
            
        }
        else
        {
            photoSize.DOAnchorPos(new Vector2(0,0), 2f);

            photoSize.DOScale(1f, 2f);
        }

        isZoomScale = !isZoomScale;

    }

}
