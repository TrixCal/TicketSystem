using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;

namespace TicketSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //logger
            NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
            logger.Info("Program started");
            //init
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
                    //type of ticket
                    Console.WriteLine("Types of Tickets; Bug, Enhancement, Task");
                    Console.Write("Type: ");
                    string input = Console.ReadLine().ToLower();
                    if(input == "bug"){
                        //Prompt bug elements
                        Bug ticket = new Bug();
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
                        Console.Write("Severity: ");
                        ticket.Severity = Console.ReadLine();
                        //add ticket to file
                        ticketFile.AddTicket(ticket);
                    }
                    else if(input == "enhancement"){
                        try{
                            //Prompt enhancement elements
                            Enhancement ticket = new Enhancement();
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
                            Console.Write("Software: ");
                            ticket.Software = Console.ReadLine();
                            Console.Write("Cost: $");
                            ticket.Cost = double.Parse(Console.ReadLine());
                            Console.Write("Reason: ");
                            ticket.Reason = Console.ReadLine();
                            Console.Write("Estimate: $");
                            ticket.Estimate = double.Parse(Console.ReadLine());
                            //add ticket to file
                            ticketFile.AddTicket(ticket);
                        }
                        catch(Exception ex){
                            logger.Error(ex.Message);
                        }
                    }
                    else if(input == "task"){
                        try{
                            //Prompt task elements
                            Task ticket = new Task();
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
                            Console.Write("Project Name: ");
                            ticket.ProjectName = Console.ReadLine();
                            Console.Write("Due Date: ");
                            ticket.DueDate = DateTime.Parse(Console.ReadLine());
                            //add ticket to file
                            ticketFile.AddTicket(ticket);
                        }
                        catch(Exception ex){
                            logger.Error(ex.Message);
                        }
                    }
                }
                if(choice == "3"){
                    //Removes ticket by ID
                    Console.Write("ID of ticket to remove: ");
                    int ticketID = int.Parse(Console.ReadLine());
                    ticketFile.RemoveTicket(ticketID);
                }
            }while(choice == "1" || choice == "2" || choice == "3");
            logger.Info("Program ended");
        }
    }
}
