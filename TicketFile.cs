using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TicketSystem{
    class TicketFile{
        public string FilePath { get; set; }
        public List<Ticket> Tickets { get; set; }

        public TicketFile(string ticketFilePath){
            FilePath = ticketFilePath;
            Tickets = new List<Ticket>();

            //populate list
            StreamReader sr = new StreamReader(FilePath);
            while(!sr.EndOfStream){
                //create instance of ticket
                Ticket ticket = new Ticket();
                string line = sr.ReadLine();
                string[] ticketDetails = line.Split(',');
                //assign variables
                ticket.ID = int.Parse(ticketDetails[0]);
                ticket.Summary = ticketDetails[1];
                ticket.Status = ticketDetails[2];
                ticket.Priority = ticketDetails[3];
                ticket.Submitter = ticketDetails[4];
                ticket.Assigned = ticketDetails[5];
                ticket.Watching = ticketDetails[6].Split('|').ToList();
                //add instance to list
                Tickets.Add(ticket);
            }
            //close file
            sr.Close();
        }

        public void Display(){
            foreach(Ticket ticket in Tickets){
                Console.WriteLine(ticket.Display());
            }
        }

        public void AddTicket(Ticket ticket){
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
        }

        public bool RemoveTicket(int id){
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
                return true;
            }
            else{
                //return false if ticket not found
                return false;
            }
        }
    }
}