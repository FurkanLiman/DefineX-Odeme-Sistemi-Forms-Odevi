using DefineX_Odeme_Sistemi_Forms_Odevi.Attributes;
using DefineX_Odeme_Sistemi_Forms_Odevi.DataAccess;
using DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces;

namespace DefineX_Odeme_Sistemi_Forms_Odevi
{
    public partial class Form1 : Form
    {
        private OdemeRepository _odemeRepo;

        private Dictionary<string, IOdeme> _odemeYontemleri;

        
        [ZorunluAlan]
        public string OdemeYontemi;

        public Form1()
        {
            InitializeComponent();
            _odemeRepo = new OdemeRepository();
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            
            if (!ZorunluAlanAttribute.Dogrula(cmbOdemeTipi.SelectedItem))
            {
                MessageBox.Show("Lütfen bir ödeme yöntemi seçiniz.g");
                return;
            }
            
            if (!ZorunluAlanAttribute.Dogrula(txtTutar) || !SayiAlanAttribute.Dogrula(txtTutar))
            {
                MessageBox.Show("Lütfen geçerli bir tutar giriniz.");
                return;
            }
            else
            {
                decimal tutar1;
                decimal.TryParse(txtTutar.Text, out tutar1);
                IOdeme yontem = _odemeYontemleri[cmbOdemeTipi.SelectedItem.ToString()];
                string sonuc = yontem.OdemeYap(tutar1);
                lblSonuc.Text = sonuc;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                Dictionary<string, string> odemeYontemleriDb = _odemeRepo.GetOdemeYontemleri();

                _odemeYontemleri = OdemeFabrikasi.OdemeYontemleriniGetir(odemeYontemleriDb);

                cmbOdemeTipi.Items.AddRange(odemeYontemleriDb.Keys.ToArray());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme yöntemleri yüklenirken hata oluþtu: " + ex.Message);
            }
        }


        private void cmbOdemeTipi_DropDown(object sender, EventArgs e)
        {
            try
            {

                Dictionary<string, string> odemeYontemleriDb = _odemeRepo.GetOdemeYontemleri();

                _odemeYontemleri = OdemeFabrikasi.OdemeYontemleriniGetir(odemeYontemleriDb);


                cmbOdemeTipi.Items.Clear();
                cmbOdemeTipi.Items.AddRange(odemeYontemleriDb.Keys.ToArray());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme yöntemleri yüklenirken hata oluþtu: " + ex.Message);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!ZorunluAlanAttribute.Dogrula(txtYeniOdeme))
            {
                MessageBox.Show("Lütfen bir ödeme yöntemi adý giriniz.");
                return;
            }
            _odemeRepo.AddOdemeYontemi(txtYeniOdeme.Text);
        }
    }
}
