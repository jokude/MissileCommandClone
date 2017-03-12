using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour {

  public static LevelControl instance = null;
  public EnemySpawner spawner;

  public Text levelText;
  public Text fatIncomingText;
  public Text thinIncomingText;
  public CanvasGroup levelScoreWindow;
  public CanvasGroup endGameText;
  public Text fatScoreText;
  public Text thinScoreText;
  public Text levelScoreText;
  public Text totalScoreText;
  public Text readyText;
  public float fatVsThinRate;
  public float fatVsThinStep;
  public float dropRate;
  public float dropRateStep;
  public int fatRocketValue;
  public int thinRocketValue;
  public int totalRockets;
  public int rocketsStep;

  private int currentLevel;
  private int rocketsExploded;
  private int fatScoreCount;
  private int thinScoreCount;
  private int levelScore;
  private int totalScore;
  private bool gameEnded;
  private BuildingControl buildingControl;
  private LauncherControl launcherControl;

  void Awake(){
    instance = this;
    instance.levelScoreWindow.gameObject.SetActive(false);
    instance.endGameText.gameObject.SetActive(false);
  }

	void Start() {
    launcherControl = GameObject.Find("Launchers").GetComponent<LauncherControl>();
    buildingControl = GameObject.Find("Buildings").GetComponent<BuildingControl>();
    StartGame();
	}

  public int IncreaseScore(EnemyRocket.type type){
    int amount;
    if (type == EnemyRocket.type.FAT) {
      amount = fatRocketValue;
      fatScoreCount++;
    } else {
      amount = thinRocketValue;
      thinScoreCount++;
    }
    levelScore += amount;
    totalScore += amount;
    totalScoreText.text = totalScore.ToString();
    return amount;
  }

  public void UpdateCount(EnemyRocket.type type, int amount){
    Text count = (type == EnemyRocket.type.FAT ? fatIncomingText : thinIncomingText);
    count.text = amount.ToString();
  }

  public void NotifyExplosion(){ 
    rocketsExploded++;
    Debug.Log (rocketsExploded);
    if(rocketsExploded == totalRockets){
      StartCoroutine(ShowLevelScore());
    }
  }

  public void NotifyBuildingsDestroyed(){
    gameEnded = true;
  }

  private void StartGame(){
    totalScore = 0;
    currentLevel = 1;
    gameEnded = false;
    buildingControl.ActivateBuildings();
    launcherControl.ActivateLaunchers();
    StartCoroutine(StartLevelIntro());
  }

  private void EndGame(){
    endGameText.gameObject.SetActive(true);
    System.Action<float> action = (alpha => { endGameText.alpha = alpha; });
    StartCoroutine(FadeIn(action));
    StartCoroutine(WaitForRetryClick());
  }

  private void PauseLevel(){
    // pause game
  }

  private void StartLevel(){

    int fatNumber = 0, thinNumber = 0;
    for(int i = 0 ; i < totalRockets ; i++){
      if(Random.Range (0, 1.0f) <= fatVsThinRate){
        fatNumber++;
      } else {
        thinNumber++;
      }
    }

    fatScoreCount = 0;
    thinScoreCount = 0;
    levelScore = 0;
    rocketsExploded = 0;
    levelText.text = currentLevel.ToString();
    fatIncomingText.text = fatNumber.ToString();
    thinIncomingText.text = thinNumber.ToString();
    spawner.StartDropping(dropRate, fatNumber, thinNumber);
  }

  private void EndLevel(){
    if(gameEnded){
      EndGame();
      return;
    }
    if(dropRate > 0){
      dropRate -= dropRateStep;
    }
    if(fatVsThinRate > 0){
      fatVsThinRate -= fatVsThinStep;
    }
    totalRockets += rocketsStep;
    currentLevel++;
    StartCoroutine(StartLevelIntro());
  }

  private IEnumerator StartLevelIntro(){
    readyText.gameObject.SetActive(true);
    Color color = readyText.color;
    System.Action<float> action = (alpha => { color.a = alpha; readyText.color = color; });
    yield return StartCoroutine(FadeIn(action));
    yield return StartCoroutine(FadeOut(action));
    readyText.gameObject.SetActive(false);
    StartLevel();
  }

  private IEnumerator ShowLevelScore(){
    fatScoreText.text = fatScoreCount.ToString();
    thinScoreText.text = thinScoreCount.ToString();
    levelScoreText.text = levelScore.ToString();
    levelScoreWindow.gameObject.SetActive(true);
    System.Action<float> action = (alpha => { levelScoreWindow.alpha = alpha; });
    yield return StartCoroutine(FadeIn(action));
    yield return new WaitForSeconds(2.0f);
    yield return StartCoroutine(FadeOut(action));
    levelScoreWindow.gameObject.SetActive(false);
    EndLevel();
  }

  private IEnumerator FadeIn(System.Action<float> callback){
    for(float i = 0 ; i < 1.0f ; i += 0.05f){
      callback(i);
      yield return new WaitForSeconds(0.05f);
    }
  }

  private IEnumerator FadeOut(System.Action<float> callback){
    for(float i = 1.0f ; i > 0 ; i -= 0.05f){
      callback(i);
      yield return new WaitForSeconds(0.05f);
    }
  }

  private IEnumerator WaitForRetryClick(){
    while(true){
      if(Input.GetMouseButtonDown(0)){
        endGameText.gameObject.SetActive(false);
        StartGame();
        yield break;
      }
      yield return null;
    }
  }
}
