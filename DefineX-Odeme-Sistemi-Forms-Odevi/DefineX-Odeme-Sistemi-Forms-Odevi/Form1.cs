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

            if (!ZorunluAlanAttribute.Dogrula(cmbOdemeTipi.SelectedItem))
            {
                MessageBox.Show("L�tfen bir �deme y�ntemi se�iniz.g");
                return;
            }
            if (!ZorunluAlanAttribute.Dogrula(txtTutar) || !SayiAlanAttribute.Dogrula(txtTutar) || !decimal.TryParse(txtTutar.Text, out decimal tutar))
            {
                MessageBox.Show("L�tfen ge�erli bir tutar giriniz.");
                return;
            }
            
            IOdeme odemeYontemi = _odemeYontemleri[cmbOdemeTipi.SelectedItem.ToString()];
            lblSonuc.Text = odemeYontemi.OdemeYap(tutar);
        }

        private void cmbOdemeTipi_DropDown(object sender, EventArgs e)
        {
            try
            {
                List<PaymentType> odemeYontemleri = _odemeRepo.GetOdemeYontemleri();

                _odemeYontemleri =  OdemeFabrikasi.OdemeYontemleriniGetir(odemeYontemleri);

                cmbOdemeTipi.Items.Clear();
                
                cmbOdemeTipi.Items.AddRange(_odemeYontemleri.Keys.ToArray());

            }
            catch (Exception ex)
            {
                MessageBox.Show("�deme y�ntemleri y�klenirken hata olu�tu: " + ex.Message);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!ZorunluAlanAttribute.Dogrula(txtYeniOdemeIsim) || !ZorunluAlanAttribute.Dogrula(txtYeniOdemeDeger))
            {
                MessageBox.Show("L�tfen bir �deme y�ntemi ad� giriniz.");
                return;
            }

            int result = _odemeRepo.AddOdemeYontemi(new PaymentType(txtYeniOdemeIsim.Text,txtYeniOdemeDeger.Text+"Odeme"));
            if (result >= 0)
            {
                lblKaydet.Text = $"{result} yeni �deme Tipi eklendi.";
            }
            else
            {
                lblKaydet.Text = "�deme Tipi eklenirken hata olu�tu.";
            }
        }

    }
}
