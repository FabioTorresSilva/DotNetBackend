namespace Radao.Exceptions
{
    public class AssociatedToAnotherFountain : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public AssociatedToAnotherFountain() : base("Device associated to a different fountain.") { }

        /// <summary>
        /// Constructor with custom message
        /// </summary>
        /// <param name="message"></param>
        public AssociatedToAnotherFountain(string message) : base(message) { }
    }
}
