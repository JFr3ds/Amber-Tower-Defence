using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform cameraMain;

    [SerializeField] private Transform ui_3d;
    [SerializeField] private Button placeTower;
    [SerializeField] private float offsetY;
    [SerializeField] private LayerMask lm;
    [SerializeField] private TMP_Text userMoney;
    [SerializeField] private TMP_Text playerHealt;
    [SerializeField] private Image playerHealtBaar;
    [SerializeField] private GameObject[] screens;

    private int indexTowerCreator;


    private void Awake()
    {
        ActionsController.OnUpdateUI += OnUpdateUI;
        ActionsController.OnSetUpUI += SetUpUi;
        ActionsController.OnUpdatePlayerHealt += OnUpdatePlayerHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray r = cameraMain.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 100f, lm))
            {
                if (!hit.transform.GetComponent<TowerCreator>().myTower)
                {
                    SetUpUi(hit.transform.position, hit.transform.GetSiblingIndex(), hit.transform.GetComponent<TowerCreator>().indexScreen);
                }
            }
            else
            {
                ui_3d.gameObject.SetActive(false);
            }
        }
    }

    void OnUpdateUI(int ammount)
    {
        userMoney.text = ammount.ToString();
    }

    private void OnDestroy()
    {
        ActionsController.OnSetUpUI -= SetUpUi;
        ActionsController.OnUpdateUI -= OnUpdateUI;
        ActionsController.OnUpdatePlayerHealt -= OnUpdatePlayerHealth;
    }

    
    void SetUpUi(Vector3 desirePos, int towerCreatorIndex, int indexScreen)
    {
        indexTowerCreator = towerCreatorIndex;
        ui_3d.transform.position = cameraMain.GetComponent<Camera>().WorldToScreenPoint(desirePos);
        //desirePos.y += offsetY;
        //ui_3d.transform.position = desirePos;
        //ui_3d.rotation = cameraMain.rotation;
        ui_3d.gameObject.SetActive(true);
        SetScreen(indexScreen);
    }
    

    public void OnCreateTower(int indexPoolTower)
    {
        ActionsController.OnBuyTower?.Invoke(
            PoolManager.Instance.poolObjects[indexPoolTower].prefObject.GetComponent<Tower>().towerPrice,
            indexTowerCreator, indexPoolTower);
    }

    void OnUpdatePlayerHealth()
    {
        playerHealt.text = $"{Player.Instance.currentLifePoints}/{Player.Instance.maxLifePoints}";
        float f = (float)Player.Instance.currentLifePoints / (float)Player.Instance.maxLifePoints;
        playerHealtBaar.fillAmount = f;
    }

    void SetScreen(int screenIndex)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(i == screenIndex);
        }
    }
}