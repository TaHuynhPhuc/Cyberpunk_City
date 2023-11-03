using System.Collections;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    public float scaleFactor = 1.5f; // Tỷ lệ tăng kích thước
    public float duration = 0.5f; // Thời gian để thực hiện hiệu ứng

    private Vector3 originalScale; // Kích thước gốc

    void Start()
    {
        originalScale = transform.localScale; // Lưu kích thước gốc
    }

    public void Enlarge()
    {
        StartCoroutine(ScaleOverTime(scaleFactor, duration)); // Gọi hàm ScaleOverTime với tỷ lệ tăng kích thước
    }

    public void Shrink()
    {
        StartCoroutine(ScaleOverTime(1.0f, duration)); // Gọi hàm ScaleOverTime với tỷ lệ gốc (1.0)
    }

    private IEnumerator ScaleOverTime(float targetScale, float time)
    {
        float elapsedTime = 0;
        Vector3 targetSize = originalScale * targetScale;

        while (elapsedTime < time)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetSize, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetSize; // Đảm bảo kích thước cuối cùng là kích thước mục tiêu
    }
}
