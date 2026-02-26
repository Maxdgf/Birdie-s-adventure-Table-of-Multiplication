using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarsCounter : MonoBehaviour
{
    [SerializeField] private Slider starsProgress;

    void Start()
    {
        TMP_Text allStarsCountView = gameObject.GetComponent<TMP_Text>(); // get tmp text component
        Levels levelsData = LevelsRegisterUtil.GetLevelRegister(); // get levels register

        int starsCount = levelsData.levelsList.Select(levels => levels.stars).Sum(); // all levels stars count
        string countData = string.Format("{0} / {1}", starsCount, Constants.STARS_REQUIRED); // format data string

        allStarsCountView.text = countData;
        starsProgress.value = starsCount;
    }
}
