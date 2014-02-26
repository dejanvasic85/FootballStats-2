using System.Collections.Generic;
using System.IO;

namespace Football.Repository
{
    public class LeagueDataCsvFileStream : FileStream
    {
        private readonly bool _ignoreHeaders;

        public LeagueDataCsvFileStream(string path, FileMode fileMode, FileAccess access, bool ignoreHeaders = true)
            : base(path, fileMode, access)
        {
            _ignoreHeaders = ignoreHeaders;
        }

        public IEnumerable<Team> GetTeamsFromFile()
        {
            // Set the current base FileStream position to the start of file
            this.Position = 0;
            int lineNumber = 0;
            using (StreamReader sr = new StreamReader(this))
            {

                do
                {
                    string line = sr.ReadLine();
                    if (line == null)
                        break; // No more data

                    lineNumber++;

                    // If there is a header line then ignore it and move on if required
                    if (_ignoreHeaders && lineNumber == 1)
                        continue;

                    // The football.csv file contains a line full of dashes (to separate some teams) so just ignore this one
                    if (line.StartsWith("-"))
                        continue;

                    string[] values = line.FromCsv();

                    // Only yield the value ( this happens only when it needs to evaluate the collection, see LINQ for reference )
                    yield return values.ConvertFromCsv<Team>();

                } while (true);

            }
        }
    }
}