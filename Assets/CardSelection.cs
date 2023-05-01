using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrainScape
{
    public class CardSelection : MonoBehaviour, XRBaseInteractable
    {

        private XRBaseInteractable baseInteractable;

        // Start is called before the first frame update
        void Start()
        {
            baseInteractable = GetComponent<XRBaseInteractable>();

            //ajouter un écouteur pour l'événement "select entered"
            baseInteractable.selectEntered.AddListener(onSelectEntered);
        }

        public void onSelectEntered(XRBaseInteractor interactor)
        {
            //verifier si la main de l'utilisateur est à proximité de la carte
            //Vector3.Distance pour mesurer la distance entre la main et la carte

            if (Vector3.Distance(interactor.transform.position, transform.position) < 0.1f)
            {
                //Déclenche une action de sélection de la carte
                SelectCard();
            }

            
            private void SelectCard()
            {
                GetComponent<Renderer>().material.color = Color.yellow;
            }

            //lorsque la main quitte la carte
            public void onSelectExited(XRBaseInteractor interactor)
            {
                 // Remet la couleur de la carte à sa couleur d'origine
                GetComponent<Renderer>().material.color = Color.white;
            }
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
