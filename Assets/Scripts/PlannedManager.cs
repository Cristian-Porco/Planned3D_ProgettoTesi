using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlannedManager
{
    private PlannedManager() { }

    private static PlannedManager _instance;

    public int pareteStanza1 = 0;
    public int pavimentoStanza1 = 0;

    public int pareteStanza2 = 0;
    public int pavimentoStanza2 = 0;

    public int pareteStanza3 = 0;
    public int pavimentoStanza3 = 0;

    public int pareteStanza4 = 0;
    public int pavimentoStanza4 = 0;

    public int pareteStanza5 = 0;
    public int pavimentoStanza5 = 0;

    public int pareteStanza6 = 0;
    public int pavimentoStanza6 = 0;

    public int cornicePorte = 0;
    public int antaPorte = 0;

    public int corniceFinestre = 0;
    public int davanzaleFinestre = 0;

    public int cornicePortone = 0;
    public int antaPortone = 0;


    public static PlannedManager getInstance() {
        if (_instance == null)
            _instance = new PlannedManager();
        return _instance;
    }


    public void setValori(int pos, int valore) {
        switch (pos) {
            case 0: pareteStanza1 = valore; break;
            case 1: pavimentoStanza1 = valore; break;
            case 2: pareteStanza2 = valore; break;
            case 3: pavimentoStanza2 = valore; break;
            case 4: pareteStanza3 = valore; break;
            case 5: pavimentoStanza3 = valore; break;
            case 6: pareteStanza4 = valore; break;
            case 7: pavimentoStanza4 = valore; break;
            case 8: pareteStanza5 = valore; break;
            case 9: pavimentoStanza5 = valore; break;
            case 10: pareteStanza6 = valore; break;
            case 11: pavimentoStanza6 = valore; break;
            case 12: cornicePorte = valore; break;
            case 13: antaPorte = valore; break;
            case 14: corniceFinestre = valore; break;
            case 15: davanzaleFinestre = valore; break;
            case 16: cornicePortone = valore; break;
            case 17: antaPortone = valore; break;
        }
    }


    public int getValori(int pos) {
        switch (pos) {
            case 0: return pareteStanza1; 
            case 1: return pavimentoStanza1; 
            case 2: return pareteStanza2; 
            case 3: return pavimentoStanza2; 
            case 4: return pareteStanza3; 
            case 5: return pavimentoStanza3; 
            case 6: return pareteStanza4; 
            case 7: return pavimentoStanza4; 
            case 8: return pareteStanza5; 
            case 9: return pavimentoStanza5; 
            case 10: return pareteStanza6; 
            case 11: return pavimentoStanza6; 
            case 12: return cornicePorte; 
            case 13: return antaPorte; 
            case 14: return corniceFinestre; 
            case 15: return davanzaleFinestre; 
            case 16: return cornicePortone; 
            case 17: return antaPortone; 
        }
        return 0;
    }
}
