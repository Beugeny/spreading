using UnityEditorInternal;
using UnityEngine;

namespace Src.utils
{
    public class ScaleController : MonoBehaviour
    {
        [SerializeField] public GameObject Target;
        private float _minScaleFactor;
        private float _maxScaleFactor;
        private RectTransform _rt;

        private void Start()
        {
            _minScaleFactor = 0.1f;
            _maxScaleFactor = 10f;

            _rt = Target.GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.mouseScrollDelta != Vector2.zero)
            {
                var sizeDelta = _rt.sizeDelta;
                var localScale = _rt.localScale;
                var w1=sizeDelta.x*localScale.x;
                var h1=sizeDelta.y*localScale.y;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_rt,Input.mousePosition,Camera.main,out var mouseLocalPoint);
                mouseLocalPoint=new Vector2(mouseLocalPoint.x*localScale.x,mouseLocalPoint.y*localScale.y);
                
                var d = Input.mouseScrollDelta;
                if (d.y > 0)
                {
                    var sf= Mathf.Min(Target.transform.localScale.x + 0.05f, _maxScaleFactor);
                    Target.transform.localScale=new Vector3(sf,sf,sf);
                }
                else if (d.y < 0)
                {
                    var sf= Mathf.Max(Target.transform.localScale.x - 0.05f, _minScaleFactor);
                    Target.transform.localScale=new Vector3(sf,sf,sf);
                }
                
                
                var newLocalScale = _rt.localScale;
                var w2=sizeDelta.x*newLocalScale.x;
                var h2=sizeDelta.y*newLocalScale.y;

                var deltaX = (w2 / w1 - 1) * mouseLocalPoint.x;
                var deltaY = (h2 / h1 - 1) * mouseLocalPoint.y;
                _rt.localPosition-=new Vector3(deltaX,deltaY);
            }
        }
    }
}