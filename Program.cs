// Jonathan Bodrero
// July 2025
//  Final Project: Wordle-ish

using System.Diagnostics;
using System.Text;  //  Allow string builder

string[] readChoiceList = File.ReadAllLines("ChoiceList.txt");  // Read in possible target words
string[] readAllWordList = File.ReadAllLines("AllWordList.txt");    // Read in all possible words
string wordsToAdd = File.ReadAllText("WordsToAdd.txt");    // Read in possible words to add.
string pastGames = File.ReadAllText("PastGames.txt");    // Read in prior games.


List<string> choiceList = new List<string>();       //  Make lists for target words, all words, words to add.
List<string> allWordList = new List<string>();
List<string> wordsToAddList = new List<string>();

Random rand = new Random();
string currentGuess = "";
int correctCount = 0;

for (int i = 0; i < readChoiceList.Length; i++)     //  Create list of choice words
{
    foreach (string word in readChoiceList[i].Split(','))
        choiceList.Add(word.Trim());
}
int targetNumber = rand.Next(0, choiceList.Count);
// Console.WriteLine(targetNumber);     Debugging
string targetWord = choiceList[targetNumber];   //  Randomly Select the secret word.
List<char> targetWordChar = new List<char>();   //  Make a list of characters from target word.
for (int i = 0; i < 5; i++)
{ targetWordChar.Add(targetWord[i]); }

//  Console.WriteLine(targetWord);  //  Used for debugging.
/*foreach (char ch in targetWordChar)
{ Console.Write($"{ch}_"); }
Console.ReadKey(true); */


int turn = 0;       //  Track with turn, start with 0.

for (int i = 0; i < readAllWordList.Length; i++)    //  Create list of all words
{
    foreach (string word in readAllWordList[i].Split(','))
        allWordList.Add(word.Trim());
}


//foreach (string word in allWordList) //  Debug
//{ Console.Write($"{word}."); }


/*Console.WriteLine("Words check");     //Ensure all target words are in AllWordList
for (int i = 0; i < choiceList.Count; i++)
{
    string choice = choiceList[i];
    if (! allWordList.Contains(choice))
    { Console.WriteLine(choice); }
}
Console.WriteLine("End words check");

Console.ReadKey();*/

Debug.Assert(IsValidGuess("aback", ref allWordList, ref wordsToAdd) == true);        //  Asserts for validation.
Debug.Assert(IsValidGuess("wxyz", ref allWordList, ref wordsToAdd) == false);

//List<char> targetCharTempTest = new List<char>(){'a', 'b', 'a', 'c', 'k'};  //  Testing CheckWord method.
//Debug.Assert(CheckWord("aback", "aback", targetCharTempTest) == true, "Should match target word.");
//Debug.Assert(CheckWord("abase", "aback", targetCharTempTest) == false, "Word doesn't match target.");


Console.Clear();        //  Give instructions.
Console.WriteLine(@"Welcome to Wordze. 
I will pick a 5-letter word and you will have up to 6 guesses.
For each guess, enter an actual 5-letter word.
After each guess, a green letter means it is in the word in the correct spot.
A yellow letter means it is in the word but not in the correct spot.
A white letter means it is not in the word at all.
Are you ready to play?  Press any key to continue.");
Console.ReadKey(true);

Console.Clear();    

Console.WriteLine(@"Guess #1: 
Guess #2: 
Guess #3: 
Guess #4: 
Guess #5: 
Guess #6: ");

// Game play, starting with getting guess.
do
{

    do
    {
        Console.SetCursorPosition(0, 7);
        Console.Write("Current guess: ");
        currentGuess = Console.ReadLine().ToLower();    //  Convert all to lower case for ease in analysis.
    } while (!IsValidGuess(currentGuess, ref allWordList, ref wordsToAdd));      // Check if word is 5-letters and in word list.
    Console.SetCursorPosition(0, 6);
    Console.WriteLine("                                                   ");
    correctCount = 0;       //  Reset correct count on each turn

    List<char> targetCharTemp = new List<char>();       //  Create a list of chars of target for comparison to guess
    foreach (char ch in targetWordChar)
    { targetCharTemp.Add(ch); }
    CheckWord(currentGuess, targetWord, targetCharTemp);    //  Check word and color letters.

    turn++;                                                 //  Prep for next turn
    Console.SetCursorPosition(0, 7);
    Console.Write("Current guess:                 ");
    if (correctCount == 5)                                  //  Win condition
    {
        //winWords = $" You won in {turn + 1} guesses!";
        break;
    }
} while (turn < 6);

//  End of game stuff
Console.SetCursorPosition(0, 6);
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"The secret word was {targetWord}.                          ");
if (turn < 6)
{ Console.WriteLine($"You won in {turn} guesses!"); }   // Win message.
else
{
    turn++;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"You didn't get it this time.  Please play again.");
}
Console.ForegroundColor = ConsoleColor.White;   

string currentGame = $"{targetWord}" + ", " + $"{turn}" + ", "; //  Add current game to stats.
pastGames = pastGames + currentGame;
File.WriteAllText("PastGames.txt", pastGames);       //  Write out to file.

File.WriteAllText("WordsToAdd.txt", wordsToAdd);        //  check for words to add to AllWordsList.txt



static bool IsValidGuess(string word, ref List<string> allWordList, ref string wordsToAdd)  //  Checks if word has 5 letters and is in allWordList
{
    bool validLength = false;
    bool validWord = false;
    string guess = word.Trim();
    guess = guess.ToLower();
    if (guess.Length != 5)
    {
        Console.SetCursorPosition(0, 6);
        Console.WriteLine($"Oops, {word} is not a 5-letter word.  Try again.\nCurrent guess:           ");
        return false;
    }
    else
    {
        validLength = true;
    }
    if (!allWordList.Contains(word))
    {
        Console.SetCursorPosition(0, 6);
        Console.WriteLine("That is not a word that I know.  Try again.   \nCurrent guess:                 ");
        
        wordsToAdd = wordsToAdd + word + ", ";
        return false;
    }
    else
    {
        validWord = true;
    }
    if (validLength && validWord)
    {
        Console.SetCursorPosition(0, 6);
        Console.WriteLine();
        return true;
    }
    else { return false; }
}

//  Check if guessed word matches target word.  Color coordinate letters.
bool CheckWord(string currentGuess, string targetWord, List<char> targetCharTemp)
{

    for (int i = 0; i < 5; i++)     // Check each letter successively.
    {
        if (currentGuess[i] == targetWord[i])       // If letter in correct spot, green.
        {
            correctCount++;                                //  Count the number of correct letters for win condition.
            Console.SetCursorPosition(10 + i, 0 + turn);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{currentGuess[i]}");
            Console.ForegroundColor = ConsoleColor.White;
            targetCharTemp.Remove(targetWordChar[i]);   //  Mark correct letter as already used in tempChar List.
            targetCharTemp.Insert(i, '-');
        }
        else if (targetCharTemp.Contains(currentGuess[i])) // If letter in word but wrong spot, yellow.
        {
            Console.SetCursorPosition(10 + i, 0 + turn);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{currentGuess[i]}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.SetCursorPosition(10 + i, 0 + turn);    //  If not in word, leave white.
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{currentGuess[i]}");

        }
    }
    return true;
}