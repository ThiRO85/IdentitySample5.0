using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using ISystem.Domain.Entities;
using ISystem.Infrastructure.Contexts;

namespace ISystem.Infrastructure.Methods
{
    public static class Novax
    {
        private static readonly ApplicationDbContext _context;

        //public static bool Login(string userId)
        //{

        //    IntegracaoPabx login = _context.IntegracaoPabx.FirstOrDefault(x => x.Users.Id == userId && x.Mantenedor.Id == 1);

        //    if (login == null)
        //    {
        //        return false;
        //    }

        //    string date = DateTime.Now.ToString("yyyyMMdd");
        //    string evento = "101";
        //    string ramal = login.Ramal;
        //    string posicao = login.Posicao;
        //    string senha = login.Senha;

        //    byte[] encodedPassword = new UTF8Encoding().GetBytes(evento + "@" + ramal + "@" + posicao + "&" + senha + "@==" + date + "==");
        //    byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
        //    string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

        //    string input = "http://172.17.1.50:6060/" + evento + "@" + ramal + "@" + posicao + "&" + senha + "@" + encoded;
        //    string pattern = @"[^0-9a-zA-Z@://.&]+";
        //    string replacement = "";
        //    Regex rgx = new Regex(pattern);
        //    string url = rgx.Replace(input, replacement);


        //    WebRequest request = WebRequest.Create(url);
        //    request.Method = "GET";
        //    string content;

        //    using (WebResponse response = request.GetResponse() as HttpWebResponse)
        //    {
        //        // Get the response stream  
        //        StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("ISO-8859-1"));
        //        content = reader.ReadToEnd();

        //    }

        //    if (content == "101@00" || content == "101@12")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public static bool LoginGlobal(string userId)
        //{
        //    IntegracaoPabx login = _context.IntegracaoPabx.FirstOrDefault(x => x.Users.Id == userId && x.Mantenedor.Id == 2);

        //    if (login == null)
        //    {
        //        return false;
        //    }

        //    string date = DateTime.Now.ToString("yyyyMMdd");
        //    string evento = "101";
        //    string ramal = login.Ramal;
        //    string posicao = login.Posicao;
        //    string senha = login.Senha;

        //    byte[] encodedPassword = new UTF8Encoding().GetBytes(evento + "@" + ramal + "@" + posicao + "&" + senha + "@==" + date + "==");
        //    byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
        //    string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

        //    string input = "http://172.17.10.1:6060/" + evento + "@" + ramal + "@" + posicao + "&" + senha + "@" + encoded;
        //    string pattern = @"[^0-9a-zA-Z@://.&]+";
        //    string replacement = "";
        //    Regex rgx = new Regex(pattern);
        //    string url = rgx.Replace(input, replacement);


        //    WebRequest request = WebRequest.Create(url);
        //    request.Method = "GET";
        //    string content;

        //    using (WebResponse response = request.GetResponse() as HttpWebResponse)
        //    {
        //        // Get the response stream  
        //        StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("ISO-8859-1"));
        //        content = reader.ReadToEnd();

        //    }

        //    if (content == "101@00" || content == "101@12")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
