using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshApi.Helpers
{
    public class CaesarCipher
    {
        private const int alphabetLength = 26;

        public static string Encrypt(string message, int key)
        {
            string encryptedMessage = "";

            foreach (char character in message)
            {
                if (char.IsLetter(character))
                {
                    char baseChar = char.IsUpper(character) ? 'A' : 'a';
                    int shiftedPosition = (character + key - baseChar) % alphabetLength;
                    char shiftedCharacter = (char)(baseChar + shiftedPosition);
                    encryptedMessage += shiftedCharacter;
                }
                else
                {
                    encryptedMessage += character;
                }
            }

            return encryptedMessage;
        }

        public static string Decrypt(string encryptedMessage, int key)
        {
            string decryptedMessage = "";

            foreach (char character in encryptedMessage)
            {
                if (char.IsLetter(character))
                {
                    char baseChar = char.IsUpper(character) ? 'A' : 'a';
                    int shiftedPosition = (character - key - baseChar + alphabetLength) % alphabetLength;
                    char shiftedCharacter = (char)(baseChar + shiftedPosition);
                    decryptedMessage += shiftedCharacter;
                }
                else
                {
                    decryptedMessage += character;
                }
            }

            return decryptedMessage;
        }
    }
}