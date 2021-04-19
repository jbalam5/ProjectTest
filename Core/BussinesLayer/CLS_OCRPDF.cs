 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCore.BussinesLayer
{
    public class CLS_OCRPDF
    {
        private const String LicenseKey = "IRONOCR-1066156E4D-141404-E66F1A-DC8C793ADC-FA75FA3-UExE7F624F3E2368D8-NASATECNOLOGIASC.IRO190927.7281.57136.PRO.1DEV.1YR.SUPPORTED.UNTIL.27.SEP.2020";

        public string GetTextIronOCR(string path)
        {
            if (IronOcr.License.IsValidLicense(LicenseKey)) {
                IronOcr.License.LicenseKey = LicenseKey;
            }
            else
            {
                return "";
            }

            var advancedOCR = new IronOcr.AdvancedOcr()
            {
                CleanBackgroundNoise = false,
                ColorDepth = 4,
                ColorSpace = IronOcr.AdvancedOcr.OcrColorSpace.GrayScale,
                EnhanceContrast = true,
                DetectWhiteTextOnDarkBackgrounds = false,
                RotateAndStraighten = false,
                Language = IronOcr.Languages.English.OcrLanguagePack,
                EnhanceResolution = false,
                InputImageType = IronOcr.AdvancedOcr.InputTypes.Document,
                ReadBarCodes = true,
                Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced
            };
            
            IronOcr.AutoOcr _OCR = new IronOcr.AutoOcr();
            IronOcr.OcrResult ocrResult = advancedOCR.ReadPdf(path, 2);
            var Pages = ocrResult.Pages;
            var Barcodes = ocrResult.Barcodes;
            var FullPdfText = ocrResult.Text;

            return FullPdfText;
        }
    }
}
