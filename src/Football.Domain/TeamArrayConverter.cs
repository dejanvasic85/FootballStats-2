using System;
using System.ComponentModel;
using System.Globalization;

namespace Football
{
    /// <summary>
    /// Ability to create a Team object from a array of string array (can be used from CSV import)
    /// </summary>
    /// <remarks>
    /// This class has the specification and hardcoded value of how many columns (commas in csv) are required to create a Team object
    /// </remarks>
    public class TeamArrayConverter : TypeConverter
    {
        private const int RequiredColumnCount = 10;
        private const int PositionIndex = 0;
        private const int TeamNameColumnIndex = 1;
        private const int GoalsForColumnIndex = 6;
        private const int GoalsAgainstColumnIndex = 8;

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof (string[]) &&  ((string[])context.Instance).Length ==  RequiredColumnCount;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // Explicit cast
            string[] teamRecord = (string[]) value;

            var team = new Team(name: teamRecord[TeamNameColumnIndex])
            {
                GoalsFor = int.Parse(teamRecord[GoalsForColumnIndex]),
                GoalsAgainst = int.Parse(teamRecord[GoalsAgainstColumnIndex]),
                Position = int.Parse(teamRecord[PositionIndex])
            };

            return team;
        }
    }
}   