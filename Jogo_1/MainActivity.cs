using Android.App;
using Android.OS;
using Android.Content;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace Jogo_1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView text_tentativas;
        TextView text_sorteado;
        EditText edit_valor;
        Button cmd_adivinhar;
        Button cmd_finalizar;

        int valor_sorteado;
        int numero_tentativas;

        //
        //================================================================================
        //
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.mainGame);

            // buscar os widgets
            text_tentativas = FindViewById<TextView>(Resource.Id.text_tentativas);
            text_sorteado = FindViewById<TextView>(Resource.Id.text_sorteado);
            edit_valor = FindViewById<EditText>(Resource.Id.edit_valor);
            cmd_adivinhar = FindViewById<Button>(Resource.Id.cmd_adivinhar);
            cmd_finalizar = FindViewById<Button>(Resource.Id.cmd_finalizar);

            // declarar eventos
            cmd_adivinhar.Click += Cmd_adivinhar_Click;
            cmd_finalizar.Click += Cmd_finalizar_Click;

            // iniciar o jogo
            mtdIniciarJogo();


        }
        //
        //================================================================================
        //
        private void Cmd_finalizar_Click(object sender, EventArgs e)
        {
            mtdFimDeJogo();
        }
        //
        //================================================================================
        //
        private void Cmd_adivinhar_Click(object sender, EventArgs e)
        {
            string msg;
            bool valido = false;
            // analisa os dados inseridos comparado 
            // com o valor sorteado
            if (edit_valor.Text == "") return;
            mtdTentativas();
            if (numero_tentativas == 4)
            {
                mtdReIniciarJogo();
                return;
            }
            // o valor inserido é igual ou superior ao sorteado
            int valor_iserido = int.Parse(edit_valor.Text);
            if (valor_iserido < valor_sorteado)
            {
                // mensagem valor inferior
                msg = "Que pena ! Você quase Acertou, que tal um pouco mais?";
                valido = true;
            }
            else if(valor_iserido> valor_sorteado)
            {
                // valor superior
                msg = "Que pena ! Você quase Acertou, que tal um pouco menos?";
                valido = true;
            }
            else
            {
                // acertou
                msg = "PARABÉNS!!!, Você acertou!";
                valido = false;

            }
            if (numero_tentativas == 1)
            {
                text_sorteado.Text = "Tentativa: 1 -> " + valor_iserido.ToString();
            }
            else
            {
                //text_sorteado.Text +=", Tentativa: "+numero_tentativas.ToString()+" -> " + valor_iserido.ToString();
                text_sorteado.Text =" Tentativa: "+numero_tentativas.ToString()+" -> " + valor_iserido.ToString();
            }
            if (!valido)
            {
                var atividadefinal = new Intent(this, typeof(gameOver));
                atividadefinal.PutExtra("sorteado",valor_sorteado.ToString());
                atividadefinal.PutExtra("tentativas",numero_tentativas.ToString());
                StartActivity(atividadefinal);
                mtdIniciarJogo();
                return;
            }
            else
            {
                mtdLimpar();
                Android.App.AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
                caixa.SetTitle("Adivinha o Número ! ");
                caixa.SetPositiveButton("OK", delegate { });
                caixa.SetMessage(msg);
                caixa.SetCancelable(false);
                caixa.Show();
            }
        }
       //
       //================================================================================
       //
        private void mtdIniciarTentativas()
        {
            numero_tentativas = -1;
            mtdTentativas();
        }
        //
        //================================================================================
        //
        private void mtdTentativas()
        {
            ++numero_tentativas;
            text_tentativas.Text = "Tentativas: " + numero_tentativas.ToString();
        }
       //
       //================================================================================
       //     
       private void mtdIniciarJogo()
       {
            // iniciar o jogo
            valor_sorteado = 0;
            Random rnd = new Random();
            valor_sorteado = rnd.Next(0, 9);
            text_sorteado.Text = "[texto]";
            mtdLimpar();
            mtdIniciarTentativas();            
       }
       //
       //================================================================================
       //     
       private void mtdReIniciarJogo()
       {
            Android.App.AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
            caixa.SetTitle("Fim de Jogo!");
            caixa.SetPositiveButton("OK", delegate { });
            caixa.SetMessage("Você excedeu 3 tentativas, vamos começar de novo!");
            caixa.SetCancelable(false);
            caixa.Show();
            mtdIniciarJogo();
       }
       //
       //================================================================================
       //     
       private void mtdFimDeJogo()
       {
            this.Finish();
       }
       //
       //================================================================================
       //
       private void mtdLimpar()
        {
            edit_valor.Text = string.Empty;
        }
    }
}