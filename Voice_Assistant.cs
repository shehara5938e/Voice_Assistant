using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Xml;

namespace Voice_Assistant
{
    public partial class Voice_Assistant : Form
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

        public Voice_Assistant()
        {
            InitializeComponent();

            //Speech Synthesis + Basic Codes//Speech Synthesis + Basic Codes

            ai.SelectVoiceByHints(VoiceGender.Female);
            //wake = false;
            ai.Rate = 1;

            if (wake == true)
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;
                label5.Text = "Active";
                panel2.BackColor = Color.SpringGreen;
            }
            else
            {
                pictureBox3.Visible = false;
                pictureBox2.Visible = true;
                label5.Text = "Passive";
                panel2.BackColor = Color.LightPink;
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
            {"active listening on", "active listening off", "restart", "hi seni", "terminate program",
             "what time is it", "whats the todays date", "whats the weather like", "whats the temperature now", "hey seni",
             "play music", "stop music", "close music", "open edge", "close edge", "open web search", "close web search",
                "open whatsapp", "close whatsapp", "open linkedin", "close linkedin", "open explorer", "close explorer"});

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
        string[] greetings = new string[3] { "Hi there", "Hello", "Hi whatz up" };
        public string greetings_action()
        {
            Random r = new Random();
            return greetings[r.Next(3)];
        }

        string[] accept = new string[4] { "Ok. Now,", "Ok sir. Now,", "Copy That. Now,", "Got it. Now," };
        public string accept_action()
        {
            Random a = new Random();
            return accept[a.Next(3)];
        }

        private void rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string r = e.Result.Text;

            //Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes
            //Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes

            //precise listening
            //if (r == "hey seni")
            //{
            //    wake = true;
            //}

            //wake sleep
            if (r == "active listening on")
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;
                label5.Text = "Active";
                panel2.BackColor = Color.SpringGreen;
                say("Active Listening On");
                wake = true;
            }
            else if (r == "active listening off")
            {
                pictureBox3.Visible = false;
                pictureBox2.Visible = true;
                label5.Text = "Passive";
                panel2.BackColor = Color.LightPink;
                say("Active Listening Off");
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
                if (r == "hi seni")
                {
                    say(greetings_action());
                }

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
                    say(accept_action() + " playing your favourite songs list on youtube.");
                    Process.Start("https://www.youtube.com/watch?v=FM7MFYoylVs&list=RDFM7MFYoylVs");
                }

                if (r == "stop music" || r == "close music")
                {
                    say(accept_action() + " Stoping Music");
                    killprog("msedge.exe");
                }

                if (r == "open web search")
                {
                    say(accept_action() + " Opening Google Search");
                    Process.Start("https://www.google.com/");
                }

                if (r == "close web search")
                {
                    say(accept_action() + " Closing Web Search");
                    killprog("msedge.exe");
                }

                if (r == "open whatsapp")
                {
                    say("Opening WhatsApp on web");
                    Process.Start("https://web.whatsapp.com/");
                }

                if (r == "close whatsapp")
                {
                    say("Closing WhatsApp");
                    killprog("msedge.exe");
                }

                if (r == "open linkedin")
                {
                    say("Opening Linkedin");
                    Process.Start("https://linkedin.com/");
                }

                if (r == "close linkedin")
                {
                    say("Closing Linkedin");
                    killprog("msedge.exe");
                }

                if (r == "open edge")
                {
                    say("Opening Edge");
                    Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Edge.lnk");
                }

                if (r == "close edge")
                {
                    say("Closing Edge");
                    killprog("msedge.exe");
                }
                //specified apps launching & closing commands
            }
            textBox1.AppendText(r + System.Environment.NewLine);
            //voice commands

            //Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes
            //Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes//Conversational Codes
        }

        //Active Listening On//Active Listening On
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
            label5.Text = "Active";
            panel2.BackColor = Color.SpringGreen;
            say("Active Listening On");
            wake = true;
        }

        //Active Listening Off//Active Listening Off
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
            label5.Text = "Passive";
            panel2.BackColor = Color.LightPink;
            say("Active Listening Off");
            wake = false;
        }

        //restart method//restart method//restart method
        public void restart()
        {
            Process.Start(@"\bin\Debug\Voice_Assistant.exe");
            Application.Exit();
        }
        //restart method//restart method//restart method

        //kill app//kill app//kill app
        public void killprog(string s)
        {
            foreach (var process in Process.GetProcessesByName(s))
            {
                process.Kill();
            }
        }
        //kill app//kill app//kill app

        //weather method//weather method
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
        //weather method//weather method

        //AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes
        //AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes
        //AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes//AI Codes

        private void Voice_Assistant_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        public void Update_Time()
        {
            label6.Text = DateTime.Now.ToString("d");
            label7.Text = DateTime.Now.ToString("hh:mm:ss:tt");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Update_Time();
        }
    }
}
