using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class Scenario : MonoBehaviour
{

    AudioManager audioManager;
    manager managerVariables;
    public TextMeshProUGUI playerText;

    public TextMeshProUGUI othersText;

    public TextMeshProUGUI lobbyText;

    public Sprite Hades;

    public Sprite King;

    public Sprite Zeus;

    public Sprite PlayerLvl1;

    public Sprite PlayerLvl2;

    public bool Actions = true;

    [SerializeField]
    GameObject subtitlesCanvas;

    [SerializeField]
    GameObject UI;

    [SerializeField]
    GameObject boss;

    [SerializeField]
    GameObject image;

    [SerializeField]
    GameObject imageOther;

    [SerializeField]
    GameObject panel;

    
  
    string[] Lvl1 = { "Kráľ Androgeos:\nPravý hrdina, presne niekoho ako ty potrebujem.","Hráč:\n???", "Kráľ Androgeos:\nKeďže si preukázal silu a odvahu ako málokto, postaráš sa o problém " +
        "ktorý ma trápi už roky.","Hráč:\n???", "Kráľ Androgeos:\nZabiješ pre mňa Minotaura, ak to dokážeš získaš bohatstvo na celý život, ak nie zomrieš so cťou. " +
        "Odmietnuť samozrejme nemôžeš, hah." };
    string[] Lvl2 = { "Kráľ Androgeos:\n\nDokázal si to. Myslel som že nevydržíš viac ako minútu, ale dokázal si to. Po zvyšok života bude o teba dobre postarané... " +
        "Čo to? Ako to?", "Zeus:\n\nTo je tvoja zásluha?", "Hráč:\n\nÁno, mocný Zeus, konal som na rozkaz svojho kráľa, prosím ušetri ma.", "Zeus:\n\nNormálne by som ťa" +
        " zmietol s povrchu zemského ale potrebujem niekoho ako ty.", "Zeus:\n\nOčividne nie si len taký človek, si poloboh, s celkom veľkou mocou." +
        " Potrebujem niekoho ako ty, Poseidon a Hádes, moji braria sa proti mne obrátili. Chcú vládu nad svetom len pre seba a ty mi ich pomôžeš zastaviť.",
        "Hráč:\n\nAko asi to mám urobiť, obaja ma zabijú len škaredím pohľadom.", "Zeus:\n\nNie s týmto brnením, aj tak sú ale tvoje šance na úspech mizivé ale nemáme veľa času," +
        " dostanem ťa k poseidonovy a zvyšok je na tebe.", "Zeus:\n\nNemárni čas slovami, budú čakať niekoho ako ty a budú sa ťa snažiť zabiť čo najskôr.", "Hráč:\n\nAle..." };
    string[] Lvl3 = { "Zeus:\n\nZatiaľ si ma nesklamal, to sa ti musí nechať, ale táto úloha nebude taká ľahká. Hádesovi sa toto páčiť nebude, musíš sa ho zbaviť.",
        "Zeus:\n\nHľadal som ho v jeho časti sveta, podsvetí a nenašiel som nič, určite už obsadil Olymp, musíš sa ho zbaviť. Svet sa na teba spolieha." };
    string[] Lvl4 = { "Hádes:\n\nTakže si to dokázal, porazil si Zeusa, dúfal som že ho oslabíš ale toto som nečakal.",
        "Hráč:\n\nOklamal si ma? Kvôli tebe som zabil Poseidona a Zeusa.", "Hádes:\n\nJa sám by som to nedokázal, to musím uznať ale tvojich päť minút slávy sa práve skončilo."
        ,"Hráč:\n\nNemôžem nechať niekoho ako ty aby vládol celému svetu.", "Hádes:\n\nStaviaš sa proti mne? Bez brnenie ktoré som ti dal nemáš proti mne šancu.", 
        "Hráč:\n\nRisknem to." };
    string[] Lvl5 = {};

    int[] Lvl1Field = {1,0,1,0,1};
    int[] Lvl2Field = {1,1,0,1,1,0,1,1,0};
    int[] Lvl3Field = { 1,1};
    int[] Lvl4Field = {1,0,1, 0, 1, 0};
    int[] Lvl5Field = {};

    int index = 0;

    

    private void Start()
    {
        managerVariables = GameObject.Find("Manager").GetComponent<manager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        clear();

        if (managerVariables.ScenarioOrder == 0)
            LobbyScenario();
        else if (managerVariables.ScenarioOrder == 1)
            Lvl1Scenario();
        else if (managerVariables.ScenarioOrder == 2)
            Lvl2Scenario();
        else if (managerVariables.ScenarioOrder == 3)
            Lvl3Scenario();
        else if (managerVariables.ScenarioOrder == 4)
            Lvl4Scenario();
        /*else if (managerVariables.ScenarioOrder == 5)
            Lvl5Scenario();*/
        
    }

    private void Update()
    {   
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (Input.GetKeyDown(KeyCode.Space))  //Input.GetMouseButtonDown(0))
        {
            if (managerVariables.ScenarioOrder == 0 && sceneName == "Lobby")
            {
                LobbyScenario();
            }

            if (managerVariables.ScenarioOrder == 1 && sceneName == "Lobby")
            {
                Lvl1Scenario();
            }

            if (managerVariables.ScenarioOrder == 2 && sceneName == "Lobby")
            {
                Lvl2Scenario();
            }

            if (managerVariables.ScenarioOrder == 3 && sceneName == "Lobby" )
            {
                Lvl3Scenario();
            }

            if (managerVariables.ScenarioOrder == 4 && sceneName == "Lobby")
            {
                Lvl4Scenario();
            }

            /*if (managerVariables.ScenarioOrder == 5 && sceneName == "Lobby")
            {
                Lvl5Scenario();
            }*/
        }       
    }

    private void LobbyScenario()
    {
        SetActions();
        if (index == 0)
        {
            panel.SetActive(true);
            lobbyText.text = "Rok 453 pred n. l., na rozkaz kráľa Kréty Androgeosa sa kráľovská armáda vydáva na výpravu za zneškodnením mýtickej " +
                "bytosti známej ako Medúza po sérii útokov po ktorých z napadnutých nezostalo nič viac ako kamenné sochy s vydeseným výrazom v tvári.\n\n(Pre pokračovanie stlač medzerník)";
            index++;
        }           
        else
        {
            lobbyText.text = "";
            panel.SetActive(false);
            //UI.SetActive(true);
        }
    }

    private void Lvl1Scenario()
    {
        clear();
        SetActions();
        imageOther.GetComponent<Image>().sprite = King;
        image.GetComponent<Image>().sprite = PlayerLvl1;

        if (index <= Lvl1.Length - 1)
        {
            if (Lvl1Field[index] == 0)
            {
                playerText.text = Lvl1[index];
                image.SetActive(true);
                audioManager.PlayPlayerDialog();
                index++;
            }
            else
            {
                othersText.text = Lvl1[index];
                imageOther.SetActive(true);
                audioManager.PlayKingDialog();
                index++;
            }
        }
        else
        {
            panel.SetActive(false);
            //UI.SetActive(true);
        }
    }

    private void Lvl2Scenario()
    {
        clear();
        SetActions();
        image.GetComponent<Image>().sprite = PlayerLvl1;

        if (index == 0)
        {
            imageOther.GetComponent<Image>().sprite = King;
            if (Lvl2Field[index] == 0)
            {
                playerText.text = Lvl2[index];
                image.SetActive(true);
                audioManager.PlayPlayerDialog();
                index++;
            }
            else
            {
                othersText.text = Lvl2[index];
                imageOther.SetActive(true);
                audioManager.PlayKingDialog();
                index++;
            }
        }
        else if(index <= Lvl2.Length - 1)
            {
            imageOther.GetComponent<Image>().sprite = Zeus;
            if (Lvl2Field[index] == 0)
            {
                playerText.text = Lvl2[index];
                image.SetActive(true);
                audioManager.PlayPlayerDialog();
                index++;
            }
            else
            {
                othersText.text = Lvl2[index];
                imageOther.SetActive(true);
                audioManager.PlayHadesDialog();
                index++;
            }
        }
        
        else
        {
            panel.SetActive(false);
            //UI.SetActive(true);
        }
    }

    private void Lvl3Scenario()
    {
        clear();
        SetActions();
        image.GetComponent<Image>().sprite = PlayerLvl2;
        imageOther.GetComponent<Image>().sprite = Zeus;

        if (index <= Lvl3.Length - 1)
        {
            if (Lvl3Field[index] == 0)
            {
                playerText.text = Lvl3[index];
                image.SetActive(true);
                audioManager.PlayPlayerDialog();
                index++;
            }
            else
            {
                othersText.text = Lvl3[index];
                imageOther.SetActive(true);
                audioManager.PlayHadesDialog();
                index++;
            }
        }
        else
        {
            panel.SetActive(false);
            //UI.SetActive(true);
        }
    }

    private void Lvl4Scenario()
    {
        clear();
        SetActions();
        imageOther.GetComponent<Image>().sprite = Hades;
        image.GetComponent<Image>().sprite = PlayerLvl2;


        if (index <= Lvl4.Length - 1)
        {
            if (Lvl4Field[index] == 0)
            {
                playerText.text = Lvl4[index];
                image.SetActive(true);
                audioManager.PlayPlayerDialog();
                index++;
            }
            else
            {
                othersText.text = Lvl4[index];
                imageOther.SetActive(true);
                audioManager.PlayHadesDialog();
                index++;
            }
        }
        else
        {
            panel.SetActive(false);
            //UI.SetActive(true);
        }
    }

    



    private void clear()
    {
        playerText.text = "";
        othersText.text = "";
        panel.SetActive(true);
        image.SetActive(false);
        imageOther.SetActive(false);
        //UI.SetActive(false);
    }

    private void SetActions()
    {
        Actions = false;
    }


}
