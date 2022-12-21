using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //これ大事！


public class ClickImageScale : MonoBehaviour
{

    //UI関連
    Canvas canvas;
    RectTransform canvasRect;
    RectTransform photoSize;
    Vector2 targetPosition;

    ///拡大機能関連
    float effectDuration;
    float magnification;
    bool isZoomScale;

    void Start()
    {
        photoSize = GetComponent<RectTransform>();
        canvas = transform.parent.GetComponent<Canvas>();
        canvasRect = transform.parent.GetComponent<RectTransform>();

        //２秒間のアニメーションで実行しています！
        effectDuration = 2.0f;

        //拡大率は2.5倍に変更しています！
        magnification = 2.5f;
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
            photoSize.DOAnchorPos(-targetPosition, effectDuration);

            photoSize.DOScale(magnification, effectDuration);
            
        }
        else
        {
            photoSize.DOAnchorPos(new Vector2(0,0), effectDuration);

            photoSize.DOScale(1f, effectDuration);
        }

        isZoomScale = !isZoomScale;

    }

}
