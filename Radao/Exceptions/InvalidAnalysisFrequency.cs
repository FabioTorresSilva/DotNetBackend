namespace Radao.Exceptions
{
    public class InvalidAnalysisFrequency : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidAnalysisFrequency() : base("Analysis Frequency is invalid.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public InvalidAnalysisFrequency(string message) : base(message) { }
    }
}
