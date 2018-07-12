using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Logik
{
	public class Regelwerk1 : Regelwerk
	{
		public Regelwerk1()
		{

		}

		public override Boolean MoveStein(Feld vonFeld, Feld zuFeld, Spieler s, Brett brett)
		{
			bool b = false;
			//Debug.Log("Ist Dame: " + vonFeld.GetSteinBesetzung().GetComponent<Stein>().IsDame());


			if (zuFeld.GetFarbe() == Farbe.Schwarz)     //ist Zielfeld ueberhaupt Schwarz?
			{
				//DamenMove:
				if (vonFeld.GetSteinBesetzung().GetComponent<Stein>().IsDame()  //ist Stein Dame?
					&& s.GetFarbe() == vonFeld.GetSteinBesetzung().GetComponent<Stein>().GetFarbe())     //gehoert Stein zu aktuellem Spieler?
				{
					Debug.Log("Dame darf ziehen");
					for (int counter = 1; counter <= brett.GetBreite(); counter++)   //Zaehle Felder ab falls jemand dazwischen TODO: 8 muss noch an flexible Brettlaenge angepasst werden
					{
						Debug.Log("zaehlt: " + counter);
						/*if (vonFeld.GetPositionX() - counter < 0 || vonFeld.GetPositionY() - counter < 0
                            || vonFeld.GetPositionX() - counter > 8 || vonFeld.GetPositionY() - counter > 8)    //ist Zaehler noch auf dem Brett TODO: Brettgroesse
                        {
                            Debug.Log("Counter aus dem Zaehlerbereich");
                            break;
                        }*/
						if (zuFeld.GetPositionX() < vonFeld.GetPositionX() && zuFeld.GetPositionY() < vonFeld.GetPositionY())   //ist Feld links unten?
						{
							Debug.Log("Feld ist links unten");
							if (vonFeld.GetPositionX()-counter >= 0
                                && vonFeld.GetPositionY()-counter>=0
                                && brett.GetFelder()[vonFeld.GetPositionX() - counter, vonFeld.GetPositionY() - counter].GetSteinBesetzung() == null)  //ist etwas im Weg?
							{
								if (zuFeld.GetPositionX() == vonFeld.GetPositionX() - counter       //Stimmen zuFeldKoordinaten?
									&& zuFeld.GetPositionY() == vonFeld.GetPositionY() - counter)
								{
									Debug.Log("Dame zieht");
									b = true;
									break;
								}
							}
							else
							{
								Debug.Log("Stein im Weg");
								break;
							}

						}
						if (zuFeld.GetPositionX() > vonFeld.GetPositionX() && zuFeld.GetPositionY() > vonFeld.GetPositionY())   //ist Feld rechts oben?
						{
							Debug.Log("Feld ist rechts oben");
                            if (vonFeld.GetPositionX() + counter < brett.GetBreite()
                                && vonFeld.GetPositionY() + counter < brett.GetBreite()
                                && brett.GetFelder()[vonFeld.GetPositionX() + counter, vonFeld.GetPositionY() + counter].GetSteinBesetzung() == null)  //ist etwas im Weg?
							{
								if (zuFeld.GetPositionX() == vonFeld.GetPositionX() + counter       //Stimmen zuFeldKoordinaten?
									&& zuFeld.GetPositionY() == vonFeld.GetPositionY() + counter)
								{
									Debug.Log("Dame zieht");
									b = true;
									break;
								}
							}
							else
							{
								Debug.Log("Stein im Weg");
								break;
							}
						}
						else if (zuFeld.GetPositionX() < vonFeld.GetPositionX() && zuFeld.GetPositionY() > vonFeld.GetPositionY())   //ist Feld links oben?
						{
							Debug.Log("Feld ist links oben");
							if (vonFeld.GetPositionX() - counter >= 0
                                && vonFeld.GetPositionY() + counter < brett.GetBreite()
                                && brett.GetFelder()[vonFeld.GetPositionX() - counter, vonFeld.GetPositionY() + counter].GetSteinBesetzung() == null)  //ist etwas im Weg?
							{
								if (zuFeld.GetPositionX() == vonFeld.GetPositionX() - counter       //Stimmen zuFeldKoordinaten?
									&& zuFeld.GetPositionY() == vonFeld.GetPositionY() + counter)
								{
									Debug.Log("Dame zieht");
									b = true;
									break;
								}
							}
							else
							{
								Debug.Log("Stein im Weg");
								break;
							}
						}
						else if (zuFeld.GetPositionX() > vonFeld.GetPositionX() && zuFeld.GetPositionY() < vonFeld.GetPositionY())   //ist Feld rechts unten?
						{
							Debug.Log("Feld ist rechts unten");
							if (vonFeld.GetPositionX() + counter < brett.GetBreite()
                                && vonFeld.GetPositionY() - counter >= 0
                                && brett.GetFelder()[vonFeld.GetPositionX() + counter, vonFeld.GetPositionY() - counter].GetSteinBesetzung() == null)  //ist etwas im Weg?
							{
								if (zuFeld.GetPositionX() == vonFeld.GetPositionX() + counter       //Stimmen zuFeldKoordinaten?
									&& zuFeld.GetPositionY() == vonFeld.GetPositionY() - counter)
								{
									Debug.Log("Dame zieht");
									b = true;
									break;
								}
							}
							else
							{
								Debug.Log("Stein im Weg");
								break;
							}
						}
					}
				}

				//NormalerStein Move:
				else if (s.GetFarbe() == Farbe.Weiß
					&& vonFeld.GetSteinBesetzung().GetComponent<Stein>().GetFarbe() == Farbe.Weiß)  //fuer weissen Stein
				{
					if (zuFeld.GetSteinBesetzung() == null)
					{
						if (zuFeld.GetPositionX() == vonFeld.GetPositionX() - 1 && zuFeld.GetPositionY() == vonFeld.GetPositionY() + 1 || zuFeld.GetPositionX() == vonFeld.GetPositionX() + 1 && zuFeld.GetPositionY() == vonFeld.GetPositionY() + 1)   // das Feld links oben oder rechts oben
						{
							//Debug.Log("Weiß zieht");
							b = true;
						}
					}
				}
				else if (s.GetFarbe() == Farbe.Schwarz
					&& vonFeld.GetSteinBesetzung().GetComponent<Stein>().GetFarbe() == Farbe.Schwarz)    //fuer schwarzen Stein
				{
					if (zuFeld.GetSteinBesetzung() == null)
					{
						if (zuFeld.GetPositionX() == vonFeld.GetPositionX() - 1 && zuFeld.GetPositionY() == vonFeld.GetPositionY() - 1 || zuFeld.GetPositionX() == vonFeld.GetPositionX() + 1 && zuFeld.GetPositionY() == vonFeld.GetPositionY() - 1)   // das Feld links unten oder rechts unten
						{
							//Debug.Log("Schwarz zieht");
							b = true;
						}
					}
				}
			}
			return b;
		}

		public override Spieler HasWon(Spieler[] spielerspeicher, Brett brett, Spieler aktuellerSpieler)
		{
            Spieler next;
            if(aktuellerSpieler.GetFarbe() == spielerspeicher[0].GetFarbe())
            {
                next = spielerspeicher[1];
            } else
            {
                next = spielerspeicher[0];
            }
			// Nächster spieler kann nicht ziehen
			if (!CanPlayerMove(next, brett))  //haswon wird am ende des zuges geprüft somit ist der !aktuelleSpieler Zugunfähig wenn er an der Reihe ist
			{
				return aktuellerSpieler;
			}
			// Nachfolgender spieler kann noch ziehen
			else
			{
				return null;
			}
		}

		public override string GetName()
		{
			return "Regelwerk1";
		}

        private List<Feld> zuSchlagendeFelder;
        private bool schlagFlag;    //wird gesetzt wenn zuFeld mit einem der möglichen Felder die man durch Schlag erreichen kann übereinstimmt
        private bool doppelDameFlag;

        private GameObject startSteinBesetzung;

        public override List<Feld> Schlagen(Feld vonFeld, Feld zuFeld, Spieler s, Brett brett)
        {
            schlagFlag = false;
            doppelDameFlag = false;
            zuSchlagendeFelder = new List<Feld>();
            startSteinBesetzung = null;


            // Nur was machen, wenn auf dem startfeld wirklich ein stein liegt und auf dem Ziel keiner liegt
            if (vonFeld.GetSteinBesetzung() != null && zuFeld.GetSteinBesetzung() == null)
            {
                startSteinBesetzung = vonFeld.GetSteinBesetzung();
                this.Search(vonFeld, zuFeld, s, brett); //setzt zuSchlagendeFelder
                doppelDameFlag = false;
            }

            if (zuSchlagendeFelder.Count < 1 || zuSchlagendeFelder[0] == null)
            {
                zuSchlagendeFelder = null;
            }
            startSteinBesetzung = null;


            return zuSchlagendeFelder;
        }

        //setzt zuSchlagendeFelder
        void Search(Feld vonFeld, Feld zuFeld, Spieler s, Brett brett)
        {
            List<Feld> moeglicheFelder = this.GetSchlagZielfelder(vonFeld, s, brett); // auf welche Felder kann man von vonFeld Schlagen?

            if (moeglicheFelder != null)
            {
                foreach (Feld f in moeglicheFelder)
                {
                    Feld geschlagenesFeld = this.Schlag(vonFeld, f, s, brett);  //welches Feld wird geschlagen
                    zuSchlagendeFelder.Add(geschlagenesFeld);

                    if (f == zuFeld)
                    {
                        schlagFlag = true;  // wenn zuFeld gefunden wurd setze Flagge
                    }
                    else
                    {
                        this.Search(f, zuFeld, s, brett);
                        if (!schlagFlag)
                        {
                            doppelDameFlag = false;
                            zuSchlagendeFelder.RemoveAt(zuSchlagendeFelder.Count - 1);   //wenn zuFeld noch nicht gefunden wurde lösche alle Felderbis zur letzten Abzweigung (nehme nöchstes foreach)
                        }
                    }
                }
            }
        }

        //Gibt Liste mit Felder zurueck, welche nach einem Schlag von einem Feld erreicht werden koennen
        public List<Feld> GetSchlagZielfelder(Feld vonFeld, Spieler s, Brett brett)
        {
            List<Feld> felder = new List<Feld>();   //alle schwarzen Felder
            List<Feld> ziel = new List<Feld>();     //alle möglichen ziel Felder welche durch schlagen erreicht werden können

            //setzen der schwarzen Felder
            for (int x = 0; x < brett.GetBreite(); x++)
            {
                for (int y = 0; y < brett.GetHöhe(); y++)
                {
                    if (brett.GetFelder()[x, y].GetFarbe() == Farbe.Schwarz)
                    {
                        felder.Add(brett.GetFelder()[x, y]);
                    }
                }
            }

            //finden der möglichen zielfelder
            foreach (Feld zuFeld in felder)
            {
                // Nur was machen, wenn auf dem Ziel keiner liegt
                if (zuFeld.GetSteinBesetzung() == null)
                {
                    bool temp = doppelDameFlag;
                    Feld f = this.Schlag(vonFeld, zuFeld, s, brett);    //Gibt Feld zurück welches geschlagen wird von vonFeld zu zuFeld
                    doppelDameFlag = temp;
                    if (f != null)
                    {
                        ziel.Add(zuFeld);   //adde das Feld welches erreicht werden kann
                    }
                }
            }

            return ziel;    //Liste mit den Feldern auf, welche man ziehen kann
        }


        public Feld Schlag(Feld vonFeld, Feld zuFeld, Spieler s, Brett brett)
        {
            Feld zuSchlagen = null;


            if (!doppelDameFlag && startSteinBesetzung.GetComponent<Stein>().IsDame()
                && s.GetFarbe() == startSteinBesetzung.GetComponent<Stein>().GetFarbe())     //gehoert Stein zu aktuellem Spieler?
            {

                Debug.Log("Feld ist rechts oben");
                for (int counter = 1; counter < brett.GetBreite(); counter++)
                {
                    //rechts oben
                    if (vonFeld.GetPositionX() + counter < brett.GetBreite()
                        && vonFeld.GetPositionY() + counter < brett.GetBreite()
                        && brett.GetFelder()[vonFeld.GetPositionX() + counter, vonFeld.GetPositionY() + counter].GetSteinBesetzung() != null
                        && brett.GetFelder()[vonFeld.GetPositionX() + counter, vonFeld.GetPositionY() + counter].GetSteinBesetzung().GetComponent<Stein>().GetFarbe() != s.GetFarbe())
                    {
                        if (zuFeld.GetPositionX() == vonFeld.GetPositionX() + counter + 1      //Stimmen zuFeldKoordinaten?
                                && zuFeld.GetPositionY() == vonFeld.GetPositionY() + counter + 1
                                && (brett.GetFelder()[vonFeld.GetPositionX() + counter - 1, vonFeld.GetPositionY() + counter - 1].GetSteinBesetzung() == null
                                    || brett.GetFelder()[vonFeld.GetPositionX() + counter - 1, vonFeld.GetPositionY() + counter - 1].GetSteinBesetzung() == startSteinBesetzung))
                        {
                            Debug.Log("Dame schlägt");
                            zuSchlagen = brett.GetFelder()[vonFeld.GetPositionX() + counter, vonFeld.GetPositionY() + counter];
                            doppelDameFlag = true;
                        }
                        break;
                    }
                }
                for (int counter = 1; counter < brett.GetBreite(); counter++)
                {
                    //links unten
                    if (vonFeld.GetPositionX() - counter > 0
                        && vonFeld.GetPositionY() - counter > 0
                        && brett.GetFelder()[vonFeld.GetPositionX() - counter, vonFeld.GetPositionY() - counter].GetSteinBesetzung() != null
                        && brett.GetFelder()[vonFeld.GetPositionX() - counter, vonFeld.GetPositionY() - counter].GetSteinBesetzung().GetComponent<Stein>().GetFarbe() != s.GetFarbe())
                    {
                        if (zuFeld.GetPositionX() == vonFeld.GetPositionX() - counter - 1      //Stimmen zuFeldKoordinaten?
                                && zuFeld.GetPositionY() == vonFeld.GetPositionY() - counter - 1
                                && (brett.GetFelder()[vonFeld.GetPositionX() - counter + 1, vonFeld.GetPositionY() - counter + 1].GetSteinBesetzung() == null
                                    || brett.GetFelder()[vonFeld.GetPositionX() - counter + 1, vonFeld.GetPositionY() - counter + 1].GetSteinBesetzung() == startSteinBesetzung))
                        {
                            Debug.Log("Dame schlägt");
                            zuSchlagen = brett.GetFelder()[vonFeld.GetPositionX() - counter, vonFeld.GetPositionY() - counter];
                            doppelDameFlag = true;
                        }
                        break;
                    }
                }

                for (int counter = 1; counter < brett.GetBreite(); counter++)
                {
                    //links oben
                    if (vonFeld.GetPositionX() - counter > 0
                            && vonFeld.GetPositionY() + counter < brett.GetBreite()
                            && brett.GetFelder()[vonFeld.GetPositionX() - counter, vonFeld.GetPositionY() + counter].GetSteinBesetzung() != null
                            && brett.GetFelder()[vonFeld.GetPositionX() - counter, vonFeld.GetPositionY() + counter].GetSteinBesetzung().GetComponent<Stein>().GetFarbe() != s.GetFarbe())
                    {
                        if (zuFeld.GetPositionX() == vonFeld.GetPositionX() - counter - 1      //Stimmen zuFeldKoordinaten?
                                && zuFeld.GetPositionY() == vonFeld.GetPositionY() + counter + 1
                                && (brett.GetFelder()[vonFeld.GetPositionX() - counter + 1, vonFeld.GetPositionY() + counter - 1].GetSteinBesetzung() == null
                                    || brett.GetFelder()[vonFeld.GetPositionX() - counter + 1, vonFeld.GetPositionY() + counter - 1].GetSteinBesetzung() == startSteinBesetzung))
                        {
                            Debug.Log("Dame schlägt");
                            zuSchlagen = brett.GetFelder()[vonFeld.GetPositionX() - counter, vonFeld.GetPositionY() + counter];
                            doppelDameFlag = true;
                        }
                        break;
                    }
                }

                for (int counter = 1; counter < brett.GetBreite(); counter++)
                {
                    //rechts unten
                    if (vonFeld.GetPositionX() + counter < brett.GetBreite()
                            && vonFeld.GetPositionY() - counter > 0
                            && brett.GetFelder()[vonFeld.GetPositionX() + counter, vonFeld.GetPositionY() - counter].GetSteinBesetzung() != null
                            && brett.GetFelder()[vonFeld.GetPositionX() + counter, vonFeld.GetPositionY() - counter].GetSteinBesetzung().GetComponent<Stein>().GetFarbe() != s.GetFarbe())
                    {
                        if (zuFeld.GetPositionX() == vonFeld.GetPositionX() + counter + 1      //Stimmen zuFeldKoordinaten?
                                && zuFeld.GetPositionY() == vonFeld.GetPositionY() - counter - 1
                                && (brett.GetFelder()[vonFeld.GetPositionX() + counter - 1, vonFeld.GetPositionY() - counter + 1].GetSteinBesetzung() == null
                                    || brett.GetFelder()[vonFeld.GetPositionX() + counter - 1, vonFeld.GetPositionY() - counter + 1].GetSteinBesetzung() == startSteinBesetzung))
                        {
                            Debug.Log("Dame schlägt");
                            zuSchlagen = brett.GetFelder()[vonFeld.GetPositionX() + counter, vonFeld.GetPositionY() - counter];
                            doppelDameFlag = true;
                        }
                        break;
                    }
                }

            }

            else if (s.GetFarbe() == Farbe.Weiß
                && startSteinBesetzung.GetComponent<Stein>().GetFarbe() == Farbe.Weiß)
            {
                if (zuFeld.GetPositionX() == vonFeld.GetPositionX() - 2
                    && zuFeld.GetPositionY() == vonFeld.GetPositionY() + 2                                               //das Feld links oben
                    && brett.GetFelder()[vonFeld.GetPositionX() - 1, vonFeld.GetPositionY() + 1].GetSteinBesetzung() != null
                    && brett.GetFelder()[vonFeld.GetPositionX() - 1, vonFeld.GetPositionY() + 1].GetSteinBesetzung().GetComponent<Stein>().GetFarbe() == Farbe.Schwarz)     //ist dazwischen ein Gegner?
                {
                    Debug.Log("Weiß schlägt links oben");
                    zuSchlagen = brett.GetFelder()[vonFeld.GetPositionX() - 1, vonFeld.GetPositionY() + 1];
                }
                else if (zuFeld.GetPositionX() == vonFeld.GetPositionX() + 2
                    && zuFeld.GetPositionY() == vonFeld.GetPositionY() + 2                                              // das Feld rechts oben
                    && brett.GetFelder()[vonFeld.GetPositionX() + 1, vonFeld.GetPositionY() + 1].GetSteinBesetzung() != null
                    && brett.GetFelder()[vonFeld.GetPositionX() + 1, vonFeld.GetPositionY() + 1].GetSteinBesetzung().GetComponent<Stein>().GetFarbe() == Farbe.Schwarz)  //ist dazwischen ein Gegner?
                {
                    Debug.Log("Weiß schlägt rechts oben");
                    zuSchlagen = brett.GetFelder()[vonFeld.GetPositionX() + 1, vonFeld.GetPositionY() + 1];
                }
            }
            else if (s.GetFarbe() == Farbe.Schwarz
                && startSteinBesetzung.GetComponent<Stein>().GetFarbe() == Farbe.Schwarz)
            {
                if (zuFeld.GetPositionX() == vonFeld.GetPositionX() - 2
                    && zuFeld.GetPositionY() == vonFeld.GetPositionY() - 2                                                  //das Feld links unten
                    && brett.GetFelder()[vonFeld.GetPositionX() - 1, vonFeld.GetPositionY() - 1].GetSteinBesetzung() != null
                    && brett.GetFelder()[vonFeld.GetPositionX() - 1, vonFeld.GetPositionY() - 1].GetSteinBesetzung().GetComponent<Stein>().GetFarbe() == Farbe.Weiß)        //ist dazwischen ein Gegner?
                {
                    Debug.Log("Schwarz schlägt links unten");
                    zuSchlagen = brett.GetFelder()[vonFeld.GetPositionX() - 1, vonFeld.GetPositionY() - 1];
                }
                else if (zuFeld.GetPositionX() == vonFeld.GetPositionX() + 2
                    && zuFeld.GetPositionY() == vonFeld.GetPositionY() - 2                                                  // das Feld rechts unten
                    && brett.GetFelder()[vonFeld.GetPositionX() + 1, vonFeld.GetPositionY() - 1].GetSteinBesetzung() != null
                    && brett.GetFelder()[vonFeld.GetPositionX() + 1, vonFeld.GetPositionY() - 1].GetSteinBesetzung().GetComponent<Stein>().GetFarbe() == Farbe.Weiß)       //ist dazwischen ein Gegner?
                {
                    Debug.Log("Schwarz schlägt rechts unten");
                    zuSchlagen = brett.GetFelder()[vonFeld.GetPositionX() + 1, vonFeld.GetPositionY() - 1];
                }
            }

            return zuSchlagen;
        }

        public override Boolean WirdZuDame(Feld zuFeld, Spieler s)      //TODO: Werte müssen bei flexibler Brettgroesse angepasst werden
		{
			bool wirdDame = false;
			//Debug.Log("Wird Stein Dame?");
			if (s.GetFarbe() == Farbe.Weiß && zuFeld.GetPositionY() == 7
				|| s.GetFarbe() == Farbe.Schwarz && zuFeld.GetPositionY() == 0)
			{
				wirdDame = true;
			}

			return wirdDame;
		}

		// Checks if a player still has moves left
		public bool CanPlayerMove(Spieler s, Brett brett) {

            for(int x = 0; x < brett.GetComponent<Brett>().GetBreite(); x++)
            {
                for(int y = 0; y < brett.GetComponent<Brett>().GetHöhe(); y++)
                {
					GameObject stein = brett.GetFelder()[x, y].GetSteinBesetzung();

					if (stein != null)
                    {
						// Returns true as soon as a single token has a single valid move left
						if (CanTokenMove(stein.GetComponent<Stein>().GetFeld(), s, brett))
						{
							return true;
						}
                    }
                }
            }
			return false;
		}

		// Returns true if there is even one move available for this token
		// Verbesserte performanz mit Damen
		public override bool CanTokenMove(Feld vonFeld, Spieler spieler, Brett brett)
		{
			Feld[,] felder = brett.GetFelder(); // All fields on the board

			// Ineffiziente billig-Methode
			// 1. Fügt alle schwarzen felder der Ziel-Liste hinzu
			for (int i = 0; i < felder.GetLength(0); i++)
			{
				for (int j = 0; j < felder.GetLength(1); j++)
				{
					if ((((i % 2 == 0) && (j % 2 == 0)) || ((i % 2 == 1) && (j % 2 == 1))) && felder[i, j].GetSteinBesetzung() == null)
					{
						if (MoveStein(vonFeld, felder[i, j], spieler, brett) || Schlagen(vonFeld, felder[i, j], spieler, brett) != null)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Gets all possible moves for one token
		public override List<Feld> GetMoves(Feld vonFeld, Spieler spieler, Brett brett) {
			List <Feld> moves = new List<Feld>(); // List of possible targets

			Feld[,] felder = brett.GetFelder(); // All fields on the board

			List<Feld> ziele = new List<Feld>(); // All black fields on the board

			// Ineffiziente billig-Methode
			// 1. Fügt alle schwarzen felder der Ziel-Liste hinzu
			for(int i=0; i < felder.GetLength(0); i++){
				for(int j=0; j < felder.GetLength(1); j++){
					if((((i % 2 == 0) && (j % 2 == 0)) || ((i % 2 == 1) && (j % 2 == 1))) && felder[i,j].GetSteinBesetzung() == null)
					{
						ziele.Add(felder[i, j]);
					}
				}
			}

			// 2. Testet alle Zielfelder, ob mit diesem startfeld ein Zug gültig ist
			for (int i = 0; i < ziele.Count; i++)
			{
				// Wenn ein normaler zug oder schlagen möglich ist, ist dies ein gültiger zug.
				if (MoveStein(vonFeld, ziele[i], spieler, brett) || Schlagen(vonFeld, ziele[i], spieler, brett) != null)
				{
					// 3. Gültige Züge werden der moves-Liste hinzugefügt.
					moves.Add(ziele[i]);
				}
			}

			// can be empty
			return moves;
		}

    }
}
