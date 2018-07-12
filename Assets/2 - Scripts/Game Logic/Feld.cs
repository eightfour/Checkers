using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Logik
{
    public class Feld
	{
        private Farbe farbe;
        private int positionX;
        private int positionY;
        private GameObject steinBesetzung;

        public Feld(int positionX, int positionY)
        {
            this.SetPositionX(positionX);
            this.SetPositionY(positionY);
            this.SetFarbe(Farbe.Weiß);
        }

        //Getter und Setter----------------------------
        public int GetPositionX()
        {
            return this.positionX;
        }
        public int GetPositionY()
        {
            return this.positionY;
        }
        public Farbe GetFarbe()
        {
            return this.farbe;
        }
        public GameObject GetSteinBesetzung()
        {
            return this.steinBesetzung;
        }
        //---------------------------------------------
        public void SetPositionX(int positionX)
        {
            if(positionX < 0)
            {
                throw new ArgumentOutOfRangeException("Feldposition X darf nicht kleiner 0 sein!");
            }
            this.positionX = positionX;
        }
        public void SetPositionY(int positionY)
        {
            if(positionY < 0)
            {
                throw new ArgumentOutOfRangeException("Feldposition Y darf nicht kleiner 0 sein!");
            }
            this.positionY = positionY;
        }
        public void SetFarbe(Farbe farbe)
        {
            this.farbe = farbe;
        }
        public void SetSteinBesetzung(GameObject steinBesetzung)
        {
            if (this.steinBesetzung != null && steinBesetzung != null)
            {
				throw new ArgumentException("Dieses Feld ist schon besetzt, es kann kein weiterer Stein hier her!");
            }
            else
            {
                this.steinBesetzung = steinBesetzung;

                if (steinBesetzung != null)
                {
                    Debug.Log("Bewege Stein nach [" + positionX + ", " + positionY + "]");
                    steinBesetzung.transform.position = new Vector3(positionX - 3.5f, 0, positionY - 3.5f);
                }
            }
            
		}
    }
}
