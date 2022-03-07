using System;
using System.IO;
using System.Collections.Generic;

namespace TicketSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            TicketFile ticketFile = new TicketFile("Tickets.txt");
            string choice;
            do{
                //menu prompt
                Console.WriteLine("1) View Current Tickets");
                Console.WriteLine("2) Add Ticket");
                Console.WriteLine("3) Remove Ticket");
                Console.WriteLine("Enter any key to exit");
                //user input
                choice = Console.ReadLine();
                if(choice == "1"){
                    //Read file and writeline ticket
                    ticketFile.Display();
                }
                if(choice == "2"){
                    //Prompt ticket elements
                    Ticket ticket = new Ticket();
                    ticket.ID = 0;
                    Console.Write("Summary: ");
                    ticket.Summary = Console.ReadLine();
                    Console.Write("Status: ");
                    ticket.Status = Console.ReadLine();
                    Console.Write("Priority: ");
                    ticket.Priority = Console.ReadLine();
                    Console.Write("Submitter: ");
                    ticket.Submitter = Console.ReadLine();
                    Console.Write("Assigned: ");
                    ticket.Assigned = Console.ReadLine();
                    Console.Write("Watching (type \"n\" to stop): ");
                    string line = Console.ReadLine();
                    List<string> names = new List<string>();
                    do{
                        names.Add(line);
                        line = Console.ReadLine();
                    }while(line != "n");
                    ticket.Watching = names;
                    //add ticket to file
                    ticketFile.AddTicket(ticket);
                }
                if(choice == "3"){
                    //Removes ticket by ID
                    Console.Write("ID of ticket to remove: ");
                    int ticketID = int.Parse(Console.ReadLine());
                    if(ticketFile.RemoveTicket(ticketID)){
                        Console.WriteLine($"Ticket #{ticketID} succesfully removed!");
                    }
                    else{
                        Console.WriteLine($"Unable to find ticket #{ticketID}");
                    }
                }
            }while(choice == "1" || choice == "2" || choice == "3");
        }
    }
}
