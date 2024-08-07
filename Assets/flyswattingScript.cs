using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class flyswattingScript : MonoBehaviour
{

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMRuleSeedable RuleSeedable;

    public TextMesh[] NumberTexts;
    public TextMesh[] FlyTexts;
    public KMSelectable[] Flies;
    public GameObject[] FlyObjects;
    public GameObject[] FlyBodies;
    public GameObject[] FuckedFlies;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    private int ix, ord;
    private string numbers, letters;
    private int[] ixs;
    private string bet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string[] chosens = { "", "", "", "", "" };
    private int[] answers = { 0, 0, 0, 0, 0 };
    private float[] flyX = { 0f, -0.0449f, 0.0449f, -0.0253f, 0.0253f };
    private float[] flyZ = { 0.0457f, 0.0086f, 0.0086f, -0.041f, -0.041f };
    private bool[] swatted = { false, false, false, false, false };
    private float[] light = { 0.075167f, 0.076057f };
    private int[] status = { 0, 0, 0, 0, 0 };

    void Awake()
    {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable Fly in Flies)
        {
            Fly.OnInteract += delegate () { FlyPress(Fly); return false; };
        }

    }

    // Use this for initialization
    void Start()
    {
        var rnd = RuleSeedable.GetRNG();
        if (rnd.Seed != 1)
            Debug.LogFormat("[Flyswatting #{0}] Using rule seed {1}.", moduleId, rnd.Seed);
        var alphabet = "L       CW          TG  Z    U    B   H  D Y  F   QN   V   EA       K  X SM  P  IRJ      O          ".ToCharArray();
        rnd.ShuffleFisherYates(alphabet);
        NewNumbers:
        ix = UnityEngine.Random.Range(0, 110);
        switch (ix)
        {
            case 0: numbers = "013"; ixs = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 53 }; break;
            case 1: numbers = "014"; ixs = new[] { 0, 1, 2, 3, 4, 16, 49, 50, 51, 52, 53, 54, 55, 57, 58, 59, 81, 82 }; break;
            case 2: numbers = "015"; ixs = new[] { 0, 1, 2, 3, 22, 48, 49, 50, 51, 52, 63, 79, 80, 81, 84, 90, 91, 92, 93, 94 }; break;
            case 3: numbers = "016"; ixs = new[] { 0, 1, 2, 28, 47, 48, 49, 50, 51, 67, 78, 79, 80, 86, 89, 90, 96, 97, 98, 99 }; break;
            case 4: numbers = "017"; ixs = new[] { 0, 1, 34, 46, 47, 48, 49, 50, 71, 72, 73, 75, 76, 77, 78, 79, 88, 89 }; break;
            case 5: numbers = "018"; ixs = new[] { 0, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 77 }; break;
            case 6: numbers = "023"; ixs = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 53 }; break;
            case 7: numbers = "024"; ixs = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 81, 82 }; break;
            case 8: numbers = "025"; ixs = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 21, 22, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 60, 61, 62, 63, 79, 80, 81, 82, 83, 84, 90, 91, 92, 93, 94 }; break;
            case 9: numbers = "026"; ixs = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 27, 28, 47, 48, 49, 50, 51, 52, 53, 54, 55, 66, 67, 78, 79, 80, 81, 82, 85, 86, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 10: numbers = "027"; ixs = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 33, 34, 46, 47, 48, 49, 50, 51, 52, 53, 54, 70, 71, 72, 73, 75, 76, 77, 78, 79, 80, 81, 87, 88, 89, 90, 91, 97, 98, 99 }; break;
            case 11: numbers = "028"; ixs = new[] { 0, 1, 2, 3, 4, 5, 6, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 74, 75, 76, 77, 78, 79, 80, 89 }; break;
            case 12: numbers = "029"; ixs = new[] { 0, 1, 2, 3, 4, 5, 45, 46, 47, 48, 49, 50 }; break;
            case 13: numbers = "034"; ixs = new[] { 11, 12, 13, 14, 15, 16, 49, 50, 51, 52, 54, 55, 56, 57, 58, 59, 81, 82 }; break;
            case 14: numbers = "035"; ixs = new[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 48, 49, 50, 51, 52, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 79, 80, 81, 82, 83, 84, 90, 91, 92, 93, 94 }; break;
            case 15: numbers = "036"; ixs = new[] { 11, 12, 13, 14, 26, 27, 28, 47, 48, 49, 50, 51, 52, 54, 55, 56, 57, 58, 59, 60, 61, 63, 64, 65, 66, 67, 78, 79, 80, 81, 82, 83, 84, 85, 86, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 16: numbers = "037"; ixs = new[] { 11, 12, 13, 32, 33, 34, 46, 47, 48, 49, 50, 51, 52, 54, 55, 56, 57, 58, 69, 70, 71, 72, 73, 75, 76, 77, 78, 79, 80, 81, 82, 83, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 17: numbers = "038"; ixs = new[] { 11, 12, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 54, 55, 56, 57, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 88, 89, 90, 91, 92, 98, 99 }; break;
            case 18: numbers = "039"; ixs = new[] { 11, 44, 45, 46, 47, 48, 49, 50, 51, 52, 54, 55, 56, 77, 78, 79, 80, 81 }; break;
            case 19: numbers = "045"; ixs = new[] { 17, 18, 19, 20, 21, 22, 48, 60, 61, 62, 63, 79, 80, 83, 84, 90, 91, 92, 93, 94 }; break;
            case 20: numbers = "046"; ixs = new[] { 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 47, 48, 60, 61, 62, 63, 64, 65, 66, 67, 78, 79, 80, 83, 84, 85, 86, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 21: numbers = "047"; ixs = new[] { 17, 18, 19, 31, 32, 33, 34, 46, 47, 48, 60, 61, 62, 63, 64, 66, 67, 68, 69, 70, 71, 72, 73, 75, 76, 77, 78, 79, 80, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 22: numbers = "048"; ixs = new[] { 17, 18, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 60, 61, 72, 73, 74, 75, 76, 77, 78, 79, 80, 83, 84, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 23: numbers = "049"; ixs = new[] { 17, 43, 44, 45, 46, 47, 48, 60, 76, 77, 78, 79, 80, 83, 89, 90, 91, 92, 93, 99 }; break;
            case 24: numbers = "056"; ixs = new[] { 23, 24, 25, 26, 27, 28, 47, 64, 65, 66, 67, 78, 85, 86, 89, 95, 96, 97, 98, 99 }; break;
            case 25: numbers = "057"; ixs = new[] { 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 46, 47, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 75, 76, 77, 78, 85, 86, 87, 88, 89, 95, 96, 97, 98, 99 }; break;
            case 26: numbers = "058"; ixs = new[] { 23, 24, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 64, 65, 66, 67, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 85, 86, 87, 88, 89, 95, 96, 97, 98, 99 }; break;
            case 27: numbers = "059"; ixs = new[] { 23, 42, 43, 44, 45, 46, 47, 64, 75, 76, 77, 78, 85, 88, 89, 95, 96, 97, 98, 99 }; break;
            case 28: numbers = "067"; ixs = new[] { 29, 30, 31, 32, 33, 34, 46, 68, 69, 70, 71, 72, 73, 75, 76, 77, 87, 88 }; break;
            case 29: numbers = "068"; ixs = new[] { 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 87, 88 }; break;
            case 30: numbers = "069"; ixs = new[] { 29, 41, 42, 43, 44, 45, 46, 68, 69, 70, 72, 73, 74, 75, 76, 77, 87, 88 }; break;
            case 31: numbers = "078"; ixs = new[] { 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 74 }; break;
            case 32: numbers = "079"; ixs = new[] { 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 74 }; break;
            case 33: numbers = "124"; ixs = new[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 56 }; break;
            case 34: numbers = "125"; ixs = new[] { 4, 5, 6, 7, 8, 9, 21, 53, 54, 55, 56, 57, 58, 60, 61, 62, 82, 83 }; break;
            case 35: numbers = "126"; ixs = new[] { 3, 4, 5, 6, 7, 8, 27, 52, 53, 54, 55, 66, 81, 82, 85, 91, 92, 93, 94, 95 }; break;
            case 36: numbers = "127"; ixs = new[] { 2, 3, 4, 5, 6, 7, 33, 51, 52, 53, 54, 70, 80, 81, 87, 90, 91, 97, 98, 99 }; break;
            case 37: numbers = "128"; ixs = new[] { 1, 2, 3, 4, 5, 6, 39, 50, 51, 52, 53, 74, 75, 76, 78, 79, 80, 89 }; break;
            case 38: numbers = "129"; ixs = new[] { 0, 1, 2, 3, 4, 5, 45, 46, 47, 48, 49, 50 }; break;
            case 39: numbers = "134"; ixs = new[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 56 }; break;
            case 40: numbers = "135"; ixs = new[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 82, 83 }; break;
            case 41: numbers = "136"; ixs = new[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 26, 27, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 63, 64, 65, 66, 81, 82, 83, 84, 85, 91, 92, 93, 94, 95 }; break;
            case 42: numbers = "137"; ixs = new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 32, 33, 51, 52, 53, 54, 55, 56, 57, 58, 69, 70, 80, 81, 82, 83, 86, 87, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 43: numbers = "138"; ixs = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 38, 39, 50, 51, 52, 53, 54, 55, 56, 57, 73, 74, 75, 76, 78, 79, 80, 81, 82, 88, 89, 90, 91, 92, 98, 99 }; break;
            case 44: numbers = "139"; ixs = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 77, 78, 79, 80, 81 }; break;
            case 45: numbers = "145"; ixs = new[] { 4, 16, 17, 18, 19, 20, 21, 53, 54, 55, 57, 58, 59, 60, 61, 62, 82, 83 }; break;
            case 46: numbers = "146"; ixs = new[] { 3, 4, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 52, 53, 54, 55, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 81, 82, 83, 84, 85, 91, 92, 93, 94, 95 }; break;
            case 47: numbers = "147"; ixs = new[] { 2, 3, 4, 16, 17, 18, 19, 31, 32, 33, 51, 52, 53, 54, 55, 57, 58, 59, 60, 61, 62, 63, 64, 66, 67, 68, 69, 70, 80, 81, 82, 83, 84, 85, 86, 87, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 48: numbers = "148"; ixs = new[] { 1, 2, 3, 4, 16, 17, 18, 37, 38, 39, 50, 51, 52, 53, 54, 55, 57, 58, 59, 60, 61, 72, 73, 74, 75, 76, 78, 79, 80, 81, 82, 83, 84, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 49: numbers = "149"; ixs = new[] { 0, 1, 2, 3, 4, 16, 17, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 57, 58, 59, 60, 76, 77, 78, 79, 80, 81, 82, 83, 89, 90, 91, 92, 93, 99 }; break;
            case 50: numbers = "156"; ixs = new[] { 3, 22, 23, 24, 25, 26, 27, 52, 63, 64, 65, 66, 81, 84, 85, 91, 92, 93, 94, 95 }; break;
            case 51: numbers = "157"; ixs = new[] { 2, 3, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 51, 52, 63, 64, 65, 66, 67, 68, 69, 70, 80, 81, 84, 85, 86, 87, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 52: numbers = "158"; ixs = new[] { 1, 2, 3, 22, 23, 24, 36, 37, 38, 39, 50, 51, 52, 63, 64, 65, 66, 67, 69, 70, 71, 72, 73, 74, 75, 76, 78, 79, 80, 81, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 53: numbers = "159"; ixs = new[] { 0, 1, 2, 3, 22, 23, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 63, 64, 75, 76, 77, 78, 79, 80, 81, 84, 85, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 54: numbers = "167"; ixs = new[] { 2, 28, 29, 30, 31, 32, 33, 51, 67, 68, 69, 70, 80, 86, 87, 90, 96, 97, 98, 99 }; break;
            case 55: numbers = "168"; ixs = new[] { 1, 2, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 50, 51, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 78, 79, 80, 86, 87, 88, 89, 90, 96, 97, 98, 99 }; break;
            case 56: numbers = "169"; ixs = new[] { 0, 1, 2, 28, 29, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 67, 68, 69, 70, 72, 73, 74, 75, 76, 77, 78, 79, 80, 86, 87, 88, 89, 90, 96, 97, 98, 99 }; break;
            case 57: numbers = "178"; ixs = new[] { 1, 34, 35, 36, 37, 38, 39, 50, 71, 72, 73, 74, 75, 76, 78, 79, 88, 89 }; break;
            case 58: numbers = "179"; ixs = new[] { 0, 1, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 71, 72, 73, 74, 75, 76, 77, 78, 79, 88, 89 }; break;
            case 59: numbers = "189"; ixs = new[] { 0, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 77 }; break;
            case 60: numbers = "235"; ixs = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 59 }; break;
            case 61: numbers = "236"; ixs = new[] { 9, 10, 11, 12, 13, 14, 26, 56, 57, 58, 59, 60, 61, 63, 64, 65, 83, 84 }; break;
            case 62: numbers = "237"; ixs = new[] { 8, 9, 10, 11, 12, 13, 32, 55, 56, 57, 58, 69, 82, 83, 86, 92, 93, 94, 95, 96 }; break;
            case 63: numbers = "238"; ixs = new[] { 7, 8, 9, 10, 11, 12, 38, 54, 55, 56, 57, 73, 81, 82, 88, 90, 91, 92, 98, 99 }; break;
            case 64: numbers = "239"; ixs = new[] { 6, 7, 8, 9, 10, 11, 44, 51, 52, 53, 54, 55, 56, 77, 78, 79, 80, 81 }; break;
            case 65: numbers = "245"; ixs = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 59 }; break;
            case 66: numbers = "246"; ixs = new[] { 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 83, 84 }; break;
            case 67: numbers = "247"; ixs = new[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 31, 32, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 66, 67, 68, 69, 82, 83, 84, 85, 86, 92, 93, 94, 95, 96 }; break;
            case 68: numbers = "248"; ixs = new[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 37, 38, 54, 55, 56, 57, 58, 59, 60, 61, 72, 73, 81, 82, 83, 84, 87, 88, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 69: numbers = "249"; ixs = new[] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 43, 44, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 76, 77, 78, 79, 80, 81, 82, 83, 89, 90, 91, 92, 93, 99 }; break;
            case 70: numbers = "256"; ixs = new[] { 9, 21, 22, 23, 24, 25, 26, 56, 57, 58, 60, 61, 62, 63, 64, 65, 83, 84 }; break;
            case 71: numbers = "257"; ixs = new[] { 8, 9, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 55, 56, 57, 58, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 82, 83, 84, 85, 86, 92, 93, 94, 95, 96 }; break;
            case 72: numbers = "258"; ixs = new[] { 7, 8, 9, 21, 22, 23, 24, 36, 37, 38, 54, 55, 56, 57, 58, 60, 61, 62, 63, 64, 65, 66, 67, 69, 70, 71, 72, 73, 81, 82, 83, 84, 85, 86, 87, 88, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 73: numbers = "259"; ixs = new[] { 6, 7, 8, 9, 21, 22, 23, 42, 43, 44, 51, 52, 53, 54, 55, 56, 57, 58, 60, 61, 62, 63, 64, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 74: numbers = "267"; ixs = new[] { 8, 27, 28, 29, 30, 31, 32, 55, 66, 67, 68, 69, 82, 85, 86, 92, 93, 94, 95, 96 }; break;
            case 75: numbers = "268"; ixs = new[] { 7, 8, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 54, 55, 66, 67, 68, 69, 70, 71, 72, 73, 81, 82, 85, 86, 87, 88, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 76: numbers = "269"; ixs = new[] { 6, 7, 8, 27, 28, 29, 41, 42, 43, 44, 51, 52, 53, 54, 55, 66, 67, 68, 69, 70, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 77: numbers = "278"; ixs = new[] { 7, 33, 34, 35, 36, 37, 38, 54, 70, 71, 72, 73, 81, 87, 88, 90, 91, 97, 98, 99 }; break;
            case 78: numbers = "279"; ixs = new[] { 6, 7, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 51, 52, 53, 54, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 87, 88, 89, 90, 91, 97, 98, 99 }; break;
            case 79: numbers = "289"; ixs = new[] { 6, 39, 40, 41, 42, 43, 44, 51, 52, 53, 74, 75, 76, 77, 78, 79, 80, 89 }; break;
            case 80: numbers = "346"; ixs = new[] { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 62 }; break;
            case 81: numbers = "347"; ixs = new[] { 14, 15, 16, 17, 18, 19, 31, 59, 60, 61, 62, 63, 64, 66, 67, 68, 84, 85 }; break;
            case 82: numbers = "348"; ixs = new[] { 13, 14, 15, 16, 17, 18, 37, 58, 59, 60, 61, 72, 83, 84, 87, 93, 94, 95, 96, 97 }; break;
            case 83: numbers = "349"; ixs = new[] { 12, 13, 14, 15, 16, 17, 43, 57, 58, 59, 60, 76, 82, 83, 89, 90, 91, 92, 93, 99 }; break;
            case 84: numbers = "356"; ixs = new[] { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 62 }; break;
            case 85: numbers = "357"; ixs = new[] { 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 84, 85 }; break;
            case 86: numbers = "358"; ixs = new[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 36, 37, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 69, 70, 71, 72, 83, 84, 85, 86, 87, 93, 94, 95, 96, 97 }; break;
            case 87: numbers = "359"; ixs = new[] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 42, 43, 57, 58, 59, 60, 61, 62, 63, 64, 75, 76, 82, 83, 84, 85, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 88: numbers = "367"; ixs = new[] { 14, 26, 27, 28, 29, 30, 31, 59, 60, 61, 63, 64, 65, 66, 67, 68, 84, 85 }; break;
            case 89: numbers = "368"; ixs = new[] { 13, 14, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 58, 59, 60, 61, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 83, 84, 85, 86, 87, 93, 94, 95, 96, 97 }; break;
            case 90: numbers = "369"; ixs = new[] { 12, 13, 14, 26, 27, 28, 29, 41, 42, 43, 57, 58, 59, 60, 61, 63, 64, 65, 66, 67, 68, 69, 70, 72, 73, 74, 75, 76, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 91: numbers = "378"; ixs = new[] { 13, 32, 33, 34, 35, 36, 37, 58, 69, 70, 71, 72, 83, 86, 87, 93, 94, 95, 96, 97 }; break;
            case 92: numbers = "379"; ixs = new[] { 12, 13, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 57, 58, 69, 70, 71, 72, 73, 74, 75, 76, 82, 83, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; break;
            case 93: numbers = "389"; ixs = new[] { 12, 38, 39, 40, 41, 42, 43, 57, 73, 74, 75, 76, 82, 88, 89, 90, 91, 92, 98, 99 }; break;
            case 94: numbers = "457"; ixs = new[] { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 65 }; break;
            case 95: numbers = "458"; ixs = new[] { 19, 20, 21, 22, 23, 24, 36, 62, 63, 64, 65, 66, 67, 69, 70, 71, 85, 86 }; break;
            case 96: numbers = "459"; ixs = new[] { 18, 19, 20, 21, 22, 23, 42, 61, 62, 63, 64, 75, 84, 85, 88, 94, 95, 96, 97, 98 }; break;
            case 97: numbers = "467"; ixs = new[] { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 65 }; break;
            case 98: numbers = "468"; ixs = new[] { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 85, 86 }; break;
            case 99: numbers = "469"; ixs = new[] { 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 41, 42, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 72, 73, 74, 75, 84, 85, 86, 87, 88, 94, 95, 96, 97, 98 }; break;
            case 100: numbers = "478"; ixs = new[] { 19, 31, 32, 33, 34, 35, 36, 62, 63, 64, 66, 67, 68, 69, 70, 71, 85, 86 }; break;
            case 101: numbers = "479"; ixs = new[] { 18, 19, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 61, 62, 63, 64, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 84, 85, 86, 87, 88, 94, 95, 96, 97, 98 }; break;
            case 102: numbers = "489"; ixs = new[] { 18, 37, 38, 39, 40, 41, 42, 61, 72, 73, 74, 75, 84, 87, 88, 94, 95, 96, 97, 98 }; break;
            case 103: numbers = "568"; ixs = new[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 68 }; break;
            case 104: numbers = "569"; ixs = new[] { 24, 25, 26, 27, 28, 29, 41, 65, 66, 67, 68, 69, 70, 72, 73, 74, 86, 87 }; break;
            case 105: numbers = "578"; ixs = new[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 68 }; break;
            case 106: numbers = "579"; ixs = new[] { 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 86, 87 }; break;
            case 107: numbers = "589"; ixs = new[] { 24, 36, 37, 38, 39, 40, 41, 65, 66, 67, 69, 70, 71, 72, 73, 74, 86, 87 }; break;
            case 108: numbers = "679"; ixs = new[] { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 71 }; break;
            case 109: numbers = "689"; ixs = new[] { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 71 }; break;
        }
        letters = ixs.Select(i => alphabet[i]).Where(x => x != ' ').Join("");
        if (letters.Length == 0) // Not necessary in rule seed 1, but in the rare case that a rule-seeded triangle contains no letters, pick a new set of numbers
            goto NewNumbers;

        ord = UnityEngine.Random.Range(0, 6);
        switch (ord)
        {
            case 0:
                NumberTexts[0].text = numbers[0].ToString(); NumberTexts[1].text = numbers[1].ToString(); NumberTexts[2].text = numbers[2].ToString();
                Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[0], numbers[1], numbers[2]);
                break;
            case 1:
                NumberTexts[0].text = numbers[0].ToString(); NumberTexts[1].text = numbers[2].ToString(); NumberTexts[2].text = numbers[1].ToString();
                Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[0], numbers[2], numbers[1]);
                break;
            case 2:
                NumberTexts[0].text = numbers[1].ToString(); NumberTexts[1].text = numbers[0].ToString(); NumberTexts[2].text = numbers[2].ToString();
                Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[1], numbers[0], numbers[2]);
                break;
            case 3:
                NumberTexts[0].text = numbers[1].ToString(); NumberTexts[1].text = numbers[2].ToString(); NumberTexts[2].text = numbers[0].ToString();
                Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[1], numbers[2], numbers[0]);
                break;
            case 4:
                NumberTexts[0].text = numbers[2].ToString(); NumberTexts[1].text = numbers[0].ToString(); NumberTexts[2].text = numbers[1].ToString();
                Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[2], numbers[0], numbers[1]);
                break;
            case 5:
                NumberTexts[0].text = numbers[2].ToString(); NumberTexts[1].text = numbers[1].ToString(); NumberTexts[2].text = numbers[0].ToString();
                Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[2], numbers[1], numbers[0]);
                break;
        }

        TryAgain:
        for (int f = 0; f < 5; f++)
        {
            chosens[f] = bet[UnityEngine.Random.Range(0, 26)].ToString();
        }
        if ((chosens[0] == chosens[1] || chosens[0] == chosens[2] || chosens[0] == chosens[3] || chosens[0] == chosens[4] || chosens[1] == chosens[2] || chosens[1] == chosens[3] || chosens[1] == chosens[4] || chosens[2] == chosens[3] || chosens[2] == chosens[4] || chosens[3] == chosens[4]) || (!(letters.Contains(chosens[0])) && !(letters.Contains(chosens[1])) && !(letters.Contains(chosens[2])) && !(letters.Contains(chosens[3])) && !(letters.Contains(chosens[4]))))
        {
            goto TryAgain;
        }

        for (int a = 0; a < 5; a++)
        {
            FlyTexts[a].text = chosens[a];
        }
        Debug.LogFormat("[Flyswatting #{0}] Letters: {1} {2} {3} {4} {5}", moduleId, chosens[0], chosens[1], chosens[2], chosens[3], chosens[4]);

        for (int t = 0; t < 5; t++)
        {
            if (letters.Contains(chosens[t]))
            {
                Debug.LogFormat("[Flyswatting #{0}] You can swat the {1} fly.", moduleId, chosens[t]);
                answers[t] = 1;
            }
            else
            {
                Debug.LogFormat("[Flyswatting #{0}] You cannot swat the {1} fly.", moduleId, chosens[t]);
            }
        }

        StartCoroutine(FliesDoBeMoving());
    }

    void Update()
    {
        for (int r = 0; r < 5; r++)
        {
            FlyObjects[r].transform.localPosition = new Vector3(flyX[r], 0f, flyZ[r]);
        }
    }

    void FlyPress(KMSelectable Fly)
    {
        for (int i = 0; i < 5; i++)
        {
            if (Fly == Flies[i] && !moduleSolved)
            {
                Fly.AddInteractionPunch();
                Swat(i);
            }
        }
    }

    void Swat(int p)
    {
        Audio.PlaySoundAtTransform("oof", transform);
        if (answers[p] == 1)
        {
            Debug.LogFormat("[Flyswatting #{0}] You swatted fly {1}. That is correct.", moduleId, chosens[p]);
            answers[p] = 0;
            if (answers[0] + answers[1] + answers[2] + answers[3] + answers[4] == 0)
            {
                Debug.LogFormat("[Flyswatting #{0}] You swatted all valid flies, module solved.", moduleId);
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
                StopAllCoroutines();
                StartCoroutine(OOOOOOSHINY());
            }
        }
        else
        {
            Debug.LogFormat("[Flyswatting #{0}] You swatted fly {1}. That is incorrect.", moduleId, chosens[p]);
            GetComponent<KMBombModule>().HandleStrike();
        }
        FlyBodies[p].SetActive(false);
        FuckedFlies[p].SetActive(true);
        swatted[p] = true;
    }

    private IEnumerator FliesDoBeMoving()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.005f);
            for (int y = 0; y < 5; y++)
            {
                if (swatted[y])
                {
                    continue;
                }
                int q = UnityEngine.Random.Range(0, 4);
                switch (q)
                {
                    case 0: //up
                        if (flyZ[y] > 0.055f)
                        {
                            flyZ[y] -= 0.001f;
                        }
                        else
                        {
                            flyZ[y] += 0.001f;
                        }
                        break;
                    case 1: //down
                        if (flyZ[y] < -0.055f)
                        {
                            flyZ[y] += 0.001f;
                        }
                        else
                        {
                            flyZ[y] -= 0.001f;
                        }
                        break;
                    case 2: //left
                        if (flyX[y] > 0.055f)
                        {
                            flyX[y] -= 0.001f;
                        }
                        else
                        {
                            flyX[y] += 0.001f;
                        }
                        break;
                    case 3: //right
                        if (flyX[y] < -0.055f)
                        {
                            flyX[y] += 0.001f;
                        }
                        else
                        {
                            flyX[y] -= 0.001f;
                        }
                        break;
                }
            }
        }
        yield return null;
    }

    private IEnumerator OOOOOOSHINY()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.005f);
            for (int e = 0; e < 5; e++)
            {
                if (swatted[e])
                {
                    continue;
                }
                int v = UnityEngine.Random.Range(0, 16);
                if (status[e] == 0)
                {
                    switch (v % 8)
                    {
                        case 0: flyZ[e] += 0.0005f; break; //u
                        case 1: flyZ[e] += 0.0005f; flyX[e] += 0.0005f; break; //ur
                        case 2: flyX[e] += 0.0005f; break; //r
                        case 3: flyZ[e] -= 0.0005f; flyX[e] += 0.0005f; break; //dr
                        case 4: flyZ[e] -= 0.0005f; break; //d
                        case 5: flyZ[e] -= 0.0005f; flyX[e] -= 0.0005f; break; //dl
                        case 6: flyX[e] -= 0.0005f; break; //l
                        case 7: flyZ[e] += 0.0005f; flyX[e] -= 0.0005f; break; //ul
                    }
                    if (flyX[e] < light[0])
                    {
                        flyX[e] += 0.001f;
                    }
                    else
                    {
                        if (v / 8 == 0)
                        {
                            flyX[e] += 0.001f;
                        }
                        else
                        {
                            flyX[e] -= 0.001f;
                        }
                    }
                    if (flyZ[e] < light[1])
                    {
                        flyZ[e] += 0.001f;
                    }
                    else
                    {
                        if (v / 8 == 0)
                        {
                            flyZ[e] += 0.001f;
                        }
                        else
                        {
                            flyZ[e] -= 0.001f;
                        }
                    }
                    if (flyX[e] > (light[0] - 0.01f) && flyZ[e] > (light[1] - 0.01f))
                    {
                        status[e] = 1;
                        FlyTexts[e].text = " ";
                        Audio.PlaySoundAtTransform("oof", transform);
                    }
                }
                else if (status[e] == 1)
                {
                    flyZ[e] -= 0.01f;
                    if (flyZ[e] < -10)
                    {
                        status[e] = 2;
                        FlyBodies[e].SetActive(false);
                    }
                }
            }
        }
        yield return null;
    }

    //toilet paper
    private bool letsAreValid(string s)
    {
        string[] lets = s.Split(' ');
        for (int w = 0; w < lets.Length; w++)
        {
            if (!(bet.Contains(lets[w])))
            {
                return false;
            }
            else if (lets[w].Length != 1)
            {
                return false;
            }
            else
            {
                for (int d = 0; d < w; d++)
                {
                    if (lets[w] == lets[d])
                    {
                        return false;
                    }
                }
                var b = 0;
                for (int u = 0; u < 5; u++)
                {
                    if (chosens[u] == lets[w])
                    {
                        b += 1;
                    }
                }
                if (b == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} swat A B C [Swats the flies with the specified letters]";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*swat\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            if (parameters.Length >= 2)
            {
                string lets = command.Substring(5);
                if (letsAreValid(lets))
                {
                    yield return null;
                    for (int k = 1; k < parameters.Length; k++)
                    {
                        for (int h = 0; h < 5; h++)
                        {
                            if (parameters[k].EqualsIgnoreCase(chosens[h]))
                            {
                                Flies[h].OnInteract();
                            }
                        }
                        yield return new WaitForSeconds(0.1f);
                    }
                    yield break;
                }
            }
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        for (int j = 0; j < 5; j++)
        {
            if (answers[j] == 1)
            {
                Flies[j].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
