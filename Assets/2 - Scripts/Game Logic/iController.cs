using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logik
{
    public interface iController
    {
        //creates a game
        void CreateGame(String regelwerk, int brettBreite, int brettHöhe);

        //wird verwendet um herauszufinden wer gewonnen hat
        void HasWon();

        //wird verwendet um herauszufinden welche Regelwerke es gibt
        String[] GetRegelwerke();

        //wird verwendet um den letzten Zug zu bekommen
        String GetZuglog();

        //wird verwendet um das Spiel zu speichern
        Boolean Save(String path);

        //wird verwendet um ein Spiel  zu laden
        Boolean Load(String path);

        //wird verwendet um seinen Stein zu bewegen
        Boolean MoveStein(Feld vonFeld, Feld zuFeld);

        //wird von neuen Spielern verwendet um sich einzuloggen
        Boolean Login(String name);
    }
}
