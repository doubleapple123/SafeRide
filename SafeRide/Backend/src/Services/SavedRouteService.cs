using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web.Helpers;
using SafeRide.src.DataAccess;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace Backend.Services;

public class SavedRouteService
{
    private ISavedRouteDAO _IsavedRouteDao;
    public SavedRouteService(ISavedRouteDAO savedRouteDao)
    {
        _IsavedRouteDao = savedRouteDao;
    }

    public string GetSavedRoute(string UserName, string RouteName)
    {
        return _IsavedRouteDao.GetSavedRoute(UserName, RouteName);
    }

    public bool AddSavedRoute(string UserName, string RouteName, string encodedJson)
    {
        return _IsavedRouteDao.AddSavedRoute(UserName, RouteName, encodedJson);
    }

    public List<SavedRoute> GetAllRoutes(string UserName)
    {
        var routeList = _IsavedRouteDao.GetAllSavedRoutes(UserName);
        // routeList.ForEach((item) => item.EncodedRoute = DecodeRoute(item.EncodedRoute));
        return routeList;
    }

    public string SendShareRoute(string UserName, string RouteName)
    {
        return EncryptRoute(_IsavedRouteDao.GetSavedRoute(UserName, RouteName));
    }

    public string ReceiveSharedRoute(string cipher)
    {
        return DecryptRoute(cipher);
    }
    // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-6.0
    public string DecryptRoute(string encryptedText)
    {
        string plaintext = null;
        var Key = new byte[] {0x20};
        var IV = new byte[] {0x20};
        var cipherText = Encoding.ASCII.GetBytes(encryptedText);

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
    public string EncodeRoute(string rawJson)
    {
        return JsonSerializer.Serialize(rawJson);
    }

    public string EncryptRoute(string plainText)
    {
        var encrypted = new byte[] { };
        var Key = new byte[] {0x20};
        var IV = new byte[] {0x20};
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }

                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        return Encoding.ASCII.GetString(encrypted);
    }
}