using System;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;
using System.Xml;

namespace SENIGMA
{
    public partial class Form1 : Form
    {
        //AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes
        //AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes
        //AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes

        SpeechSynthesizer ai = new SpeechSynthesizer();
        SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
        Choices list = new Choices();
        Boolean wake = true;
        string greeting;
        string temp;
        string condition;
        string high;
        string low;

        public Form1()
        {
            InitializeComponent();

            //Speech Synthesis + Basic Codes//Speech Synthesis + Basic Codes


            ai.SelectVoiceByHints(VoiceGender.Female);
            //wake = false;
            ai.Rate = 1;

            if (wake = true)
            {
                label5.Text = "Awake";
            }
            else
            {
                label5.Text = "Sleep";
            }

            DateTime currentTime = DateTime.Now;
            int currentHour = currentTime.Hour;

            if (currentHour >= 00 && currentHour < 12)
            {
                greeting = "Good Morning!";
            }
            else if (currentHour >= 12 && currentHour < 18)
            {
                greeting = "Good Afternoon!";
            }
            else
            {
                greeting = "Good Evening!";
            }

            ai.Speak(greeting + " How can I help you today?");

            //Speech Synthesis + Basic Codes//Speech Synthesis + Basic Codes


            //Speech Recognition//Speech Recognition//Speech Recognition

            list.Add(new string[]
            {"wakeup", "sleep", "restart", "hi alexa", "terminate program",
             "what time is it", "whats the todays date", "whats the weather like", "whats the temperature now", "hey alexa", 
             "play music", "stop music", "close music", "open edge", "close edge", 
             "open search", "close search", "open whatsapp", "close whatsapp", "open linkedin", "close linkedin"});

            Grammar gr = new Grammar(new GrammarBuilder(list));

            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammar(gr);
                rec.SpeechRecognized += rec_SpeechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch { return; }

            //Speech Recognition//Speech Recognition//Speech Recognition           
        }

        public void say(string h)
        {
            ai.Speak(h);
            //wake = false;
            textBox2.AppendText(h + System.Environment.NewLine);
        }

        //randome response
        string[] greetings = new string[3] {"Hi there", "Hello", "Hi"};
        public string greetings_action()
        {
            Random r = new Random();
            return greetings[r.Next(3)];
        }

        private void rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string r = e.Result.Text;

            //Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes
            //Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes

            //precise listening
            //if (r == "hey alexa")
            //{
            //    wake = true;
            //}

            //wake sleep
            if (r == "wakeup")
            {
                say("System Is Now Online");
                label5.Text = "Awake";
                wake = true;
            }
            else if (r == "sleep")
            {
                say("System Is Now Offline");
                label5.Text = "Sleep";
                wake = false;
            }
            //wake sleep

            //voice commands
            if (wake == true)
            {
                //essential commands
                if (r == "terminate program")
                {
                    say("terminating program in 3, 2, 1.");
                    Application.Exit();
                }

                if (r == "restart")
                {
                    say("Restarting Program");
                    restart();
                }
                //essential commands

                //information commands
                //if (r == "hi alexa")
                //{
                //    say(greetings_action());
                //}

                if (r == "what time is it")
                {
                    say("Now the time is " + DateTime.Now.ToString("h:mm tt"));
                }

                if (r == "whats the todays date")
                {
                    say("Today is " + DateTime.Now.ToString("M/d/yyyy"));
                }

                if (r == "whats the weather like")
                {
                    say("Now the sky is " + GetWeather("cond"));
                }

                if (r == "whats the temperature now")
                {
                    say("Now the temperature is " + GetWeather("temp") + " degrees.");
                }
                //information commands

                //specified apps launching & closing commands
                if (r == "play music")
                {
                    say("Playing your favourite songs list on youtube.");
                    Process.Start("https://www.youtube.com/watch?v=FM7MFYoylVs&list=RDFM7MFYoylVs");
                }

                if (r == "stop music")
                {
                    say("Programs closing sequence is under development, sorry for the inconvenience.");
                }

                if (r == "open search")
                {
                    say("Opening Google Search");
                    Process.Start("https://www.google.com/");
                }

                if (r == "close search")
                {
                    say("Programs closing sequence is under development, sorry for the inconvenience.");
                }

                if (r == "open whatsapp")
                {
                    say("Opening WhatsApp on web");
                    Process.Start("https://web.whatsapp.com/");
                }

                if (r == "close whatsapp")
                {
                    say("Programs closing sequence is under development, sorry for the inconvenience.");
                }

                if (r == "open linkedin")
                {
                    say("Opening Linkedin");
                    Process.Start("https://linkedin.com/");
                }

                if (r == "close linkedin")
                {
                    say("Programs closing sequence is under development, sorry for the inconvenience.");
                }

                if (r == "open edge")
                {
                    say("Opening Edge");
                    Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Edge.lnk");
                }

                if (r == "close edge")
                {
                    say("Programs closing sequence is under development, sorry for the inconvenience.");
                    //killprog("msedge.exe");
                }
                //specified apps launching & closing commands
            }
            textBox1.AppendText(r + System.Environment.NewLine);
            //voice commands

            //Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes
            //Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes
        }

        //restart method//restart method//restart method
        //restart method//restart method//restart method
        public void restart()
        {
            Process.Start(@"D:\SE Projects\SENIGMA\bin\Debug\SENIGMA.exe");
            Application.Exit();
        }
        //restart method//restart method//restart method
        //restart method//restart method//restart method

        //kill app//kill app//kill app
        //kill app//kill app//kill app
        public void killprog(string s)
        {
            try
            {
                Process[] procs = Process.GetProcessesByName(s);

                if (procs != null)
                {
                    foreach (Process proc in procs)
                    {
                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                        proc.Dispose();
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }
        }
        //kill app//kill app//kill app
        //kill app//kill app//kill app

        //weather method//weather method//weather method
        //weather method//weather method//weather method
        public String GetWeather(String input)
        {
            String query = String.Format("https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='Colombo, Sri Lanka')&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
            XmlDocument wData = new XmlDocument();
            wData.Load(query);

            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
            manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

            XmlNode channel = wData.SelectSingleNode("query").SelectSingleNode("results").SelectSingleNode("channel");
            XmlNodeList nodes = wData.SelectNodes("query/results/channel");
            try
            {
                temp = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;
                condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;
                high = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["high"].Value;
                low = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["low"].Value;
                if (input == "temp")
                {
                    return temp;
                }
                if (input == "high")
                {
                    return high;
                }
                if (input == "low")
                {
                    return low;
                }
                if (input == "cond")
                {
                    return condition;
                }
            }
            catch
            {
                return "Error Reciving data";
            }
            return "error";
        }
        //weather method//weather method//weather method
        //weather method//weather method//weather method

        //AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes
        //AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes
        //AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
