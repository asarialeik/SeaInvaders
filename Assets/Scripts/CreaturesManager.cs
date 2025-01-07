using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesManager : MonoBehaviour
{
    public static CreaturesManager Instance { get; private set; } //Singleton

    //For grid config
    public GameObject[] prefabs;
    public int rows = 3;
    public int columns = 5;
    private float spaceBetweenRows = 0.75f;
    private float spaceBetweenColumns = 1.1f;
    private float creaturesGridPadding = 4.2f;
    private int creaturesTotal;

    //For movement
    public Transform leftBound;
    public Transform rightBound;
    public bool borderCollision = false;
    private Vector3 direction = Vector2.right;
    private bool right = true;
    private float movementSpeed = 0.5f;

    //For fire config
    private float fireRate = 1f;
    public GameObject proyectile;
    private GameObject chosenCreature;
    [SerializeField]
    public List<List<GameObject>> columnsCreatures = new List<List<GameObject>>();

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        for (int col = 0; col < this.columns; ++col)
        {
            columnsCreatures.Add(new List<GameObject>());
        }

        for (int row = 0; row < this.rows; ++row)
        {
            float width = spaceBetweenColumns * (this.columns - 1);
            float height = spaceBetweenRows * (this.rows - 1);
            Vector2 centerPosition = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centerPosition.x, centerPosition.x + (spaceBetweenRows * row) + creaturesGridPadding, 0.0f);


            for (int col = 0; col < this.columns; ++col)
            {
                GameObject creature = Instantiate(this.prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += col * spaceBetweenColumns;
                creature.transform.position = position;
                columnsCreatures[col].Add(creature);
            }
        }

        creaturesTotal = rows * columns;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Fire), this.fireRate, this.fireRate); //Shooting rate for creatures
    }

    private void Update()
    {
        if (GameManager.Instance.playing == true)
        {
            this.transform.position += direction * movementSpeed * Time.deltaTime;
            DirectionChange();
        }
    }

    public void DirectionChange()
    {

        foreach (Transform creature in this.transform)
        {
            if (!creature.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (right == true && creature.position.x >= rightBound.position.x)
            {
                direction = Vector2.left;
                Vector3 position = this.transform.position;
                position.y -= (spaceBetweenRows / 2);
                right = false;
                this.transform.position = position;
            }
            else if (right == false && creature.position.x <= leftBound.position.x)
            {
                direction = Vector2.right;
                Vector3 position = this.transform.position;
                position.y -= (spaceBetweenRows / 2);
                this.transform.position = position;
                right = true;
            }
        }
    }

    public void KilledCreature()
    {
        movementSpeed += 0.4f;
        creaturesTotal -= 1;
        if (creaturesTotal <= 0)
        {
            GameManager.Instance.Winning();
        }
        else
        {
            GameManager.Instance.AddPoints();
        }
    }

    void CreatureChoser()
    {

        List<GameObject> lastCreatures = new List<GameObject>();

        for (int col = 0; col < this.columns; ++col)
        {
            GameObject lastCreature = GetLastCreatureInColumn(col);
            if (lastCreature != null)
            {
                lastCreatures.Add(lastCreature);
            }
        }
        if (lastCreatures.Count > 0)
        {
            int randomIndex = Random.Range(0, lastCreatures.Count);
            chosenCreature = lastCreatures[randomIndex];
        }
    }

    public GameObject GetLastCreatureInColumn(int col)
    {
        if (col >= 0 && col < columnsCreatures.Count)
        {
            for (int i = 0; i < columnsCreatures[col].Count; i++)
            {
                GameObject selectedCreature = columnsCreatures[col][i];
                if (selectedCreature != null && selectedCreature.activeSelf)
                {
                    return selectedCreature;
                }
            }
        }
        return null;
    }

    private void Fire()
    {
        if (GameManager.Instance.playing == true && creaturesTotal > 0)
        {
            CreatureChoser();
            Instantiate(this.proyectile, chosenCreature.transform.position, Quaternion.identity);
        }
    }
}