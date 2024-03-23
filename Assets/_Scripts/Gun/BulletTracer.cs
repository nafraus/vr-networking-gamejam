using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracer : MonoBehaviour
{
    [SerializeField] private float secondsOfFullFade = 0.25f;
    [SerializeField] private float secondsToFadeOut = 0.5f;
    Material mat;
    LineRenderer line;

    public void Init(Vector3 startpos, Vector3 endpos)
    {
        line = GetComponent<LineRenderer>();
        mat = line.material;
        line.SetPosition(0, startpos);
        line.SetPosition(1, endpos);
        StartCoroutine(BulletCoroutine());
    }

    public IEnumerator BulletCoroutine()
    {
        yield return new WaitForSeconds(secondsOfFullFade);

        float maxalpha = mat.color.a;
        Color color = Color.white;

        while (true)
        {
            color.a -= Time.fixedDeltaTime * maxalpha / secondsToFadeOut;
            mat.color = color;

            if (color.a <= 0) break;

            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(DestroyGameObjectAfterSeconds(gameObject, 0.5f));
    }

    IEnumerator DestroyGameObjectAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
