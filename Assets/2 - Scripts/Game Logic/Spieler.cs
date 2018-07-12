using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Logik
{
    public class Spieler
    {
        private Stein[] steineSpeicher;
        private Farbe farbe;
        private String name;

        public Spieler(Farbe farbe,int anzahlSteine)
        {
            this.SetFarbe(farbe);
            this.SetSteine(anzahlSteine);
        }

        public override String ToString(){
            return (this.name +" mit "+ this.farbe);
        }

        //Getter und Setter----------------------------
        public Stein[] GetSteine()
        {
            return this.steineSpeicher;
        }
        public Farbe GetFarbe()
        {
            return this.farbe;
        }
        public String GetName()
        {
            return this.name;
        }
		//---------------------------------------------
		public void SetSteine(int anzahlSteine)
		{
			this.steineSpeicher = new Stein[anzahlSteine];
			for (int i = 0; i < this.steineSpeicher.Length; i++)
			{
		//		this.steineSpeicher[i] = new Stein(this.farbe);
			}
		}
		public void SetFarbe(Farbe farbe)
        {
            this.farbe = farbe;
        }
        public void SetName(String name)
        {
            if(name.Length > 15 || name.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Spielername zwischen 2 und 15 Zeichen!");
            }
            this.name = name;
        }
    }
}
