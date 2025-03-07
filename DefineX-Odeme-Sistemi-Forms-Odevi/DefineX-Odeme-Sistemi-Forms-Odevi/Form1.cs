using DefineX_Odeme_Sistemi_Forms_Odevi.DataAccess;
using DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces;

namespace DefineX_Odeme_Sistemi_Forms_Odevi
{
    public partial class Form1 : Form
    {
        private OdemeRepository _odemeRepo;

        private Dictionary<string, IOdeme> _odemeYontemleri;
        public Form1()
        {
            InitializeComponent();
            _odemeRepo = new OdemeRepository();
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            if (cmbOdemeTipi.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir ödeme yöntemi seçiniz.");
                return;
            }
            decimal tutar;
            if (decimal.TryParse(txtTutar.Text, out tutar))
            {
                IOdeme yontem = _odemeYontemleri[cmbOdemeTipi.SelectedItem.ToString()];
                string sonuc = yontem.OdemeYap(tutar);
                lblSonuc.Text = sonuc;
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir tutar giriniz.");
                return;
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
    }
}
