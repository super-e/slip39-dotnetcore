using superenrico.slip39_dotnetcore.interfaces;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace superenrico.slip39_dotnetcore.model;

public class SecretSlitter : ISecretSplitter
{
    const int maxShares = 16;
    const int minThreshold = 0;

    public ReadOnlyCollection<byte[]> SplitSecret(int threshold, int totalShares, byte[] byteSecret)
    {
        var result = new List<byte[]>();

        // Check inputs
        
        // Check totalShares:
        if ( totalShares > maxShares)  throw new ArgumentOutOfRangeException(nameof(totalShares), $"Input {nameof(totalShares)} is {totalShares}, but should be between {nameof(threshold)} ({threshold}) and {maxShares}.");
        if (totalShares < 1) throw new ArgumentOutOfRangeException(nameof(totalShares), $"Input {nameof(totalShares)} is {totalShares}, but should be between 1 and {maxShares}.");
        // Check threshold:
        if (totalShares  < threshold) throw new ArgumentOutOfRangeException(nameof(threshold), $"Input {nameof(threshold)} must be less or equal than {totalShares}.");
        if (threshold < minThreshold) throw new ArgumentOutOfRangeException(nameof(threshold), $"Input {nameof(threshold)} must be greater than {minThreshold}.");

        
        if (byteSecret.Length == 0) return result.AsReadOnly();
        if (byteSecret.Length < 16) throw new ArgumentOutOfRangeException(nameof(byteSecret), $"Input {nameof(byteSecret)} should be at least 128 bit.");
        if (byteSecret.Length % 2 != 0) throw new ArgumentException($"Input {nameof(byteSecret)} length should be a multiple of 16 bit.");

        // In case the threshold is 1, all share are equal to the secret
        if (threshold == 1)
        {
            for (int i = 0; i < totalShares; i++)
            {
                result.Add(byteSecret);
            }
        }

        var checksum = CalculateChecksum(byteSecret);

        
            
            
        var rnd = RandomNumberGenerator.Create();

        for(int j = 0; j < byteSecret.Length; j++)
        {
            List<(FiniteFieldElement x, FiniteFieldElement y)> startingValues = new List<(FiniteFieldElement, FiniteFieldElement)>();
            byte[] tempByte = new byte[1];
            for (byte i = 0; i < threshold - 2; i++)
            {
                
                startingValues.Add((i, tempByte[0]));
            }
      
            startingValues.Add((254, checksum[j]));
            startingValues.Add((255, byteSecret[j]));

            List<(FiniteFieldElement x, FiniteFieldElement y)> calculatedShares = new List<(FiniteFieldElement x, FiniteFieldElement y)>();
            for( byte t = (byte) (threshold - 2); t < totalShares; t++)
            {
                byte shareResult = 0;
                foreach (var element in startingValues)
                {
                    byte parameter = 1;
                    foreach (var otherElement in startingValues)
                    {
                        if (element.x == otherElement.x) continue;
                        parameter *= (t - otherElement.x) / (element.x - otherElement.x);
                    }
                    shareResult += element.y * parameter;
                }
                calculatedShares.Add((t, shareResult));
            }
            startingValues.Remove((254, checksum[j]));
            startingValues.Remove((255, byteSecret[j]));
            startingValues.AddRange(calculatedShares);
        }
        



        return result.AsReadOnly();
    }
    public ReadOnlyCollection<byte[]> SplitSecret(int threshold, int totalShares, string secret)
    {
        byte[] byteSecret;
        // Check secret:
        if (secret == null) throw new ArgumentNullException($"Input {nameof(secret)} is null, it should be a hexdecimal string.");

        try
        {
            byteSecret = Convert.FromHexString(secret);
        }
        catch(FormatException ex)
        {
            throw new ArgumentException($"Input {nameof(secret)} is \"{secret}\" which is of length {secret.Length}: length should be zero or a multiple of 2 and the input should only containe hexadecimal characters.", nameof(secret), ex);
        }
        return SplitSecret(threshold: threshold, totalShares: totalShares, byteSecret: byteSecret);
    }


        static public byte[] CalculateChecksum(byte[] byteSecret)
        {
            int secretLength = byteSecret.Length;
            var hmacKey = new byte[secretLength - 4];
            var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(hmacKey);
            var stringKey = Convert.ToHexString(hmacKey);
            using (HMACSHA256 hmac = new HMACSHA256(hmacKey))
            {
                var calculatedHmac = hmac.ComputeHash(byteSecret, 0, secretLength);
                return calculatedHmac.Take<byte>(4).Union<byte>(hmacKey).ToArray<byte>();     
            }
        }
    protected bool ValidateChecksum(byte[] checksum)
    {
        List<int> GEN = new List<int>() {
            0xE0E040,
            0x1C1C080,
            0x3838100,
            0x7070200,
            0xE0E0009,
            0x1C0C2412,
            0x38086C24,
            0x3090FC48,
            0x21B1F890,
            0x3F3F120
        };

        var chk = 1;

        foreach (var checksumByte in checksum)
        {
            int b = chk >> 20;
            chk = (chk & 0xFFFFF) << 10 ^ checksumByte;

            for (int i = 0; i < 10; i++) 
            {
            int gen = (b >> i & 1) != 0 ? GEN[i] : 0;
            chk = chk ^ gen;
            }
        }

        return chk == 1;
    }
}