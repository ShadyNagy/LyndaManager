using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class ToolCookie
    {

        public string GenrateSendCookies(string AllCookies)
        {
            List<string> CookiesList = ParseCookies(AllCookies);
            CookiesList = CleareCookiesExist(CookiesList);

            List<KeyValuePair<string, string>> cookiesListPair = new List<KeyValuePair<string, string>>();
            for (int i = 0; i < CookiesList.Count; i++)
            {
                cookiesListPair.Add(new ToolCookie().GetKeyValue(CookiesList[i]));
            }

            return CreateCookies(cookiesListPair);
        }

        public string CreateCookies(List<KeyValuePair<string, string>> CookiesPair)
        {
            string data = "";
            for(int i=0; i<CookiesPair.Count; i++)
            {
                data = data + CookiesPair[i].Key + "=" + CookiesPair[i].Value + ";";
            }

            return data;
        }

        public List<string> CleareCookiesExist(List<string> Cookies)
        {
            for (int i = 0; i < Cookies.Count; i++)
            {
                for (int z = i+1; z < Cookies.Count; z++)
                {
                    if(Cookies[i].IndexOf(Cookies[z].Substring(0, 20)) >= 0)
                    {
                        Cookies.RemoveAt(z);
                    }
                }
            }

            return Cookies;
        }

        //path=/; secure; HttpOnly
        public List<string> ParseCookies(string AllCookies)
        {
            List<string> CookiesList = new List<string>();

            string tmp = "";
            for (int i=0; i<AllCookies.Length; i++)
            {
               
                if(AllCookies[i] == ',')
                {
                    if(tmp.IndexOf("path") >= 0)
                    {
                        CookiesList.Add(tmp);
                        tmp = "";
                        continue;
                    }
                }
                tmp += AllCookies[i];

                if(i == AllCookies.Length-1)
                    CookiesList.Add(tmp);
            }

            return CookiesList;
        }

        public KeyValuePair<string, string> GetKeyValue(string Cookie)
        {
            string tmpKey = "";
            string tmpVal = "";
            Boolean startKey = true;
            Boolean startVal = false;

            for (int i = 0; i < Cookie.Length; i++)
            {
                if (startKey)
                {
                    if (Cookie[i] == '=')
                    {
                        startVal = true;
                        startKey = false;
                        continue;
                    }
                }    
                
                if(Cookie[i] == ';')
                {
                    break;
                }           

                if(startKey)
                    tmpKey += Cookie[i];

                if (startVal)
                    tmpVal += Cookie[i];
            }

            return new KeyValuePair<string, string>(tmpKey, tmpVal);
        }

    }
}
