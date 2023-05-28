using UnityEngine;
using UnityEngine.InputSystem;

namespace Givreton.GrabAndDrop
{
    public class GADInputEmitter : MonoBehaviour
    {
        [SerializeField] private GADInteractor m_interactor = null;

#if ENABLE_INPUT_SYSTEM
        public void OnGrab(InputValue value)
        {
            if (value.isPressed)
            {
                m_interactor.Grab();
            }
        }

        public void OnDrop(InputValue value)
        {
            if (value.isPressed)
            {
                m_interactor.Drop();
            }
        }

        public void OnThrow(InputValue value)
        {
            if (value.isPressed)
            {
                m_interactor.Throw();
            }
        }
#else
        [SerializeField] private KeyCode m_grabKey = KeyCode.E;
        [SerializeField] private KeyCode m_dropKey = KeyCode.E;
        [SerializeField] private KeyCode m_throwKey = KeyCode.F;

        private void Update()
        {
            if (Input.GetKeyDown(m_throwKey))
            {
                m_interactor.Throw();
            }
            else if (Input.GetKeyDown(m_dropKey))
            {
                m_interactor.Drop();
            }
            else if (Input.GetKeyDown(m_grabKey))
            {
                m_interactor.Grab();
            }
        }
#endif
    }
}
