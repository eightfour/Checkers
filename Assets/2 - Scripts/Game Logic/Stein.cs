using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Logik
{
    public class Stein : MonoBehaviour
    {
		private Farbe farbe;
		private bool dame = false;
		private Feld feld;

        public void instantiate(Farbe farbe, Feld feld)
        {
            this.SetFarbe(farbe);
			this.SetFeld(feld);
          //  this.SetDame(false);
        }

        //Getter und Setter----------------------------
        public bool IsDame()
        {
            return this.dame;
        }
        public Feld GetFeld()
        {
            return this.feld;
        }
        public Farbe GetFarbe(){
            return this.farbe;
        }
        //---------------------------------------------
        public void SetDame()
        {
            this.dame = true;
            GameObject dame = Resources.Load("Dame") as GameObject;
            GameObject d = Instantiate(dame) as GameObject;
            Debug.Log("Stein Set inactive");
            this.gameObject.SetActive(false);
            Debug.Log("Dame Set active");
            d.AddComponent<Stein>();
            this.GetFeld().SetSteinBesetzung(null);
            this.GetFeld().SetSteinBesetzung(d);
            d.GetComponent<Stein>().instantiate(this.GetFarbe(), this.GetFeld());
            d.GetComponent<Stein>().dame = true;
            

            // TODO CREATE DAME MODEL HERE
        }
        public void SetFeld(Feld feld)
        {
            if (feld != null && feld.GetFarbe() != Farbe.Schwarz)
            {
                throw new ArgumentException("Steine können nur auf Schwarzen Feldern laufen");
            }
            this.feld = feld;
		}

        public void SetFarbe(Farbe farbe){
            this.farbe = farbe;
			if(this.farbe.Equals(Farbe.Schwarz)){
				Material matBlack = (Material)Resources.Load("Black", typeof(Material));
				GetComponentInChildren<Renderer>().material = matBlack;
			} else {
				Material matWhite = (Material)Resources.Load("White", typeof(Material));
				GetComponentInChildren<Renderer>().material = matWhite;
			}
		
		}

	}
}
