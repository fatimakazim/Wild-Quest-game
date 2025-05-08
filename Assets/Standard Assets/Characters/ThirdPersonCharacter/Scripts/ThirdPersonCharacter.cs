using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonCharacter : MonoBehaviour
    {
        [SerializeField] float m_MovingTurnSpeed = 360f;
        [SerializeField] float m_StationaryTurnSpeed = 180f;
        [SerializeField] float m_JumpPower = 12f;
        [Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
        [SerializeField] float m_RunCycleLegOffset = 0.2f;
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;
        public AudioClip jumpSound; // Drag your jump sound here in Inspector
        private AudioSource audioSource;
        // Air control
        [SerializeField] float m_AirControlAcceleration = 5f;
        [SerializeField] float m_MaxAirSpeed = 3f;

        Rigidbody m_Rigidbody;
        Animator m_Animator;
        bool m_IsGrounded;
        float m_OrigGroundCheckDistance;
        const float k_Half = 0.5f;
        float m_TurnAmount;
        float m_ForwardAmount;
        Vector3 m_GroundNormal;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        CapsuleCollider m_Capsule;
        bool m_Crouching;

        Vector3 m_CurrentMove;

        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX |
                                       RigidbodyConstraints.FreezeRotationY |
                                       RigidbodyConstraints.FreezeRotationZ;

            m_OrigGroundCheckDistance = m_GroundCheckDistance;
            audioSource=GetComponent<AudioSource>();
        }

        public void Move(Vector3 move, bool crouch, bool jump)
        {
            // reset ground-check distance each frame
            m_GroundCheckDistance = m_OrigGroundCheckDistance;

            // normalize & store input
            if (move.magnitude > 1f) move.Normalize();
            m_CurrentMove = move;

            // convert to local space and project onto ground
            move = transform.InverseTransformDirection(move);
            CheckGroundStatus();
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            ApplyExtraTurnRotation();

            if (m_IsGrounded)
                HandleGroundedMovement(crouch, jump);
            else
                HandleAirborneMovement();

            ScaleCapsuleForCrouching(crouch);
            PreventStandingInLowHeadroom();
            UpdateAnimator(move);
        }

        void HandleGroundedMovement(bool crouch, bool jump)
        {
            if (jump && !crouch && m_IsGrounded)
            {
                m_Rigidbody.velocity = new Vector3(
                    m_Rigidbody.velocity.x,
                    m_JumpPower,
                    m_Rigidbody.velocity.z);

                m_IsGrounded = false;
                m_Animator.applyRootMotion = false;
                m_GroundCheckDistance = 0.1f;

                if (jumpSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(jumpSound);
                }
            }
        }

        void HandleAirborneMovement()
        {
            // extra gravity
            Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
            m_Rigidbody.AddForce(extraGravityForce);

            // mild in-air steering
            Vector3 airMove = Vector3.ProjectOnPlane(m_CurrentMove, Vector3.up);
            m_Rigidbody.AddForce(
                airMove * m_AirControlAcceleration * m_MoveSpeedMultiplier,
                ForceMode.Acceleration
            );

            // clamp horizontal speed
            Vector3 vel = m_Rigidbody.velocity;
            Vector3 horiz = new Vector3(vel.x, 0f, vel.z);
            if (horiz.magnitude > m_MaxAirSpeed)
            {
                horiz = horiz.normalized * m_MaxAirSpeed;
                m_Rigidbody.velocity = new Vector3(horiz.x, vel.y, horiz.z);
            }

            m_GroundCheckDistance = (m_Rigidbody.velocity.y < 0f)
                ? m_OrigGroundCheckDistance
                : 0.01f;
        }

        void ScaleCapsuleForCrouching(bool crouch)
        {
            if (m_IsGrounded && crouch)
            {
                if (m_Crouching) return;
                m_Capsule.height = m_Capsule.height / 2f;
                m_Capsule.center = m_Capsule.center / 2f;
                m_Crouching = true;
            }
            else
            {
                Ray crouchRay = new Ray(
                    m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half,
                    Vector3.up
                );
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                if (Physics.SphereCast(
                        crouchRay,
                        m_Capsule.radius * k_Half,
                        crouchRayLength,
                        Physics.AllLayers,
                        QueryTriggerInteraction.Ignore))
                {
                    m_Crouching = true;
                    return;
                }
                m_Capsule.height = m_CapsuleHeight;
                m_Capsule.center = m_CapsuleCenter;
                m_Crouching = false;
            }
        }

        void PreventStandingInLowHeadroom()
        {
            if (!m_Crouching)
            {
                Ray crouchRay = new Ray(
                    m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half,
                    Vector3.up
                );
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                if (Physics.SphereCast(
                        crouchRay,
                        m_Capsule.radius * k_Half,
                        crouchRayLength,
                        Physics.AllLayers,
                        QueryTriggerInteraction.Ignore))
                {
                    m_Crouching = true;
                }
            }
        }

        void UpdateAnimator(Vector3 move)
        {
            m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
            m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
            m_Animator.SetBool("Crouch", m_Crouching);
            m_Animator.SetBool("OnGround", m_IsGrounded);
            if (!m_IsGrounded)
            {
                m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
            }

            float runCycle =
                Mathf.Repeat(
                    m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset,
                    1f);
            float jumpLeg = (runCycle < k_Half ? 1f : -1f) * m_ForwardAmount;
            if (m_IsGrounded)
            {
                m_Animator.SetFloat("JumpLeg", jumpLeg);
            }

            if (m_IsGrounded && move.magnitude > 0f)
                m_Animator.speed = m_AnimSpeedMultiplier;
            else
                m_Animator.speed = 1f;
        }

        void ApplyExtraTurnRotation()
        {
            float turnSpeed = Mathf.Lerp(
                m_StationaryTurnSpeed,
                m_MovingTurnSpeed,
                m_ForwardAmount
            );
            transform.Rotate(0f, m_TurnAmount * turnSpeed * Time.deltaTime, 0f);
        }

        public void OnAnimatorMove()
        {
            if (m_IsGrounded && Time.deltaTime > 0f)
            {
                Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
                v.y = m_Rigidbody.velocity.y;
                m_Rigidbody.velocity = v;
            }
        }

        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            Debug.DrawLine(
                transform.position + (Vector3.up * 0.1f),
                transform.position + (Vector3.up * 0.1f)
                + (Vector3.down * m_GroundCheckDistance)
            );
#endif
            if (Physics.Raycast(
                    transform.position + (Vector3.up * 0.1f),
                    Vector3.down,
                    out hitInfo,
                    m_GroundCheckDistance))
            {
                m_GroundNormal = hitInfo.normal;
                m_IsGrounded = true;
                m_Animator.applyRootMotion = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
                m_Animator.applyRootMotion = false;
            }
        }
        public void Collapse()
        {
            Debug.Log("Collapse triggered");

            // Disable animation and character control
            m_Animator.enabled = false;
            this.enabled = false;

            // Allow rotation and movement
            m_Rigidbody.constraints = RigidbodyConstraints.None;

            // Optional: Add torque for dramatic fall
            m_Rigidbody.AddTorque(Random.onUnitSphere * 200f);

            // Enable gravity
            m_Rigidbody.useGravity = true;
        }
    }
}
