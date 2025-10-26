using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Presenters.QRCode;

public sealed record QRCodeResponse(string ImageBase64, string Link);
