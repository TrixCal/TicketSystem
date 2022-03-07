using System;
using System.Collections.Generic; 

namespace TicketSystem{
    class Ticket{
        public int ID { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Submitter { get; set; }
        public string Assigned { get; set; }
        public List<string> Watching { get; set; }

        public Ticket(){
            Watching = new List<string>();
        }
        public string Display(){
            return $"Id: {ID}\nSummary: {Summary}\nStatus: {Status}\nPriority: {Priority}\nSubmitter: {Submitter}\nAssigned: {Assigned}\nWatching: {string.Join(", ", Watching)}\n";
        }
        public override string ToString(){
            return $"{ID},{Summary},{Status},{Priority},{Submitter},{Assigned},{string.Join("|", Watching)}";
        }
    }
}