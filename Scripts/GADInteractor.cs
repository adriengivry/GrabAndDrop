using UnityEngine;

namespace Givreton.GrabAndDrop
{
    public struct JointCreationSettings
    {
        public float drag;
        public float angularDrag;
        public float damper;
        public float spring;
        public float massScale;
        public float breakingDistance;
    }

    public class GADInteractor : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float m_grabMaxDistance = 2.5f;
        [SerializeField] private float m_jointAnchorDistance = 2.5f;
        [SerializeField] private float m_throwingForce = 10.0f;

        [Header("Joint Settings")]
        [SerializeField] private float m_drag = 10.0f;
        [SerializeField] private float m_angularDrag = 5.0f;
        [SerializeField] private float m_damper = 4.0f;
        [SerializeField] private float m_spring = 100.0f;
        [SerializeField] private float m_massScale = 1.0f;
        [SerializeField] private float m_breakingDistance = 3.0f;

        private bool m_locked = false;
        private GADInteractible m_target = null;

        private void Update()
        {
            if (m_locked)
            {
                Vector3 anchorPosition = transform.position + transform.forward * m_jointAnchorDistance;
                
                // If the interactible cannot follow anymore, force the dropping
                if (!m_target.Follow(anchorPosition))
                {
                    Drop();
                }
            }
        }

        public void Grab()
        {
            if (!m_locked)
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, m_grabMaxDistance))
                {
                    m_target = hitInfo.transform.GetComponent<GADInteractible>();

                    if (m_target)
                    {
                        // Lock th
                        m_target.Lock(new JointCreationSettings
                        {
                            drag = m_drag,
                            angularDrag = m_angularDrag,
                            damper = m_damper,
                            spring = m_spring,
                            massScale = m_massScale,
                            breakingDistance = m_breakingDistance
                        });
                        Debug.Log("Grabbing");
                        m_locked = true;
                    }
                }
                else
                {
                    Debug.Log("There is nothing to grab");

                }
            }
            else
            {
                 Debug.Log("You are already holding something");
            }
        }

        public void Drop(bool addForce = false)
        {
            if (m_locked)
            {
                m_target.Unlock();

                if (addForce)
                {
                    m_target.Push(transform.forward * m_throwingForce);
                }

                m_target = null;
                m_locked = false;
            }
            else
            {
                Debug.Log("You are not holding anything");
            }
        }

        public void Throw() => Drop(true);
    }
}
