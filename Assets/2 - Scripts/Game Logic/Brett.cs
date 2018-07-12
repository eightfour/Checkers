using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Logik
{
    public class Brett : MonoBehaviour
	{
		//public GameObject spielbrett;

        private int breite;
        private int höhe;
        private Feld[,] felder;

        public void instantiate(int breite, int höhe)
        {
            if (breite != höhe)
            {
                throw new ArgumentException("Ein Dame-Brett muss quadratisch sein breite = höhe!");
            }
            this.SetBreite(breite);
            this.SetHöhe(höhe);
            this.SetFelder(breite,höhe);
        }


        //Getter und Setter----------------------------
        public int GetBreite()
        {
            return this.breite;
        }
        public int GetHöhe()
        {
            return this.höhe;
        }
        public Feld[,] GetFelder()
        {
            return this.felder;
        }
        //---------------------------------------------
        public void SetBreite(int breite)
        {
            if(breite < 8)
            {
                throw new ArgumentOutOfRangeException("Brett-Breite sollte nicht kleiner 8 sein!");
            }
            this.breite = breite;
        }
        public void SetHöhe(int höhe)
        {
            if(höhe < 8)
            {
                throw new ArgumentOutOfRangeException("Brett-Höhe sollte nicht kleiner 8 sein!");
            }
            this.höhe = höhe;
        }

        public void SetFelder(int breite, int höhe)
        {
            this.felder = new Feld[breite,höhe];
            for(int x=0; x < breite; x++)
            {
                for(int y=0; y<höhe; y++)
                {
                    this.felder[x,y] = new Feld(x,y);
                    //if(x gerade sette alle geraden y Schwarz)
                    if (x%2==0)
                    {
                        if (y%2==0)
                        {
                            this.felder[x, y].SetFarbe(Farbe.Schwarz);
                        }
                    }
                    //if(x ungerade sette alle ungeraden y Schwarz)
                    if (x%2!=0)
                    {
                        if (y%2!=0)
                        {
                            this.felder[x, y].SetFarbe(Farbe.Schwarz);
                        }
                    }
                }
            }
        }

    }
}
