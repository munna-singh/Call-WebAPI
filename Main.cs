 public partial class CallAPI : System.Web.UI.Page
    {
        private static List<Cookie> _cookiesColl = null;
        private static CookieContainer cookies = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Login();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress =
                    new Uri(
                        "http://local-worldviewselect/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/catalog/hotel/availability/c20d9c0b97d75acb6053efe07bcb551d/results/1_0028276").Result;
                var abcresponse = response.Content.ReadAsStringAsync().Result;

            }
        }

        private void Login()
        {
            cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            handler.UseDefaultCredentials = true;

            string loginCred = "{\"username\":\"abc.com\",\"password\":\"abcpasspord\",\"email-address\":\"\",\"password1\":\"\",\"password2\":\"\"}";
            //HttpContent content = new StringContent(loginCred);
            HttpContent content = new StringContent(loginCred, Encoding.Unicode, "application/json");
            using (var client = new HttpClient(handler))
            {
                Uri uri = new Uri("http://local-worldviewselect/api/auth");

                // New code:
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 

                // New code:
                HttpResponseMessage response = client.PostAsync(uri.OriginalString, content).Result;
                var abcresponse = response.Content.ReadAsStringAsync().Result;
                //var cook = cookies.GetCookies(uri);                                       
                //_cookiesColl = cookies.GetCookies(uri).Cast<Cookie>().ToList();
            }
        }
    }
