using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ZIPGO.Application.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(Stream imageStream, string fileName);
    }
}