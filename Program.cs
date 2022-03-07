using System;
using System.IO;
using System.Collections.Generic;

namespace TicketSystem
{
    class Program
    {
        static string[] FileToArr(string file){
            StreamReader sr = new StreamReader(file);
            List<string> strings = new List<string>();
            while(!sr.EndOfStream){
                strings.Add(sr.ReadLine());
              }
            sr.Close();
            return strings.ToArray();
        }
        static void Main(string[] args)
        {
            string file = "Tickets.txt";
            string choice;
            Random random = new Random();
            do{
                //menu prompt
                Console.WriteLine("1) View Current Tickets");
                Console.WriteLine("2) Add Ticket");
                Console.WriteLine("3) Remove Ticket");
                Console.WriteLine("Enter any key to exit");
                //user input
                choice = Console.ReadLine();
                if(choice == "1"){
                    //Read file and writeline tickets
                    if(File.Exists(file)){
                        string[] lines = FileToArr(file);
                        foreach(string line in lines){
                            string[] arr = line.Split(',');
                            Console.WriteLine($"{arr[0]}: Summary) {arr[1]}; Status) {arr[2]}; Priority) {arr[3]}; Submitter) {arr[4]}; Assigned) {arr[5]}; Watching) {arr[6]}");
                            Console.WriteLine("---------------------");
                        }
                    }
                    else{
                        Console.WriteLine("File doesn't exist, add a ticket to begin.");
                    }
                }
                if(choice == "2"){
                    //Prompt ticket elements and write to file
                    string[] newTicket = new string[7];
                    //Generates a random id and checks if it doens't exist in the file
                    int id;
                    bool isAvailable;
                    do{
                        isAvailable = false;
                        id = random.Next(1, 999);
                        if(File.Exists(file)){
                            string[] lines = FileToArr(file);
                            foreach(string line in lines){
                                string[] arr = line.Split(',');
                                if(id.ToString() == arr[0]) isAvailable = true;
                            }
                        }
                    }while(isAvailable);
                    newTicket[0] = id.ToString();
                    //Summary
                    Console.Write("Summary: ");
                    newTicket[1] = Console.ReadLine();
                    //Status
                    Console.Write("Status: ");
                    newTicket[2] = Console.ReadLine();
                    //Priority
                    Console.Write("Priority: ");
                    newTicket[3] = Console.ReadLine();
                    //Submitter
                    Console.Write("Submitter: ");
                    newTicket[4] = Console.ReadLine();
                    //Assigned
                    Console.Write("Assigned: ");
                    newTicket[5] = Console.ReadLine();
                    //Watching
                    Console.Write("Watching(Type \"n\" To Stop): ");
                    string person = Console.ReadLine();
                    while(person.ToLower() != "n"){
                        if(newTicket[6] == null)
                            newTicket[6] += person;
                        else newTicket[6] += "|" + person;
                        person = Console.ReadLine();
                    }
                    
                    //Writes ticket to document
                    string ticketOutput = "";
                    foreach(string i in newTicket){
                        if(ticketOutput == "") ticketOutput += i;
                        else ticketOutput += "," + i;
                    }
                    StreamWriter sw = new StreamWriter(file, true);
                    sw.WriteLine(ticketOutput);
                    sw.Close();

                }
                if(choice == "3"){
                    //Removes ticket by ID
                    if(File.Exists(file)){
                        string[] tickets = FileToArr(file);
                        //Prompt
                        Console.Write("What ticket would you like to delete?(ID): ");
                        //Checks if ticket exists
                        string id;
                        int i = 0;
                        bool ticketExists = false;
                        do{
                            id = Console.ReadLine();
                            ticketExists = false;
                            for(i = 0; i < tickets.Length; i++){
                                string[] arr = tickets[i].Split(',');
                                if(id == arr[0]){
                                    ticketExists = true;
                                    break;
                                }
                            }
                            if(!ticketExists) Console.WriteLine("Ticket doesn't exist.");
                        }while(!ticketExists);
                        //Writes new file
                        StreamWriter sw = new StreamWriter(file, false);
                        for(int a = 0; a < tickets.Length; a++){
                            if(a != i){
                                sw.WriteLine(tickets[a]);
                            }
                        }
                        sw.Close();
                    }

                }
            }while(choice == "1" || choice == "2" || choice == "3");
        }
    }
}
