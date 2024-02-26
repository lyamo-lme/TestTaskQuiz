using System.Security.Cryptography;
using System.Text;
using TestTaskQuiz.Core.Services;

namespace TestTaskQuiz.Service;

public class PasswordHash : IPasswordHash
{
    readonly string _prefix = "ZW7M@%;qhEvl_AUC@252CCTVobt8vz4%";
    readonly string _suffix = "xkK*QBrb*E&BL47i=P^j!cOJ#RxT*_F=";

    public PasswordHash()
    {
    }

    public PasswordHash(string suffix, string prefix)
    {
        _prefix = prefix;
        _suffix = suffix;
    }

    public bool ComparePassword(string hashPassword, string password)
    {
        return hashPassword.Equals(HashPassword(password));
    }

    public string HashPassword(string password)
    {
        string saltedPassword = $"{_prefix}.{password}.{_suffix}";

        using (var hash = SHA256.Create())
        {
            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}