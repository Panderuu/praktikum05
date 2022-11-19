using System;
using System.Diagnostics;
using System.Formats.Asn1;

namespace praktikum;

class main
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Bitte Tag, Montat, Jahr für ein Datum eingeben!");
        Console.Write("Tag: ");
        int tag = Convert.ToInt32(Console.ReadLine());
        Console.Write("Monat: ");
        int monat = Convert.ToInt32(Console.ReadLine());
        Console.Write("Jahr: ");
        int jahr = Convert.ToInt32(Console.ReadLine());

        if (PruefeDatum(tag, monat, jahr) == false) //Wenn falsche eingabe
        {
            Console.WriteLine("ungültige Eingabe");
        }
        else
        {
            Console.WriteLine(Wochentag(tag, monat, jahr)); //Führt code aus.
            Morgen(ref tag, ref monat, ref jahr);
        }
    }
    static bool PruefeDatum(int t, int m, int j) //Prüft möglichkeiten, ob Datumseingabe richtig oder falsch ist und gibt das auch zurück
    {
        if (t > 31 || m > 12 || j < 1000 || t <= 0 || m <= 0 || j > 9999)
        {
            return false;
        }
        else if (m <= 7 && m % 2 == 0 && t == 31)
        {
            return false;
        }
        else if (m >= 8 && m % 2 == 1 && t == 31)
        {
            return false;
        }
        else if (IstSchaltjahr(j) == true && m == 2 && t > 29)
        {
            return false;
        }
        else if (IstSchaltjahr(j) == false && m == 2 && t >= 29)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    static bool IstSchaltjahr(int j)
    {
        if (j % 4 == 0) //Alle vier Jahre ist Schaltjahr
        {
            if (j % 100 == 0) //Alle Hundert Jahre ist kein Schaltjahr mehr
            {
                if (j % 400 == 0) //Alle 400 Jahre ist wieder ein Schaltjahr.
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        return false;
    }
    static void Morgen(ref int t, ref int m, ref int j)
    {
        string Schaltjahr = "(kein Schaltjahr)";
        if (IstSchaltjahr(j) == true)
        {
            Schaltjahr = "(ist Schaltjahr)";
        }
        Console.WriteLine($"{t}.{m}.{j} {Schaltjahr}"); //Gibt heutiges Datum aus
        t++; //erweitere Tag um 1
        if (PruefeDatum(t, m, j) == false) //Ist datum nicht korrekt nach anpassung
        {
            t = 1; //Setze tage auf 1 und passe Monate an
            m++;
            if (PruefeDatum(t, m, j) == false) //Prüfe nochmal, ob datum stimmt
            {
                m = 1; //wenn nicht setze monat auch auf 1 und passe Jahr um 1 an
                j++;
            }
        }
        Console.WriteLine($"Datum Morgen: {t}.{m}.{j}");
    }
    static string Wochentag(int t, int m, int j) //Berechnet mit gegebenen Alogrithmus den Wochentag
    {
        if (m != 1 && m != 2)
        {
            m -= 3;
        }
        else
        {
            m += 9;
            j--;
        }

        int jahrhundert = j / 100;
        int ja = j % 100;
        Console.WriteLine($"{jahrhundert} {ja} {t}");
        int tag = ((146097 * jahrhundert) / 4 + (1461 * ja) / 4 + (153 * m + 2) / 5 + t + 1721119) % 7;
        string strgTag;
        switch (tag)
        {
            case 0:
                strgTag = "Montag";
                break;
            case 1:
                strgTag = "Dienstag";
                break;
            case 2:
                strgTag = "Mittwoch";
                break;
            case 3:
                strgTag = "Donnerstag";
                break;
            case 4:
                strgTag = "Freitag";
                break;
            case 5:
                strgTag = "Samstag";
                break;
            case 6:
                strgTag = "Sonntag";
                break;
            default:
                return null;
        }
        return strgTag;
    }
}