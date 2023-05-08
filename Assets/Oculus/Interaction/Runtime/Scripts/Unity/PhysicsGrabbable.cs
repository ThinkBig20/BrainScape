/**
* @file PhysicsGrabbable.cs
* ce script  est un autre script fourni par Oculus qui permet de rendre un objet manipulable en VR, en utilisant la physique pour simuler les interactions avec l'utilisateur.
* Plus précisément, ce script ajoute des composants de rigidbody et de jointure (jointure de poignet) à l'objet, ce qui permet à l'utilisateur de le saisir et de le lâcher de manière réaliste. Il utilise également un système de détection de collision pour détecter quand l'utilisateur entre en contact avec l'objet, ce qui déclenche la possibilité de le saisir.
* on la modifie pour gerer la prise de l objet par l utilisateur selon la distance entre l objet et l utilisateur et 
* l envoie vers le basket cible 
*/

using System;
using UnityEngine;
using Scripts;


namespace Oculus.Interaction
{
    public class PhysicsGrabbable : MonoBehaviour
    {
        /// le grabbable objet que ce script gere
        [SerializeField]
        private Grabbable _grabbable;
        /// instance du game manager pour gerer les evenements
        GameManager manager;

        /// le rigidbody de l objet que ce script gere
        [SerializeField]
        private Rigidbody _rigidbody;
        /// la main gauche de l utilisateur
        [SerializeField]
        private GameObject leftHand;

        /// la main droite de l utilisateur
        [SerializeField]
        private GameObject rightHand;
        /// la distance entre l objet et l utilisateur pour que l objet soit pris
        public float catchRadius = 0.1f;
        /// variable pour verifier si l objet est pris ou pas
        private bool touched;

        [SerializeField]
        [Tooltip("If enabled, the object's mass will scale appropriately as the scale of the object changes.")]
        private bool _scaleMassWithSize = true;

        private bool _savedIsKinematicState = false;
        private bool _isBeingTransformed = false;
        private Vector3 _initialScale;
        private bool _hasPendingForce;
        private Vector3 _linearVelocity;
        private Vector3 _angularVelocity;
       



        protected bool _started = false;
        public event Action<Vector3, Vector3> WhenVelocitiesApplied = delegate { };

        /// cette fonction permet de reinitialiser les 2 objets grabbable et rigidbody associe a note gamecomponnent
        private void Reset()
        {
            _grabbable = this.GetComponent<Grabbable>();
            _rigidbody = this.GetComponent<Rigidbody>();
        }
        
        
        /// cette fonction de  unity permet de gerer les evenements de collision
        void Update(){
            /// variable pour verifier si l objet est proche de la main gauche de l utilisateur
            bool isLeftHandNear = Vector3.Distance(leftHand.transform.position, transform.position) < catchRadius;
            /// variable pour verifier si l objet est proche de la main droite de l utilisateur
            bool isRightHandNear = Vector3.Distance(rightHand.transform.position, transform.position) < catchRadius;

            if (isLeftHandNear || isRightHandNear)
            {   
                if(touched==false){
                    touched=true;
                    manager.OnSuccessfulCatch();
                    DisablePhysics();
                } 
                
            }
            
            if(touched)
            {
                /// instance du game object qui represente le Basket cible de l utilisateur
                GameObject basket = GameObject.FindWithTag("Basket");
                /// distance entre les mains et le basket
                float distanceHandBasket = Vector3.Distance(leftHand.transform.position,basket.transform.position);
                /// distance entre la balle et le basket
                float distanceBallBasket = Vector3.Distance(transform.position,basket.transform.position);
                if(distanceHandBasket<distanceBallBasket)
                {
                    manager.OnMissedThrow();
                    touched=false;
                }
            }

        }

        /// cette fonction de unity permet de faire les initialisations necessaires
        protected virtual void Start()
        {
            this.BeginStart(ref _started);
            this.AssertField(_grabbable, nameof(_grabbable));
            this.AssertField(_rigidbody, nameof(_rigidbody));
            this.EndStart(ref _started);
            touched=false;
            manager = FindObjectOfType<GameManager>();
            // lineRenderer = this.GetComponent<LineRenderer>();
        }

        /// cette fonction permet de gerer les evenements de collision entre l objet et d autres objets comme le plan et le basket
        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Basket") )
            {
                manager.OnSuccessfulThrow();
                DisablePhysics();
                Invoke("HideBall",2f);
            }
            if(collision.gameObject.CompareTag("Plan"))
            {
                DisablePhysics();
                manager.OnMissedCatch();
            }
        }


        /// cette fonction permet de cacher l objet apres un certain temps
        void HideBall(){
            gameObject.SetActive(false);
        }
        

        protected virtual void OnEnable()
        {
            if (_started)
            {
                _grabbable.WhenPointerEventRaised += HandlePointerEventRaised;
            }
        }

        protected virtual void OnDisable()
        {
            if (_started)
            {
                _grabbable.WhenPointerEventRaised -= HandlePointerEventRaised;
            }
        }
           
        /// cette fonction gere les evenements de prise et de lacher de l objet par l utilisateur
        private void HandlePointerEventRaised(PointerEvent evt)
        {
            switch (evt.Type)
            {
                case PointerEventType.Select:
                    if (_grabbable.SelectingPointsCount == 1 && !_isBeingTransformed)
                    {
                        DisablePhysics();
                    }
                    break;
                case PointerEventType.Unselect:
                    if (_grabbable.SelectingPointsCount == 0)
                    {
                        ReenablePhysics();
                        Vector3 throwDirection = transform.forward;
                        float throwForce = 5f; // Changer la valeur de la force de lancer selon votre besoin
                        // add a force to the rigidbody in the direction of the throw
                        _rigidbody.AddRelativeForce(throwDirection * throwForce, ForceMode.Impulse);
                    }
                    break;
            }
        }

        /// cette fonction permet de desactiver la physique de l objet (isKinematic = true)
        private void DisablePhysics()
        {
            _isBeingTransformed = true;
            CachePhysicsState();
            _rigidbody.isKinematic = true;
        }

        /// cette fonction permet le reactivation de la physique de l objet (isKinematic = false)
        private void ReenablePhysics()
        {
            _isBeingTransformed = false;
            // update the mass based on the scale change
            if (_scaleMassWithSize)
            {
                float initialScaledVolume = _initialScale.x * _initialScale.y * _initialScale.z;

                Vector3 currentScale = _rigidbody.transform.localScale;
                float currentScaledVolume = currentScale.x * currentScale.y * currentScale.z;

                float changeInMassFactor = currentScaledVolume / initialScaledVolume;
                _rigidbody.mass *= changeInMassFactor;
            }
            // revert the original kinematic state
            _rigidbody.isKinematic = _savedIsKinematicState;
        }

        public void ApplyVelocities(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            _hasPendingForce = true;
            _linearVelocity = linearVelocity;
            _angularVelocity = angularVelocity;
        }

        private void FixedUpdate()
        {
            if (_hasPendingForce)
            {
                _hasPendingForce = false;
                _rigidbody.AddForce(_linearVelocity, ForceMode.VelocityChange);
                _rigidbody.AddTorque(_angularVelocity, ForceMode.VelocityChange);
                WhenVelocitiesApplied(_linearVelocity, _angularVelocity);
            }
        }

        private void CachePhysicsState()
        {
            _savedIsKinematicState = _rigidbody.isKinematic;
            _initialScale = _rigidbody.transform.localScale;
        }

        #region Inject

        public void InjectAllPhysicsGrabbable(Grabbable grabbable, Rigidbody rigidbody)
        {
            InjectGrabbable(grabbable);
            InjectRigidbody(rigidbody);
        }

        public void InjectGrabbable(Grabbable grabbable)
        {
            _grabbable = grabbable;
        }

        public void InjectRigidbody(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void InjectOptionalScaleMassWithSize(bool scaleMassWithSize)
        {
            _scaleMassWithSize = scaleMassWithSize;
        }

        #endregion
    }
}
