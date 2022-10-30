using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class Scenario : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI subtitles;
    [SerializeField]
    GameObject subtitlesCanvas;
    [SerializeField]
    GameObject boss;

    private bool captions = true;
    private bool callOnce = false;



    private void Start()
    {

    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (captions)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            if (sceneName == "Lobby" && callOnce == false)
            {
                StartCoroutine(LobbySequence());
                callOnce = true;
            }
            else if (sceneName == "LVL1" && callOnce == false)
            {
                StartCoroutine(Lvl1Sequence());
                callOnce= true;
            }
            else if (sceneName == "LVL2" && boss.IsDestroyed() && callOnce == false)
            {
                Debug.Log(boss.IsDestroyed());
                StartCoroutine(Lvl2Sequence());
                callOnce = true;
            }
            else if (sceneName == "LVL3" && callOnce == false)
            {
                StartCoroutine(Lvl3Sequence());
                callOnce = true;
            }
            else if (sceneName == "LVL4" && callOnce == false)
            {
                StartCoroutine(Lvl4Sequence());
                callOnce = true;
            }
            else if (sceneName == "LVL5" && callOnce == false)
            {
                StartCoroutine(Lvl5Sequence());
                callOnce = true;
            }
            else
            {

            }
        }
    }

    IEnumerator LobbySequence()
    {
        yield return new WaitForSeconds(5);
        subtitles.text = "Vitaj, dúfame že si našu hru užiješ!";
        yield return new WaitForSeconds(5);
        subtitles.text = "";
        subtitlesCanvas.SetActive(false);


    }
    IEnumerator Lvl1Sequence()
    {
        yield return new WaitForSeconds(1);
        subtitles.text = "Pravý hrdina, doneste mu jedlo a dobré víno a odpracte tú medúzu odtiaľto.";
        yield return new WaitForSeconds(2);
        subtitles.text = "";
        yield return new WaitForSeconds(2);
        subtitles.text = "To je tvoja práca?";
        yield return new WaitForSeconds(2);
        subtitles.text = "Áno, myslím že áno";
        yield return new WaitForSeconds(2);
        subtitles.text = "Odvaha ti nechýba, ale to neznamená že nie si hlupák, presne niekoho takého potrebujem";
        yield return new WaitForSeconds(2);
        subtitles.text = "Smiem aspoň vedieť kto si a na čo by si ma potreboval než  to odkývam?";
        yield return new WaitForSeconds(2);
        subtitles.text = "Som Zeus, vládca gréckych bohov a teba som si vybral aby si zabil Minotaura, tá obluda už mi ide dlho na nervy, na tvoju odpoveď nie je čas, priprav sa na labyrint a veľa šťastia, budeš ho potrebovať ale veľa času ti nedávam a budeš lízať podlahu.";
        yield return new WaitForSeconds(2);
        subtitlesCanvas.SetActive(false);


    }

    IEnumerator Lvl2Sequence()
    {
        yield return new WaitForSeconds(1);
        subtitles.text = "Asi som ťa podcenil, všetka česť.";
        yield return new WaitForSeconds(2);
        subtitles.text = "Prečo si sa o to nepostaral sám, si predsa najsilnejší z celého Olympu?";
        yield return new WaitForSeconds(2);
        subtitles.text = "Nemám čas sa zahadzovať s takýmito maličkosťami, na to mám takých hlupáčikov ako ty, ale to ako sa ti podarilo prežiť je naozaj obdivuhodné. Nejakým spôsobom sa ti podarilo použiť medúzinu schopnosť. Musíš byť poloboh s veľkým darom. Poseidon v poslednej dobe začal robiť problémy, mal by si sa o neho v mojom mene postarať kým si to niekto odskáče. Nie je čas navyše, priprav sa. Ó a inak skoro som zabudol, tu máš nejaké vybavenie nech po tebe nezostane len mastný fľak.";
        yield return new WaitForSeconds(2);
        subtitlesCanvas.SetActive(false);
    }

    IEnumerator Lvl3Sequence()
    {
        yield return new WaitForSeconds(1);
        subtitles.text = "Zatiaľ si ma nesklamal, to sa ti musí nechať, ale táto úloha nebude taká ľahká. Hádesovi sa toto páčiť nebude, možno vypukne vojna, treba vykonať potrebné kroky skôr ako ich podnikne on. Musíš sa ho zbaviť. Spolieham na teba.";
        yield return new WaitForSeconds(2);
        subtitlesCanvas.SetActive(false);
    }

    IEnumerator Lvl4Sequence()
    {
        yield return new WaitForSeconds(1);
        subtitles.text = "To si bol ty? To ty si porazil Poseidona? Nebuď smiešny, poď bližšie nech to s tebou rýchlo skoncujem.";
        yield return new WaitForSeconds(2);
        subtitlesCanvas.SetActive(false);
    }

    IEnumerator Lvl5Sequence()
    {
        yield return new WaitForSeconds(1);
        subtitles.text = "Väčšieho mongola som ešte nevidel, urobil si všetku prácu za mňa, Zeus a Poseidon si naozaj mysleli že sa uspokojím s ríšou mŕtvych, tak to boli na omyle a vďaka tebe teraz môžem vládnuť celému svetu. Bež kým si to rozmyslím, nechám ťa žiť ale tvoje vybavenie si beriem späť.";
        yield return new WaitForSeconds(2);
        subtitles.text = "Celú dobu si ma klamal, toto celé ti nemôžem nechať len tak prejsť.";
        yield return new WaitForSeconds(2);
        subtitles.text = "Staviaš sa proti mne? No poť ty opica, nech to mám za sebou  a môžem vládnuť svojmu novému svetu.";
        yield return new WaitForSeconds(2);
        subtitlesCanvas.SetActive(false);
    }




    
}
