using System.Collections.Generic;
using SimpleORM;
using SimpleORM.QueryBuilder;

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

        public long State { get; set; }
    }

    public class Factor
    {
        public long Id { get; set; }

        public long OutcomeId { get; set; }

        public float Value { get; set; }

        public float RawValue { get; set; }

        public long State { get; set; }
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
        public static ThroughFunc<Bet, TicketBet> TicketBets = (b, tb) => b.Id == tb.BetId;
        
        public static ThroughFunc<Ticket, TicketBet> BetTickets = (t, tb) => t.Id == tb.TicketId;

        public long TicketId { get; set; }

        public long BetId { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IQueryBuilder queryBuilder = new QueryBuilder();

            var queryEvent1 = queryBuilder.Get<Event>(e => e.Id == Parameter.Next);

            var queryEvent2 = queryBuilder.Get<Event>(e => e.Id == Parameter.Next, throwIfNotExists: false);

            //Load only outcome state column and primary key
            var queryOutcomes = queryBuilder.Collect<Outcome>(o => o.EventId == Parameter.Next, o => o.State);

            var outcomes = new List<Outcome>();

            var loadEvent = queryBuilder.ForEach<Outcome>().Load(o => o.Event, (o, e) => o.EventId == e.Id);

            //Load factors (only primary key, value and raw_value columns) for every outcome
            var loadFactors = queryBuilder.ForEach<Outcome>().Load(o => o.Factors, (o, f) => o.Id == f.OutcomeId, f => new { f.Value, f.RawValue });

            //Getting all bets through ticket-bet table
            var queryTicketBets1 = queryBuilder.Collect<Bet>().Through(TicketBet.TicketBets, t => t.TicketId == Parameter.Next);


            //Getting bets through ticket-bet table with expression
            var queryTicketBets2 = queryBuilder.Collect<Bet>(b => b.Amount > Parameter.Next).Through(TicketBet.TicketBets, t => t.TicketId == Parameter.Next);

            var tickets = new List<Ticket>();

            //Load all bets through ticket-bet into every ticket from tickets collection
            var queryTicketsBets = queryBuilder.ForEach<Ticket>().Load(t => t.Bets).Through(TicketBet.BetTickets, TicketBet.TicketBets);

            //Load bets through ticket-bet into every ticket from tickets  collection with expression
            var queryTicketsBetsCondition = queryBuilder.ForEach<Ticket>().Load(t => t.Bets, b => b.Amount > Parameter.Next).Through(TicketBet.BetTickets, TicketBet.TicketBets);
        }
    }
}