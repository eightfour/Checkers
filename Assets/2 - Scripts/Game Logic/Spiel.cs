using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Logik
{
	public class Spiel : MonoBehaviour, iController
	{

        INetzwerk netzwerk = new NetzwerkImpl();

        List<string> messageQueue = new List<string>();

        Spieler self = null;

        
        public void Start()
        {
            CreateGame("Regelwerk1", 8, 8);

            if (NetworkModel.server)
            {
                netzwerk.Starten(this, OperationMode.Server);
                self = spielerSpeicher[0];
            }
            else if (NetworkModel.client)
            {
                netzwerk.Starten(this, OperationMode.Client);
                GameObject.Find("Main Camera").GetComponent<CameraControl>().SpinCamera();
                self = spielerSpeicher[1];
            }

            this.SetAktuellerSpieler(this.spielerSpeicher[0]);

            GameObject.Find("BlackCountMessage").GetComponent<Text>().text = "Schwarz: " + backCount;
            GameObject.Find("WhiteCountMessage").GetComponent<Text>().text = "Weiß: " + whiteCount;
        }

        void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				MenuBackground.SetActive(!MenuBackground.activeSelf);
				if(MenuBackground.activeSelf && GameObject.Find("VictoryMenu") != null)
				{
					GameObject.Find("VictoryMenu").SetActive(false);
				}
			}

            while (messageQueue.Count > 0)
            {
                //HandleMessage(messageQueue[0]);

                var received = messageQueue[0].TrimEnd(new[] { '>' });

                //koordinate hier raus
                var payload = received.Split(';');

                Debug.Log(payload);

                switch (payload[0])
                {
                    case "player":
                        //e. g. player;Tom
                        break;
                    case "move":
                        var coordinates = payload[1].ToCharArray();
                        foreach (var marker in GameObject.FindGameObjectsWithTag("MarkerAvailable"))
                        {
                            Destroy(marker);
                        }
                        MoveStein(brett.GetFelder()[Int32.Parse(coordinates[0].ToString()), Int32.Parse(coordinates[1].ToString())], brett.GetFelder()[Int32.Parse(coordinates[2].ToString()), Int32.Parse(coordinates[3].ToString())]);
                        break;
                    case "join":
                        Debug.Log("Client has joined the game!");
                        break;
                    default:
                        Debug.Log("Unknown payload");
                        break;
                }

                messageQueue.RemoveAt(0);
            }
		}

		public GameObject MenuBackground;


		private Spieler[] spielerSpeicher;
		private Brett brett;
		private Regelwerk regelwerk;
		private Spieler aktuellerSpieler;
		private Regelwerk[] regelwerkspeicher = { new Regelwerk1(), new Regelwerk2() };

		private int whiteCount = 12; // TODO Dynamisch mit anderem Regelwerk
		private int backCount = 12; // TODO Dynamisch mit anderem Regelwerk

		private Feld vonFeld;
		private Feld zuFeld;

		public Spiel()
		{
		}

		//Ziemlich zähe Sache - spontan nix besseres eingefallen
		public void VerteileSteine()
		{
			GameObject stein = Resources.Load("stein1") as GameObject;
			int höheSteineverteilung = this.brett.GetHöhe() / 2;
			int steinNr = 1;

			for (int y = 0; y < höheSteineverteilung - 1; y++)
			{
				for (int x = 0; x < this.brett.GetBreite(); x++)
				{
					if (this.brett.GetFelder()[x, y].GetFarbe() == Farbe.Schwarz && this.brett.GetFelder()[x, y].GetSteinBesetzung() == null)
					{
						GameObject s = Instantiate(stein) as GameObject;
						s.transform.parent = GameObject.Find("Brett").transform;
						s.name = "Stein " + steinNr + " (Weiß)";
						s.AddComponent<Stein>();
						s.GetComponent<Stein>().instantiate(Farbe.Weiß, (this.brett.GetFelder()[x, y]));
						this.brett.GetFelder()[x, y].SetSteinBesetzung(s);
                        steinNr++;
					}
				}
			}
			//das selbe für schwarz andersrum
			steinNr = 1;

			for (int y = this.brett.GetHöhe() - 1; y > höheSteineverteilung; y--) // <--anders zu oben
			{
				for (int x = 0; x < this.brett.GetBreite(); x++)
				{
					if (this.brett.GetFelder()[x, y].GetFarbe() == Farbe.Schwarz && this.brett.GetFelder()[x, y].GetSteinBesetzung() == null)
					{
						GameObject s = Instantiate(stein) as GameObject;
						s.transform.parent = GameObject.Find("Brett").transform;
						s.name = "Stein " + steinNr + " (Schwarz)";
						s.AddComponent<Stein>();
						s.GetComponent<Stein>().instantiate(Farbe.Schwarz, (this.brett.GetFelder()[x, y]));
						this.brett.GetFelder()[x, y].SetSteinBesetzung(s);
                        steinNr++;
					}
				}
			}
		}


		//Getter und Setter----------------------------
		public Spieler[] GetSpielerSpeicher()
		{
			return this.spielerSpeicher;
		}
		public Brett GetBrett()
		{
			return this.brett;
		}
		public Spieler GetAktuellerSpieler() {
			return this.aktuellerSpieler;
		}
		//---------------------------------------------
		public void SetSpielerSpeicher(int anzahlSteine)
		{
			if (anzahlSteine < 12)
			{
				throw new ArgumentOutOfRangeException("Jeder Spieler sollte zu Beginn mindestens 12 Steine haben!");
			}
			this.spielerSpeicher = new Spieler[2];
			this.spielerSpeicher[0] = new Spieler(Farbe.Weiß, anzahlSteine);
			this.spielerSpeicher[1] = new Spieler(Farbe.Schwarz, anzahlSteine);
		}
		public void SetBrett(Brett brett)
		{
			if (brett == null)
			{
				throw new ArgumentException("Brett darf nicht null sein");
			}
			this.brett = brett;
		}
		public void SetAktuellerSpieler(Spieler aktuellerSpieler) {
			if (aktuellerSpieler == null) {
				throw new ArgumentException("Der aktuelle Spieler muss gesetzt werden!");
			}
			this.aktuellerSpieler = aktuellerSpieler;

			GameObject.Find("CurrentPlayerMessage").GetComponent<Text>().text = "Am Zug: " + aktuellerSpieler.GetFarbe();

			// Mark all tokens that can move this turn
			MarkMovableTokens();
		}



		//creates a game
		public void CreateGame(String regelwerk, int brettBreite, int brettHöhe)
		{
			foreach (Regelwerk r in this.regelwerkspeicher) {
				if (r.GetName().Equals(regelwerk)) {
					this.regelwerk = r;
					break;
				}
			}

			if (this.regelwerk == null) {
				throw new ArgumentException("Unbekanntes Regelwerk");
			}

			this.SetBrett(gameObject.AddComponent<Brett>());
			this.brett.instantiate(brettBreite, brettHöhe);
			this.SetSpielerSpeicher(((brettBreite * brettHöhe) / 2 - brettBreite) / 2);//gibt SpielsteineAnzahl weiter
			this.VerteileSteine();
		}

		//Gibt Informationen zum aktuellen Spieler
		public String GetAktuellerSpielerInfo()
		{
			return ("Spieler " + this.aktuellerSpieler + " ist an der Reihe");
		}

		//wird verwendet um herauszufinden wer gewonnen hat
		public void HasWon()
		{
			Spieler gewinner = this.regelwerk.HasWon(this.spielerSpeicher, this.brett, this.aktuellerSpieler);
			if (gewinner != null)
			{
				GameObject.Find("Audio Source Victory").GetComponent<AudioSource>().Play();
				MenuBackground.SetActive(true);
				GameObject.Find("PauseMenu").SetActive(false);
				GameObject.Find("VictoryMenu").SetActive(true);
				GameObject.Find("VictoryMessage").GetComponent<Text>().text = "Glückwunsch!\n" + gewinner.GetFarbe() + " hat das Spiel gewonnen!";
			}
		}

		//wird verwendet um herauszufinden welche Regelwerke es gibt
		public String[] GetRegelwerke()
		{
			String[] s = new String[this.regelwerkspeicher.Length];
			int i = 0;
			foreach (Regelwerk r in this.regelwerkspeicher) {
				s[i] = r.GetName();
				i++;
			}

			return s;
		}

		//wird verwendet um den letzten Zug zu bekommen
		public String GetZuglog()
		{
			String s = "";
			//...
			return s;
		}

		//wird verwendet um das Spiel zu speichern
		public Boolean Save(String path)
		{
			return false;
		}

		//wird verwendet um ein Spiel  zu laden
		public Boolean Load(String path)
		{
			return false;
		}

		//wird verwendet um seinen Stein zu bewegen
		public Boolean MoveStein(Feld vonFeld, Feld zuFeld)
		{
			bool b = false;     //boolean zum ziehen
			List<Feld> c = null;     //boolean zum schlagen
			bool wirdDame = false;

			/*
				if(vonFeldX<0||vonFeldX>this.brett.GetBreite() || 
				   vonFeldY<0||vonFeldY>this.brett.GetHöhe() ||  unnötig
				   zuFeldX<0||zuFeldX>this.brett.GetBreite() || 
				   zuFeldY<0||zuFeldY>this.brett.GetHöhe()){
					b = false;  //Koordinaten stimmen nicht

				}else{ }
				*/


			/* TODO AUSKOMMENTIEREN      wird in regelwerk geregelt
			if (this.aktuellerSpieler.GetFarbe() != vonFeld.GetSteinBesetzung().GetComponent<Stein>().GetFarbe())
			{
				b = false;    //Spieler besitzt den Stein nicht bzw. den Stein auf diesem Feld gibt es nicht
			}
			else       
			{ }
			*/
			c = this.regelwerk.Schlagen(vonFeld, zuFeld, this.aktuellerSpieler, this.brett);      //darf bzw. will ich schlagen?
			if (c != null)      //schlagen des Gegners
			{
				b = true;   //stein wird nachher verlegt
				GameObject.Find("Audio Source Jump").GetComponent<AudioSource>().Play();
			}
			else    //sonst muss man prüfen ob es ein normaler zug war
			{
				b = this.regelwerk.MoveStein(vonFeld, zuFeld, this.aktuellerSpieler, this.brett);
				if(b){
					GameObject.Find("Audio Source Move").GetComponent<AudioSource>().Play();
				
				}
			}

			if (b)      //verlegen des Steins   //gueltiger Zug
			{
				


				zuFeld.SetSteinBesetzung(vonFeld.GetSteinBesetzung());   //setze Stein auf neues Feld
				vonFeld.GetSteinBesetzung().GetComponent<Stein>().SetFeld(zuFeld);    //setze neues Feld im Stein	
				vonFeld.SetSteinBesetzung(null);  //loesche Stein von altem Feld
                if (c != null)
                {
                    foreach (Feld f in c)
                    {
                        if (f.GetSteinBesetzung() != null)
                        {
                            //Debug.Log("Geschlagener Stein wird entfernt");
                            if (f.GetSteinBesetzung().GetComponent<Stein>().GetFarbe() == Farbe.Schwarz)
                            {
                                GameObject.Find("BlackCountMessage").GetComponent<Text>().text = "Schwarz: " + --this.backCount;
                            }
                            else
                            {
                                GameObject.Find("WhiteCountMessage").GetComponent<Text>().text = "Weiß: " + --this.whiteCount;
                            }

                            f.GetSteinBesetzung().GetComponent<Stein>().SetFeld(null);   //loeschen des Feldes des Steins
                            Destroy(f.GetSteinBesetzung().GetComponent<Stein>().transform.gameObject); // loeschen des Gameobjects
                            f.SetSteinBesetzung(null);  //loeschen des Steins auf dem Feld

                            //Debug.Log("Geschlagener Stein wurde entfernt");
                        }
                    }
				}
				wirdDame = this.regelwerk.WirdZuDame(zuFeld, this.aktuellerSpieler);
				if (wirdDame)
				{
					if (zuFeld.GetSteinBesetzung().GetComponent<Stein>().IsDame()){
						Debug.Log("Stein ist bereits Dame");
					} else {
						Debug.Log("Stein wird zu Dame");
						zuFeld.GetSteinBesetzung().GetComponent<Stein>().SetDame();

						GameObject.Find("Audio Source Dame").GetComponent<AudioSource>().Play();

						/*/ ALTERNATIVE: Beim erstellen der Steine auch gleich allen ein Dame-Modell geben, dieses dann hier sichtbar schalten
						Stein steinSkript = zuFeld.GetSteinBesetzung().GetComponent<Stein>(); // get old script
						GameObject dame = Instantiate(Resources.Load("Dame")) as GameObject; // load new model
						dame.AddComponent<Stein>(); // add old script to new model
						dame.GetComponent<Stein>().instantiate(steinSkript.GetFarbe(), steinSkript.GetFeld());
						dame.GetComponent<Stein>().SetDame(true);
						Destroy(zuFeld.GetSteinBesetzung()); // delete old model (and script)
						zuFeld.SetSteinBesetzung(null);
						zuFeld.SetSteinBesetzung(dame); // put model on the board
						*/
					}
				}



				this.HasWon();
				this.EndTurn();

                // Will break with board > 10
                // Example: move;7564
			}
			else
			{
				Debug.Log("Ungueltiger Zug");
			}

			return b;
		}

		//wird von neuen Spielern verwendet um sich einzuloggen
		public Boolean Login(String name)
		{

			bool b = false;
			foreach (Spieler s in spielerSpeicher) {
				if (s.GetName() == null) {
					s.SetName(name);
					b = true;
					break;
				}
			}
			return b;
		}

		void EndTurn()
		{
            //GameObject.Find("Main Camera").GetComponent<CameraControl>().SpinCamera();

            Debug.Log("Seitenwechsel");
			if (this.aktuellerSpieler == this.spielerSpeicher[0])
			{
				this.SetAktuellerSpieler(this.spielerSpeicher[1]);
			}
			else
			{
				this.SetAktuellerSpieler(this.spielerSpeicher[0]);
			}
		
		}

		private void MarkMovableTokens()
		{
            if (self == null || self.GetFarbe() == aktuellerSpieler.GetFarbe())
            {
                List<Feld> markerList = new List<Feld>();

                // Alle steine des aktuellen spielers zwischenspeichern
                foreach (Feld f in brett.GetFelder())
                {
                    // Stein vorhanden, steinfarbe == spielerfarbe
                    if (f.GetSteinBesetzung() != null && f.GetSteinBesetzung().GetComponent<Stein>().GetFarbe() == this.aktuellerSpieler.GetFarbe())
                    {
                        // Wenn dieser stein auch nur einen Zug hat, markieren lassen
                        if (this.regelwerk.CanTokenMove(f, this.GetAktuellerSpieler(), this.brett))
                        {
                            markerList.Add(f);
                        }
                    }
                }

                MarkFields(markerList, Tag.Verfuegbar);
            }
		}

		// Mouse clicked on the Board. Make clicked field either the start or target
		void OnMouseDown()
		{
			// Only react on Mouse click if the Pause menu is not open
			if (!MenuBackground.activeSelf && (self == null || (self.GetFarbe() == aktuellerSpieler.GetFarbe())))
			{
				float hitdistance = 0.0f;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Plane plane = new Plane(transform.up, transform.position);

				if (plane.Raycast(ray, out hitdistance))
				{
					Vector3 hitPoint = ray.GetPoint(hitdistance);
					Feld newFeld = this.brett.GetFelder()[(int)(hitPoint.x + 4), (int)(hitPoint.z + 4)];

					// 1. Start noch nicht ausgewählt
					if (vonFeld == null)
					{
						if (newFeld != null)
						{
							vonFeld = newFeld;

							// 1.1 Start enthät stein
							if (vonFeld.GetSteinBesetzung() != null)
							{

								if (vonFeld.GetSteinBesetzung().GetComponent<Stein>().GetFarbe() == aktuellerSpieler.GetFarbe())
								{
									Debug.Log("Startfeld gesetzt.");
									// Mark respective fields (Visual feedback)
									List<Feld> moves = this.regelwerk.GetMoves(vonFeld, this.GetAktuellerSpieler(), this.brett);
									MarkFields(new List<Feld> { vonFeld }, Tag.Start); // Starting field
									MarkFields(moves, Tag.Ziel); // Target fields

									// Lösche Marker (gelb)
									foreach (var marker in GameObject.FindGameObjectsWithTag("MarkerAvailable"))
									{
										Destroy(marker);
									}
								}
								else
								{
									Debug.Log("Dies ist kein gültiges Feld.");
									vonFeld = null;
								}

							}
							// 1.2 Start enthält keinen Stein
							else
							{
								Debug.Log("Auf diesem Feld befindet sich kein Spielstein.");
								vonFeld = null;
							}
						}
						else
						{
							Debug.Log("Auswahl ungültig.");
						}
					}

					// 2. Start ausgewählt, aber neue Auswahl == alte auswahl
					else if (newFeld.GetPositionX() == vonFeld.GetPositionX() && newFeld.GetPositionY() == vonFeld.GetPositionY())
					{
						vonFeld = null;
						Debug.Log("Auswahl deselektiert.");

						// Lösche Marker (grün, cyan)
						foreach (var marker in GameObject.FindGameObjectsWithTag("MarkerSelect"))
						{
							Destroy(marker);
						}

						// Erzeuge Marker (gelb)
						MarkMovableTokens();
					}

					// 3. Start ausgewählt, Ziel noch nicht ausgewählt
					else if (zuFeld == null)
					{
						zuFeld = this.brett.GetFelder()[(int)(hitPoint.x + 4), (int)(hitPoint.z + 4)];

						// 3.1 Ziel enthält keinen anderen Stein
						if (zuFeld.GetSteinBesetzung() == null)
						{
							if (zuFeld.GetFarbe() == Farbe.Schwarz)
							{
								Debug.Log("Zielfeld gesetzt.");
								if (MoveStein(vonFeld, zuFeld))
								{
                                    netzwerk.Send("move;" + vonFeld.GetPositionX() + "" + vonFeld.GetPositionY() + "" + zuFeld.GetPositionX() + "" + zuFeld.GetPositionY());

                                    vonFeld = zuFeld = null;

									// Lösche Marker (grün, cyan)
									foreach (var marker in GameObject.FindGameObjectsWithTag("MarkerSelect"))
									{
										Destroy(marker);
									}
								}
								else
								{
									zuFeld = null;
								}
							}
							else
							{
								Debug.Log("Dies ist kein gültiges Feld.");
								zuFeld = null;
							}
						}

						// 3.2 Ziel enthält einen Stein
						else
						{
							Debug.Log("Auf diesem Feld befindet sich bereits ein Spielstein.");
							zuFeld = null;
						}
					}
				}
			}
		}

		public void MarkFields(List<Feld> felder, Tag tag)
		{
			Material mat = (Material)Resources.Load("FieldMarker", typeof(Material));
			Color farbe;
			GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
			plane.transform.parent = GameObject.Find("Brett").transform;
			Destroy(plane.GetComponent<Collider>());

			switch (tag)
			{
				case Tag.Start:
					plane.tag = "MarkerSelect";
					plane.name = "MarkerStart";
					farbe = new Color(0, 0.75f, 0, 0.3f);
					break;
				case Tag.Ziel:
					plane.tag = "MarkerSelect"; // Gleicher Tag wie der Start-Marker, da beide immer nur gemeinsam auftreten und verschwinden.
					plane.name = "MarkerTarget";
					farbe = new Color(0, 0.5f, 0.5f, 0.3f);
					break;
				case Tag.Verfuegbar:
					plane.tag = "MarkerAvailable";
					plane.name = "MarkerAvailable";
					farbe = new Color(1, 1, 0, 0.3f);
					break;
				default:
					Debug.Log("Fehler bei der Feldermarkierung.");
					farbe = new Color(0, 0, 0, 0);
					break;
			}

			for (int i = 0; i < felder.Count; i++)
			{
				GameObject planeInstance = Instantiate(plane);			
				Renderer renderer = planeInstance.GetComponentInChildren<Renderer>();
		
				
				renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
				renderer.material = mat;
				renderer.material.SetColor("_Color", farbe);

				planeInstance.transform.localScale = new Vector3(0.08f, 1f , 0.08f);
				planeInstance.transform.position = new Vector3(felder[i].GetPositionX() - 3.5f, 0.01f , felder[i].GetPositionY() - 3.5f);

				Destroy(planeInstance.GetComponent<Collider>());
			}
			Destroy(plane);
		}

        public void ProcessNetworkPayload(string received)
        {
            messageQueue.Add(received);
        }
	}
}