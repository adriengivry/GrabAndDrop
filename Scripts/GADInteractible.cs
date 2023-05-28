using UnityEngine;

namespace Givreton.GrabAndDrop
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class GADInteractible : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float m_damperScale = 1.0f;
        [SerializeField] private float m_springScale = 1.0f;
        [SerializeField] private float m_dragScale = 1.0f;
        [SerializeField] private float m_angularDragScale = 1.0f;

        // Components References
        private Rigidbody m_rigidbody = null;
        private Joint m_joint = null;

        // Settings
        private float m_breakingDistance = 0.0f;

        // Joint Interaction Cache
        private float m_previousAngularDrag = 0.0f;
        private float m_previousDrag = 0.0f;
        private float m_previousSleepThreshold = 0.0f;
        private RigidbodyInterpolation m_previousInterpolation = RigidbodyInterpolation.None;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        public void Lock(JointCreationSettings jointCreationSettings)
        {
            // Resets rigidbody velocity
            m_rigidbody.velocity = Vector3.zero;
            m_rigidbody.angularVelocity = Vector3.zero;

            // Create the joint
            SpringJoint springJoint = gameObject.AddComponent<SpringJoint>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.minDistance = 0.0f;
            springJoint.maxDistance = 0.0f;
            springJoint.damper = jointCreationSettings.damper * m_damperScale;
            springJoint.spring = jointCreationSettings.spring * m_springScale;
            springJoint.massScale = jointCreationSettings.massScale;
            springJoint.anchor = Vector3.zero;

            m_previousAngularDrag = m_rigidbody.angularDrag;
            m_previousDrag = m_rigidbody.drag;
            m_previousSleepThreshold = m_rigidbody.sleepThreshold;
            m_previousInterpolation = m_rigidbody.interpolation;

            m_rigidbody.angularDrag = jointCreationSettings.angularDrag * m_angularDragScale;
            m_rigidbody.drag = jointCreationSettings.drag * m_dragScale;
            m_rigidbody.sleepThreshold = 0.0f;
            m_rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            m_breakingDistance = jointCreationSettings.breakingDistance;

            m_joint = springJoint;
        }

        public void Unlock()
        {
            Destroy(m_joint);

            m_joint = null;

            m_rigidbody.angularDrag = m_previousAngularDrag;
            m_rigidbody.drag = m_previousDrag;
            m_rigidbody.sleepThreshold = m_previousSleepThreshold;
            m_rigidbody.interpolation = m_previousInterpolation;
        }

        public bool Follow(Vector3 position)
        {
            m_joint.connectedAnchor = position;

            if (Vector3.Distance(transform.position, position) > m_breakingDistance)
            {
                return false;
            }

            return true;
        }

        public void Push(Vector3 force)
        {
            m_rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
