# FinalProject
Final Project for CS 1400 Summer 2025 by Jonathan Bodrero

Overview and reflection
For this final project, I decided to create a Wordle-like game.  It would have basically the same game-play as the namesake, although I was under no delusion it would look quite as good as the original since it would be console based (using what I have learned).  
This project is interesting to me because I like the challenge of Wordle but you can only get one word a day.  My game would allow you to play it as many times as you desire in a day.  I like how the game is a fun challenge where you have to build off of previous work to arrive at the solution.  It encourages persistence and logical thinking.  I wanted to be able to have a game I could play repeatedly and even share with my students to help them practice logical thinking and persistence.  
I had hoped to make the game as close to the actual Wordle as possible.  I have a functioning game that shows the guesses with the correct color coding of letters.  It even records words that the user tries but aren’t in the list so I can verify that they are actual words (not just misspellings) and incorporate them into the list of possible words.  I even have it record recent words and the number of guesses for each so I could possibly run some statistics (given enough data).  One limitation is that the original Wordle has an alphabet display showing which letters have been guessed and whether they were in the word or not.  I have not been able to do that but the game is still functional.
This project has helped me learn/review many concepts from the course.  I’ve practiced reading and writing to files, arrays and lists, for loops, console display (cursor position and coloring), methods, and some (mostly print based) error checking (with numerous trial runs to verify functionality).  I’ve tried to make the code robust but there is probably still a way to break it.  I’ve also learned that programming takes a lot of practice and patience (and more time that you think).

Pseudocode and flowchart:
Start
Load wordLists
Display instructions
Show guess lines, prompt
Set numCorrect = 0
Get guess
Convert to all lower case 
Check if valid (in wordList), if not valid, try again.
For each letter in guess, check if in targetWord
If not in targetWord, update color to red
Else if in targetWord and correct location, update color green
Else if in targetWord and incorrect location, update color yellow
Update guess line with color letters
Turn++
If turn = 6, lose condition.
Else, loop to get guess.
End

I'll try to upload an image of the flowchart in Canvas.