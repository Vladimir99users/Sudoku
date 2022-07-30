using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griid_sudoku : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float square_offset = 0.0f;
    public GameObject grid_square;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    public float square_scale = 1.0f;
    public float square_gap = 0.1f;
    public Color Line_higlight_color = Color.red;



    private List<GameObject> Grid_squares = new List<GameObject>();
    private int selected_grid_data = -1;

    void Start()
    {
        if (grid_square.GetComponent<Gridsquare>() == null)
        {
            Debug.LogError("This GameObject need to have GridSquare script attached!");
        }


        CreateGrid();

        //////////////////////////
        if (Game_settings.Instance.GetContinuePreviousGame())
        {
            SetGridFromFile();
        } else
        {
            SetGridNumber(Game_settings.Instance.GetGameMode());
        }

        ADC_SUdoku._instence.ShowBanner();
    }


    public void SetGridFromFile()
    {
        string level = Game_settings.Instance.GetGameMode();
        selected_grid_data = Configuration.ReadGameBoardLevel();
        var data = Configuration.ReadGridData();
        //////////////////////////
        setGridSqaureData(data);
        SetGridNotes(Configuration.GetGridNotes());
    }

    private void SetGridNotes(Dictionary<int,List<int>> notes)
    {
        foreach(var note in notes){
            Grid_squares[note.Key].GetComponent<Gridsquare>().SetGridNotes(note.Value);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {
        int square_index = 0;
        //0,1,2,3,4,5,6,7,8,9.....
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                Grid_squares.Add(Instantiate(grid_square) as GameObject);
                Grid_squares[Grid_squares.Count - 1].GetComponent<Gridsquare>().SetSquareIndex(square_index);
                Grid_squares[Grid_squares.Count - 1].transform.parent = this.transform;//instantiate this game object as a child f the object hiolding this scripts.
                Grid_squares[Grid_squares.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale);
                square_index++;
            }
        }
    }

    private void SetSquaresPosition()
    {
        var sqaure_rect = Grid_squares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
        bool row_moved = false;
        offset.x = sqaure_rect.rect.width * sqaure_rect.transform.localScale.x + square_offset;
        offset.y = sqaure_rect.rect.height * sqaure_rect.transform.localScale.y + square_offset;

        int column_number = 0;
        int row_number = 0;
        foreach (GameObject sqaure in Grid_squares)
        {
            if (column_number + 1 > columns)
            {
                row_number++;
                column_number = 0;
                square_gap_number.x = 0;
                row_moved = false;

            }



            var pos_x_offset = offset.x * column_number + (square_gap_number.x * square_gap);
            var pos_y_offset = offset.y * row_number + (square_gap_number.y*square_gap);

            if(column_number > 0 && column_number % 3 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += square_gap;
            }

            if(row_number>0 && row_number %3 ==0 && row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += square_gap;
            }


            sqaure.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_number++;
        }

    }

    private void SetGridNumber(string Level)
    {
        //мы тут убдем теперь выбирать какой уровень сложности, это зависит на степень заполнения судоку
        selected_grid_data = Random.Range(0, SudokuData.Instance.sudoku_game[Level].Count);

        /////////////////////
        var data = SudokuData.Instance.sudoku_game[Level][selected_grid_data];
        setGridSqaureData(data);

      /*  foreach (var square in Grid_squares)
        {
            square.GetComponent<Gridsquare>().SetNumber(Random.Range(0, 10));
        }*/
    }

    private void setGridSqaureData(SudokuData.SudokuBoardData data)
    {
        for(int index = 0; index < Grid_squares.Count;index++)
        {
            Grid_squares[index].GetComponent<Gridsquare>().SetNumber(data.unsolved_data[index]);
            Grid_squares[index].GetComponent<Gridsquare>().SetCorrectNumber(data.solved_data[index]);
            //Изменение
            Grid_squares[index].GetComponent<Gridsquare>().SetHasDefaultValue(data.unsolved_data[index] != 0 && data.unsolved_data[index] == data.solved_data[index]);
        }
    }


    private void OnEnable()
    {
        GameEvents.OnsquareSelected += OnsquareSelected;
        GameEvents.OnUpdateSquareNumber += CheckBoardCompleted;
    }


    private void OnDisable()
    {
        GameEvents.OnsquareSelected -= OnsquareSelected;
        GameEvents.OnUpdateSquareNumber -= CheckBoardCompleted;

        /*******/
        var solved_data = SudokuData.Instance.sudoku_game[Game_settings.Instance.GetGameMode()][selected_grid_data].solved_data;
        ////////////
        int[] unsolved_data = new int[81];
        Dictionary<string, List<string>> grid_notes = new Dictionary<string, List<string>>();

       
        for (int i = 0; i < Grid_squares.Count; i++)
        {
            var comp = Grid_squares[i].GetComponent<Gridsquare>();
            unsolved_data[i] = comp.getSquareNumber();

            string key = "square_note:" + i.ToString();
            grid_notes.Add(key, comp.GetSquareNotes());
        }

        SudokuData.SudokuBoardData current_game_data = new SudokuData.SudokuBoardData(unsolved_data,solved_data);
        if(Game_settings.Instance.GetExtiAfterWon() == false)//не сохраняем данные когда вся доска заполнилась
        {//передаем в функцию все наши значения
            //передаёт все верно
            Configuration.SaveBoardData(current_game_data,
                Game_settings.Instance.GetGameMode(), selected_grid_data,
                Lives.Instance.getErrorNumber(),grid_notes);
        } else
        {
            Configuration.DeleteDataFile();
        }

        ADC_SUdoku._instence.HideBanner();

        Game_settings.Instance.setExitAfterWon(false);
    }




    private void SetSquareColor(int[] data,Color color)
    {
        foreach(var index in data)
        {
            var conp = Grid_squares[index].GetComponent<Gridsquare>();
            if (conp.hasWrongValue() == false &&  conp.isSelected() == false)
            {
                conp.SetSqareColour(color);
            }
        }
    }

    public void OnsquareSelected(int square_index)
    {
        var horizontal_line = Indicator_line_griid._instance.GetHorizontalLine(square_index);
        var vertical_line  =Indicator_line_griid._instance.GetVerticalLine(square_index);
        var square = Indicator_line_griid._instance.GetSquare(square_index);

        SetSquareColor(Indicator_line_griid._instance.GetAllsquareIndexes(), Color.white);

        SetSquareColor(horizontal_line, Line_higlight_color);
        SetSquareColor(vertical_line, Line_higlight_color);
        SetSquareColor(square, Line_higlight_color);
    }



    private void CheckBoardCompleted(int number)
    {
        foreach (var square in Grid_squares)
        {
            var comp = square.GetComponent<Gridsquare>();
            if (comp.iscorrectNemberset() == false)
            {
                return;
            }
        }

        GameEvents.OnBoardComlitedMethod();

    }

    public void SolveSudoku()
    {
        foreach(var square in Grid_squares)
        {
            var comp = square.GetComponent<Gridsquare>();
            comp.SetCorrectNumber();
        }
        CheckBoardCompleted(0);
    }
}
