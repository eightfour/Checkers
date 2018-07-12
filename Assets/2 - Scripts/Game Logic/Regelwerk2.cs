using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Logik
{
	public class Regelwerk2 : Regelwerk
	{
		public Regelwerk2()
		{
		}

		public override Boolean MoveStein(Feld vonFeld, Feld zuFeld, Spieler s, Brett brett)
		{
			return false;
		}

		public override Spieler HasWon(Spieler[] spielerspeicher, Brett brett, Spieler aktuellerSpieler)
		{
			Spieler s = null;
			return s;
		}

		public override string GetName()
		{
			return "Regelwerk2";
		}

		public override List<Feld> Schlagen(Feld vonFeld, Feld zuFeld, Spieler s, Brett brett)
		{
			return null;
		}

		public override Boolean WirdZuDame(Feld zuFeld, Spieler s)
		{
			return false;
		}

		public override List<Feld> GetMoves(Feld vonFeld, Spieler spieler, Brett brett)
		{
			return null;
		}

		public override bool CanTokenMove(Feld vonFeld, Spieler spieler, Brett brett)
		{
			throw new NotImplementedException();
		}
	}
}
