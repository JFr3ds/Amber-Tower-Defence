using System;
using UnityEditor.Animations;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int reward;
    [SerializeField] private Anim actualAnimation;
    [SerializeField] private AnimController anim;
    [SerializeField] private float maxlifePoints;
    private float currentLifePoints;
    [SerializeField] public float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform lifeBaarParent;
    [SerializeField] private Image lifeBaar;

    [SerializeField] private int actualIndex;
    private Vector3 target;
    private Quaternion rotationLifeBaar;
    [SerializeField] private int damage;
    
    //Flock

    [SerializeField] private Vector3 vCentre;
    [SerializeField] private Vector3 vAvoid;
    [SerializeField] private float gSpeed;
    [SerializeField] private float nDistance;
    [SerializeField] private int groupSize;
    
    

    private void Start()
    {
        rotationLifeBaar = Camera.main.transform.rotation;
    }

    private void Update()
    {
        Move();
        Rotate();
        lifeBaarParent.rotation = rotationLifeBaar;
        
    }

    protected void Move()
    {
        transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
    }

    protected void Rotate()
    {
        if (Vector3.Distance(transform.position, LevelPath.Instance.points[actualIndex]) <
            LevelPath.Instance.minDistance)
        {
            actualIndex++;
        }

        if (actualIndex >= LevelPath.Instance.points.Count)
        {
            ActionsController.OnEnemyDone?.Invoke();
            actualIndex = 0;
            Player.Instance.UpdateLifePoints(damage);
            gameObject.SetActive(false);
        }

        target = new Vector3(LevelPath.Instance.points[actualIndex].x, transform.position.y,
                     LevelPath.Instance.points[actualIndex].z) - transform.position;
        Quaternion angle = Quaternion.LookRotation(target, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, rotationSpeed * Time.deltaTime);
    }

    public void Initialize()
    {
        actualIndex = 1;
        currentLifePoints = maxlifePoints;
        transform.position = LevelPath.Instance.points[0];
        transform.rotation = ToolBox.GetDesireRotation(LevelPath.Instance.points[1], transform.position);
        anim.SetAnimator(actualAnimation);
        UpdaleLifeBaar();
    }

    public void GetDamage(float damage)
    {
        currentLifePoints -= damage;
        UpdaleLifeBaar();
        if (currentLifePoints <= 0)
        {
            anim.SetAnimator(Anim.Die);
            gameObject.SetActive(false);
            ActionsController.OnGetReward?.Invoke(reward);
            ActionsController.OnEnemyDone?.Invoke();
        }
    }

    void UpdaleLifeBaar()
    {
        lifeBaar.fillAmount = currentLifePoints / maxlifePoints;
    }

    void Flock()
    {
        vCentre = Vector3.zero;
        vAvoid = Vector3.zero;
        gSpeed = 0.01f;
        groupSize = 0;
        
        if (Vector3.Distance(transform.position, LevelPath.Instance.points[actualIndex]) <
            LevelPath.Instance.minDistance)
        {
            actualIndex++;
        }
        
        
        if (actualIndex >= LevelPath.Instance.points.Count)
        {
            gameObject.SetActive(false);
            actualIndex = 0;
        }
        
        for (int i = 0; i < 3; i++)
        {
            foreach (GameObject go in PoolManager.Instance.poolObjects[i].objects)
            {
                if (go.activeSelf && go != gameObject)
                {
                    nDistance = Vector3.Distance(go.transform.position, transform.position);
                    if (nDistance <= PoolManager.Instance.neighbourDistance)
                    {
                        vCentre += go.transform.position;
                        groupSize++;
                        if (nDistance < 3f)
                        {
                            vAvoid = vAvoid + (transform.position - go.transform.position);
                        }

                        Enemy enemy = go.GetComponent<Enemy>();
                        gSpeed = gSpeed + enemy.movementSpeed;
                    }
                }
            }
        }

        if (groupSize > 0)
        {
            vCentre = vCentre / groupSize + (LevelPath.Instance.points[actualIndex] - transform.position);
            movementSpeed = gSpeed / groupSize;
            Vector3 direction = (vCentre + vAvoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }
}