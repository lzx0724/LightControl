using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CognitiveServices.Speech;
using Newtonsoft.Json;
<<<<<<< HEAD

<<<<<<< HEAD
namespace LightControl
{
=======
=======
using System.Speech.Synthesis;

>>>>>>> first commit of project

namespace LightControl
{
    

<<<<<<< HEAD
>>>>>>> second commit
    public partial class Form1 : Form
    {
=======
    public partial class Form1 : Form
    {


>>>>>>> first commit of project
        public Form1()
        {
            InitializeComponent();
            button1.Text = "开始";
            pictureBox1.Load("LightOff.png");
            pictureBox2.Load("LightOff.png");
        }

        // 语音识别器
        SpeechRecognizer recognizer;
        bool isRecording = false;

<<<<<<< HEAD
<<<<<<< HEAD
=======
        //记录电器状态
        bool control = false;

>>>>>>> second commit
=======
        //记录电器状态
        bool control = false;

        //语音朗读
        SpeechSynthesizer ss = new SpeechSynthesizer();

        
        

>>>>>>> first commit of project
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 第一步
                // 初始化语音服务SDK并启动识别器，进行语音转文本
                // 密钥和区域可在 https://azure.microsoft.com/zh-cn/try/cognitive-services/my-apis/?api=speech-services 中找到
                // 密钥示例: 5ee7ba6869f44321a40751967accf7a9
                // 区域示例: westus
                SpeechFactory speechFactory = SpeechFactory.FromSubscription("984f2ea266cc4cfa882ba4a97d7b7ccc", "westus");

                // 识别中文
                recognizer = speechFactory.CreateSpeechRecognizer("zh-CN");

                // 识别过程中的中间结果
                recognizer.IntermediateResultReceived += Recognizer_IntermediateResultReceived;
                // 识别的最终结果
                recognizer.FinalResultReceived += Recognizer_FinalResultReceived;
                // 出错时的处理
                recognizer.RecognitionErrorRaised += Recognizer_RecognitionErrorRaised;
            }
            catch (Exception ex)
            {
                if (ex is System.TypeInitializationException)
                {
                    Log("语音SDK不支持Any CPU, 请更改为x64");
                }
                else
                {
                    Log("初始化出错，请确认麦克风工作正常");
                    Log("已降级到文本语言理解模式");

                    TextBox inputBox = new TextBox();
                    inputBox.Text = "";
                    inputBox.Size = new Size(300, 26);
                    inputBox.Location = new Point(10, 10);
                    inputBox.KeyDown += inputBox_KeyDown;
                    Controls.Add(inputBox);

                    button1.Visible = false;
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            isRecording = !isRecording;
            if (isRecording)
            {
                // 启动识别器
                await recognizer.StartContinuousRecognitionAsync();
                button1.Text = "停止";
<<<<<<< HEAD
=======
                
>>>>>>> first commit of project
            }
            else
            {
                // 停止识别器
                await recognizer.StopContinuousRecognitionAsync();
                button1.Text = "开始";
<<<<<<< HEAD
=======
                ss.Speak("谢谢使用");
>>>>>>> first commit of project
            }

            button1.Enabled = true;
        }

        // 识别过程中的中间结果
        private void Recognizer_IntermediateResultReceived(object sender, SpeechRecognitionResultEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Result.Text))
            {
                Log("中间结果: " + e.Result.Text);
            }
        }

        // 识别的最终结果
        private void Recognizer_FinalResultReceived(object sender, SpeechRecognitionResultEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Result.Text))
            {
                Log("最终结果: " + e.Result.Text);
                ProcessSttResult(e.Result.Text);
            }
        }

        // 出错时的处理
        private void Recognizer_RecognitionErrorRaised(object sender, RecognitionErrorEventArgs e)
        {
            Log("错误: " + e.FailureReason);
        }

        private async void ProcessSttResult(string text)
        {
            // 第二步
            // 调用语言理解服务取得用户意图
            string intent = await GetLuisResult(text);

            // 第三步
            // 按照意图控制灯

<<<<<<< HEAD
=======
            ss.Rate = 1;
            ss.Volume = 100;
            //ss.SpeakAsync("Hello word");

>>>>>>> first commit of project
            /*if (!string.IsNullOrEmpty(intent))
            {
                if (intent.Equals("TurnOn", StringComparison.OrdinalIgnoreCase))
                {
                    OpenLight();
                }
                else if (intent.Equals("TurnOff", StringComparison.OrdinalIgnoreCase))
                {
                    CloseLight();
                }
            }*/
            if (!string.IsNullOrEmpty(intent))
            {
                if (intent.Equals("chuon", StringComparison.OrdinalIgnoreCase))
                {
                    OpenChuLight();
<<<<<<< HEAD
=======
                    ss.Speak("ok");
>>>>>>> first commit of project
                }
                else if (intent.Equals("chuoff", StringComparison.OrdinalIgnoreCase))
                {
                    CloseChuLight();
<<<<<<< HEAD
=======
                    ss.Speak("ok");
>>>>>>> first commit of project
                }
                else if (intent.Equals("keon", StringComparison.OrdinalIgnoreCase))
                {
                    OpenKeLight();
<<<<<<< HEAD
=======
                    ss.Speak("ok");
>>>>>>> first commit of project
                }
                else if (intent.Equals("keoff", StringComparison.OrdinalIgnoreCase))
                {
                    CloseKeLight();
<<<<<<< HEAD
                }
<<<<<<< HEAD
=======
=======
                    ss.Speak("ok");
                }
>>>>>>> first commit of project
                else if (intent.Equals("allon", StringComparison.OrdinalIgnoreCase))
                {
                    OpenChuLight();
                    OpenKeLight();
<<<<<<< HEAD
=======
                    ss.Speak("ok");
>>>>>>> first commit of project
                }
                else if (intent.Equals("alloff", StringComparison.OrdinalIgnoreCase))
                {
                    CloseChuLight();
                    CloseKeLight();
<<<<<<< HEAD
=======
                    ss.Speak("ok");
>>>>>>> first commit of project
                }
                else if (intent.Equals("kefollow", StringComparison.OrdinalIgnoreCase))
                {
                    if (control)
                    {
                        OpenKeLight();
                    }
                    else if (!control)
                    {
                        CloseKeLight();
                    }
<<<<<<< HEAD
=======
                    ss.Speak("ok");
>>>>>>> first commit of project
                }
                else if (intent.Equals("chufollow", StringComparison.OrdinalIgnoreCase))
                {
                    if (control)
                    {
                        OpenChuLight();
                    }
                    else if (!control)
                    {
                        CloseChuLight();
<<<<<<< HEAD
                    }
                }
>>>>>>> second commit
=======
                        
                    }
                    ss.Speak("ok");
                }
>>>>>>> first commit of project
            }

        }

        // 第二步
        // 调用语言理解服务取得用户意图
        
        private async Task<string> GetLuisResult(string text)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // LUIS 终结点地址, 示例: https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/102f6255-0c32-4f36-9c79-fe12fea4d6c4?subscription-key=9004421650254a74876cf3c888b1d11f&verbose=true&timezoneOffset=0&q=
                // 可在 https://www.luis.ai 中进入app右上角publish中找到
                string luisEndpoint = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/e60190e7-bb01-496c-8681-588ef0ea34ce?subscription-key=de9efec2fb4e4a1d9dd993638d187d87&verbose=true&timezoneOffset=480&q= ";
                string luisJson = await httpClient.GetStringAsync(luisEndpoint + text);

                try
                {
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(luisJson);
                    string intent = (string)result.topScoringIntent.intent;
                    double score = (double)result.topScoringIntent.score;
                    Log("意图: " + intent + "\r\n得分: " + score + "\r\n");

                    return intent;
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                    return null;
                }
            }
        }
        
        
        #region 界面操作

        private void Log(string message)
        {
            MakesureRunInUI(() =>
            {
                textBox1.AppendText(message + "\r\n");
            });
        }

        /*private void OpenLight()
        {
            MakesureRunInUI(() =>
            {
                pictureBox1.Load("LightOn.png");
            });
        }

        private void CloseLight()
        {
            MakesureRunInUI(() =>
            {
                pictureBox1.Load("LightOff.png");
            });
        }*/

        private void OpenChuLight()
        {
            MakesureRunInUI(() =>
            {
                pictureBox1.Load("LightOn.png");
            });
<<<<<<< HEAD
<<<<<<< HEAD
=======
            control = true;
>>>>>>> second commit
=======
            control = true;
>>>>>>> first commit of project
        }

        private void CloseChuLight()
        {
            MakesureRunInUI(() =>
            {
                pictureBox1.Load("LightOff.png");
            });
<<<<<<< HEAD
<<<<<<< HEAD
=======
            control = false;
>>>>>>> second commit
=======
            control = false;
>>>>>>> first commit of project
        }

        private void OpenKeLight()
        {
            MakesureRunInUI(() =>
            {
                pictureBox2.Load("LightOn.png");
            });
<<<<<<< HEAD
<<<<<<< HEAD
=======
            control = true;
>>>>>>> second commit
=======
            control = true;
>>>>>>> first commit of project
        }

        private void CloseKeLight()
        {
            MakesureRunInUI(() =>
            {
                pictureBox2.Load("LightOff.png");
            });
<<<<<<< HEAD
<<<<<<< HEAD
=======
            control = false;
>>>>>>> second commit
=======
            control = false;
>>>>>>> first commit of project
        }

        private void MakesureRunInUI(Action action)
        {
            if (InvokeRequired)
            {
                MethodInvoker method = new MethodInvoker(action);
                Invoke(action, null);
            }
            else
            {
                action();
            }
        }

        #endregion

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && sender is TextBox)
            {
                TextBox textBox = sender as TextBox;
                e.Handled = true;
                Log(textBox.Text);
                ProcessSttResult(textBox.Text);
                textBox.Text = string.Empty;
            }
        }

<<<<<<< HEAD
<<<<<<< HEAD
        /*private void pictureBox1_Click(object sender, EventArgs e)
        {

        }*/
=======
        
>>>>>>> second commit
=======
        
>>>>>>> first commit of project
    }
}
