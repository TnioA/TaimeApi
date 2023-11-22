using HashidsNet;

namespace Taime.Application.Helpers
{
    public static class HashIdHelper
    {
        internal const string _salt = "6hfghGDFSasderGF";

        private readonly static Hashids _hashIds = Build();

        public static Hashids Build(int size = 15)
        {
            return new Hashids(_salt, size);
        }

        public static string Encode(int decodedValue)
        {
            var hashId = _hashIds.Encode(decodedValue);

            return hashId.ToString();
        }

        public static int Decode(string encodedValue)
        {
            if (string.IsNullOrWhiteSpace(encodedValue))
                return 0;

            return _hashIds.Decode(encodedValue).FirstOrDefault();
        }
    }   
}
