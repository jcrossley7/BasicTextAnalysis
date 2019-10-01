//assignmentv2.cs
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
public class TextAnalysis{
	public static void Main (string []args){
		Console.WriteLine("CMP1127 Assignment Text Analysis Program - Produced by Jack Crossley 2016");
		Console.WriteLine("Enter one of the following numbers to indicate how you wish to enter the text:");
		Console.WriteLine("1. Do you want to enter the text manually via the keyboard?");
		Console.WriteLine("2. Do you want to read the text from a file?");
		int userChoice = Convert.ToInt32(Console.ReadLine());
		//Method of correcting invalid input that isn't number 1 or 2
		
		if(userChoice != 1 && userChoice != 2){
			Console.WriteLine("Error, please try again.");
			userChoice=Convert.ToInt32(Console.ReadLine());
		}
		
		string textToAnalyse = "";
		string fileName = "CMP1127M Assignment 1 Text.txt";
		switch (userChoice){
			case 1:
				//Manual string entry
				Console.WriteLine("Enter the text you wish to analyse, using * to indicate the end of the entry.");
				while (!textToAnalyse.EndsWith("*")){
				textToAnalyse = textToAnalyse + Console.ReadLine();
				}
				break;
			case 2:
				//Import file to string
				textToAnalyse = File.ReadAllText(fileName);
				Console.WriteLine("Text to be analysed from the file: ");
				Console.WriteLine(textToAnalyse);
				Console.ReadKey();
				Console.Clear();
				break;
			default:
				Console.WriteLine("Error, try again!");
				break;
		}
		
		//Text analysis code for upper and lower case letters, and determining vowels and consonants
		int vowelCount = 0, sentenceCount = 0, consonantCount = 0, upperCount = 0, lowerCount = 0;
		foreach(char myChar in textToAnalyse){
			//Using .IsUpper on the char to determine if it is upper case.
			if (char.IsUpper(myChar)){
				upperCount++;
			}
			else {
				lowerCount++;
			}
			//Calling the isVowel method from the Tools class using the char.
			if (Tools.isVowel(myChar)){
				vowelCount++;
			}
			else if (!Tools.isVowel(myChar)){
				consonantCount++;
			}
		}
		
		//Counting the frequency of each individual letter
		
		int[] letterArray = new int[25];
		var count = 0;
		char aChar = 'a';
		/*This loop starts with the letter a for the char to count all occurrences. The final value is assigned to an array.
		The char is then increased to b, then c, etc. whilst the array position is also increased to correspond. */
		for (int i = 0; i<letterArray.Length; i++){
			count = textToAnalyse.Count(c => char.ToLower(c) == aChar);
			letterArray[i] = count;
			count = 0;
			aChar++;
		}
		
		//sentence counter
		
		char prevChar = ' ';
		foreach (char presentChar in textToAnalyse){
			/*instead of just checking for full stops, I have included exclamation marks, question marks, and the asterisk so that the last sentence
			of the string is counted. In addition this checks for the current character and compares it with the previous one.*/
			if((prevChar == '.' || prevChar == '?' || prevChar == '!' || prevChar == '*') && (!char.IsNumber(presentChar) && !char.IsLetter(presentChar))){
				sentenceCount++;
			}
			prevChar = presentChar;
		}
		
		//Outputting results
		Console.WriteLine("Number of sentences entered: {0}", sentenceCount);
		Console.WriteLine("Number of vowels: {0}", vowelCount);
		Console.WriteLine("Number of consonants: {0}", consonantCount);
		Console.WriteLine("Number of upper case letters: {0}", upperCount);
		Console.WriteLine("Number of lower case letters: {0}", lowerCount);
		Console.WriteLine("Frequency of individual letters:");
		
		//Outputting individual letter frequencies
		/*This array is filled with the alphabet so there is a way of recalling each letter of the alphabet without wasting space
		by doing Console.WriteLine("A": aCount); which would mean using 26 separate variables for counting letters, and also 26 if statements. */
		char[] alpha ="ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
		
		//This for loop just writes out each letter of the alphabet from one array and the count for that letter from the other arry.
		for (int i = 0; i<letterArray.Length; i++){
		Console.WriteLine("{0}: {1}" , alpha[i], letterArray[i]);
		}
		
		//Finding long words

		//This splits all the punctuation in the text so we just have words, this is then assigned to a string array.
		string[] longWords = textToAnalyse.Split(new Char [] {',' , '.' , '!' , '?', ' '} );
		//establishing the filename for the text file containing all the long words.
		string filePath = "longWordList.txt";
		//Creating a List for all of the long words
		List<string> longwordlist = new List<string>();
		//for each word within the longWords array
		foreach (string word in longWords){
			//replacing any punctuation with spaces
			word.Replace(".", "");
			word.Replace(",", "");
			word.Replace("?", "");
			if (word.Length > 7)
				{
					//if the word has more than 7 characters, it is added to the long word List and written to the console.
					longwordlist.Add(word);
					Console.WriteLine(word);
				}
	}
	//Writing the entire long word list to the filename specified previously.
		File.WriteAllLines(filePath,longwordlist.ToArray());
		
	
	//Option to output results to a file
		Console.WriteLine("Do you wish to save these results? Enter y for yes, or n for no. Note: choosing no will terminate the program.");
		string choice2 = Console.ReadLine().ToString();
		//creating the filename for the results file and using StreamWriter to create it.
		string resultsFile = "Text Analysis Results.txt";
		using (StreamWriter sw = File.AppendText(resultsFile)){
			sw.WriteLine("Text Analysis Results:");
		}
		//implementing second switch statement, with y for yes and n for no, also allowing use of capitals.
		switch(choice2){
			//appending text to the file using streamwriter
			case "y":
			using (StreamWriter sw = File.AppendText(resultsFile)){
				sw.WriteLine(textToAnalyse);
				sw.WriteLine("Number of sentences entered: {0}", sentenceCount);
				sw.WriteLine("Number of vowels: {0}", vowelCount);
				sw.WriteLine("Number of consonants: {0}", consonantCount);
				sw.WriteLine("Number of upper case letters: {0}", upperCount);
				sw.WriteLine("Number of lower case letters: {0}", lowerCount);
				sw.WriteLine("Frequency of individual letters:");
				for (int i = 0; i<letterArray.Length; i++){
					sw.WriteLine("{0}: {1}" , alpha[i], letterArray[i]);
				}
				break;
		}
			case "Y":
			using (StreamWriter sw = File.AppendText(resultsFile)){
				sw.WriteLine(textToAnalyse);
				sw.WriteLine("Number of sentences entered: {0}", sentenceCount);
				sw.WriteLine("Number of vowels: {0}", vowelCount);
				sw.WriteLine("Number of consonants: {0}", consonantCount);
				sw.WriteLine("Number of upper case letters: {0}", upperCount);
				sw.WriteLine("Number of lower case letters: {0}", lowerCount);
				sw.WriteLine("Frequency of individual letters:");
				for (int i = 0; i<letterArray.Length; i++){
					sw.WriteLine("{0}: {1}" , alpha[i], letterArray[i]);
				}
			}
			break;
			//exiting the program if user doesn't want to save the results.
			case "n":
				Environment.Exit(0);
			break;
			case "N":
				Environment.Exit(0);
			break;
	}
	
}
//separate class for character analysis operations
public static class Tools{
	//determining vowels
	public static bool isVowel(char letter){
		switch(letter){
		case 'a':
		case 'e':
		case 'i':
		case 'o':
		case 'u':
		return true;
		//if the char isn't a vowel then isVowel is false.
		default:
			return false;
		}
	}
}
}
