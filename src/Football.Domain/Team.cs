using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Football
{
    [TypeConverter(typeof(TeamArrayConverter))]
    public class Team
    {
        public Team(string name) : this(name, 0, 0)
        { }

        public Team(string name, int goalsFor, int goalsAgainst)
        {
            this.Name = name;
            this.GoalsFor = goalsFor;
            this.GoalsAgainst = goalsAgainst;
            this.Players = new Collection<Player>();
        }

        public string Name { get; private set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
        public ICollection<Player> Players { get; set; }

        /// <summary>
        /// Returns the difference between GoalsFor and GoalsAgainst. If useAbsoluteValue is true, then it will always return a positive value.
        /// </summary>
        /// <remarks>
        /// Assumption - the goal difference is always positive so just get the one with the least amount
        /// This is not usually the case in football... so it doesn't make much sense (team with worst goal difference is a negative number)
        /// But anyway we can use parameter that is currently defaulted to true (useAbsoluteValue)
        /// </remarks>
        public int GetGoalDifference(bool useAbsoluteValue = true)
        {
            if (useAbsoluteValue)
            {
                return Math.Abs(GoalsFor - GoalsAgainst);
            }

            return GoalsFor - GoalsAgainst;
        }
    }
}
