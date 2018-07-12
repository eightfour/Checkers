using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Logik
{
    public abstract class Regelwerk
    {
        public abstract Boolean MoveStein(Feld vonFeld, Feld zuFeld, Spieler s, Brett brett);

        public abstract Spieler HasWon(Spieler[] spielerspeicher, Brett brett, Spieler aktuellerSpieler);

        public abstract String GetName();

        public abstract List<Feld> Schlagen(Feld vonFeld, Feld zuFeld, Spieler s, Brett brett);

        public abstract Boolean WirdZuDame(Feld zuFeld, Spieler s);

		public abstract List<Feld> GetMoves(Feld vonFeld, Spieler spieler, Brett brett);

		// For performance
		public abstract bool CanTokenMove(Feld vonFeld, Spieler spieler, Brett brett);
	}
}
