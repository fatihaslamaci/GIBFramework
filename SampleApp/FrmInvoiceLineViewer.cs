using GIBInterface.UBLTR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class FrmInvoiceLineViewer : Form
    {

        private InvoiceLineType _InvoiceLine;
        public InvoiceLineType InvoiceLine { set { SetInvoiceLine(value); } get { return GetInvoiceLine(); } }



        private void SetInvoiceLine(InvoiceLineType InvoiceLine)
        {
            _InvoiceLine = InvoiceLine;
            if (InvoiceLine != null)
            {
                txtUrunAdi.Text = InvoiceLine.Item != null ? InvoiceLine.Item.Name.Value : "";
                txtMarka.Text = InvoiceLine.Item.BrandName != null ? InvoiceLine.Item.BrandName.Value : "";
                txtModel.Text = InvoiceLine.Item.ModelName != null ? InvoiceLine.Item.ModelName.Value : "";
                txtAciklama.Text = InvoiceLine.Item.Description != null ? InvoiceLine.Item.Description.Value : "";

                txtAliciKodu.Text = GetItemIdentification(InvoiceLine.Item.BuyersItemIdentification);
                txtUreticiKodu.Text = GetItemIdentification(InvoiceLine.Item.ManufacturersItemIdentification);
                txtSaticiKodu.Text = GetItemIdentification(InvoiceLine.Item.SellersItemIdentification);

                txtSatirNotu.Text = GetSatirNotu(InvoiceLine.Note);

                GetIskonto(InvoiceLine.AllowanceCharge);
                txtFiyat.Text = GetAmountType(InvoiceLine.Price.PriceAmount);
                txtMiktar.Text = InvoiceLine.InvoicedQuantity.Value.ToString();
                txtBirim.Text = InvoiceLine.InvoicedQuantity.unitCode;
                GetKDV(InvoiceLine.TaxTotal);
                txtToplamTutar.Text = InvoiceLine.LineExtensionAmount.Value.ToString();
            }
        }

        private void GetKDV(TaxTotalType taxTotal)
        {
            txtKdvTutar.Text = "";
            txtKdvOrani.Text = "";

            if (taxTotal!=null)
            {
                txtKdvTutar.Text = taxTotal.TaxAmount.Value.ToString();
                txtKdvOrani.Text = taxTotal.TaxSubtotal[0].Percent.Value.ToString();
            }
        }

        private string GetAmountType(AmountType1 Amount)
        {
            if (Amount!=null)
            {
                return Amount.Value.ToString();
            }
            else
            {
                return "";
            }
        }

        private void GetIskonto(AllowanceChargeType[] allowanceCharge)
        {

            txtIskontoOrani.Text = "0";
            txtIskontoTutar.Text = "0";

            if((allowanceCharge!=null)&&(allowanceCharge.Length>0))
            {
                if(allowanceCharge[0]!=null)
                {
                    if(allowanceCharge[0].MultiplierFactorNumeric != null)
                    {
                        txtIskontoOrani.Text = (allowanceCharge[0].MultiplierFactorNumeric.Value * 100).ToString();
                    }
                    if (allowanceCharge[0].Amount != null)
                    {
                        txtIskontoTutar.Text = (allowanceCharge[0].Amount.Value ).ToString();
                    }
                }

            }


        }

        private string GetSatirNotu(NoteType[] note)
        {
            string r = "";
            if ((note != null)&&(note.Length>0))
            {
                r = note[0].Value;
            }
            return r;
        }

        private string GetItemIdentification(ItemIdentificationType ItemIdentification)
        {
            string r = "";
            if (ItemIdentification!=null)
            {
                if (ItemIdentification.ID!=null)
                {
                    r = ItemIdentification.ID.Value;
                }
            }
            return r;
        }

        private InvoiceLineType GetInvoiceLine()
        {

            return _InvoiceLine;
        }


        public FrmInvoiceLineViewer()
        {
            InitializeComponent();
        }

        private InvoiceLineType SatirDoldur()
        {

            if (_InvoiceLine==null)
            {
                _InvoiceLine = new InvoiceLineType();
            }

            var line = new InvoiceLineType
            {
                //Ürün Açıklamaları
                Item = CreateItemType(_InvoiceLine.Item),

                AllowanceCharge = CreateAllowanceCharge(_InvoiceLine.AllowanceCharge),
                //Birim Fiyat
                Price = new PriceType { PriceAmount = new PriceAmountType { Value = Convert.ToDecimal(txtFiyat.Text), currencyID = "TRL" } },
                //Miktar
                InvoicedQuantity = new InvoicedQuantityType { unitCode = txtBirim.Text, Value = Convert.ToDecimal(txtMiktar.Text) }, //NIU =Adet
                //Not
                Note = new NoteType[] { new NoteType { Value = txtSatirNotu.Text } },
                // KDV ve Diğer Vergiler
                TaxTotal = new TaxTotalType
                {
                    TaxSubtotal = new TaxSubtotalType[]{ 
                    //Vergi 1 KDV
                    new TaxSubtotalType{
                    Percent = new PercentType1{ Value=Convert.ToDecimal(txtKdvOrani.Text)},
                    TaxCategory = new TaxCategoryType{TaxScheme = new TaxSchemeType{ TaxTypeCode = new TaxTypeCodeType{  Value = "0015"}, Name =new NameType1{ Value="KDV"} }, TaxExemptionReason=new TaxExemptionReasonType{ Value="Tanıtım Ürünü"}},
                    TaxAmount = new TaxAmountType{ Value = ((100+Convert.ToDecimal(txtKdvOrani.Text))/100) * Convert.ToDecimal(txtFiyat.Text) * (Convert.ToDecimal(txtMiktar.Text)), currencyID= "TRL" },
                    PerUnitAmount = new PerUnitAmountType{Value = ((100+Convert.ToDecimal(txtKdvOrani.Text))/100)* Convert.ToDecimal(txtFiyat.Text), currencyID= "TRL"},
                    }
                },
                TaxAmount = new TaxAmountType { Value = Convert.ToDecimal(txtKdvTutar.Text) }
                },

                ID = new IDType { Value = "1" },
                LineExtensionAmount = new LineExtensionAmountType { Value = Convert.ToDecimal(txtToplamTutar.Text), currencyID = "TRL" }
            };

            return line;

        }

       

       

        private void FrmInvoiceLineViewer_Load(object sender, EventArgs e)
        {

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            _InvoiceLine = SatirDoldur();

            DialogResult = DialogResult.OK;

        }


        private AllowanceChargeType[] CreateAllowanceCharge(AllowanceChargeType[] allowanceCharge)
        {
            AllowanceChargeType[] r = new AllowanceChargeType[1];

            r[0] = new AllowanceChargeType
            {
                ChargeIndicator = new ChargeIndicatorType { Value = true },
                MultiplierFactorNumeric = new MultiplierFactorNumericType { Value = Convert.ToDecimal(txtIskontoOrani.Text) / 100 },
                Amount = new AmountType2 { Value = Convert.ToDecimal(txtIskontoOrani.Text), currencyID = "TRL" },
            };

            return r;
        }

        public ItemType CreateItemType(ItemType Item)
        {

            if (Item == null)
            {
                Item = new ItemType();
            }

            Item.Name = CreateTextType<NameType1>(txtUrunAdi.Text);
            Item.BrandName = CreateTextType<BrandNameType>(txtMarka.Text);
            Item.ModelName = CreateTextType<ModelNameType>(txtModel.Text);
            Item.Description = CreateTextType<DescriptionType>(txtAciklama.Text);
            Item.ManufacturersItemIdentification = CreateItemIdentificationType(txtUreticiKodu.Text);
            Item.SellersItemIdentification = CreateItemIdentificationType(txtSaticiKodu.Text);
            Item.BuyersItemIdentification = CreateItemIdentificationType(txtAliciKodu.Text);

            return Item;
        }


        public static ItemIdentificationType CreateItemIdentificationType(string text)
        {

            if (string.IsNullOrWhiteSpace(text) == false)
            {
                return new ItemIdentificationType { ID = new IDType { Value = text } };
            }
            else
            {
                return null;
            }
        }
        public static T CreateTextType<T>(string text)
        {
            
            if (string.IsNullOrWhiteSpace(text) == false)
            {
                var obj = (T)Activator.CreateInstance(typeof(T));
                if(obj is TextType)
                {
                    (obj as TextType).Value = text;
                }
                return obj;
            }else
            {
                return default(T);
            }

            
        }



        void Texts_Changed(object sender, EventArgs e)
        {
            try
            {
                txtToplamTutar.Text = (Convert.ToDecimal(txtMiktar.Text) * Convert.ToDecimal(txtFiyat.Text) * ((100 - Convert.ToDecimal(txtIskontoOrani.Text)) / 100) * ((Convert.ToDecimal(txtKdvOrani.Text) + 100) / 100)).ToString();
                txtKdvTutar.Text = (Convert.ToDecimal(txtMiktar.Text) * Convert.ToDecimal(txtFiyat.Text) * ((100 - Convert.ToDecimal(txtIskontoOrani.Text)) / 100) * (Convert.ToDecimal(txtKdvOrani.Text) / 100)).ToString();
                txtIskontoTutar.Text = (Convert.ToDecimal(txtMiktar.Text) * Convert.ToDecimal(txtFiyat.Text) * (Convert.ToDecimal(txtIskontoOrani.Text) / 100)).ToString();
            }
            catch (Exception)
            {

                return;
            }

        }


    }
}
