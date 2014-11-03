using System;
using System.Collections.Generic;
using SimpleORM;
using SimpleORM.Extensions;

namespace Test
{
    public class Event
    {
        public long Id { get; set; }

        public IList<Outcome> Outcomes { get; set; }
    }

    public class Outcome
    {
        public long Id { get; set; }

        public Event Event { get; set; }

        public long EventId { get; set; }

        public IList<Factor> Factors { get; set; }
    }

    public class Factor
    {
        public long Id { get; set; }

        public long OutcomeId { get; set; }
    }

    public class Bet
    {
        public long Id { get; set; }
        
        public float Amount { get; set; }
    }

    public class Ticket
    {
        public long Id { get; set; }

        public long Gambler { get; set; }

        public IList<Bet> Bets { get; set; }
    }

    public class TicketBet
    {
        public static Func<Bet, TicketBet, bool> TicketBets = (b, tb) => b.Id == tb.BetId;

        public static Func<Ticket, Bet, TicketBet, bool> TicketsBets = (t, b, tb) => t.Id == tb.TicketId && tb.BetId == b.Id;

        public long TicketId { get; set; }

        public long BetId { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IQueryBuilder queryBuilder = new QueryBuilder();
            var connection = new Connection();

            var queryEvent1 = queryBuilder.GetSingle<Event>(e => e.Id == 123);
            
            var queryEvent2 = queryBuilder.GetSingleOrDefault<Event>(e => e.Id == 123);
            
            var queryOutcomes = queryBuilder.GetCollection<Outcome>(o => o.EventId == 333);

            var outcomes = connection.Get(queryOutcomes);

            var loadEvent = queryBuilder.LoadSingle(outcomes, o => o.Event, (o, e) => o.EventId == e.Id);

            //Load factors for every outcome
            var loadFactors = queryBuilder.LoadCollection(outcomes, o => o.Factors, (o, f) => o.Id == f.OutcomeId);

            //Getting bets through ticket-bet table
            var queryTicketBets = queryBuilder.GetCollection(TicketBet.TicketBets, (b, tb) => tb.TicketId == 111 && b.Amount > 100);

            var tickets = new List<Ticket>();

            //Load all bets through ticket-bet into every ticket from tickets collection
            var queryTicketsBets = queryBuilder.LoadCollection(tickets, t => t.Bets, TicketBet.TicketsBets);

            //Load bets through ticket-bet into every ticket from tickets  collection with expression
            var queryTicketsBetsCondition = queryBuilder.LoadCollection(tickets, t => t.Bets, TicketBet.TicketsBets, b => b.Amount > 100);

            connection.LoadCollection(tickets, t => t.Bets, TicketBet.TicketsBets, b => b.Amount > 100);
        }
    }
}