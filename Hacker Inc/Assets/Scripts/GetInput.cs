using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using System;       // to allow for error catching

public class GetInput : MonoBehaviour {
	// Update is called once per frame
	private List<string> characters = new List<string>(); 						//characters in one line
	public DoorList listOfDoors;

	private string newLine;
	private string output = "";
	private string startingLine = ":/User/ ";									// line that prints at the beginning of every new line

    private Door door;
	//private bool doorOpen = false;

    void Start()
    {
		characters.Add ("\n" + startingLine);                                   //starts the opening line with startingLine
        this.GetComponent<TextMesh>().text = characters[0]; 					// prints :// on the Screen object
    }


    void Update () {
		string myString = ""; // resets the string to NULL

		if (Input.anyKeyDown) {

			if (Input.GetKeyDown(KeyCode.Backspace)) {
				if (!(characters[characters.Count-1].Contains(startingLine)))			// if previous string is :// , do not delete otherwise:
				    characters.RemoveAt (characters.Count - 1); 				// deletes the last character added
				//Debug.Log (characters.Count);
			}
            else if (Input.GetKeyDown(KeyCode.Return)) 							// if there is an enter
			{
                executeCommand();
            }
			else {
				characters.Add (Input.inputString); 							// adds new character to end of list of character
				//Debug.Log (characters.Count);
			}

			myString = string.Join (myString, characters.ToArray ());			// converts the characters into a string
			newLine = myString;													 // stores all the characters for one line, resets when a new line starts
			this.GetComponent<TextMesh> ().text = output + myString; 			 // prints the string on the Screen object
		}
	}

    void executeCommand()
    {
        characters.Clear();                                                 // clear characters for new line

        if (newLine.Contains("clear"))
        {
            endLine();
            output = "";
            return;
        }

        if (startingLine.Contains("Doors"))
        {
            doors();
            return;
        }

        if (newLine.Contains("ls"))
           {
               if (listOfDoors.getSize() != 0)                                     // checks if there are doors in DoorList
                  newLine = newLine + "\n\nDoors\n";                              // if yes, prints that Doors exist
           }
        if (newLine.Contains("cd"))
           {
               if (newLine.Contains("Doors"))                                      // if user wants to go into Doors
                   startingLine = startingLine + "Doors/ ";                        // add Doors/ to the startingLine
           }


        if (newLine.Contains("help"))
              help();

        endLine();
    }

    void doors()
    {
        if (newLine.Contains("ls"))
        {
            characters.Add("\n\nDoors available:" + listOfDoors.getNames());                         // gets all the names of doors in DoorList
        }

        if (newLine.Contains("clear"))
        {
            endLine();
            output = "";
            return;
        }

        if (newLine.Contains("help"))
            help();
        else
        {
            try
            {
                int index = int.Parse(newLine.Substring(newLine.Length - 1));       // gets the Door number
                door = listOfDoors.getDoor(index - 1);                                  // gets the Door from DoorList
            }
            catch (FormatException)
            {
                characters.Add("\n\nERROR: Please enter name of door\n");
                endLine();
                return;
            }

            if (newLine.Contains("open Door"))
            {

                if (!door.doorMoved)
                {                                               //checks if the door is open
                    StartCoroutine(door.Move());                                    // sends a signal to Move() in Door script
                                                                                    //doorOpen = true;
                                                                                    //Debug.Log("Open");
                    characters.Add("\n\n\t*** DOOR OPENED *** \n");
                }
            }

            if (newLine.Contains("close Door"))
            {
                if (door.doorMoved)
                {
                    StartCoroutine(door.Move());                                    // close door

                    //doorOpen = false;
                    //Debug.Log("Close");
                    characters.Add("\n\n\t*** DOOR CLOSED *** \n");
                }
            }
        }

        endLine();
    }

    void endLine(){
		output = string.Concat (output, newLine);								// adds the previous line input to output so that it does not get deleted
		characters.Add("\n" + startingLine);												// start new line with ://
	}

    void help()
    {
        characters.Add("\n\nWelcome. Here are the list of commands you can try: \n" +
            "\tls: list out folders/files available\n"+
            "\tcd: go to a folder\n"+
            "\tclear: clears the screen\n"+
            "\topen/close: actions that can be done on a file\n" +
            "\nNow clear your screen to continue\n");
    }
}

/*
 * Things to work on: 
 * - ERROR when commands do not exist
 * - checks the command when user hits ENTER
 * 
*/
