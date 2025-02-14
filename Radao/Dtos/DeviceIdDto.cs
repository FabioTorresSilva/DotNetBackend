namespace Radao.Dtos
{
    public class DeviceIdDto
    {
        /// <summary>
        /// Device Id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Device Model 
        /// </summary>
        public String Model { get; set; }

        /// <summary>
        /// Device Serial Number 
        /// </summary>
        public String SerialNumber { get; set; }

        /// <summary>
        /// Device Expiration Date
        /// </summary>
        public DateOnly ExpirationDate { get; set; }

        /// <summary>
        /// 4 Arguments DeviceFullDto constructor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="serialNumber"></param>
        /// <param name="expirationDate"></param>
        public DeviceIdDto(int id, string model, string serialNumber, DateOnly expirationDate)
        {
            Id = id;
            Model = model;
            SerialNumber = serialNumber;
            ExpirationDate = expirationDate;
        }
    }
}
