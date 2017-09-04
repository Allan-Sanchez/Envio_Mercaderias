using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envio_de_Mercaderia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (txtPeso.Text == "") 
            {
                MessageBox.Show("Debes ingresar un peso de la mercancia: ");
                txtPeso.Focus();
                return;
            }
            decimal peso;
            try
            {
                peso = Convert.ToDecimal(txtPeso.Text);

            }
            catch (Exception)
            {

                peso = 0;
            }
            if (peso <= 0)
            {
                MessageBox.Show("Debes ingresar un peso de la mercaderia mayor a cero : ","Erro");
                txtPeso.Focus();
                txtPeso.Text = "";
                
                return;
            
            }
            if (txtValor.Text == "")
            {
                MessageBox.Show("Debes ingresar un valor valido: ");
                txtValor.Focus();
                return;
            }
            decimal valor;
            try
            {
                valor = Convert.ToDecimal(txtValor.Text);

            }
            catch (Exception)
            {

                valor = 0;
            }
            if (valor <= 0)
            {
                MessageBox.Show("Debes ingresar un valor mayor a cero : ", "Erro");
                txtValor.Focus();
                txtValor.Text = "";

                return;

            }

            decimal tarifa = CalcularTarifa(peso, valor);
            decimal descuento = CalcularDescuento(peso, tarifa);
            decimal promocion = CalcularPromocion(rbtSi.Checked, rbtEfectivo.Checked,valor,tarifa);

            string mensaje;
            mensaje = string.Format("Tarifa: {0:N2}", tarifa);

            if (promocion > 0)
            {

                mensaje += string.Format("\nPromocion: {0:N2}", promocion);
                mensaje += string.Format("\nTotal: {0:N2}", tarifa - promocion);

                
            }
            else
            {
                mensaje += string.Format("\nDescuento: {0:N2}", descuento);
                mensaje += string.Format("\nTotal: {0:N2}", tarifa - descuento);
                
            }
            MessageBox.Show(mensaje,"Listo");
    

        }


        



        private decimal CalcularPromocion(bool esLunes, bool esEfectivo, decimal valor, decimal tarifa)
        {
            if (esLunes && !esEfectivo) return tarifa * 0.50M;
            if (esEfectivo && valor > 1000000) return tarifa * 0.40M;
            return 0;
        }

        

        private decimal CalcularDescuento(decimal valor, decimal tarifa)
        {
            if (valor < 300000) return 0;
            if (valor <= 600000) return tarifa * 0.10M;
            if (valor <= 1000000) return tarifa * 0.20M;
            return tarifa * 0.30M;
        }

        private decimal CalcularTarifa(decimal peso, decimal valor)
        {

            if (peso < 100) return 20000;
            if (peso <= 150) return 25000;
            if (peso <= 200) return 30000;
            return 35000 +(int) ((peso - 200) / 10) * 2000;
        }
    }
}
