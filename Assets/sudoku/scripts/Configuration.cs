using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;//regex
using UnityEngine;


public class Configuration : MonoBehaviour
{
#if UNITY_ANDROID && !UNITY_EDITOR

    private static string dir = Application.persistentDataPath;//что такое?

#else
    private static string dir = Directory.GetCurrentDirectory();
#endif 
  private  static string file = @"\Board_data.ini";
  private  static string path = dir + file;

    public static void DeleteDataFile()
    {
        File.Delete(path);
    }
    //разобраться что это 
    //передает левел правильно
    public static void SaveBoardData(SudokuData.SudokuBoardData boardData,string Level,int board_index,
        int ErrorNumber,Dictionary<string,List<string>> grid_notes)
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);
        string currentTime = "#Time:" + Clock_Sudoku.GetCurrentTime();
        string Level_string = "#Level:" + Level;
        string error_Number_string = "#Error:" + ErrorNumber;
        string Board_index_string = "#Board_index:" + board_index.ToString();
        string unsolved_string = "#unsolved:";
        string solved_string = "#solved:";
        foreach (var unsolved_data in boardData.unsolved_data)
        {
            unsolved_string += unsolved_data.ToString() + ",";
        }
        foreach (var solved_data in boardData.solved_data)
        {
            solved_string += solved_data.ToString() + ",";
        }

        writer.WriteLine(currentTime);
        writer.WriteLine(Level_string);
        writer.WriteLine(error_Number_string);
        writer.WriteLine(Board_index_string);
        writer.WriteLine(unsolved_string);
        writer.WriteLine(solved_string);
        //grid_loads = grid_notes;
        foreach (var square in grid_notes)
        {
            string square_string = "#" + square.Key + ":";
            bool save = false;
            foreach(var note in square.Value)
            {
                if(note != " ")
                {
                    square_string += note + ",";
                    save = true;
                }
            }


            if (save)
            {
                writer.WriteLine(square_string);
            }
        }

        writer.Close();
    }
    /// <summary>
    /// ///////////////////////
    /// </summary>
    /// <returns></returns>
    public static Dictionary<int,List<int>>GetGridNotes()
    {
        Dictionary<int, List<int>> grid_notes = new Dictionary<int, List<int>>();
        string line;
        StreamReader file = new StreamReader(path);
    
        while ((line = file.ReadLine()) != null)
        {
            
            string[] word = line.Split(':');
            if (word[0] == "#square_note")
            {
                int square_index = -1;
                List<int> notes = new List<int>();
                int.TryParse(word[1], out square_index);
                 
                string[] substring = Regex.Split(word[2],",");

                foreach (var note in substring)
                {
                    int note_number = -1;
                    int.TryParse(note, out note_number);
                    if(note_number > 0)
                    {
                        notes.Add(note_number);
                    }

                  
                }
                grid_notes.Add(square_index, notes);
            }
        }
    
       
        file.Close();

        return grid_notes;
    }

    public static string ReadBoardLevel()
    {
        string line;
     
        string level = "";
        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#Level")
            {

                level = word[1];
            }
        }
     //   Debug.Log(level);
        file.Close();
        return level;
    }


    public static SudokuData.SudokuBoardData ReadGridData()
    {
        string line;
        StreamReader file = new StreamReader(path);
        int[] unsolved_data = new int[81];
        int[] solved_data = new int[81];
        int unsolved_index = 0;
        int solved_index = 0;

        while((line = file.ReadLine())!= null)
        {
            string[] word = line.Split(':');
            if(word[0] == "#unsolved")
            {
                string[] substrings = Regex.Split(word[1], ",");
                foreach (var value in substrings)
                {
                    int square_number = -1;
                    if(int.TryParse(value, out square_number))
                    {
                        unsolved_data[unsolved_index] = square_number;
                        unsolved_index++;
                    }
                }
            }

            if (word[0] == "#solved")
            {
                string[] substrings = Regex.Split(word[1], ",");
                foreach (var value in substrings)
                {
                    int square_number = -1;
                    if (int.TryParse(value, out square_number))
                    {
                        solved_data[solved_index] = square_number;
                        solved_index++;
                    }
                }
            }
        }

        file.Close();
        return new SudokuData.SudokuBoardData(unsolved_data,solved_data);
    }


    public static int ReadGameBoardLevel()
    {

        int level = -1;
        string line;
        StreamReader file = new StreamReader(path);

        while((line = file.ReadLine())!= null)
        {
            string[] word = line.Split(':');
            if(word[0] == "#Board_index")
            {
                int.TryParse(word[1], out level);
            }
        }

        file.Close();
        return level;
    }


  

    public static float ReadGameTime()
    {
        float time = -1.0f;
        string line;
        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#Time")
            {
                float.TryParse(word[1], out time);
            }
        }
        file.Close();
        return time;

    }


    public static int ErrorNumber()
    {
        int errors = 0;
        string line;
        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#Error")
            {
                int.TryParse(word[1], out errors);
            }
        }
        file.Close();
        return errors;
    }

    public static bool GameFileExist()
    {
        return File.Exists(path);
    }
}
