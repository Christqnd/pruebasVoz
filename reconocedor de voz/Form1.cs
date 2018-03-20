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

namespace reconocedor_de_voz
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine escucha = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("es-Es"));

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                escucha.RequestRecognizerUpdate();
                escucha.SetInputToDefaultAudioDevice(); //abrimos el audio
                escucha.LoadGrammar(new DictationGrammar()); //leer la gramatica (le permite al programa que palabra decimos)
                escucha.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs> (Reconocedor); 
                //lo que escucha lo envia al metodo reconocedor

                escucha.SetInputToDefaultAudioDevice();

                escucha.RecognizeAsync(RecognizeMode.Multiple); 
                //se utiliza para que el programa sepa que va a reconocer mas de una palabra
                
                escucha.AudioLevelUpdated += nivel_audio; //aumentar el progressbar mediante nuestra voz
                escucha.Recognize(); 
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("uso del microfono ok !");
            }
        }

        public void Reconocedor(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (RecognizedWordUnit palabra in e.Result.Words)
            {
                textBox1.Text += palabra.Text + " ";

                if (palabra.Text == "Validar")
                {
                    Form2 f2 = new Form2();
                    f2.Show();
                }
            }
        }

        public void nivel_audio(object sender, AudioLevelUpdatedEventArgs e)
        {
            int audio = e.AudioLevel;
            progressBar1.Value = audio;
        }
    }
}
