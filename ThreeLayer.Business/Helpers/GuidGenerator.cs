namespace ThreeLayer.Business.Helpers
{
    public class GuidGenerator
    {
        public static Guid Create()
        {
            var timestamp = DateTime.UtcNow.Ticks;
            var timestampBytes = BitConverter.GetBytes(timestamp);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var guidBytes = Guid.NewGuid().ToByteArray();

            Array.Copy(timestampBytes, 0, guidBytes, 0, 8);

            return new Guid(guidBytes);
        }
    }
}
