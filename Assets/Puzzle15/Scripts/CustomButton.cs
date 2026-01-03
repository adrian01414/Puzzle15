using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Puzzle15
{
    [RequireComponent(typeof(Animator))]
    public abstract class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
    {
        public event Action OnButtonDown;
        public event Action OnButtonUp;

        [SerializeField] private AnimationClip _buttonUpAnimationClip;
        [SerializeField] private AnimationClip _buttonDownAnimationClip;

        private Animator _animator;

        protected bool _isEntered = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (_isEntered)
            {
                OnButtonDown?.Invoke();

                if (!_animator)
                    return;

                if (eventData.button != PointerEventData.InputButton.Left)
                    return;

                if (!_buttonDownAnimationClip)
                    return;

                _animator.Play(_buttonDownAnimationClip.name);
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (_isEntered)
                OnButtonUp?.Invoke();

            if (!_animator)
                return;

            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!_buttonUpAnimationClip)
                return;

            _animator.Play(_buttonUpAnimationClip.name);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isEntered = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isEntered = true;
        }
    }
}
