using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Scripts.Arrow
{
    public class ArrowMover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private ArrowType _type;

        private float _baseAngle = 0;
        private Vector2 _postion;

        public float Rotation => transform.eulerAngles.z;

        public event Action<float, ArrowType> Rotate = delegate { };

        public void OnBeginDrag(PointerEventData eventData)
        {
            _postion = transform.position;
            _postion = eventData.pressPosition - _postion;
            _baseAngle = Mathf.Atan2(_postion.y, _postion.x) * Mathf.Rad2Deg;
            _baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _postion = transform.position;
            _postion = eventData.position - _postion;
            float ang = Mathf.Atan2(_postion.y, _postion.x) * Mathf.Rad2Deg - _baseAngle;
            transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Rotate.Invoke(Rotation - 360f, _type);
        }
    }
}