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

        public InvoiceLineType InvoiceLine { set { SetInvoiceLine(value); } get { return GetInvoiceLine(); } }



        private void SetInvoiceLine(InvoiceLineType InvoiceLine)
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

        private void GetKDV(TaxTotalType taxTotal)
        {
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

            return null;
        }


        public FrmInvoiceLineViewer()
        {
            InitializeComponent();
        }

        private void SatirDoldur()
        {

            var line = new InvoiceLineType
            {
                //Ürün Açıklamaları
                Item = new ItemType
                {
                    Name = new NameType1 { Value = txtUrunAdi.Text },
                    BrandName = new BrandNameType { Value = txtMarka.Text },
                    BuyersItemIdentification = new ItemIdentificationType { ID = new IDType { Value = txtAliciKodu.Text } },
                    ModelName = new ModelNameType { Value = txtModel.Text },
                    Description = new DescriptionType { Value = txtAciklama.Text },
                    ManufacturersItemIdentification = new ItemIdentificationType { ID = new IDType { Value = txtUreticiKodu.Text } },
                    SellersItemIdentification = new ItemIdentificationType { ID = new IDType { Value = txtSaticiKodu.Text } },

                },

                AllowanceCharge = GetIskonto(),
                //Birim Fiyat
                Price = new PriceType { PriceAmount = new PriceAmountType { Value = Convert.ToDecimal(txtFiyat.Text), currencyID = "TRL" } },
                //Miktar
                InvoicedQuantity = new InvoicedQuantityType { unitCode = "NIU", Value = Convert.ToDecimal(txtMiktar.Text) }, //NIU =Adet
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

        }

        private AllowanceChargeType[] GetIskonto()
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

        private void FrmInvoiceLineViewer_Load(object sender, EventArgs e)
        {

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
