using Adapters.Presenters.QRCode;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Mappers
{
    public class QRCodeMapper
    {

        public static QRCodeResponse QRCodeMapperDTO(QrCode qrCode)
        {
            return new QRCodeResponse(qrCode.ImageBase64, qrCode.Link);
        }
    }
}
