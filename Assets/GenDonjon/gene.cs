using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] ouverture = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject Cell;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;

        public bool obligatory;

        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn 1 - can spawn 2 - HAS to spawn

            if (x>= minPosition.x && x<=maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }

            return 0;
        }

    }

    public int nb_monde;
    public Vector2Int size;
    public int startPos = 0;
    public Rule[] Cells;
    public Vector2 offset;

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        float coXboss = 0f;
        float coYboss = 0f;
        // MazeGenerator(startPos);
        // coXboss = GenerateDungeon();

        for (int i = 0; i <nb_monde; i++)
        {
            MazeGenerator(startPos);
            (coXboss,coYboss )= GenerateDungeon(i,coXboss,coYboss);
            // int coXbossInt = (int)coXboss;
            // startPos = coXbossInt;
        }

    }

    private (float,float) GenerateDungeon(int itteration,float bossX , float bossY)
    {
        bool firstSalle = true;
        float coX = 0f;
        float coY = 0f;
        GameObject newCell = new GameObject();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                if (currentCell.visited)
                {
                    int randomCell = -1;
                    List<int> availableCells = new List<int>();

                    for (int k = 0; k < Cells.Length; k++)
                    {
                        int p = Cells[k].ProbabilityOfSpawning(i, j);

                        if(p == 2)
                        {
                            randomCell = k;
                            break;
                        } else if (p == 1)
                        {
                            availableCells.Add(k);
                        }
                    }

                    if(randomCell == -1)
                    {
                        if (availableCells.Count > 0)
                        {
                            randomCell = availableCells[Random.Range(0, availableCells.Count)];
                        }
                        else
                        {
                            randomCell = 0;
                        }
                    }


                    var Lacase = board[(i + j * size.x)];
                    int nb = Nbopen(Lacase);
                    int QuelSalle = 0;
                    float toRotate = 0;
                    coX = i * offset.x + bossX;
                    coY = -j * offset.y +bossY;
                    (QuelSalle , toRotate) = WichSalle(Lacase,nb);
                    
                    //var newCell = Instantiate(Cells[QuelSalle].Cell, new Vector3(i * offset.x, 0, -j * offset.y), larota, transform).GetComponent<GenProce>();
                    if (firstSalle)
                    {
                        newCell = Instantiate(Cells[5].Cell, new Vector3(coX, 0, coY), Quaternion.identity, transform);
                        firstSalle = false;
                    }
                    else
                    {
                        newCell = Instantiate(Cells[QuelSalle].Cell, new Vector3(coX, 0, coY), Quaternion.identity, transform);
                    }
                    
                    newCell.transform.rotation = Quaternion.Euler(0,toRotate,0);

                }
            }
        }

        Destroy(newCell);
        newCell = Instantiate(Cells[6].Cell, new Vector3(coX, 0, coY), Quaternion.identity, transform);
        return (coX , coY);
        
    }

    void MazeGenerator(int startpos)
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startpos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k<1000)
        {
            k++;

            board[currentCell].visited = true;
            
            if(currentCell == board.Count - 1)
            {
                break;
            }
            
            //Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].ouverture[2] = true;
                        currentCell = newCell;
                        board[currentCell].ouverture[3] = true;
                    }
                    else
                    {
                        board[currentCell].ouverture[1] = true;
                        currentCell = newCell;
                        board[currentCell].ouverture[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].ouverture[3] = true;
                        currentCell = newCell;
                        board[currentCell].ouverture[2] = true;
                    }
                    else
                    {
                        board[currentCell].ouverture[0] = true;
                        currentCell = newCell;
                        board[currentCell].ouverture[1] = true;
                    }
                }

            }

        }
       
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check up neighbor
        if (cell - size.x >= 0 && !board[(cell-size.x)].visited)
        {
            neighbors.Add((cell - size.x));
        }

        //check down neighbor
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        
        //check right neighbor
        if ((cell+1) % size.x != 0 && !board[(cell +1)].visited)
        {
            neighbors.Add((cell +1));
        }

        //check left neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell -1));
        }

        return neighbors;
    }
    int Nbopen(Cell Salle)
    {
        int nb = 0;
        for (int i = 0; i < 4; i++)
        {
            if (Salle.ouverture[i] == true)
            {
                nb++;
            }
        }

        return nb;
    }
    (int,float) WichSalle(Cell Salle,int nbouv)
    {
        // 0 - Up   1 -Down   2 - Right   3- Left
        if (nbouv == 1)
        {
            return (4, 0);
        }
        if (nbouv == 2)
        {
            if (Salle.ouverture[0] == true && Salle.ouverture[1]) // droit de haut en bas
            {
                return (0,0);
            }
            else if (Salle.ouverture[2] == true && Salle.ouverture[3]) // droit de droite a gauche
            {
                return (0, 90);
            }
            else if (Salle.ouverture[0] == true && Salle.ouverture[2]) // L de haut a droite
            {
                return (3, 0);
            }
            else if (Salle.ouverture[0] == true && Salle.ouverture[3]) // L de haut a gauche
            {
                return (3, 270);
            }
            else if (Salle.ouverture[1] == true && Salle.ouverture[2]) // L de bas a droite
            {
                return (3, 90);
            }
            else if (Salle.ouverture[1] == true && Salle.ouverture[3]) // L de bas a gauche
            {
                return (3, 180);
            }
        }else if (nbouv == 3)
        {
            // 0 - Up   1 -Down   2 - Right   3- Left
            if (Salle.ouverture[0] == true && Salle.ouverture[1] && Salle.ouverture[2]) // haut droit bas
            {
                return (1,0);
            }else if (Salle.ouverture[2] == true && Salle.ouverture[1] && Salle.ouverture[3]) //  droit bas gauche
            {
                return (1,90);
            }else if (Salle.ouverture[1] == true && Salle.ouverture[3] && Salle.ouverture[0]) // bas gauche haut
            {
                return (1, 180);
            }else if (Salle.ouverture[3] == true && Salle.ouverture[0] && Salle.ouverture[2]) // gauche haut droit
            {
                return (1,270);
            }
        }else
        {
            return (2, 0);
        }

        return (2, 0);
    }

}