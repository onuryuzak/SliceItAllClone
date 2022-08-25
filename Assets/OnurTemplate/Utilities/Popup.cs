using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Onur.Template.UI {
    public class Popup : MonoBehaviour
    {
        #region Inspector Variables

        [SerializeField] private bool canAndroidBackClose = true;

        [Header("Anim Settings")]
        [SerializeField] protected float animDuration;
        [SerializeField] protected float animClosedDuration;
        [SerializeField] protected Ease animOpening;
        [SerializeField] protected Ease animClosing;
        [SerializeField] protected RectTransform animContainer;

        #endregion

        #region Properties

        public bool CanAndroidBackClose { get { return canAndroidBackClose; } }

        #endregion

        #region Member Variables

        private PopupClosed callback;

        #endregion

        #region Delegates

        public delegate void PopupClosed(bool cancelled, object[] outData);

        #endregion
    
        #region Public Methods

        public virtual void Initialize()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            Show(null, null);
        }

        public bool Show(object[] inData, PopupClosed callback)
        {
            this.callback = callback;

            // Show the popup object
            gameObject.SetActive(true);
            gameObject.transform.SetAsLastSibling();

            animContainer.localScale = Vector3.one * .2f;

            animContainer.DOScale(1, animDuration).SetEase(animOpening);

            OnShowing(inData);

            return true;
        }
    
        public void Hide(bool cancelled)
        {
            Hide(cancelled, null);
        }

        public void Hide(bool cancelled, object[] outData)
        {

            animContainer.DOScale(Vector3.one * .1f, animClosedDuration).SetEase(animClosing).OnComplete(()=> {
                gameObject.SetActive(false);
            });

            callback?.Invoke(cancelled, outData);

            OnHiding();
        }

        public virtual void OnShowing(object[] inData)
        {

        }

        public virtual void OnHiding()
        {
            PopupManager.instance.OnPopupHiding(this);
        }
        #endregion
    }
}
