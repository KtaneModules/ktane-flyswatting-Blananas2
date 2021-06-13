using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class flyswattingScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;

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
    private string bet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string[] chosens = {"", "", "", "", ""};
    private int[] answers = {0, 0, 0, 0, 0};
    private float[] flyX = {0f,      -0.0449f, 0.0449f, -0.0253f,  0.0253f};
    private float[] flyZ = {0.0457f,  0.0086f, 0.0086f, -0.041f,  -0.041f};
    private bool[] swatted = {false, false, false, false, false};
    private float[] light = {0.075167f, 0.076057f};
    private int[] status = {0, 0, 0, 0, 0};

    void Awake () {
        moduleId = moduleIdCounter++;
        
        foreach (KMSelectable Fly in Flies) {
            Fly.OnInteract += delegate () { FlyPress(Fly); return false; };
        }
        
    }

    // Use this for initialization
    void Start () {
        ix = UnityEngine.Random.Range(0,110);
        switch (ix) {
            case 0: numbers = "013"; letters = "FKG"; break;
            case 1: numbers = "014"; letters = "FKNAXO"; break;
            case 2: numbers = "015"; letters = "FCNPBY"; break;
            case 3: numbers = "016"; letters = "FCTZV"; break;
            case 4: numbers = "017"; letters = "FQCHM"; break;
            case 5: numbers = "018"; letters = "FQWS"; break;
            case 6: numbers = "023"; letters = "FKG"; break;
            case 7: numbers = "024"; letters = "FKNAXUGOR"; break;
            case 8: numbers = "025"; letters = "FKCNAPUXOBEYI"; break;
            case 9: numbers = "026"; letters = "FKCNAPXTZBLV"; break;
            case 10: numbers = "027"; letters = "FQKCNHAPTM"; break;
            case 11: numbers = "028"; letters = "FQKWCNHSJ"; break;
            case 12: numbers = "029"; letters = "FQK"; break;
            case 13: numbers = "034"; letters = "NAUXOR"; break;
            case 14: numbers = "035"; letters = "CNAUPXOBERYI"; break;
            case 15: numbers = "036"; letters = "CNAUPTXOZBEYLV"; break;
            case 16: numbers = "037"; letters = "QCNHAUPXTMOZBD"; break;
            case 17: numbers = "038"; letters = "QWCNHASJPUTX"; break;
            case 18: numbers = "039"; letters = "QWCNAU"; break;
            case 19: numbers = "045"; letters = "CPBEYI"; break;
            case 20: numbers = "046"; letters = "CPTZBELYVI"; break;
            case 21: numbers = "047"; letters = "QCHPTMZBEDYL"; break;
            case 22: numbers = "048"; letters = "QWCHSJPTMZB"; break;
            case 23: numbers = "049"; letters = "QWCHPE"; break;
            case 24: numbers = "056"; letters = "TZLV"; break;
            case 25: numbers = "057"; letters = "QHTMZDLV"; break;
            case 26: numbers = "058"; letters = "QWHSJTMZDL"; break;
            case 27: numbers = "059"; letters = "QWHTZ"; break;
            case 28: numbers = "067"; letters = "QHMD"; break;
            case 29: numbers = "068"; letters = "QWHSJMD"; break;
            case 30: numbers = "069"; letters = "QWHJMD"; break;
            case 31: numbers = "078"; letters = "WSJ"; break;
            case 32: numbers = "079"; letters = "WSJ"; break;
            case 33: numbers = "124"; letters = "GUR"; break;
            case 34: numbers = "125"; letters = "KAUXOEI"; break;
            case 35: numbers = "126"; letters = "KNAPXBL"; break;
            case 36: numbers = "127"; letters = "KNAPT"; break;
            case 37: numbers = "128"; letters = "KCNHJ"; break;
            case 38: numbers = "129"; letters = "FQK"; break;
            case 39: numbers = "134"; letters = "GUR"; break;
            case 40: numbers = "135"; letters = "KAGUXOERI"; break;
            case 41: numbers = "136"; letters = "KNAGUPXOBEYL"; break;
            case 42: numbers = "137"; letters = "KNAGUPXTOBZD"; break;
            case 43: numbers = "138"; letters = "KCNHAGJUPTX"; break;
            case 44: numbers = "139"; letters = "FQKWCNAGU"; break;
            case 45: numbers = "145"; letters = "KAXOEI"; break;
            case 46: numbers = "146"; letters = "KNAPXOBELYI"; break;
            case 47: numbers = "147"; letters = "KNAPTXOZBEDLY"; break;
            case 48: numbers = "148"; letters = "KCNHAJPXTMZBOE"; break;
            case 49: numbers = "149"; letters = "FQKWCNAHPXOE"; break;
            case 50: numbers = "156"; letters = "NPBLY"; break;
            case 51: numbers = "157"; letters = "NPTZBDLYV"; break;
            case 52: numbers = "158"; letters = "CNHJPTMZBDLY"; break;
            case 53: numbers = "159"; letters = "FQWCNHPTZBY"; break;
            case 54: numbers = "167"; letters = "TZDV"; break;
            case 55: numbers = "168"; letters = "CHJTMZDV"; break;
            case 56: numbers = "169"; letters = "FQWCHJTMZDV"; break;
            case 57: numbers = "178"; letters = "CHJM"; break;
            case 58: numbers = "179"; letters = "FQWCHSJM"; break;
            case 59: numbers = "189"; letters = "FQWS"; break;
            case 60: numbers = "235"; letters = "GR"; break;
            case 61: numbers = "236"; letters = "GUOEY"; break;
            case 62: numbers = "237"; letters = "GUXOZBD"; break;
            case 63: numbers = "238"; letters = "AGUPTX"; break;
            case 64: numbers = "239"; letters = "WCNAGU"; break;
            case 65: numbers = "245"; letters = "GR"; break;
            case 66: numbers = "246"; letters = "GUOERYI"; break;
            case 67: numbers = "247"; letters = "GUXOZBERDLY"; break;
            case 68: numbers = "248"; letters = "AGUPXTMZBEOR"; break;
            case 69: numbers = "249"; letters = "WCNHAGUPXOER"; break;
            case 70: numbers = "256"; letters = "UOEYI"; break;
            case 71: numbers = "257"; letters = "UXOZBEDLYVI"; break;
            case 72: numbers = "258"; letters = "AUPTXMOZBDELYI"; break;
            case 73: numbers = "259"; letters = "WCNHAPUTXZBOEYI"; break;
            case 74: numbers = "267"; letters = "XZBDLV"; break;
            case 75: numbers = "268"; letters = "APTXMZBDLV"; break;
            case 76: numbers = "269"; letters = "WCNHAJPXTMZBDLV"; break;
            case 77: numbers = "278"; letters = "APTM"; break;
            case 78: numbers = "279"; letters = "WCNHASJPTM"; break;
            case 79: numbers = "289"; letters = "WCNHSJ"; break;
            case 80: numbers = "346"; letters = "RI"; break;
            case 81: numbers = "347"; letters = "ERLY"; break;
            case 82: numbers = "348"; letters = "MOZBER"; break;
            case 83: numbers = "349"; letters = "HPXOER"; break;
            case 84: numbers = "356"; letters = "RI"; break;
            case 85: numbers = "357"; letters = "ERLYVI"; break;
            case 86: numbers = "358"; letters = "MOZBERDLYI"; break;
            case 87: numbers = "359"; letters = "HPTXOZBERYI"; break;
            case 88: numbers = "367"; letters = "ELYV"; break;
            case 89: numbers = "368"; letters = "MOZBDELYV"; break;
            case 90: numbers = "369"; letters = "HJPTXMOZBDELYV"; break;
            case 91: numbers = "378"; letters = "MOZBD"; break;
            case 92: numbers = "379"; letters = "HSJPEXMOZBD"; break;
            case 93: numbers = "389"; letters = "HSJPTX"; break;
            case 94: numbers = "457"; letters = "VI"; break;
            case 95: numbers = "458"; letters = "DLYI"; break;
            case 96: numbers = "459"; letters = "TZBYI"; break;
            case 97: numbers = "467"; letters = "VI"; break;
            case 98: numbers = "468"; letters = "DLYVI"; break;
            case 99: numbers = "469"; letters = "JTMZBDLYVI"; break;
            case 100: numbers = "478"; letters = "DLY"; break;
            case 101: numbers = "479"; letters = "SJTMZB"; break;
            case 102: numbers = "489"; letters = "DLY"; break;
            case 103: numbers = "568"; letters = "V"; break;
            case 104: numbers = "569"; letters = "JMDLV"; break;
            case 105: numbers = "578"; letters = "V"; break;
            case 106: numbers = "579"; letters = "SJMDV"; break;
            case 107: numbers = "589"; letters = "SJMDL"; break;
            case 108: numbers = "679"; letters = "S"; break;
            case 109: numbers = "689"; letters = "S"; break;
        }

        ord = UnityEngine.Random.Range(0,6);
        switch (ord) {
            case 0: NumberTexts[0].text = numbers[0].ToString(); NumberTexts[1].text = numbers[1].ToString(); NumberTexts[2].text = numbers[2].ToString(); 
            Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[0], numbers[1], numbers[2]);
            break;
            case 1: NumberTexts[0].text = numbers[0].ToString(); NumberTexts[1].text = numbers[2].ToString(); NumberTexts[2].text = numbers[1].ToString(); 
            Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[0], numbers[2], numbers[1]);
            break;
            case 2: NumberTexts[0].text = numbers[1].ToString(); NumberTexts[1].text = numbers[0].ToString(); NumberTexts[2].text = numbers[2].ToString(); 
            Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[1], numbers[0], numbers[2]);
            break;
            case 3: NumberTexts[0].text = numbers[1].ToString(); NumberTexts[1].text = numbers[2].ToString(); NumberTexts[2].text = numbers[0].ToString(); 
            Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[1], numbers[2], numbers[0]);
            break;
            case 4: NumberTexts[0].text = numbers[2].ToString(); NumberTexts[1].text = numbers[0].ToString(); NumberTexts[2].text = numbers[1].ToString(); 
            Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[2], numbers[0], numbers[1]);
            break;
            case 5: NumberTexts[0].text = numbers[2].ToString(); NumberTexts[1].text = numbers[1].ToString(); NumberTexts[2].text = numbers[0].ToString(); 
            Debug.LogFormat("[Flyswatting #{0}] Numbers: {1} {2} {3}", moduleId, numbers[2], numbers[1], numbers[0]);
            break;
        }

        TryAgain:
        for (int f = 0; f < 5; f++) {
            chosens[f] = bet[UnityEngine.Random.Range(0,26)].ToString();
        }
        if ((chosens[0] == chosens[1] || chosens[0] == chosens[2] || chosens[0] == chosens[3] || chosens[0] == chosens[4] || chosens[1] == chosens[2] || chosens[1] == chosens[3] || chosens[1] == chosens[4] || chosens[2] == chosens[3] || chosens[2] == chosens[4] || chosens[3] == chosens[4]) || (!(letters.Contains(chosens[0])) && !(letters.Contains(chosens[1])) && !(letters.Contains(chosens[2])) && !(letters.Contains(chosens[3])) && !(letters.Contains(chosens[4])))) {
            goto TryAgain;
        }

        for (int a = 0; a < 5; a++) {
            FlyTexts[a].text = chosens[a];
        }
        Debug.LogFormat("[Flyswatting #{0}] Letters: {1} {2} {3} {4} {5}", moduleId, chosens[0], chosens[1], chosens[2], chosens[3], chosens[4]);

        for (int t = 0; t < 5; t++) {
            if (letters.Contains(chosens[t])) {
                Debug.LogFormat("[Flyswatting #{0}] You can swat the {1} fly.", moduleId, chosens[t]);
                answers[t] = 1;
            } else {
                Debug.LogFormat("[Flyswatting #{0}] You cannot swat the {1} fly.", moduleId, chosens[t]);
            }
        }

        StartCoroutine(FliesDoBeMoving());
    }

    void Update() {
        for (int r = 0; r < 5; r++) {
            FlyObjects[r].transform.localPosition = new Vector3(flyX[r], 0f, flyZ[r]);
        }
    }

    void FlyPress(KMSelectable Fly) {
        for (int i = 0; i < 5; i++) {
            if (Fly == Flies[i] && !moduleSolved) {
                Fly.AddInteractionPunch();
                Swat(i);
            }
        }
    }
    
    void Swat (int p) {
		Audio.PlaySoundAtTransform("oof", transform);
        if (answers[p] == 1) {
            Debug.LogFormat("[Flyswatting #{0}] You swatted fly {1}. That is correct.", moduleId, chosens[p]);
            answers[p] = 0;
            if (answers[0] + answers[1] + answers[2] + answers[3] + answers[4] == 0) {
                Debug.LogFormat("[Flyswatting #{0}] You swatted all valid flies, module solved.", moduleId);
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
                StopAllCoroutines();
                StartCoroutine(OOOOOOSHINY());
            }
        } else {
            Debug.LogFormat("[Flyswatting #{0}] You swatted fly {1}. That is incorrect.", moduleId, chosens[p]);
            GetComponent<KMBombModule>().HandleStrike();
        }
        FlyBodies[p].SetActive(false);
        FuckedFlies[p].SetActive(true);
        swatted[p] = true;
    }

    private IEnumerator FliesDoBeMoving () {
        while (true) {
            yield return new WaitForSeconds(0.005f);
            for (int y = 0; y < 5; y++) {
                if (swatted[y]) {
                    continue;
                }
                int q = UnityEngine.Random.Range(0,4);
                switch (q) {
                    case 0: //up
                        if (flyZ[y] > 0.055f) {
                            flyZ[y] -= 0.001f;
                        } else {
                            flyZ[y] += 0.001f;
                        }
                    break;
                    case 1: //down
                        if (flyZ[y] < -0.055f) {
                            flyZ[y] += 0.001f;
                        } else {
                            flyZ[y] -= 0.001f;
                        }
                    break;
                    case 2: //left
                        if (flyX[y] > 0.055f) {
                            flyX[y] -= 0.001f;
                        } else {
                            flyX[y] += 0.001f;
                        }
                    break;
                    case 3: //right
                        if (flyX[y] < -0.055f) {
                            flyX[y] += 0.001f;
                        } else {
                            flyX[y] -= 0.001f;
                        }
                    break;
                }
            }
        }
        yield return null;
    }

    private IEnumerator OOOOOOSHINY () {
        while (true) {
            yield return new WaitForSeconds(0.005f);
            for (int e = 0; e < 5; e++) {
                if (swatted[e]) {
                    continue;
                }
                int v = UnityEngine.Random.Range(0,16);
                if (status[e] == 0) {
                    switch (v % 8) {
                        case 0: flyZ[e] += 0.0005f; break; //u
                        case 1: flyZ[e] += 0.0005f; flyX[e] += 0.0005f; break; //ur
                        case 2: flyX[e] += 0.0005f; break; //r
                        case 3: flyZ[e] -= 0.0005f; flyX[e] += 0.0005f; break; //dr
                        case 4: flyZ[e] -= 0.0005f; break; //d
                        case 5: flyZ[e] -= 0.0005f; flyX[e] -= 0.0005f; break; //dl
                        case 6: flyX[e] -= 0.0005f; break; //l
                        case 7: flyZ[e] += 0.0005f; flyX[e] -= 0.0005f; break; //ul
                    }
                    if (flyX[e] < light[0]) {
                        flyX[e] += 0.001f;
                    } else {
                        if (v/8 == 0) {
                            flyX[e] += 0.001f;
                        } else {
                            flyX[e] -= 0.001f;
                        }
                    }
                    if (flyZ[e] < light[1]) {
                        flyZ[e] += 0.001f;
                    } else {
                        if (v/8 == 0) {
                            flyZ[e] += 0.001f;
                        } else {
                            flyZ[e] -= 0.001f;
                        }
                    }
                    if (flyX[e] > (light[0] - 0.01f) && flyZ[e] > (light[1] - 0.01f)) {
                        status[e] = 1;
                        FlyTexts[e].text = " ";
						Audio.PlaySoundAtTransform("oof", transform);
                    }
                } else if (status[e] == 1) {
                    flyZ[e] -= 0.01f;
                    if (flyZ[e] < -10) {
                        status[e] = 2;
                        FlyBodies[e].SetActive(false);
                    }
                }
            }
        }
        yield return null;
    }

    //toilet paper
    private bool letsAreValid (string s) {
        string[] lets = s.Split(' ');
        for (int w = 0; w < lets.Length; w++) {
            if (!(bet.Contains(lets[w]))) {
                return false;
            } else if (lets[w].Length != 1) {
                return false;
            } else {
                for (int d = 0; d < w; d++) {
                    if (lets[w] == lets[d]) {
                        return false;
                    }
                }
                var b = 0;
                for (int u = 0; u < 5; u++) {
                    if (chosens[u] == lets[w]) {
                        b += 1;
                    }
                }
                if (b == 0) {
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
                if (letsAreValid(lets)) {
                    yield return null;
                    for (int k = 1; k < parameters.Length; k++)
                    {
                        for (int h = 0; h < 5; h++) {
                            if (parameters[k].EqualsIgnoreCase(chosens[h])) {
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
        for (int j = 0; j < 5; j++) {
            if (answers[j] == 1) {
                Flies[j].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
