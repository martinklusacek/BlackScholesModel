using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace BlackScholesModel
{
    public partial class Form1 : Form
    {
        //Ref https://www.codearmo.com/python-tutorial/options-trading-black-scholes-model

        double S_CurrentPrice = 0;
        double K_StrikePrice = 0;
        double R_Rate_of_intrest = 0;
        int T_TimeofExpiry = 0;
        double IV_ImpliedVolatiity = 0;
        string CallPut_selection = String.Empty;
        double Output_model =0;
        public Form1()
        {
            InitializeComponent();
            StopFormResizing();
            this.AdditemtoCombobox();
            FillDefaultperiod();

        }

        private void StopFormResizing()
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;


            //Application Name
            //this.Name = "Quant : Calculate Black Scholes using c# (.NET Framework) ";
            this.Text = "Quant : Calculate Black Scholes using c# (.NET Framework) ";
        }
        private void AdditemtoCombobox()
        {
            this.comboBox1.Items.Add("CE");
            this.comboBox1.Items.Add("PE");
        }
        private void FillDefaultperiod()
        {
            try
            {
                this.S_CurrentPrice = 100;
                this.K_StrikePrice = 100;
                this.R_Rate_of_intrest = 0.1;
                this.T_TimeofExpiry = 1;
                this.IV_ImpliedVolatiity = 0.3;

                this.textBox1.Text = this.S_CurrentPrice.ToString();
                this.textBox2.Text = this.K_StrikePrice.ToString();
                this.textBox3.Text = this.R_Rate_of_intrest.ToString();
                this.textBox4.Text = this.T_TimeofExpiry.ToString();
                this.textBox5.Text = this.IV_ImpliedVolatiity.ToString();
                this.textBox6.Text = "NA";
                this.comboBox1.Text = "Select";
            }
            catch (Exception e)
            {
                MessageBox.Show(" Error(s) ");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(!IsvalidInput())
            {
                MessageBox.Show(" Error(s) Please Enter Valid Input");
            }
            GetInput_datapoint();

            StartProcessingBSM();
        }

        private void GetInput_datapoint()
        {
            this.S_CurrentPrice = Convert.ToDouble( this.textBox1.Text);
            this.K_StrikePrice = Convert.ToDouble(this.textBox2.Text)  ;
            this.R_Rate_of_intrest= Convert.ToDouble(this.textBox3.Text) ;
            this.T_TimeofExpiry = Convert.ToInt32(this.textBox4.Text);
            this.IV_ImpliedVolatiity = Convert.ToDouble(this.textBox5.Text);

        }

        private bool IsvalidInput()
        {
            bool result = false;

            try
            {
                if(textBox1.Text is null)
                {
                    return result;
                }

                if (textBox2.Text is null)
                {
                    return result;
                }

                if (textBox3.Text is null)
                {
                    return result;
                }

                if (textBox4.Text is null)
                {
                    return result;
                }

                if (textBox5.Text is null)
                {
                    return result;
                }

                if (textBox6.Text is null)
                {
                    return result;
                }
                if(comboBox1.SelectedItem is null)
                {
                    return result;
                }

                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
    }

        private void StartProcessingBSM()
        {
            if (this.comboBox1.SelectedItem is null)
                return;

            string Selectedcallput = this.comboBox1.SelectedItem.ToString();

            if (Selectedcallput is null)
                return;

            double result = -1;

            if (Selectedcallput.Equals("CE"))
            {
                result = Call_bsm(this.S_CurrentPrice, this.K_StrikePrice, this.T_TimeofExpiry, this.R_Rate_of_intrest, this.IV_ImpliedVolatiity);
            }
            else if (Selectedcallput.Equals("PE"))
            {
                result =  put_bsm(this.S_CurrentPrice, this.K_StrikePrice, this.T_TimeofExpiry, this.R_Rate_of_intrest, this.IV_ImpliedVolatiity);
            }

            if (result !=  -1)
            {
                this.textBox6.Text =result.ToString();  
            }
            else
            {
                this.textBox6.Text = "Error";
                MessageBox.Show("Error ! Please verify the Input");
            }
        }

        #region SECTION : Implement Black Schole Model

        
        private double Call_bsm( double StockPrice, double StrikePrice , int TimePeriod , double RateofIntrest , double roh_Volatility  )
        {
            //ref https://www.codearmo.com/python-tutorial/options-trading-black-scholes-model
            double model = -1;
            try
            {
                //d1 denote Get 
                double d1 = (Math.Log(StockPrice / StrikePrice) + (RateofIntrest + Math.Pow(roh_Volatility, 2) / 2) * TimePeriod) / (roh_Volatility * Math.Sqrt(TimePeriod));

                //d2 denote Pay 
                double d2 =  d1 - roh_Volatility * Math.Sqrt(TimePeriod);

                model = StockPrice * CDF(d1) - StrikePrice * Math.Exp(-RateofIntrest * TimePeriod) * CDF(d2);
                return model;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error Ocurred(s) {ex.Message}");
            }
            return model;
        }

        private double put_bsm(double StockPrice, double StrikePrice, int TimePeriod, double RateofIntrest, double roh_Volatility)
        {
            //ref https://www.codearmo.com/python-tutorial/options-trading-black-scholes-model
            double model = -1;
            try
            {
                //d1 denote Get 
                double d1 = (Math.Log(StockPrice / StrikePrice) + (RateofIntrest + Math.Pow(roh_Volatility, 2) / 2) * TimePeriod) / (roh_Volatility * Math.Sqrt(TimePeriod));

                //d2 denote Pay 
                double d2 = d1 - roh_Volatility * Math.Sqrt(TimePeriod);

                model = StrikePrice * Math.Exp(-RateofIntrest * TimePeriod) * CDF(-d2) - (StockPrice * CDF( -d1) );
                return model;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error Ocurred(s) {ex.Message}");
            }
            return model;
        }

        private double CDF(double x)
        {

            //ref https://www.johndcook.com/blog/csharp_phi/

            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x) / Math.Sqrt(2.0);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return 0.5 * (1.0 + sign * y);
        }

        #endregion


        #region : SECTION CONTROL EVENTS
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FillDefaultperiod();
        }
        #endregion
    }
}
