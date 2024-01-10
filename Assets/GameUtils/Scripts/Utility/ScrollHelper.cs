using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public static class ScrollHelper {
    public static void ScrollTo(this ScrollRect scrollRect, GameObject gameObject) {
        scrollRect.content.anchoredPosition = scrollRect.GetAnchorPositionToScrollTo(gameObject);
    }

    public static Vector2 GetAnchorPositionToScrollTo(this ScrollRect scrollRect, GameObject gameObject) {
        RectTransform selectedRectTransform = gameObject.transform as RectTransform;
        float scrollViewMinY = scrollRect.content.anchoredPosition.y;
        float scrollViewMaxY = scrollRect.content.anchoredPosition.y + (scrollRect.transform as RectTransform).rect.height;
        float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + (selectedRectTransform.rect.height / 2);

        if (selectedPositionY > scrollViewMaxY) {
            float newY = selectedPositionY - (scrollRect.transform as RectTransform).rect.height;
            return new Vector2(scrollRect.content.anchoredPosition.x, newY);
        } else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY) {
            return new Vector2(scrollRect.content.anchoredPosition.x, Mathf.Abs(selectedRectTransform.anchoredPosition.y) - (selectedRectTransform.rect.height / 2));
        }
        return scrollRect.content.anchoredPosition;
    }
}
