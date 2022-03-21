using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NLog.Web;

namespace TicketSystem{
    class TicketFile{
        public string FilePath { get; set; }
        public List<Ticket> Tickets { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public TicketFile(string ticketFilePath){
            FilePath = ticketFilePath;
            Tickets = new List<Ticket>();
            try{
                //populate list
                StreamReader sr = new StreamReader(FilePath);
                while(!sr.EndOfStream){
                    //read line
                    string line = sr.ReadLine();
                    string[] ticketDetails = line.Split(',');
                    //create instance of ticket
                    if(ticketDetails.Length == 11){
                        Enhancement ticket = new Enhancement();
                        ticket.ID = int.Parse(ticketDetails[0]);
                        ticket.Summary = ticketDetails[1];
                        ticket.Status = ticketDetails[2];
                        ticket.Priority = ticketDetails[3];
                        ticket.Submitter = ticketDetails[4];
                        ticket.Assigned = ticketDetails[5];
                        ticket.Watching = ticketDetails[6].Split('|').ToList();
                        ticket.Software = ticketDetails[7];
                        ticket.Cost = double.Parse(ticketDetails[8]);
                        ticket.Reason = ticketDetails[9];
                        ticket.Estimate = double.Parse(ticketDetails[10]);
                        //add enhancement to list
                        Tickets.Add(ticket);
                    }
                    else if(ticketDetails.Length == 9){
                        Task ticket = new Task();
                        ticket.ID = int.Parse(ticketDetails[0]);
                        ticket.Summary = ticketDetails[1];
                        ticket.Status = ticketDetails[2];
                        ticket.Priority = ticketDetails[3];
                        ticket.Submitter = ticketDetails[4];
                        ticket.Assigned = ticketDetails[5];
                        ticket.Watching = ticketDetails[6].Split('|').ToList();
                        ticket.ProjectName = ticketDetails[7];
                        ticket.DueDate = DateTime.Parse(ticketDetails[8]);
                        //add task to list
                        Tickets.Add(ticket);
                    }
                    else{
                        Bug ticket = new Bug();
                        ticket.ID = int.Parse(ticketDetails[0]);
                        ticket.Summary = ticketDetails[1];
                        ticket.Status = ticketDetails[2];
                        ticket.Priority = ticketDetails[3];
                        ticket.Submitter = ticketDetails[4];
                        ticket.Assigned = ticketDetails[5];
                        ticket.Watching = ticketDetails[6].Split('|').ToList();
                        ticket.Severity = ticketDetails.Length == 8 ? ticketDetails[7] : "Unassigned";  //accepts older style tickets as bugs
                        //add bug to list
                        Tickets.Add(ticket);
                    }
                }
                //close file
                sr.Close();
                logger.Info($"Tickets in file: {Tickets.Count()}");
            }
            catch(Exception ex){
                logger.Error(ex.Message);
            }
        }

        public void Display(){
            foreach(Ticket ticket in Tickets){
                Console.WriteLine(ticket.Display());
            }
        }

        public void AddTicket(Bug ticket){
            try{
                //generate id for ticket
                if(Tickets.Count() > 0)
                    ticket.ID = Tickets.Max(m => m.ID) + 1;
                else
                    ticket.ID = 1;
                //write to file
                StreamWriter sw = new StreamWriter(FilePath, true);
                sw.WriteLine(ticket.ToString());
                sw.Close();
                //add to list
                Tickets.Add(ticket);
                logger.Info($"Successfully Added Ticket #{ticket.ID}");
            }
            catch(Exception ex){
                logger.Error(ex.Message);
            }
        }
        public void AddTicket(Enhancement ticket){
            try{
                //generate id for ticket
                if(Tickets.Count() > 0)
                    ticket.ID = Tickets.Max(m => m.ID) + 1;
                else
                    ticket.ID = 1;
                //write to file
                StreamWriter sw = new StreamWriter(FilePath, true);
                sw.WriteLine(ticket.ToString());
                sw.Close();
                //add to list
                Tickets.Add(ticket);
                logger.Info($"Successfully Added Ticket #{ticket.ID}");
            }
            catch(Exception ex){
                logger.Error(ex.Message);
            }
        }
        public void AddTicket(Task ticket){
            try{
                //generate id for ticket
                if(Tickets.Count() > 0)
                    ticket.ID = Tickets.Max(m => m.ID) + 1;
                else
                    ticket.ID = 1;
                //write to file
                StreamWriter sw = new StreamWriter(FilePath, true);
                sw.WriteLine(ticket.ToString());
                sw.Close();
                //add to list
                Tickets.Add(ticket);
                logger.Info($"Successfully Added Ticket #{ticket.ID}");
            }
            catch(Exception ex){
                logger.Error(ex.Message);
            }
        }

        public void RemoveTicket(int id){
            try{
                //search for matching ticket id to remove
                int foundTicketID = -1;
                for(int i = 0; i < Tickets.Count(); i++){
                    if(id == Tickets[i].ID) foundTicketID = i;
                }
                if(foundTicketID >= 0){
                    //remove ticket if found
                    Tickets.RemoveAt(foundTicketID);
                    //rewrite file
                    StreamWriter sw = new StreamWriter(FilePath);
                    foreach(Ticket ticket in Tickets){
                        sw.WriteLine(ticket.ToString());
                    }
                    //close file
                    sw.Close();
                    logger.Info($"Successfully Removed Ticket #{id}");
                }
                else{
                    //if ticket not found
                    logger.Info($"Unable to Remove Ticket #{id}");
                }
            }
            catch(Exception ex){
                logger.Error(ex.Message);
            }
        }
    }
}