using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Jogo_1
{
    [Activity(Label = "gameOver")]
    public class gameOver : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.gameOver);

            // criando os widgets
            TextView text_final_sorteado = FindViewById<TextView>(Resource.Id.text_final_sorteado);
            TextView text_final_tentaivas = FindViewById<TextView>(Resource.Id.text_final_tentativas);
            Button cmd_final_terminar = FindViewById<Button>(Resource.Id.cmd_terminar);

            // pegar os valores enviados para esta activity
            int sorteado   = int.Parse(Intent.GetStringExtra("sorteado"));
            int tentativas = int.Parse(Intent.GetStringExtra("tentativas"));
            // atualizar os textviews
            text_final_sorteado.Text = "Numero Sorteado: "+sorteado.ToString();
            text_final_tentaivas.Text = "Numero de Tentativas: "+tentativas.ToString();

            // eventos
            cmd_final_terminar.Click += delegate
             {
                 this.Finish();
             };

        }
    }
}