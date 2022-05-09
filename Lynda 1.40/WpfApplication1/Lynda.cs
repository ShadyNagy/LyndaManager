using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


///ajax/course/videotranscripts?courseId=572160&videoId=580126
///get sub

//POST /portal/sip?org=spl.org&triedlogout=true HTTP/1.1
//Host www.lynda.com
//User-Agent Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:51.0) Gecko/20100101 Firefox/51.0
// Accept text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
// Accept-Language	en-US,en;q=0.5
// Accept-Encoding	gzip, deflate, br
// Referer	https://www.lynda.com/portal/sip?org=spl.org&triedlogout=true
// Cookie	throttle-5ea93727a74a3f569eb5208599b30e54=1; throttle-a64030d4f0078f1211744e69b24c818f=1; throttle-6d224299407bfb31a5838d5586143992=1; throttle-a08346b1d77e5ed99edf7159d1d154d9=1; show-member-tour-cta=true; optimizelyExp=5307411637; optimizelyEndUserId=oeu1461147232087r0.6707808742135364; optimizelySegments=%7B%222093600156%22%3A%22referral%22%2C%222107140276%22%3A%22false%22%2C%222116220016%22%3A%22ff%22%2C%222642881670%22%3A%22cd14621%22%7D; optimizelyBuckets=%7B%225757682474%22%3A%225754882736%22%2C%225946692239%22%3A%225956322415%22%2C%226029582430%22%3A%226029792446%22%2C%226118690873%22%3A%226120141283%22%2C%225732011207%22%3A%225742850117%22%2C%226903155883%22%3A%226909231267%22%7D; __utma=203495949.61238286.1461147244.1486385971.1486495340.121; __utmz=203495949.1475340109.83.8.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); LyndaLoginStatus=Member-Not-Logged-In; _ga=GA1.2.61238286.1461147244; utag_main=v_id:0154623e90580009f74f499081960704c003a00900ac2$_sn:172$_ss:1$_st:1486497138552$_pn:1%3Bexp-session$ses_id:1486495338552%3Bexp-session; AMCV_4710C8A1547F36100A4C98BC%40AdobeOrg=1256414278%7CMCMID%7C68431684757466496548520039923420901903%7CMCAAMLH-1486988475%7C6%7CMCAAMB-1486988475%7CNRX38WO0n5BH8Th-nqAG_A%7CMCAID%7CNONE; throttle-4ec203c8c9ab4f719a0686240dde784e=0; throttle-e260c8c1b4b9a71bf5b7947b958badac=1; LyndaAccess=LyndaAccess=2/6/2017 5:11:41 AM&p=0&data=9,2/1/2018,1,191505; __utmv=203495949.|1=Persona=Enterprise-User-Status-Active-Type-Regular=1^3=Product=lyndaLibrary=1; throttle-9c141ac35cd620cc696a4ed9dad825a3=1; throttle-4453026aaca8a70c11fde80fab09ff10=1; throttle-6566341c4226c060a46d875b7b1a4ca9=0; player=%7B%22volume%22%3A0.32583328247070314%2C%22muted%22%3Afalse%2C%22ccLang%22%3A%22en%22%2C%22theaterMode%22%3Atrue%7D; throttle-16dde5ef3a0528f2d59eeea6b5e0ba48=1; throttle-19917df26f9e03aa55c2b853c5be7c14=1; throttle-d7fc3221d8becd70e971cdc4d5c3cdc2=1; throttle-7232cff2578c15d7bc81e9422129e42e=1; bcookie=6306994cc63e4b6fa2441e2f89243bfd4023e787eafe4b7ebd9181fb6dc90a65; throttle-9bab2b349ce2014ed23ae7fd2d0ff859=1; throttle-7ec9e50e30bceb862b136f9ad57936cf=1; throttle-46a32029c620044463e49e878a5b32cf=3; tcookie=1225dabc-5c71-42ec-98fd-f8336cad5517; SSOLogin=OrgUrl=http%3A%2F%2Fwww.lynda.com%2Fportal%2Fsip%3Forg%3Dspl.org&OrgName=Seattle%20Public%20Library; throttle-8060f52bc45640562231d42e40eb239c=1; throttle-298d317de21a27fe5bc8206bf5f5711f=1; throttle-b150eca7d68024b9934b963b59f1e5fd=0; throttle-a3517aa110f46d9b6682338b7674a785=0; throttle-da519bf9a83d37efee8bf49146ecc227=0; throttle-8b05479e97a6f7932e990590fa6e8aa4=0; throttle-ca41482c9d2aedebf2b5aa6aded5aef1=1; throttle-5fbc909edaf450cbe9e26787fbf4887d=1; throttle-d201655ba950250086b4aae57d9c13f1=0; throttle-1a22c6995518b4ff2ab79b00324d2eb8=1; throttle-30e7edda819c424cb83c826e9a36422b=1; throttle-5986a879b0053b3749a732b32b906a46=0; throttle-96ccabd1c8d526974d54a682e698a828=1; throttle-d4132c9b332c6366af80ea92bb7fa757=7; throttle-cc6e24142c9b2f691b86349a86409bdb=1; throttle-3c5741c140a21d068b5f80d70805ccb1=1; _utmcmd=affiliate; _utmcsrc=ldc_affiliate; _utmccn=CD14621; throttle-e4e24be7401cc145832b01f2a30c1086=3; throttle-0ad8a776acaca660e6e9be58013705d2=0; throttle-779033e25fdda8ed909c93de141607ca=1; throttle-fd2d35c5dcec23e045559375fa71620f=1; throttle-5ba94950d443193910efd61e10a71479=1; throttle-2f863007df8063983415bedc8dfb38b6=1; throttle-7a6873842cfb3ade05214c6e8f633927=1; throttle-fcc41b5952df7ea0746eff3c71b72bc7=1; throttle-2acceef5da62f8fdf803dea373b50f21=1; throttle-af7175e2bb70bf2a86f5ddf2d9837e8d=1; throttle-b8b1cb3ef236f57532c515db9614be44=1; litrk-srcveh=srcValue=or-search&vehValue=www.google.nl&prevSrc=direct/none&prevVeh=www.google.com.eg; throttle-f01d5e1760e46dd67ba9e947530269e4=1; throttle-34b71ebdd1275c6b9611f8bd38bdf4c5=1; throttle-18907b1c5158f580dbf9e3cacd4d3ea8=0; throttle-81caf7c4bbc10c18b29b105cfcbcdbee=1; throttle-8fa02d52ac8b90701e0e4f96516f92e8=0; throttle-9ecc4e578ac856334c0a44bd43f10619=1; throttle-118eeb625d9ab0f5091747b6aedc9791=0; throttle-bf01e020137cb85eaa7a5e6a2f331834=1; throttle-732f7e4aa38bcbe1c96ebd1487f00f5a=2; throttle-7604570023d0842134ccba69fb4a8978=0; throttle-1541cf51b317649a0e2e58b81f433f80=1; throttle-debc254721129a3be83a175fa3f635de=1; throttle-716593d917cc3fea919a49f1f2518316=0; throttle-a5ca6acbd2f547f2914c0b113231bee3=1; throttle-47481a8f590db61e7b4703e070ade640=1; throttle-c11615159f419c517259fe7c6889fbc7=1; throttle-446eccf1798a99315a9cfa2786d9d36f=1; throttle-e0cb8e4541d2401ae2437d4836b8d8cd=1; throttle-931af3b8c4457b9db32bb90a1bda8f97=0; player_settings_0_7=player_type=2&video_format=2&cc_status=2&window_extra_height=148&volume_percentage=50&resolution=0; throttle-ad15fee1459e8f3e1ae3d8d711f77883=1; player_settings_10076646_7=player_type=2&video_format=2&cc_status=2&window_extra_height=148&volume_percentage=50&resolution=540; throttle-81dd6f846ff10d3c73ac4131d2f970f1=1; plugin_list=Flash; _we_wk_ss_lsf_=true; ncp=1; preference-practiceenvironmentenabled=1; __utmc=203495949; s_cc=true; s_sq=%5B%5BB%5D%5D; token=557c5eba-fa8c-4db5-a226-ddc36c1e7391,ebd5b7091c6c978ce65a5e9dc44f3e22,zJ8pceHuiLPHbhwsj+Ky+bq9QebtE1k5hQZUgpi1llWairrcBuWgj+F/9Pf1d8wiZ6SmtNIHEaMgrfZjsTuufg==; NSC_ed11_xxx-iuuqt_wt=ffffffff096d9e5245525d5f4f58455e445a4a423661; _dc_gtm_UA-512865-1=1; __utmb=203495949.1.9.1486495341344; _gat_tealium_0=1
// Content-Type	application/x-www-form-urlencoded
// Content-Length	167
//libraryCardNumber=10001260012-2&libraryCardPin=1234&libraryCardPasswordVerify=&org=spl.org&currentView=login&seasurf=KfAbnHfv%2FIVcXthJ%2FRFNlilHDaCX0yJM6jSDawmbDJo%3D

namespace WpfApplication1
{

    public class WebClientEx : WebClient
    {
        private CookieContainer _cookieContainer = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = _cookieContainer;
            }
            return request;
        }

    }


    class Lynda
    {
        public Lynda()
        {

        }

        public string ParseCookies2(string data)
        {
            string res = "";
            string[] finaaaa = new string[300];
            int cnt = 0;

            for (int i = 0; i < data.Length; i++)
            {
                res += data[i];

                if (data[i] == ';')
                {
                    finaaaa[cnt] = res;
                    cnt++;
                    res = "";
                    continue;
                }
            }

            res = "";
            for (int i = 0; i < cnt; i++)
            {
                res += finaaaa[i];
            }

            return res;
        }

        private List<KeyValuePair<string, string>> CookiesHeader(string Cookies)
        {
            string data = ParseCookies2(Cookies);

            List<KeyValuePair<string, string>> kvpList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3"),
            };

            kvpList.Insert(0, new KeyValuePair<string, string>("New Key 1", "New Value 1"));

            return kvpList;
        }

        //private static void fixCookies(HttpWebRequest request, HttpWebResponse response)
        //{
        //    for (int i = 0; i < response.Headers.Count; i++)
        //    {
        //        string name = response.Headers.GetKey(i);
        //        if (name != "Set-Cookie")
        //            continue;
        //        string value = response.Headers.Get(i);
        //        foreach (var singleCookie in value.Split(','))
        //        {
        //            Match match = Regex.Match(singleCookie, "(.+?)=(.+?);");
        //            if (match.Captures.Count == 0)
        //                continue;
        //            response.Cookies.Add(
        //                new Cookie(
        //                    match.Groups[1].ToString(),
        //                    match.Groups[2].ToString(),
        //                    "/",
        //                    request.Host.Split(':')[0]));
        //        }
        //    }
        //}

        //public bool TryAddCookie(this WebRequest webRequest, Cookie cookie)
        //{
        //    HttpWebRequest httpRequest = webRequest as HttpWebRequest;
        //    if (httpRequest == null)
        //    {
        //        return false;
        //    }

        //    if (httpRequest.CookieContainer == null)
        //    {
        //        httpRequest.CookieContainer = new CookieContainer();
        //    }

        //    httpRequest.CookieContainer.Add(cookie);
        //    return true;
        //}


        //(Request-Line)	GET /portal/sip?org=spl.org&triedlogout=true HTTP/1.1

        //Host www.lynda.com

        //User-Agent Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:51.0) Gecko/20100101 Firefox/51.0

        //Accept text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8

        //Accept-Language	en-US,en;q=0.5

        //Accept-Encoding	gzip, deflate, br

        public string ParseCookies(string data)
        {
            string res="";
            string[] finaaaa = new string[300];
            int cnt = 0;

            for (int i=0; i<data.Length; i++)
            {
                res += data[i];

                if (data[i] == ';')
                {
                    res = res.Replace(" path=/,", "");
                    res = res.Replace(" HttpOnly,", "");

                    if((res.IndexOf("expires") < 0) && (res.IndexOf("domain") < 0) && 
                        (res.IndexOf("path") < 0) && (res.IndexOf("secure") < 0))
                    {
                        finaaaa[cnt] = res;
                        cnt++;                        
                    }
                    res = "";
                    continue;
                }
            }

            res = "";
            for (int i = 0; i < cnt; i++)
            {
                res += finaaaa[i];               
            }

            return res;
        }
        public string GetCourseName(string htmlData)
        {
            ParseHTML parseHTML = new ParseHTML();
            var nodes = parseHTML.getNodes(htmlData, "h1", "", "default-title");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    string tmp = WebUtility.HtmlDecode(node.InnerText);
                    tmp = Regex.Replace(tmp, @"\s+", " ");
                    tmp = Regex.Replace(tmp, @"[^0-9a-zA-Z]+", " ");
                    tmp = tmp.TrimEnd(' ');
                    tmp = tmp.TrimStart(' ');
                    tmp = MakeValidFileName(tmp);
                    return tmp;                    
                }
            }

            return "";            
        }

        

        public List<string> GetVideoLinks(string htmlData)
        {

            List<string> VideoLiks = new List<string>();
            ParseHTML parseHTML = new ParseHTML();
            var nodes = parseHTML.getNodes(htmlData, "a", "", "item-name video-name ga");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    VideoLiks.Add(parseHTML.getAttribute(node, "href").Value);
                }
            }

            return VideoLiks;
        }

        public List<string> GetVideoName(string htmlData)
        {

            List<string> VideoNames = new List<string>();
            ParseHTML parseHTML = new ParseHTML();
            var nodes = parseHTML.getNodes(htmlData, "a", "", "item-name video-name ga");

            int cnt = 0;
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    string tmp = WebUtility.HtmlDecode(node.InnerText);
                    tmp = ClearName(tmp);                    
                    VideoNames.Add(tmp);
                    cnt++;
                }
            }

            return VideoNames;
        }

        public List<string> ClearFixVideoNames(List<string> VideoNamesB)
        {
            List<string> tmp = new List<string>();
            for(int i=0; i<VideoNamesB.Count; i++)
            {            
                string xxx = WebUtility.HtmlDecode(VideoNamesB[i]);
                xxx = ClearName(xxx);
                tmp.Add(xxx);
            }

            return tmp;
        }

        

        private string AddExtMP4(string Name)
        {
            return Name + ".mp4";
        }

        private string AddExtSRT(string Name)
        {
            return Name + ".srt";
        }

        private string AddNumberName(string Name, int nu)
        {
            return nu.ToString("000") + "- " + Name;
        }

        private string ClearName(string Name)
        {
            string tmp = Name;
            tmp = Regex.Replace(tmp, @"\s+", " ");
            tmp = Regex.Replace(tmp, @"[^0-9a-zA-Z]+", " ");
            tmp = tmp.TrimEnd(' ');
            tmp = tmp.TrimStart(' ');

            return tmp;
        }

        public void CreateSubFile(string CourseName, string FileName, List<Sub> subs)
        {

            string pathString = System.IO.Path.Combine(CourseName);
            System.IO.Directory.CreateDirectory(pathString);
            string path = pathString + @"\" + FileName;
            if (!File.Exists(pathString))
            {

                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    for (int i = 0; i < subs.Count; i++)
                    {
                        tw.WriteLine(subs[i].id);
                        tw.WriteLine(subs[i].start + " --> " + subs[i].end);
                        tw.WriteLine(subs[i].data);
                        tw.WriteLine("");
                    }
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, false))
                {
                    for (int i = 0; i < subs.Count; i++)
                    {
                        tw.WriteLine(subs[i].id);
                        tw.WriteLine(subs[i].start + " --> " + subs[i].end);
                        tw.WriteLine(subs[i].data);
                        tw.WriteLine("");
                    }
                    tw.Close();
                }
            }
        }

        public string ConvertTimeToSRTFormat(float tm)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(tm * 1000);
            string answer = string.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);

            return answer;
        }

        public string ParseVideoLink(string htmlData)
        {
            return new ParseHTML().GetAttributeFromClass(htmlData, "video", "", "player", "data-src");
        }

        public List<Sub> ParseSub(string htmlData)
        {
            List<Sub> subs = new List<Sub>();
            ParseHTML parseHTML = new ParseHTML();
            var nodes = parseHTML.getNodes(htmlData, "span", "", "transcript ga");
            int cnt = 0;
            float start = 0;
            float end = 0;
            float duration = 0;

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    Sub sub = new Sub();
                    
                    start = float.Parse(parseHTML.getAttribute(node, "data-duration").Value);
                    if(cnt == nodes.Count-1)
                        end = 0;
                    else
                        end = float.Parse(parseHTML.getAttribute(nodes[cnt+1], "data-duration").Value);
                    duration = end - start + 1;
                    sub.start = ConvertTimeToSRTFormat(start);
                    sub.end = ConvertTimeToSRTFormat(end);
                    sub.duration = ConvertTimeToSRTFormat(duration);
                    sub.id = cnt + "";
                    sub.data = WebUtility.HtmlDecode(node.InnerText);

                    subs.Add(sub);
                 
                    cnt++;
                }
            }

            return subs;
        }

        public string ParseExerciseLinkReal(string htmlData)
        {
            ParseHTML parseHTML = new ParseHTML();
            var node = parseHTML.getNode(htmlData, "a", "", "");

            return parseHTML.getAttribute(node, "href").Value;            
        }

        public List<Exercise> ParseExercises(string htmlData)
        {
            List<Exercise> exercises = new List<Exercise>();
            ParseHTML parseHTML = new ParseHTML();
            var nodes = parseHTML.getNodes(htmlData, "a", "", "course-file clearfix ga");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    Exercise exercise = new Exercise();                   
                    exercise.Link = parseHTML.getAttribute(node, "href").Value;
                    if (exercise.Link != null)
                        exercise.Link = "https://www.lynda.com" + exercise.Link;

                    string htmlData2 = node.InnerHtml;
                    var nodeSub = parseHTML.getNode(htmlData2, "span", "", "exercise-name");
                    if (nodeSub != null)
                    {
                            exercise.Name = nodeSub.InnerText;
                    }

                    var nodeSub2 = parseHTML.getNode(htmlData2, "span", "", "file-size");
                    if (nodeSub2 != null)
                    {
                        exercise.Size = nodeSub2.InnerText;
                    }

                    exercises.Add(exercise);
                }
            }

            return exercises;
        }

        private List<string> CreateSubNames(List<string> VideoNames)
        {
            List<string> SubNames = new List<string>();

            for(int i=0; i<VideoNames.Count; i++)
            {
                string tmp = VideoNames[i];
                tmp = AddNumberName(tmp, i);
                SubNames.Add(AddExtSRT(tmp));
            }

            return SubNames;
        }

        private List<string> CreateVideoNames(List<string> VideoNames)
        {
            List<string> Mp4Names = new List<string>();

            for (int i = 0; i < VideoNames.Count; i++)
            {
                string tmp = VideoNames[i];
                tmp = AddNumberName(tmp, i);
                Mp4Names.Add(AddExtMP4(tmp));
            }

            return Mp4Names;
        }


        //public void GetCourseSub(string url)
        //{
        //    string htmlData = OpenLyndaPage(url);
        //    if (htmlData == "")
        //        return;

        //    string CourseName = GetCourseName(htmlData);
        //    List<string> VideoNames= GetVideoName(htmlData);
        //    List<string> VideoLinks = GetVideoLinks(htmlData);
        //    List<string> SubNames = CreateSubNames(VideoNames);

        //    MainWindow.main.SetMaxProgress(VideoLinks.Count);

        //    for (int i=0; i<VideoLinks.Count; i++)
        //    {
        //        int cnt = i + 1;
        //        MainWindow.main.SetTxtPbStatus("(" + cnt + "/" + VideoLinks.Count + ")");
        //        string tmp = OpenLyndaPage(VideoLinks[i]);
        //        List<Sub> sub = ParseSub(tmp);
        //        if(sub.Count > 0)
        //            CreateSubFile(CourseName, SubNames[i], sub);
        //        MainWindow.main.AddTextOut(SubNames[i] + " Ok");
        //        MainWindow.main.SetProgress(i+1);                
        //    }                       
        //}

        //public void GetCourseVideos(string url)
        //{
        //    string htmlData = OpenLyndaPage(url);
        //    if (htmlData == "")
        //        return;

        //    string CourseName = GetCourseName(htmlData);
        //    List<string> VideoNames = GetVideoName(htmlData);
        //    List<string> VideoLinks = GetVideoLinks(htmlData);
        //    List<string> FixedVideoNames = CreateVideoNames(VideoNames);

        //    MainWindow.main.SetMaxProgress(VideoLinks.Count);

        //    for (int i = 0; i < VideoLinks.Count; i++)
        //    {
        //        int cnt = i + 1;
        //        MainWindow.main.SetTxtPbStatus("(" + cnt + "/" + VideoLinks.Count + ")");
        //        string tmp = OpenLyndaPage(VideoLinks[i]);
        //        string VideoLink = ParseVideoLink(tmp);
        //        if(DownloadVideo(VideoLink, FixedVideoNames[i], CourseName))
        //            MainWindow.main.AddTextOut(FixedVideoNames[i] + " Ok");
        //        else
        //            MainWindow.main.AddTextOut(FixedVideoNames[i] + " Error");
        //        MainWindow.main.SetProgress(i + 1);                
        //    }
        //}

        private Boolean DownloadExercise(Exercise exercise, string CourseName)
        {
            string htmlData = GetExerciseRealLink(exercise);
            exercise.Link = ParseExerciseLinkReal(htmlData);

            return DownloadExerciseFile(exercise, CourseName);                         
        }

        private string GetExerciseRealLink(Exercise exercise)
        {
            string responseString = "";

            try
            {
                Thread.Sleep(5000);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(exercise.Link);
                //KeepLive Activeit
                var sp = request.ServicePoint;
                var prop = sp.GetType().GetProperty("HttpBehaviour", BindingFlags.Instance | BindingFlags.NonPublic);
                prop.SetValue(sp, (byte)0, null);

                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:51.0) Gecko/20100101 Firefox/51.0";
                request.Accept = "video/webm,video/ogg,video/*;q=0.9,application/ogg;q=0.7,audio/*;q=0.6,*/*;q=0.5";
                request.AllowAutoRedirect = false;
                request.ServicePoint.Expect100Continue = false;
                request.KeepAlive = true;
                request.Timeout = 20000;
                request.AddRange(1778948);

                WebHeaderCollection requestCollection = request.Headers;
                requestCollection.Add("Accept-Language", "en-GB,en;q=0.5");
                requestCollection.Add("Accept-Encoding", "gzip, deflate, br");
                requestCollection.Add("Upgrade-Insecure-Requests", "1");
                requestCollection.Add("If-Range", "477973204af59694b3cca4feca67337f:1485536586");


                if (!LoginInfo.IsLogin)
                    LoginInfo.SendCookies = ParseCookies(LoginInfo.ReceivedCookies);

                if (LoginInfo.SendCookies == "")
                    return responseString;

                request.Headers["Cookie"] = LoginInfo.SendCookies;
                request.Referer = "https://www.lynda.com";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                responseString = sr.ReadToEnd();
                sr.Close();

                return responseString;                
            }
            catch (WebException ex)
            {
                MainWindow.main.AddLog(ex.ToString());

                return responseString;
            }
            catch (Exception ex)
            {
                MainWindow.main.AddLog(ex.ToString());

                return responseString;
            }
        }
        private static string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }
        private Boolean DownloadExerciseFile(Exercise exercise, string CourseName)
        {
            string pathString = System.IO.Path.Combine(CourseName);
            System.IO.Directory.CreateDirectory(pathString);
            System.IO.Directory.CreateDirectory(pathString + @"\exercises");
            string path = pathString + @"\exercises\" + exercise.Name;
            string tmpPath = pathString + @"\tmp";

            if (File.Exists(path))
            {
                return true;
            }

            try
            {
                Thread.Sleep(5000);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(exercise.Link);
                //KeepLive Activeit
                var sp = request.ServicePoint;
                var prop = sp.GetType().GetProperty("HttpBehaviour", BindingFlags.Instance | BindingFlags.NonPublic);
                prop.SetValue(sp, (byte)0, null);

                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:51.0) Gecko/20100101 Firefox/51.0";
                request.Accept = "video/webm,video/ogg,video/*;q=0.9,application/ogg;q=0.7,audio/*;q=0.6,*/*;q=0.5";
                request.AllowAutoRedirect = false;
                request.ServicePoint.Expect100Continue = false;
                request.KeepAlive = true;
                request.Timeout = 20000;
                request.AddRange(1778948);

                WebHeaderCollection requestCollection = request.Headers;
                requestCollection.Add("Accept-Language", "en-GB,en;q=0.5");
                requestCollection.Add("Accept-Encoding", "gzip, deflate, br");
                requestCollection.Add("Upgrade-Insecure-Requests", "1");
                requestCollection.Add("If-Range", "477973204af59694b3cca4feca67337f:1485536586");


                if (!LoginInfo.IsLogin)
                    LoginInfo.SendCookies = ParseCookies(LoginInfo.ReceivedCookies);

                if (LoginInfo.SendCookies == "")
                    return false;

                request.Headers["Cookie"] = LoginInfo.SendCookies;
                request.Referer = "https://www.lynda.com";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                int VideoSize = (int)response.ContentLength;
                MainWindow.main.SetMaxPbSmallStatus(VideoSize);

                // we will read data via the response stream
                Stream ReceiveStream = response.GetResponseStream();

                byte[] buffer = new byte[8192];
                if (File.Exists(tmpPath))
                {
                    File.Delete(tmpPath);
                }

                if (!File.Exists(tmpPath))
                {

                    FileStream outFile = new FileStream(tmpPath, FileMode.Create);

                    int bytesRead;
                    int bytesReaded = 0;
                    while ((bytesRead = ReceiveStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        outFile.Write(buffer, 0, bytesRead);
                        MainWindow.main.SetPbSmall(bytesReaded);
                        bytesReaded += bytesRead;
                    }


                    // Or using statement instead
                    outFile.Close();

                    System.IO.File.Move(tmpPath, path);
                }

                LoginInfo.ReceivedCookies = response.Headers["Set-Cookie"];

                response.Close();
                return true;

            }
            catch (WebException ex)
            {
                MainWindow.main.AddLog(ex.ToString());

                if (File.Exists(tmpPath))
                {
                    File.Delete(tmpPath);
                }
                return false;
            }
            catch (Exception ex)
            {
                MainWindow.main.AddLog(ex.ToString());

                if (File.Exists(tmpPath))
                {
                    File.Delete(tmpPath);
                }
                return false;
            }
        }

        private Boolean DownloadVideo(string VideoLink, string VideoName, string CourseName)
        {
            string pathString = System.IO.Path.Combine(CourseName);
            System.IO.Directory.CreateDirectory(pathString);
            string path = pathString + @"\" + VideoName;
            string tmpPath = pathString + @"\tmp";

            if (File.Exists(path))
            {
                return true;
            }

            try
            {
                Thread.Sleep(5000);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(VideoLink);
                //KeepLive Activeit
                var sp = request.ServicePoint;
                var prop = sp.GetType().GetProperty("HttpBehaviour", BindingFlags.Instance | BindingFlags.NonPublic);
                prop.SetValue(sp, (byte)0, null);

                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:51.0) Gecko/20100101 Firefox/51.0";
                request.Accept = "video/webm,video/ogg,video/*;q=0.9,application/ogg;q=0.7,audio/*;q=0.6,*/*;q=0.5";
                request.AllowAutoRedirect = false;
                request.ServicePoint.Expect100Continue = false;
                request.KeepAlive = true;
                request.Timeout = 20000;
                request.AddRange(1778948);                   

                WebHeaderCollection requestCollection = request.Headers;
                requestCollection.Add("Accept-Language", "en-GB,en;q=0.5");
                requestCollection.Add("Accept-Encoding", "gzip, deflate, br");
                requestCollection.Add("Upgrade-Insecure-Requests", "1");
                requestCollection.Add("If-Range", "477973204af59694b3cca4feca67337f:1485536586");


                if (!LoginInfo.IsLogin)
                    LoginInfo.SendCookies = ParseCookies(LoginInfo.ReceivedCookies);

                if (LoginInfo.SendCookies == "")
                    return false;

                request.Headers["Cookie"] = LoginInfo.SendCookies;
                request.Referer = "https://www.lynda.com";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                int VideoSize = (int)response.ContentLength;
                MainWindow.main.SetMaxPbSmallStatus(VideoSize);

                // we will read data via the response stream
                Stream ReceiveStream = response.GetResponseStream();

                byte[] buffer = new byte[8192];                
                if (File.Exists(tmpPath))
                {
                    File.Delete(tmpPath);
                }

                if (!File.Exists(tmpPath))
                {

                    FileStream outFile = new FileStream(tmpPath, FileMode.Create);

                    int bytesRead;
                    int bytesReaded = 0;
                    while ((bytesRead = ReceiveStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        outFile.Write(buffer, 0, bytesRead);
                        MainWindow.main.SetPbSmall(bytesReaded);
                        bytesReaded += bytesRead;
                    }
                        

                    // Or using statement instead
                    outFile.Close();

                    System.IO.File.Move(tmpPath, path);
                }

                LoginInfo.ReceivedCookies = response.Headers["Set-Cookie"];

                response.Close();
                return true;

            }
            catch (WebException ex)
            {                
                MainWindow.main.AddLog(ex.ToString());

                if (File.Exists(tmpPath))
                {
                    File.Delete(tmpPath);
                }
                return false;
            }
            catch (Exception ex)
            {
                MainWindow.main.AddLog(ex.ToString());

                if (File.Exists(tmpPath))
                {
                    File.Delete(tmpPath);
                }
                return false;
            }                    
        }

        public Boolean IsLogedIn()
        {
            String htmlData = "";

            htmlData = OpenLyndaPage("https://www.lynda.com");
            if (htmlData == "")
            {
                LoginInfo.IsLogin = false;
                return false;
            }
                

            if (htmlData.IndexOf("My Playlists") < 0)
            {
                LoginInfo.IsLogin = false;
                return false;
            }

            LoginInfo.IsLogin = true;
            return true;            
        }

        public Boolean TryOpenPage(string url)
        {
            String htmlData = "";

            htmlData = OpenLyndaPage(url);
            if (htmlData == "")
                return false;

            if (htmlData.IndexOf("My Playlists") < 0)
                return false;

            return true;
        }

        public string GetPlaneHtml(string url)
        {
            return new WebClient().DownloadString(url);
        }

        public string OpenLyndaPage(string url)
        {
            var responseString = "";

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                //KeepLive Activeit
                var sp = request.ServicePoint;
                var prop = sp.GetType().GetProperty("HttpBehaviour", BindingFlags.Instance | BindingFlags.NonPublic);
                prop.SetValue(sp, (byte)0, null);

                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:51.0) Gecko/20100101 Firefox/51.0";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.AllowAutoRedirect = false;
                request.ServicePoint.Expect100Continue = false;
                request.KeepAlive = true;

                WebHeaderCollection requestCollection = request.Headers;
                requestCollection.Add("Accept-Language", "en-GB,en;q=0.5");
                requestCollection.Add("Accept-Encoding", "gzip, deflate, br");
                requestCollection.Add("Upgrade-Insecure-Requests", "1");

                if (!LoginInfo.IsLogin)
                    LoginInfo.SendCookies = ParseCookies(LoginInfo.ReceivedCookies);

                if (LoginInfo.SendCookies == "")
                    return "";

                request.Headers["Cookie"] = LoginInfo.SendCookies;
                request.Referer = "https://www.lynda.com";

                var response = (HttpWebResponse)request.GetResponse();
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                LoginInfo.ReceivedCookies = response.Headers["Set-Cookie"];
            }
            catch(Exception ex)
            {
                MainWindow.main.AddLog(ex.ToString());
            }

            return responseString.ToString();
        }




        public string GetLoginPageHTMLAndCokies(string LibraryLink)
        {
            string responseString = "";
            try
            {
                CookieContainer cookieJar = new CookieContainer();
                var request = (HttpWebRequest)WebRequest.Create(LibraryLink);
                //KeepLive Activeit
                var sp = request.ServicePoint;
                var prop = sp.GetType().GetProperty("HttpBehaviour", BindingFlags.Instance | BindingFlags.NonPublic);
                prop.SetValue(sp, (byte)0, null);

                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:51.0) Gecko/20100101 Firefox/51.0";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                WebHeaderCollection requestCollection = request.Headers;
                requestCollection.Add("Accept-Language", "en-GB,en;q=0.5");
                requestCollection.Add("Accept-Encoding", "gzip, deflate, br");
                requestCollection.Add("Upgrade-Insecure-Requests", "1");

                request.AllowAutoRedirect = false;
                request.ServicePoint.Expect100Continue = false;
                request.KeepAlive = true;
                //request.CookieContainer = cookieJar;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                responseString = sr.ReadToEnd();
                sr.Close();

                string TmpCookies = response.Headers["Set-Cookie"];




                ParseHTML parseHTML = new ParseHTML();
                LoginInfo.SeasurfID = parseHTML.GetSeasurfID(responseString.ToString());
                LoginInfo.SeasurfIDEn = Uri.EscapeDataString(LoginInfo.SeasurfID);
                LoginInfo.ReceivedCookies = TmpCookies;
                LoginInfo.SendCookies = ParseCookies(new ToolCookie().GenrateSendCookies(LoginInfo.ReceivedCookies));
                //LoginInfo.cookies2 = cookieJar.GetCookieHeader(request.RequestUri);

                //LoginInfo.cookie = cookieJar.GetCookies(request.RequestUri);
                //LoginInfo.cookies2 = cookieJar.GetCookieHeader(request.RequestUri);
                //foreach (Cookie c in cookieJar.GetCookies(request.RequestUri))
                //{
                //    LoginInfo.cookies2 += c.Name + "=" + c.Value;
                //    LoginInfo.cookies2 += ";";
                //}

                request.Abort();  // !! Yes, abort the request
                if (response != null)
                    response.Close();
            }
            catch (Exception ex)
            {
                MainWindow.main.AddLog(ex.ToString());
            }

            return  responseString.ToString();
        }

        public string DoLoginUserPass(string CardNumber, string PinNumber, string LibraryLink)
        {
            string responseString = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(LibraryLink);

                //KeepLive Activeit
                var sp = request.ServicePoint;
                var prop = sp.GetType().GetProperty("HttpBehaviour", BindingFlags.Instance | BindingFlags.NonPublic);
                prop.SetValue(sp, (byte)0, null);

                string org = "";
                foreach (string item in LibraryLink.Split('&'))
                {
                    string[] parts = item.Replace('?', ' ').Split('=');
                    org = parts[1];
                    break;
                }

                //Uri myUri = new Uri(LibraryLink);
                //string org = HttpUtility.ParseQueryString(myUri.Query).Get("org");

                var postData = "libraryCardNumber=" + CardNumber;
                postData += "&libraryCardPin=" + PinNumber;
                postData += "&libraryCardPasswordVerify=";
                postData += "&org=" + org;
                postData += "&currentView=login";

                string EscapeDataPost = Uri.EscapeDataString(LoginInfo.SeasurfID);
                postData += "&seasurf=" + EscapeDataPost;
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:51.0) Gecko/20100101 Firefox/51.0";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //request.CookieContainer = LoginInfo.cookie;
                request.AllowAutoRedirect = false;
                request.ServicePoint.Expect100Continue = false;
                request.KeepAlive = true;

                WebHeaderCollection requestCollection = request.Headers;
                requestCollection.Add("Accept-Language", "en-GB,en;q=0.5");
                requestCollection.Add("Accept-Encoding", "gzip, deflate, br");
                requestCollection.Add("Upgrade-Insecure-Requests", "1");

                if (LoginInfo.SendCookies != "")
                    request.Headers["Cookie"] = LoginInfo.SendCookies;

                //requestCollection.Add("Cookie", LoginInfo.cookies);

                request.Referer = LibraryLink;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
                var response2 = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response2.GetResponseStream());
                responseString = sr.ReadToEnd();
                sr.Close();

                LoginInfo.ReceivedCookies = "";
                LoginInfo.ReceivedCookies = response2.Headers["Set-Cookie"];
                LoginInfo.SendCookies = ParseCookies(new ToolCookie().GenrateSendCookies(LoginInfo.ReceivedCookies));

                request.Abort();  // !! Yes, abort the request
                if (response2 != null)
                    response2.Close();
            }
            catch(Exception ex)
            {
                MainWindow.main.AddLog(ex.ToString());
            }



            return responseString.ToString();
        }


        public string GetCourseHTML(string CourseLink)
        {
            var responseString = "";
            string dd = "";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://www.lynda.com/portal/sip?org=spl.org&triedlogout=true");

                var postData = "libraryCardNumber=10001260012-2";
                postData += "&libraryCardPin=1234";
                postData += "&libraryCardPasswordVerify=";
                postData += "&org=spl.org";
                postData += "&currentView=login";
                postData += "&seasurf=" + "fSBxq9DXzTAELmYBb8NLkpW4ofvcSvkB6sE1OTouaPQ=";// Uri.EscapeDataString("fSBxq9DXzTAELmYBb8NLkpW4ofvcSvkB6sE1OTouaPQ=");
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.UserAgent = "User - Agent  Mozilla / 5.0(X11; Ubuntu; Linux x86_64; rv: 51.0) Gecko / 20100101 Firefox / 51.0";
                request.Accept = "text / html,application / xhtml + xml,application / xml; q = 0.9,*/*;q=0.8";

                WebHeaderCollection requestCollection = request.Headers;
                requestCollection.Add("Accept-Language:da");
                requestCollection.Add("Accept-Language", "en;q=0.8");

                request.Referer = "https://www.lynda.com/portal/sip?org=spl.org&triedlogout=true";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                dd = response.Headers["Set-Cookie"];
            }
            catch(Exception ex)
            {
                MainWindow.main.AddLog(ex.ToString());
            }

            return dd + " site = " +  responseString.ToString();
        }

        //public string GetCourseID(string CourseID)
        //{
        //    dynamic stuff = JsonConvert.DeserializeObject(jsonData);

        //    return stuff.CourseId;            
        //}

        private string GetJsonFromID(string CourseID, string VideoID)
        {
            string jsonData = OpenLyndaPage("https://www.lynda.com/ajax/player/conviva?courseId=" + CourseID + "&videoId=" + VideoID);
            if (jsonData == "")
                return "";

            return jsonData;
        }

        private string GetSubFromID(string CourseID, string VideoID)
        {
            string subData = OpenLyndaPage("https://www.lynda.com/ajax/course/videotranscripts?courseId=" + CourseID + "&videoId=" + VideoID);
            if (subData == "")
                return "";

            return subData;
        }

        public string GetCourseTitle(string CourseID)
        {
            string jsonData = GetJsonFromID(CourseID, "5464");            
            if (jsonData == "")
                return "";

            dynamic stuff = JsonConvert.DeserializeObject(jsonData);
            string tmp = stuff.CourseTitle;
            string CourseName = MakeValidFileName(tmp);

            return CourseName;
        }

        public string GetVideoTitle(string CourseID, string VideoID)
        {
            string jsonData = GetJsonFromID(CourseID, VideoID);
            if (jsonData == "")
                return "";

            dynamic stuff = JsonConvert.DeserializeObject(jsonData);

            return stuff.VideoTitle;
        }

        public List<string> GetVideoTitles(string CourseID, List<string> VideoIds)
        {
            List<string> VideoTitles = new List<string>();

            for(int i=0; i < VideoIds.Count; i++)
            {
                VideoTitles.Add(GetVideoTitle(CourseID, VideoIds[i]));
            }

            return VideoTitles;
        }

        public string GetVideoUrl(string CourseID, string VideoID)
        {
            string jsonData = GetJsonFromID(CourseID, VideoID);
            if (jsonData == "")
                return "";

            dynamic stuff = JsonConvert.DeserializeObject(jsonData);

            return stuff.Url;
        }

        public List<string> GetVideoUrls(string CourseID, List<string> VideoIds)
        {
            List<string> VideoUrls = new List<string>();

            for (int i = 0; i < VideoIds.Count; i++)
            {
                VideoUrls.Add(GetVideoUrl(CourseID, VideoIds[i]));
            }

            return VideoUrls;
        }

        //public string GetCourseAuthor(string CourseID)
        //{
        //    dynamic stuff = JsonConvert.DeserializeObject(jsonData);

        //    return stuff.Author;            
        //}

        //public string GetCourseSkillLevel(string CourseID)
        //{
        //    dynamic stuff = JsonConvert.DeserializeObject(jsonData);

        //    return stuff.SkillLevel;
        //}


        //public string GetVideoID(string CourseID, string VideoID)
        //{
        //    dynamic stuff = JsonConvert.DeserializeObject(jsonData);

        //    return stuff.VideoId;           
        //}









        //public List<string>  GetVideosIDs(string CourseID, List<string> VideoIds)
        //{
        //    ParseHTML parseHTML = new ParseHTML();
        //    var nodes = parseHTML.getNodes(htmlData, "h1", "", "default-title");

        //    if (nodes != null)
        //    {
        //        foreach (var node in nodes)
        //        {
        //            string tmp = WebUtility.HtmlDecode(node.InnerText);
        //            tmp = Regex.Replace(tmp, @"\s+", " ");
        //            tmp = Regex.Replace(tmp, @"[^0-9a-zA-Z]+", " ");
        //            tmp = tmp.TrimEnd(' ');
        //            tmp = tmp.TrimStart(' ');
        //            return tmp;
        //        }
        //    }

        //    return "";
        //}

        //public CourseInfo GetCourseInfo(string CourseLink)
        //{
        //    //https://www.lynda.com/Hadoop-tutorials/Hadoop-Fundamentals/191942-2.html
        //    //https://www.lynda.com/ajax/player/conviva?courseId=572160&videoId=580125
        //    //{"Duration":80,"CourseId":572160,"VideoId":580125,"UserId":0,"ReleaseYear":2017,"Quality":540,"SkillLevel":"Intermediate","CourseTitle":"Learning Angular 2","VideoTitle":"What you should know","Software":"AngularJS","Subjects":"Web","Topics":"Web Design,Web Development","Url":"https://lynda_files2-a.akamaihd.net/secure/courses/572160/VBR_MP4h264_main_SD/572160_00_02_XR15_What_You_Should_Know.mp4?c3.ri=3770791939066623803\u0026hashval=1488283254_9d175bc9c6da943dcafc92a1ae51ef37","Author":"Ray Villalobos","CdnName":"AKAMAI","VideoDescription":"","QualitiesAvailable":["360","540","720","64"]}
        //    string htmlData = OpenLyndaPage(CourseLink);
        //    if (htmlData == "")
        //        return null;

        //}

        public string GetCourseIDFromLink(string CourseLink)
        {
            string url = CourseLink;
            int urlLength = url.Length;
            int endIdIndex = url.IndexOf(".html");
            int startIndex = url.LastIndexOf("/") + 1;

            string Tmp = url.Substring(startIndex, endIdIndex - startIndex);

            int startIndexDash = Tmp.LastIndexOf("-");
            return Tmp.Substring(0, startIndexDash);
        }

        public string GetVideoIDFromLink(string VideoLink)
        {
            string url = VideoLink;
            int urlLength = url.Length;
            int endIdIndex = url.IndexOf(".html");
            int startIndex = url.LastIndexOf("/") + 1;

            string Tmp = url.Substring(startIndex, endIdIndex - startIndex);

            int startIndexDash = Tmp.LastIndexOf("-");
            return Tmp.Substring(0, startIndexDash);
        }

        public string GetCourseTitleOnly(string url)
        {
            string htmlData = OpenLyndaPage(url);
            if (htmlData == "")
                return "";

            string CourseID = GetCourseIDFromLink(url);
            return GetCourseTitle(CourseID);
        }

        public void GetCourseVideos(string url)
        {
            string htmlData = OpenLyndaPage(url);
            if (htmlData == "")
                return;

            List<string> VideoLinksForIDs = GetVideoLinks(htmlData);
            List<string> VideoIds = GetVideoIDsFromLinks(VideoLinksForIDs);
            string CourseID = GetCourseIDFromLink(url);
            string CourseName = GetCourseTitle(CourseID);

            List<string> VideoNamess = GetVideoTitles(CourseID, VideoIds);
            List<string> VideoNames = ClearFixVideoNames(VideoNamess);
            List<string> FixedVideoNames = CreateVideoNames(VideoNames);

            List<string> VideoLinks = GetVideoUrls(CourseID, VideoIds);

            MainWindow.main.SetMaxProgress(VideoLinks.Count);

            for (int i = 0; i < VideoLinks.Count; i++)
            {
                if (MainWindow.Stop) {
                    MainWindow.DoneWithMessage("Canceled By User!");
                    return;
                }
                int cnt = i + 1;
                MainWindow.main.SetTxtPbStatus("(" + cnt + "/" + VideoLinks.Count + ")");
                if (DownloadVideo(VideoLinks[i], FixedVideoNames[i], CourseName))
                    MainWindow.main.AddTextOut(FixedVideoNames[i] + " Ok");
                else
                    MainWindow.main.AddTextOut(FixedVideoNames[i] + " Error");
                MainWindow.main.SetProgress(i + 1);
            }
        }

        public List<string> GetVideoIDsFromLinks(List<string> VideoLinks)
        {
            List<string> VideosIDs = new List<string>();
            for (int i = 0; i < VideoLinks.Count; i++){
                VideosIDs.Add(GetVideoIDFromLink(VideoLinks[i]));
            }

            return VideosIDs;
        }

        private List<string> CreateSubHtml(string CourseID, List<string> VideoIds)
        {

            //https://www.lynda.com/ajax/course/videotranscripts?courseId=572160&videoId=580125

            List<string> SubHtmls = new List<string>();
            for (int i=0; i < VideoIds.Count; i++)
            {
                if (MainWindow.Stop)
                {
                    MainWindow.DoneWithMessage("Canceled By User!");
                    return SubHtmls;
                }
                SubHtmls.Add(GetSubFromID(CourseID, VideoIds[i]));
            }

            return SubHtmls;
        }

        public void GetCourseSub(string url)
        {
            string htmlData = OpenLyndaPage(url);
            if (htmlData == "")
                return;
          
            List<string> VideoLinksForIDs = GetVideoLinks(htmlData);
            List<string> VideoIds = GetVideoIDsFromLinks(VideoLinksForIDs);
            string CourseID = GetCourseIDFromLink(url);
            string CourseName = GetCourseTitle(CourseID);

            List<string> VideoNamess = GetVideoTitles(CourseID, VideoIds);
            List<string> VideoNames = ClearFixVideoNames(VideoNamess);
            List<string> SubNames = CreateSubNames(VideoNames);
            List<string> VideoSubs = CreateSubHtml(CourseID, VideoIds);

            MainWindow.main.SetMaxProgress(VideoSubs.Count);

            for (int i = 0; i < VideoSubs.Count; i++)
            {
                if (MainWindow.Stop)
                {
                    MainWindow.DoneWithMessage("Canceled By User!");
                    return;
                }
                int cnt = i + 1;
                MainWindow.main.SetTxtPbStatus("(" + cnt + "/" + VideoSubs.Count + ")");
                ParseSubData(VideoSubs[i]);
                List<Sub> sub = ParseSubData(VideoSubs[i]);
                if (sub.Count > 0)
                {
                    CreateSubFile(CourseName, SubNames[i], sub);
                    MainWindow.main.AddTextOut(SubNames[i] + " Ok");
                }
                else
                {
                    MainWindow.main.AddTextOut(SubNames[i] + " Error");
                }
                MainWindow.main.SetProgress(i + 1);
            }
        }

        public void GetCourseExercise(string url)
        {
            string htmlData = OpenLyndaPage(url);
            if (htmlData == "")
                return;

            string CourseID = GetCourseIDFromLink(url);
            string CourseName = GetCourseTitle(CourseID);

            List<Exercise> exercises = ParseExercises(htmlData);

            MainWindow.main.SetMaxProgress(exercises.Count);

            int cnt = 0;
            foreach (Exercise exercise in exercises)
            {
                if (MainWindow.Stop)
                {
                    MainWindow.DoneWithMessage("Canceled By User!");
                    return;
                }

                cnt++;
                MainWindow.main.SetTxtPbStatus("(" + cnt + "/" + exercises.Count + ")");

                MainWindow.main.AddTextOut("Downloading " + exercise.Name + " ---> Size=" + exercise.Size);

                if (DownloadExercise(exercise, CourseName))
                    MainWindow.main.AddTextOut(exercise.Name + " Ok");
                else
                    MainWindow.main.AddTextOut(exercise.Name + " Error");
                
                MainWindow.main.SetProgress(cnt);
            }
        }

        private List<Sub> ParseSubData(string ajxData)
        {
            List<Sub> subList = new List<Sub>();
            dynamic stuff = JsonConvert.DeserializeObject(ajxData);
            string tmp2 = stuff.html;
            string replacement = Regex.Replace(tmp2, @"\t|\n|\r", "");
            subList = ParseSub(replacement);

            return subList;
        }

        
    }
}
