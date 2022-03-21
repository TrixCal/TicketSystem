using System;
using System.Collections.Generic; 

namespace TicketSystem{
    abstract class Ticket{
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
        public virtual string Display(){
            return $"Id: {ID}\nSummary: {Summary}\nStatus: {Status}\nPriority: {Priority}\nSubmitter: {Submitter}\nAssigned: {Assigned}\nWatching: {string.Join(", ", Watching)}\n";
        }
        public override string ToString(){
            return $"{ID},{Summary},{Status},{Priority},{Submitter},{Assigned},{string.Join("|", Watching)}";
        }
    }

    class Bug : Ticket{
        public string Severity { get; set; }
        
        public override string Display(){
            return $"Id: {ID}\nSummary: {Summary}\nStatus: {Status}\nPriority: {Priority}\nSubmitter: {Submitter}\nAssigned: {Assigned}\nWatching: {string.Join(", ", Watching)}\nSeverity: {Severity}\n";
        }
        public override string ToString()
        {
            return $"{ID},{Summary},{Status},{Priority},{Submitter},{Assigned},{string.Join("|", Watching)},{Severity}";
        }
    }

    class Enhancement : Ticket{
        public string Software { get; set; }
        public double Cost { get; set; }
        public string Reason { get; set; }
        public double Estimate { get; set; }

        public override string Display(){
            return $"Id: {ID}\nSummary: {Summary}\nStatus: {Status}\nPriority: {Priority}\nSubmitter: {Submitter}\nAssigned: {Assigned}\nWatching: {string.Join(", ", Watching)}\nSoftware: {Software}\nCost: {Cost:C}\nReason: {Reason}\nEstimate: {Estimate:C}\n";
        }
        public override string ToString()
        {
            return $"{ID},{Summary},{Status},{Priority},{Submitter},{Assigned},{string.Join("|", Watching)},{Software},{Cost},{Reason},{Estimate}";
        }
    }

    class Task : Ticket{
        public string ProjectName { get; set; }
        public DateTime DueDate { get; set; }

        public override string Display(){
            return $"Id: {ID}\nSummary: {Summary}\nStatus: {Status}\nPriority: {Priority}\nSubmitter: {Submitter}\nAssigned: {Assigned}\nWatching: {string.Join(", ", Watching)}\nProject Name: {ProjectName}\nDue By: {DueDate:d}\n";
        }
        public override string ToString()
        {
            return $"{ID},{Summary},{Status},{Priority},{Submitter},{Assigned},{string.Join("|", Watching)},{ProjectName},{DueDate}";
        }
    }
}