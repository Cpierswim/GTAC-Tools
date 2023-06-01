using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

/// <summary>
/// Summary description for USAIDEncryptor
/// </summary>
public class USAIDEncryptor
{
    public enum EncryptionStatus { Encrypted, Unencrypted };
    private String _USAID;
    

    public USAIDEncryptor(String USAID, EncryptionStatus status)
    {
        if (status == EncryptionStatus.Unencrypted)
            this._USAID = USAID;
        else
            this._USAID = Decrypt(USAID);
    }

    public String GetUSAID(EncryptionStatus status)
    {
        if (status == EncryptionStatus.Unencrypted)
            return this._USAID;
        else
            return Encrypt(this._USAID);
    }

    private String Decrypt(String EncryptedUSAID)
    {
        String BirthdayPart = EncryptedUSAID.Substring(0, 6);
        String LettersPart = EncryptedUSAID.Substring(6);
        char[] NumbersArray = BirthdayPart.ToCharArray();
        char[] LettersArray = LettersPart.ToCharArray();

        for (int i = 0; i < NumbersArray.Length; i++)
            NumbersArray[i] = ChangeLetterToNumber(NumbersArray[i]);

        for (int i = 0; i < LettersArray.Length; i++)
            LettersArray[i] = SubtractThreeLetters(LettersArray[i]);
        String ReturnString = "";
        for (int i = 0; i < NumbersArray.Length; i++)
            ReturnString += NumbersArray[i];
        for (int i = 0; i < LettersArray.Length; i++)
            ReturnString += LettersArray[i];
        return ReturnString;
    }

    private String Encrypt(String UnencryptedUSAID)
    {
        String BirthdayPart = UnencryptedUSAID.Substring(0, 6);
        String LettersPart = UnencryptedUSAID.Substring(6);
        char[] NumbersArray = BirthdayPart.ToCharArray();
        char[] LettersArray = LettersPart.ToCharArray();

        for (int i = 0; i < NumbersArray.Length; i++)
            NumbersArray[i] = ChangeNumberToLetter(NumbersArray[i]);

        for (int i = 0; i < LettersArray.Length; i++)
            LettersArray[i] = AddThreeLetters(LettersArray[i]);
        String ReturnString = "";
        for (int i = 0; i < NumbersArray.Length; i++)
            ReturnString += NumbersArray[i];
        for (int i = 0; i < LettersArray.Length; i++)
            ReturnString += LettersArray[i];
        return ReturnString;
    }

    private char AddThreeLetters(char letter)
    {
        return char.ConvertFromUtf32(char.ConvertToUtf32(letter.ToString(), 0) + 3).ToCharArray()[0];
    }
    private char SubtractThreeLetters(char letter)
    {
        return char.ConvertFromUtf32(char.ConvertToUtf32(letter.ToString(), 0) - 3).ToCharArray()[0];
    }

    private char ChangeNumberToLetter(char number)
    {
        switch (number)
        {
            case '0':
                return 'I';
            case '1':
                return 'J';
            case '2':
                return 'K';
            case '3':
                return 'L';
            case '4':
                return 'M';
            case '5':
                return 'N';
            case '6':
                return 'O';
            case '7':
                return 'P';
            case '8':
                return 'Q';
            case '9':
                return 'R';
        }

        return 'X';
    }
    private char ChangeLetterToNumber(char letter)
    {
        switch (letter)
        {
            case 'I':
                return '0';
            case 'J':
                return '1';
            case 'K':
                return '2';
            case 'L':
                return '3';
            case 'M':
                return '4';
            case 'N':
                return '5';
            case 'O':
                return '6';
            case 'P':
                return '7';
            case 'Q':
                return '8';
            case 'R':
                return '9';
        }

        throw new Exception("Error Decrypting.");
    }
}