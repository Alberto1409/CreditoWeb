using System;
using System.ComponentModel.DataAnnotations;

namespace CreditoWeb.Models
{
    public class Tarjeta
    {
        [Required(ErrorMessage = "El número de tarjeta es necesario.")]
        //[CreditCard]
        public string TarjetaNum { get; set; }
        public TipoTarjeta TipoTarjeta { get; set; }

        public bool Valida { get; set; }
     
        public Tarjeta(string tarjetaNum)
        {
            this.TarjetaNum = tarjetaNum;
            Valida = esValida();
            TipoTarjeta = tipoDeTarjeta();            
        }


        /// Basado en el algoritmo de Luhn determinar si esta tarjeta es valida
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private bool esValida()
        {
            StringBuilder digitsOnly = new StringBuilder();
            foreach(var c in TarjetaNum){
                
                if (Char.IsDigit(c)) digitsOnly.Append(c);
            }

            if (digitsOnly.Length > 18 || digitsOnly.Length < 15) return false;

            int sumar = 0;
            int num = 0;
            int nuevonum = 0;
            bool tokyo = false;

            for (int i = digitsOnly.Length - 1; i >= 0; i--)
            {
                num = Int32.Parse(digitsOnly.ToString(i, 1));
                if (tokyo)
                {
                    nuevonum = num * 2;
                    if (nuevonum > 9)
                    {
                        nuevonum -= 9;
                    }
                }
                else
                {
                    nuevonum = num;
                }
                sumar += nuevonum;
                tokyo = !tokyo;
            }
            return (sumar % 10) == 0;

        }


        /// Si la tarjeta es valida determinar de cuál tipo es VISA, MASTERCARD, AMERICANEXPRESS
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private TipoTarjeta tipoDeTarjeta()
        {
            var opcion=TipoTarjeta.NOVALIDA;
            if((TarjetaNum[0]=='3'|| TarjetaNum[1]=='4')||(TarjetaNum[0]=='3'|| TarjetaNum[1]=='7')){
                opcion=TipoTarjeta.AMERICANEXPRESS;
            }
            if((TarjetaNum[0]=='5'|| TarjetaNum[1]=='1')||(TarjetaNum[0]=='5'||TarjetaNum[1]=='2')||(TarjetaNum[0]=='5'|| TarjetaNum[1]=='3')||(TarjetaNum[0]=='5'||TarjetaNum[1]=='4')||(TarjetaNum[0]=='5'|| TarjetaNum[1]=='5')){
                opcion=TipoTarjeta.MASTERCARD;
            }
            if((TarjetaNum[0]=='4')){
                opcion=TipoTarjeta.VISA;
            }
            return opcion;
        
        }



    }

    public enum TipoTarjeta
    {
        VISA,
        MASTERCARD,
        AMERICANEXPRESS,
        NOVALIDA
    }
}