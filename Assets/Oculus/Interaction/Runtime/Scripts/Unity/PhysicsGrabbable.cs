/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using UnityEngine;
using Scripts;


namespace Oculus.Interaction
{
    public class PhysicsGrabbable : MonoBehaviour
    {
        [SerializeField]
        private Grabbable _grabbable;
        GameManager manager;
        public string StuckObject="Wall";

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private GameObject leftHand;
        [SerializeField]
        private GameObject rightHand;
        public float catchRadius = 0.4f;
        private bool touched;
      

        
        // LineRenderer lineRenderer;
        

        [SerializeField]
        [Tooltip("If enabled, the object's mass will scale appropriately as the scale of the object changes.")]
        private bool _scaleMassWithSize = true;

        private bool _savedIsKinematicState = false;
        private bool _isBeingTransformed = false;
        private Vector3 _initialScale;
        private bool _hasPendingForce;
        private Vector3 _linearVelocity;
        private Vector3 _angularVelocity;

       

        // number of points on the line
       // public float numPoints = 20;

        // distance between points
        // public float pointDistance = 1;


        protected bool _started = false;
        public event Action<Vector3, Vector3> WhenVelocitiesApplied = delegate { };

        private void Reset()
        {
            _grabbable = this.GetComponent<Grabbable>();
            _rigidbody = this.GetComponent<Rigidbody>();
        }
        
        

        void Update(){
            bool isLeftHandNear = Vector3.Distance(leftHand.transform.position, transform.position) < catchRadius;
            bool isRightHandNear = Vector3.Distance(rightHand.transform.position, transform.position) < catchRadius;

            if (isLeftHandNear || isRightHandNear)
            {   
                if(touched==false){
                    touched=true;
                    manager.OnSuccessfulCatch();
                    DisablePhysics();
                } 
                
            }
            else if(!touched && transform.position.z<0)
            {
                touched=false;
                manager.OnMissedCatch();
            }
            GameObject basket = GameObject.FindWithTag("Basket");
            if(basket!=null){
                if(touched && transform.position.z<0)
                {
                    manager.OnMissedThrow();
                }
            }
        }

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

        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Basket") )
            {
                manager.OnSuccessfulThrow();
                DisablePhysics();
                Invoke("HideBall",2f);
            }
        }

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

        private void DisablePhysics()
        {
            _isBeingTransformed = true;
            CachePhysicsState();
            _rigidbody.isKinematic = true;
        }

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
